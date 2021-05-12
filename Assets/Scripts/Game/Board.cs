using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private Surface surface;

    public List<Vector3> Vertices;
    public List<int> Triangles { get; private set; }

    public Board(Surface surface)
    {
        this.surface = surface;

        Vertices = new List<Vector3>();
        Triangles = new List<int>();

        foreach (Face f in surface.Faces)
        {
            foreach (TriangleVertexIdentifiers vName in Triangle.Vertices)
            {
                Vector3 vertex = f.GetVertex(vName).Position;
                if (!Vertices.Contains(vertex))
                {
                    Vertices.Add(vertex);
                }

                Triangles.Add(Vertices.IndexOf(vertex));
            }
        }
    }

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
}