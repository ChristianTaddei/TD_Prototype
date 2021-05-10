using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Surface
{
    private List<IPoint> vertices;
    private List<Face> faces;

    public List<Face> Faces { get => faces; set => faces = value; }

    public Surface()
    {
        this.Faces = new List<Face>();
    }

    internal void AddFace(Face face)
    {
        Faces.Add(face);
    }

    internal List<Face> neighboursOf(Face startingFace)
    {
        List<Face> neighbours = new List<Face>();
        Faces.Where(candidateNeighbour => areNeighbours(candidateNeighbour, startingFace));
        return neighbours;
    }

    private bool areNeighbours(Face f1, Face f2)
    {
        foreach (IPoint v1 in f1.Triangle.Vertices)
        {
            foreach (IPoint v2 in f2.Triangle.Vertices)
            {
                if (v1 == v2) return true;
            }
        }

        return false;
    }

    public Face AddFace(CartesianPoint cartesianPoint1, CartesianPoint cartesianPoint2, CartesianPoint cartesianPoint3)
    {
        Face newFace = new Face(this, cartesianPoint1, cartesianPoint2, cartesianPoint3);
        Faces.Add(newFace);
        return newFace;
    }

    public Maybe<SurfacePath> MakeDirectPath(SurfacePoint startPoint, SurfacePoint endPoint)
    {
        if (startPoint.Face.Surface != this || endPoint.Face.Surface != this)
        {
            return new Maybe<SurfacePath>.Nothing();
        }

        List<SurfacePoint> crossingPoints = new List<SurfacePoint>();

        SurfacePoint currentPoint = startPoint;
        while (currentPoint.Face != endPoint.Face)
        {
            Maybe<SurfacePoint> intersection = GetIntersectionToward(currentPoint, endPoint);
            if (intersection.HasValue())
            {
                crossingPoints.Add(intersection.Value);
                currentPoint = intersection.Value; // TODO: Points with multiple representations...
            }
            else
            {
                return new Maybe<SurfacePath>.Nothing();
            }
        }

        // check last inters + dir crosses end

        List<SurfacePoint> allPoints = new List<SurfacePoint>();
        allPoints.Add(startPoint);
        allPoints.AddRange(crossingPoints);
        allPoints.Add(endPoint);

        return new Maybe<SurfacePath>.Just(new SurfacePath(allPoints));
    }

    public Maybe<SurfacePoint> GetIntersectionToward(SurfacePoint start, SurfacePoint end)
    {
        // TODO: Corner Cases, if more than one intersection found, use furthest away from start 
        // (so if start is an inters already its not automatically returned)
        if (start.Coordinates == end.Coordinates)
        {
            if (start.BarycentricVector.BarycentricCoordinates.a == 0.0
                || start.BarycentricVector.BarycentricCoordinates.b == 0.0
                || start.BarycentricVector.BarycentricCoordinates.c == 0.0)
            {
                return new Maybe<SurfacePoint>.Just(start);
            }
            else
            {
                return new Maybe<SurfacePoint>.Nothing();
            }
        }

        BarycentricVector endInStartBase = end.BarycentricVector.ChangeBase(start.BarycentricVector.Base);
        BarycentricVector startToEnd = endInStartBase - start.BarycentricVector;

        HashSet<TriangleVertices> changedCoordinates = new HashSet<TriangleVertices>();
        foreach (TriangleVertices c in Enum.GetValues(typeof(TriangleVertices)))
        {
            if (endInStartBase.BarycentricCoordinates.GetCoord(c) < 0) // <= 0 need refinement when starting at intersection
            {
                changedCoordinates.Add(c);
            }
        }

        if (changedCoordinates.Count == 0)
        {
            return new Maybe<SurfacePoint>.Nothing();
        }

        TriangleVertices changedCoordinate = changedCoordinates.First();

        float coefficient = -start.BarycentricVector.BarycentricCoordinates.GetCoord(changedCoordinate)/startToEnd.BarycentricCoordinates.GetCoord(changedCoordinate); 

        BarycentricVector intersectionVector =
            new BarycentricVector(
                start.Face.Triangle,
                (start.BarycentricVector.BarycentricCoordinates + coefficient * startToEnd.BarycentricCoordinates));
        // Could check if this is normalized (must be if calc are correct)
        

        HashSet<TriangleVertices> sharedVertices = new HashSet<TriangleVertices>((TriangleVertices[])Enum.GetValues(typeof(TriangleVertices)));
        sharedVertices.RemoveWhere(sv => changedCoordinates.Contains(sv));

        HashSet<Face> facesSharingChangedCoordinates = start.Face.GetFacesFromSharedVertices(sharedVertices);
        if (facesSharingChangedCoordinates.Count == 0)
        {
            return new Maybe<SurfacePoint>.Nothing();
        }

        Face nextFace = facesSharingChangedCoordinates.First(); // TODO: Corner Cases

        intersectionVector = intersectionVector.ChangeBase(nextFace.Triangle);

        return new Maybe<SurfacePoint>.Just(new SurfacePoint(nextFace, intersectionVector));
    }

    // public bool TryMakeSurfacePointFrom(CartesianPoint cartesianPoint, out SurfacePoint newSurfacePoint)
    // {
    //     BarycentricVector bv;

    //     if (BarycentricVector.FromPoint(face.Triangle, cp, out bv))
    //     {
    //         newSurfacePoint = new SurfacePoint(face, bv);
    //         return true;
    //     }

    //     newSurfacePoint = new SurfacePoint();
    //     return false;
    // }

    // public List<Vertex> Vertices;
    // public int[] Triangles { get; private set; }
    // private List<Face> faces;

    // public Surface(int edgeSize)
    // {
    //     // Vertices
    //     Vertices = new Vertex[edgeSize * edgeSize].ToList();

    //     // Create a vertex at every (i,j) integer position
    //     int vc = 0;
    //     for (int z = 0; z < edgeSize; z++)
    //     {
    //         for (int x = 0; x < edgeSize; x++)
    //         {
    //             Vertex newVertex = new Vertex();
    //             Vertices[x + (edgeSize * z)] = newVertex;

    //             // InitalState.VertexStates.Add(newVertex, new VertexState(new Vector3(x, 0, z)));
    //         }
    //     }

    //     // Connect all vertices closer than 2.0f
    //     foreach (Vertex vertex in Vertices)
    //     {
    //         foreach (Vertex otherVertex in Vertices)
    //         {
    //             // if (Vector3.Distance(
    //             //         InitalState.VertexStates[vertex].Position,
    //             //         InitalState.VertexStates[otherVertex].Position) < 2.0f
    //             //     && !vertex.Equals(otherVertex))
    //             // {
    //             //     vertex.AddNeighbour(otherVertex);
    //             // }
    //         }
    //     }

    //     // Mesh and faces
    //     Triangles = new int[(edgeSize - 1) * (edgeSize - 1) * 6];
    //     faces = new List<Face>();

    //     for (int i = 0; i < Vertices.Count; i++)
    //     {
    //         int row = i % edgeSize;
    //         int col = i / edgeSize;

    //         if (row < edgeSize - 1 && col < edgeSize - 1)
    //         {
    //             int triangleStartingIndex = (row + (col * (edgeSize - 1))) * 6;

    //             faces.Add(new Face(
    //                         Vertices[i],
    //                         Vertices[i + 1],
    //                         Vertices[i + edgeSize]));

    //             Triangles[triangleStartingIndex] = i;
    //             Triangles[triangleStartingIndex + 1] = i + edgeSize;
    //             Triangles[triangleStartingIndex + 2] = i + 1; 

    //             faces.Add(new Face(
    //                         Vertices[i + 1 + edgeSize],
    //                         Vertices[i + edgeSize],
    //                         Vertices[i + 1]));

    //             Triangles[triangleStartingIndex + 3] = i + 1 + edgeSize;
    //             Triangles[triangleStartingIndex + 4] = i + 1;
    //             Triangles[triangleStartingIndex + 5] = i + edgeSize; 
    //         }
    //     }
    // }

    // // internal SurfacePoint MakeBPFrom2d(Vector3 destination2d)
    // // {
    // //     foreach (Face face in faces)
    // //     {
    // //         SurfacePoint candidate = new SurfacePoint(
    // //             InitalState,
    // //             face,
    // //             destination2d);

    // //         if (candidate.Barycentrics.CheckInternal())
    // //         {
    // //             if(Vector3.Distance(destination2d, candidate.GetCartesians(InitalState)) > 0.1f){
    // //                 // Debug.Log("makebp broke vector");
    // //             }
    // //             return candidate;
    // //         }
    // //     }

    // //     Debug.Log("makeBP failed");
    // //     return null;
    // // }

    // public bool TryGetFaceFromIndex(int triangleIndex, out Face face)
    // {
    //     // FIXME: what if faces does not have a value?
    //     if (triangleIndex < faces.Count)
    //     {
    //         face = faces[triangleIndex];
    //         return true;
    //     }

    //     face = default;
    //     return false;
    // }

    // // public SurfacePoint SumBarAndCart(BoardState boardState, SurfacePoint boardPosition, Vector3 movementVector)
    // // {
    // //     // Face face2d = ProjectFaceOn2D(boardPosition.Face);
    // //     Vector3 destination = boardPosition.GetCartesians(boardState) + movementVector;
    // //     Vector3 destination2d = new Vector3(
    // //         destination.x, 0, destination.z);

    // //     // BoardPosition flatBP = new BoardPosition(simState, face2d, destination2d);
    // //     SurfacePoint flatBP = new SurfacePoint(SimulationManager.Instance.Board.InitalState, boardPosition.Face, destination2d);
    // //     if (flatBP.Barycentrics.CheckInternal())
    // //     {
    // //         return new SurfacePoint(boardPosition.Face, flatBP.Barycentrics);
    // //     }

    // //     List<Face> farNeighbours = new List<Face>();
    // //     foreach (Face nextFace in boardPosition.Face.GetNeighbourFaces())
    // //     {
    // //         // Face nextFace2d = ProjectFaceOn2D(nextFace);
    // //         // flatBP = new BoardPosition(simState, nextFace2d, destination2d);
    // //         flatBP = new SurfacePoint(SimulationManager.Instance.Board.InitalState, nextFace, destination2d);
    // //         if (flatBP.Barycentrics.CheckInternal())
    // //         {
    // //             return new SurfacePoint(nextFace, flatBP.Barycentrics);
    // //         }

    // //         farNeighbours.AddRange(nextFace.GetNeighbourFaces());
    // //     }

    // //     foreach (Face nextFace in farNeighbours)
    // //     {
    // //         // Face nextFace2d = ProjectFaceOn2D(nextFace);
    // //         // flatBP = new BoardPosition(simState, nextFace2d, destination2d);
    // //         flatBP = new SurfacePoint(SimulationManager.Instance.Board.InitalState, nextFace, destination2d);
    // //         if (flatBP.Barycentrics.CheckInternal())
    // //         {
    // //             return new SurfacePoint(nextFace, flatBP.Barycentrics);
    // //         }
    // //     }

    // //     Debug.Log("find next face failed");
    // //     return null;
    // // }

    // // private Face ProjectFaceOn2D(Face face)
    // // {
    // //     return new Face(
    // //                  new Vertex(
    // //                      new Vector3(
    // //                         face.a.Position.x,
    // //                         0,
    // //                         face.a.Position.z)),
    // //                  new Vertex(
    // //                      new Vector3(
    // //                         face.b.Position.x,
    // //                         0,
    // //                         face.b.Position.z)),
    // //                  new Vertex(
    // //                      new Vector3(
    // //                         face.c.Position.x,
    // //                         0,
    // //                         face.c.Position.z)));
    // // }

    // // private static Vector3 ProjectVectorOnFace(Face face, Vector3 vector)
    // // {
    // //     Vector3 op = vector;
    // //     Vector3 ap = face.a.Position;
    // //     Vector3 n = Vector3.Cross(
    // //         face.b.Position - face.a.Position,
    // //         face.c.Position - face.a.Position);
    // //     Vector3 projectedDest = op - (Vector3.Dot(ap, n) / n.sqrMagnitude) * n;
    // //     return projectedDest;
    // // }

    // // public BoardPosition ToBoardPosition(Vertex vertex)
    // // {
    // //     Face foundFace = faces.First(face => face.a == vertex || face.b == vertex || face.c == vertex);
    // //     return new BoardPosition(foundFace, vertex.Position);
    // // }
}
