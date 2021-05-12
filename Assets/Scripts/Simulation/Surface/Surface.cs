using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Surface
{
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
        foreach (TriangleVertexIdentifiers v1 in Triangle.Vertices)
        {
            foreach (TriangleVertexIdentifiers v2 in Triangle.Vertices)
            {
                if (f1.GetVertex(v1) == f1.GetVertex(v2)) return true;
            }
        }

        return false;
    }

    public Face AddFace(CartesianVector cartesianPoint1, CartesianVector cartesianPoint2, CartesianVector cartesianPoint3)
    {
        Face newFace = new Face(this, new Triangle(cartesianPoint1, cartesianPoint2, cartesianPoint3));
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

        int tries = 0;
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

            tries++;
            if (tries > 1000)
            {
                throw new Exception("Too many tries to get intersection");
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
        if (start.Position == end.Position)
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

        // TODO: exctract some ugliness to Vector.FlatProject or something
        Triangle flatStartBase = new Triangle(
            new CartesianVector(
                new Vector3(
                    start.BarycentricVector.Base.A.Position.x,
                    start.BarycentricVector.Base.A.Position.y,
                    0)),
            new CartesianVector(
                new Vector3(
                    start.BarycentricVector.Base.B.Position.x,
                    start.BarycentricVector.Base.B.Position.y,
                    0)),
            new CartesianVector(
                new Vector3(
                    start.BarycentricVector.Base.C.Position.x,
                    start.BarycentricVector.Base.C.Position.y,
                    0)));

        BarycentricVector flatEnd = new BarycentricVector(
            flatStartBase,
            new CartesianVector(end.BarycentricVector.Position).Project(flatStartBase));

        BarycentricVector endInStartBase = new BarycentricVector(
            start.BarycentricVector.Base,
            flatEnd.BarycentricCoordinates);
        BarycentricVector startToEnd = endInStartBase - start.BarycentricVector;

        HashSet<TriangleVertexIdentifiers> changedCoordinates = new HashSet<TriangleVertexIdentifiers>();
        foreach (TriangleVertexIdentifiers c in BarycentricCoordinates.Coordinates)
        {
            if (endInStartBase.BarycentricCoordinates.GetCoordinate(c) < 0) // TODO: Corner Case, <= 0 need refinement when starting at intersection
            {
                changedCoordinates.Add(c);
            }
        }

        if (changedCoordinates.Count == 0)
        {
            return new Maybe<SurfacePoint>.Nothing();
        }

        TriangleVertexIdentifiers changedCoordinate = changedCoordinates.First();

        float coefficient = -start.BarycentricVector.BarycentricCoordinates.GetCoordinate(changedCoordinate) / startToEnd.BarycentricCoordinates.GetCoordinate(changedCoordinate);

        BarycentricVector intersectionVector =
            new BarycentricVector(
                start.Face,
                (start.BarycentricVector.BarycentricCoordinates + coefficient * startToEnd.BarycentricCoordinates));
        // Could check if this is normalized (must be if calc are correct)


        HashSet<TriangleVertexIdentifiers> sharedVertices = new HashSet<TriangleVertexIdentifiers>(BarycentricCoordinates.Coordinates);
        sharedVertices.RemoveWhere(sv => changedCoordinates.Contains(sv));

        HashSet<Face> facesSharingChangedCoordinates = start.Face.GetFacesFromSharedVertices(sharedVertices);
        if (facesSharingChangedCoordinates.Count == 0)
        {
            return new Maybe<SurfacePoint>.Nothing();
        }

        Face nextFace = facesSharingChangedCoordinates.First(); // TODO: Corner Cases

        intersectionVector = intersectionVector.ChangeBase(nextFace);

        return new Maybe<SurfacePoint>.Just(new SurfacePoint(nextFace, intersectionVector));
    }

    // Surface made of squares
    public Surface(float edgeSize) : this() 
    {
        for (int i = 0; i < edgeSize; i++)
        {
            for (int j = 0; j < edgeSize; j++)
            {
                makeSquareAt(new Vector3(i,j,0));
            }
        }
    }

    public List<CartesianVector> makeSquareAt(Vector3 point)
    {
        CartesianVector _A = point + new Vector3(1, 0, 0);
        CartesianVector _B = point + new Vector3(1, 1, 0);
        CartesianVector _C = point + new Vector3(0, 1, 0);
        CartesianVector _D = point + new Vector3(0, 0, 0);

        Face ABC = AddFace(_A, _B, _C);
        Face ADC = AddFace(_A, _D, _C);

        return new List<CartesianVector>() { _A, _B, _C, _D };
    }

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
    //     // FIX: what if faces does not have a value?
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
