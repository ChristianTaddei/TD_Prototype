using System;
using System.Linq;
using UnityEngine;

public class SurfacePoint : IPoint
{
    private BarycentricVector barycentricVector;

    public Vector3 Coordinates => barycentricVector.Coordinates;
    public Face Face { get; private set; }

    public SurfacePoint(Face face, BarycentricVector barycentricVector)
    {
        this.Face = face;
        this.barycentricVector = barycentricVector;
    }

    private SurfacePoint() { }

    internal bool TryGetIntersectionToward(CartesianVector direction, out SurfacePoint intersection)
    {
        intersection = new SurfacePoint();

        // BarycentricVector projectedDirection = BarycentricVector.FormCartesianVector(direction);

        return false;
    }

    // public SurfacePoint(Vertex spawningPosition)
    // // TODO: tryGet 
    //     : this(boardState, spawningPosition.Faces.First(), boardState.VertexStates[spawningPosition].Position)
    // {

    // }

    // public static BarycentricCoordinates FromFaceAndPoint(Face face, Vector3 p)
    // {

    //     Vector3 a = boardState.VertexStates[face.a].Position;
    //     Vector3 b = boardState.VertexStates[face.b].Position;
    //     Vector3 c = boardState.VertexStates[face.c].Position;

    //     Vector3 n = Vector3.Cross(b - a, c - a);
    //     Vector3 n_a = Vector3.Cross(c - b, p - b);
    //     Vector3 n_b = Vector3.Cross(a - c, p - c);
    //     Vector3 n_c = Vector3.Cross(b - a, p - a);

    //     float squareMag = n.magnitude * n.magnitude;

    //     return new BarycentricCoordinates(
    //         Vector3.Dot(n, n_a) / squareMag,
    //         Vector3.Dot(n, n_b) / squareMag,
    //         Vector3.Dot(n, n_c) / squareMag);
    // }
}
