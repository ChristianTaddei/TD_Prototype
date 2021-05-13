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

    public bool TryGetSurfacePointFromPosition(Vector3 point, out SurfacePoint sp)
    {
        foreach (Face face in faces)
        {
            sp = new SurfacePoint(face, new BarycentricVector(face, new CartesianVector(point)));
            if (sp.BarycentricVector.IsPointOnBaseTriangle())
            {
                return true;
            }
        }

        sp = default; // FIXME: monads here
        return false;
    }

    public Face AddFace(CartesianVector cartesianPoint1, CartesianVector cartesianPoint2, CartesianVector cartesianPoint3)
    {
        Face newFace = new Face(this, new Triangle(cartesianPoint1, cartesianPoint2, cartesianPoint3));
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
                if (intersection.Value.Position == endPoint.Position) break;

                crossingPoints.Add(intersection.Value);
                currentPoint = intersection.Value;
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

        List<TriangleVertexIdentifiers> already0Coordinates = new List<TriangleVertexIdentifiers>();
        foreach (TriangleVertexIdentifiers c in Triangle.Vertices)
        {
            if (start.BarycentricVector.BarycentricCoordinates.GetCoordinate(c) <= 0.0f)
            {
                already0Coordinates.Add(c);
            }
        }


        // TODO: exctract some ugliness to Vector.FlatProject or something
        Triangle flatStartBase = ProjectOnPlane_Oy(start.BarycentricVector.Base);

        BarycentricVector endInFlatStartBase = new BarycentricVector(
            flatStartBase,
            new CartesianVector(end.BarycentricVector.Position).Project(flatStartBase));

        BarycentricVector flatEndInStartBase = new BarycentricVector(
            start.BarycentricVector.Base,
            endInFlatStartBase.BarycentricCoordinates);

        BarycentricVector startToFlatEnd = flatEndInStartBase - start.BarycentricVector;

        HashSet<TriangleVertexIdentifiers> coordinatesToCrossToReachEnd = new HashSet<TriangleVertexIdentifiers>();
        foreach (TriangleVertexIdentifiers c in BarycentricCoordinates.Coordinates)
        {
            float cValue = flatEndInStartBase.BarycentricCoordinates.GetCoordinate(c);
            if (cValue <= 0)
            {
                coordinatesToCrossToReachEnd.Add(c);
            }
        }

        if (coordinatesToCrossToReachEnd.Count == 0)
        {
            return new Maybe<SurfacePoint>.Nothing();
        }

        // if (crossedCoordinates.Count > 1) { Debug.Log("more than one coordinate changed"); }
        List<TriangleVertexIdentifiers> actuallyCrossedCoordinates = // Tries not to cross edges if moving parallel
                    coordinatesToCrossToReachEnd
                    .Where(c => !already0Coordinates.Contains(c))
                    .ToList();

        TriangleVertexIdentifiers crossedCoordinate = TriangleVertexIdentifiers.A; // FIXME: not int
        float coefficient = float.MaxValue; // FIXME: min or max coeff? (min? abs min?)
        foreach (TriangleVertexIdentifiers c in actuallyCrossedCoordinates)
        {
            float newCoefficient = -start.BarycentricVector
                        .BarycentricCoordinates.GetCoordinate(c)
                        / startToFlatEnd.BarycentricCoordinates.GetCoordinate(c);
            if (Mathf.Abs(newCoefficient) < Mathf.Abs(coefficient))
            {
                coefficient = newCoefficient;
                crossedCoordinate = c;
            }

        }

        BarycentricCoordinates intersectionCoordinates = new BarycentricCoordinates(
           /* crossedCoordinates2.Contains(TriangleVertexIdentifiers.A) ? 0 :*/
           (start.BarycentricVector.BarycentricCoordinates + coefficient * startToFlatEnd.BarycentricCoordinates).a,
           /* crossedCoordinates2.Contains(TriangleVertexIdentifiers.B) ? 0 :*/
           (start.BarycentricVector.BarycentricCoordinates + coefficient * startToFlatEnd.BarycentricCoordinates).b,
           /*crossedCoordinates2.Contains(TriangleVertexIdentifiers.C) ? 0 :*/
           (start.BarycentricVector.BarycentricCoordinates + coefficient * startToFlatEnd.BarycentricCoordinates).c
        );

        BarycentricVector intersectionVector =
            new BarycentricVector(
                start.Face,
                intersectionCoordinates);

        intersectionVector = intersectionVector.Normalize();

        if (!intersectionVector.IsPointOnBaseTriangle())
        {
            Debug.Log("Before change, not norm");
        }

        HashSet<TriangleVertexIdentifiers> sharedVertices = new HashSet<TriangleVertexIdentifiers>(BarycentricCoordinates.Coordinates);
        // sharedVertices.RemoveWhere(sv => actuallyCrossedCoordinates.Contains(sv));
        sharedVertices.Remove(crossedCoordinate);

        HashSet<Face> facesSharingChangedCoordinates = start.Face.GetFacesFromSharedVertices(sharedVertices);
        facesSharingChangedCoordinates.Remove(start.Face);
        if (facesSharingChangedCoordinates.Count == 0)
        {
            facesSharingChangedCoordinates = start.Face.GetFacesFromAtLeastOneSharedVertex(sharedVertices);
            facesSharingChangedCoordinates.Remove(start.Face);
        }

        if (facesSharingChangedCoordinates.Count == 0
            || facesSharingChangedCoordinates
                .Where(face => intersectionVector.ChangeBase(face).IsPointOnBaseTriangle())
                .Count() == 0)
        {
            return new Maybe<SurfacePoint>.Nothing();
        }

        Face nextFace = facesSharingChangedCoordinates
            .Where(face => intersectionVector.ChangeBase(face).IsPointOnBaseTriangle())
            .First();

        intersectionVector = intersectionVector.ChangeBase(nextFace);
        intersectionVector = intersectionVector.Normalize();

        if (!intersectionVector.IsPointOnBaseTriangle())
        {
            Debug.Log("After change, not norm");
        }

        return new Maybe<SurfacePoint>.Just(new SurfacePoint(nextFace, intersectionVector));
    }

    private static Triangle ProjectOnPlane_Oy(Triangle triangle)
    {
        return new Triangle(
            new CartesianVector(
                new Vector3(triangle.A.Position.x, 0, triangle.A.Position.z)),
            new CartesianVector(
                new Vector3(triangle.B.Position.x, 0, triangle.B.Position.z)),
            new CartesianVector(
                new Vector3(triangle.C.Position.x, 0, triangle.C.Position.z)));
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
