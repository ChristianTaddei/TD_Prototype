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

        List<SurfacePoint> noDuplicatePositions = new List<SurfacePoint>();
        noDuplicatePositions.Add(allPoints[0]);
        for (int i = 1; i < allPoints.Count; i++)
        {
            SurfacePoint p1 = allPoints[i - 1], p2 = allPoints[i];
            if (!(UnityEngine.Mathf.Abs(p1.Position.x - p2.Position.x) < 0.0001f
                && UnityEngine.Mathf.Abs(p1.Position.y - p2.Position.y) < 0.0001f
                && UnityEngine.Mathf.Abs(p1.Position.z - p2.Position.z) < 0.0001f))
            {
                noDuplicatePositions.Add(p2);
            }
        }

        return new Maybe<SurfacePath>.Just(new SurfacePath(noDuplicatePositions));
    }

    public Maybe<SurfacePoint> GetIntersectionToward(SurfacePoint start, SurfacePoint end)
    {
        if (start.Position == end.Position)
        {
            return new Maybe<SurfacePoint>.Just(start);
        }

        Triangle flatStartBase = ProjectOnPlane_Oy(start.BarycentricVector.Base);

        BarycentricVector endInFlatStartBase = new BarycentricVector(
            flatStartBase,
            new CartesianVector(end.BarycentricVector.Position).Project(flatStartBase));

        BarycentricVector flatEndInStartBase = new BarycentricVector(
            start.BarycentricVector.Base,
            endInFlatStartBase.BarycentricCoordinates);

        BarycentricVector startToFlatEnd = flatEndInStartBase - start.BarycentricVector;

        float coefficient = float.MaxValue;
        foreach (TriangleVertexIdentifiers c in Triangle.Vertices)
        {
            if (start.BarycentricVector.BarycentricCoordinates.GetCoordinate(c) == 0.0f)
            {
                if (startToFlatEnd.BarycentricCoordinates.GetCoordinate(c) >= 0.0f)
                {
                    continue;
                }
            }

            float denominator = startToFlatEnd.BarycentricCoordinates.GetCoordinate(c);
            if (denominator == 0.0f)
            {
                coefficient = 0;
                break; // not necessary
            }
            else
            {
                float partialCoefficient;
                // if (denominator >= 0.0f)
                // {
                partialCoefficient = -start.BarycentricVector.BarycentricCoordinates.GetCoordinate(c) / denominator;
                // }
                // else
                // {
                //     partialCoefficient = (1 - start.BarycentricVector.BarycentricCoordinates.GetCoordinate(c)) / denominator;
                // }

                if (partialCoefficient >= 0 && partialCoefficient < coefficient)
                {
                    coefficient = partialCoefficient;
                }
            }
        }
        // }

        BarycentricCoordinates intersectionCoordinates = new BarycentricCoordinates(
           (start.BarycentricVector.BarycentricCoordinates + coefficient * startToFlatEnd.BarycentricCoordinates).a,
           (start.BarycentricVector.BarycentricCoordinates + coefficient * startToFlatEnd.BarycentricCoordinates).b,
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

        HashSet<TriangleVertexIdentifiers> sharedVertices = new HashSet<TriangleVertexIdentifiers>(Triangle.Vertices);
        foreach (TriangleVertexIdentifiers c in Triangle.Vertices)
        {
            if (intersectionCoordinates.GetCoordinate(c) != 1.0f)
            {
                sharedVertices.Add(c);
            }
            // if (intersectionCoordinates.GetCoordinate(c) == 0.0f)
            // {
            //     HashSet<TriangleVertexIdentifiers> otherVertices = new HashSet<TriangleVertexIdentifiers>(Triangle.Vertices);
            //     otherVertices.Remove(c);
            //     sharedVertices.Union(otherVertices);
            // }
        }

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
            .ToArray()[
                new System.Random().Next(
                    facesSharingChangedCoordinates
                    .Where(face => intersectionVector.ChangeBase(face).IsPointOnBaseTriangle())
                    .ToArray().Length)];
        // .OrderBy(face => Vector3.Distance(
        //     new SurfacePoint(face,
        //     new BarycentricVector(face, new Vector3(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f))).Position,
        //     end.Position))
        // .First();

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
