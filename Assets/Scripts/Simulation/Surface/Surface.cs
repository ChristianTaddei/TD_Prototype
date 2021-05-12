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

    public bool TryGetSurfacePointFromPosition(int triangleIndex, Vector3 point, out SurfacePoint sp)
    {
        sp = new SurfacePoint(faces[triangleIndex], new BarycentricVector(faces[triangleIndex], new CartesianVector(point)));
        return true;
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
                    0,
                    start.BarycentricVector.Base.A.Position.z)),
            new CartesianVector(
                new Vector3(
                    start.BarycentricVector.Base.B.Position.x,
                    0,
                    start.BarycentricVector.Base.B.Position.z)),
            new CartesianVector(
                new Vector3(
                    start.BarycentricVector.Base.C.Position.x,
                    0,
                    start.BarycentricVector.Base.C.Position.z)));

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
                addSquareAt(new Vector3(i, 0, j));
            }
        }
    }

    public void addSquareAt(Vector3 point)
    {
        CartesianVector _A = point + new Vector3(1, 0, 0);
        CartesianVector _B = point + new Vector3(1, 0, 1);
        CartesianVector _C = point + new Vector3(0, 0, 1);
        CartesianVector _D = point + new Vector3(0, 0, 0);

        Face ACB = AddFace(_A, _C, _B);
        Face ADC = AddFace(_A, _D, _C);
    }
}
