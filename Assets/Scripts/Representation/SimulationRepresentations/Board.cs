using System;
using System.Collections.Generic;
using UnityEngine;

// TODO: try to remove coupling to surface (merge into?) and geometry
public class Board
{
    private ConcreteSurface surface;

    public List<Vector3> Vertices;
    public List<int> Triangles { get; private set; }

    public Board(ConcreteSurface surface)
    {
        this.surface = surface;

        Vertices = new List<Vector3>();
        Triangles = new List<int>();

        UpdateVertices();
    }

    internal void UpdateVertices()
    {
        Vertices.Clear();
        foreach (ConcreteFace f in surface.Faces)
        {
            foreach (TriangleVertexIdentifiers vName in Triangle.Vertices)
            {
                Vector3 vertex = f.GetVertex(vName).FloatRepresentation;
                if (!Vertices.Contains(vertex))
                {
                    Vertices.Add(vertex);
                }

                Triangles.Add(Vertices.IndexOf(vertex));
            }
        }
    }

    public Maybe<SurfacePoint> GetSurfacePoint(int triangleIndex, Vector3 point)
    {
        return surface.GetSurfacePoint(triangleIndex, point);
    }
}