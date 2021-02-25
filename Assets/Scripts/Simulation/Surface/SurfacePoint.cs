using System.Linq;
using UnityEngine;

public class SurfacePoint
{
    public BarycentricCoordinates Barycentrics { get; private set; }
    public Face Face { get; private set; }

    public Vector3 GetCartesians(BoardState boardState)
    {
        return boardState.VertexStates[Face.a].Position * Barycentrics.a
                + boardState.VertexStates[Face.b].Position * Barycentrics.b
                + boardState.VertexStates[Face.c].Position * Barycentrics.c;
    }

    public SurfacePoint(Face face, BarycentricCoordinates barycentric)
    {

        this.Face = face;
        this.Barycentrics = barycentric;
    }

    public SurfacePoint(BoardState boardState, Face face, Vector3 cartesianCoordinate)
    {
        this.Face = face;
        this.Barycentrics = FromFaceAndPoint(boardState, face, cartesianCoordinate);
    }

    public SurfacePoint(BoardState boardState, Vertex spawningPosition)
    // TODO: tryGet 
        : this(boardState, spawningPosition.Faces.First(), boardState.VertexStates[spawningPosition].Position)
    {

    }

    public static BarycentricCoordinates FromFaceAndPoint(BoardState boardState, Face face, Vector3 p)
    {

        Vector3 a = boardState.VertexStates[face.a].Position;
        Vector3 b = boardState.VertexStates[face.b].Position;
        Vector3 c = boardState.VertexStates[face.c].Position;

        Vector3 n = Vector3.Cross(b - a, c - a);
        Vector3 n_a = Vector3.Cross(c - b, p - b);
        Vector3 n_b = Vector3.Cross(a - c, p - c);
        Vector3 n_c = Vector3.Cross(b - a, p - a);

        float squareMag = n.magnitude * n.magnitude;

        return new BarycentricCoordinates(
            Vector3.Dot(n, n_a) / squareMag,
            Vector3.Dot(n, n_b) / squareMag,
            Vector3.Dot(n, n_c) / squareMag);
    }
}
