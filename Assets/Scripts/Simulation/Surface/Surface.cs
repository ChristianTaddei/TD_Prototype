using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Surface
{
    private List<IPoint> vertices;
    private List<Face> faces;

    public Surface()
    {
        this.faces = new List<Face>();
    }

    internal void AddFace(Face face)
    {
        faces.Add(face);
    }

    internal List<Face> neighboursOf(Face startingFace) // TODO: make constant not linear
    {
        List<Face> neighbours = new List<Face>();
        faces.Where(candidateNeighbour => areNeighbours(candidateNeighbour, startingFace));
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
        faces.Add(newFace);
        return newFace;
    }

    public bool TryMakeDirectPath(SurfacePoint startPoint, SurfacePoint endPoint, out SurfacePath path)
    {
        if (startPoint.Face.Surface != this || endPoint.Face.Surface != this)
        {
            path = SurfacePath.NO_PATH;
            return false;
        }

        List<SurfacePoint> crossingPoints = new List<SurfacePoint>();

        SurfacePoint currentPoint = startPoint;
        while (currentPoint.Face != endPoint.Face)
        {
            SurfacePoint intersection;
            Face nextFace;
            if (TryGetIntersectionToward(currentPoint, endPoint, out intersection, out nextFace))
            {
                crossingPoints.Add(intersection);
                currentPoint = intersection; // TODO: multi face points
            }
            else
            {
                path = SurfacePath.NO_PATH;
                return false;
            }
        }

        // check last inters + dir crosses end

        List<SurfacePoint> allPoints = new List<SurfacePoint>();
        allPoints.Add(startPoint);
        allPoints.AddRange(crossingPoints);
        allPoints.Add(endPoint);

        path = new SurfacePath(allPoints);

        return true;
    }

    private bool TryGetIntersectionToward(SurfacePoint start, SurfacePoint end, out SurfacePoint intersection, out Face otherFace)
    {
        BarycentricVector endInStartBase = end.BarycentricVector.ChangeBase(start.BarycentricVector.Base);
        BarycentricVector startToEnd = endInStartBase - start.BarycentricVector;
        float coefficient;

        Func<double, double, bool> changedSign = (double before, double after) => before * after <= 0;

        // TODO: need iterable on coordinates and such
        bool aChangedSign = changedSign(
            start.BarycentricVector.BarycentricCoordinates.a,
            endInStartBase.BarycentricCoordinates.a);
        bool bChangedSign = changedSign(
            start.BarycentricVector.BarycentricCoordinates.b,
            endInStartBase.BarycentricCoordinates.b);
        bool cChangedSign = changedSign(
            start.BarycentricVector.BarycentricCoordinates.c,
            endInStartBase.BarycentricCoordinates.c);

        if (aChangedSign || bChangedSign || cChangedSign)
        {
            if (aChangedSign)
            {
                coefficient = startToEnd.BarycentricCoordinates.a / -start.BarycentricVector.BarycentricCoordinates.a;
            }
            else if (bChangedSign)
            {
                coefficient = startToEnd.BarycentricCoordinates.b / -start.BarycentricVector.BarycentricCoordinates.b;
            }
            else
            {
                coefficient = startToEnd.BarycentricCoordinates.c / -start.BarycentricVector.BarycentricCoordinates.c;
            }

            BarycentricVector intersectionVector =
                start.BarycentricVector + new BarycentricVector(
                                            start.Face.Triangle, 
                                            new BarycentricCoordinates(
                                                startToEnd.BarycentricCoordinates.a * coefficient,
                                                startToEnd.BarycentricCoordinates.b * coefficient,
                                                startToEnd.BarycentricCoordinates.c * coefficient));

            intersection = new SurfacePoint(start.Face, intersectionVector);
            otherFace = end.Face; // FIXME: not end.Face, use face iterator to get which neight it is
            return true;
        }
        else
        {
            intersection = SurfacePoint.NO_POINT;
            otherFace = Face.NO_FACE;
            return false; // Stupid # needs monads
        }
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

    // //     // TODO: make like tryGet
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
