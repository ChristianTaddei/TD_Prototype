using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Triangle
{
    public static IEnumerable<TriangleVertexIdentifiers> Vertices =>
        (TriangleVertexIdentifiers[])Enum.GetValues(typeof(TriangleVertexIdentifiers));

    public abstract Vector A { get; }
    public abstract Vector B { get; }
    public abstract Vector C { get; }


    public Vector GetVertex(TriangleVertexIdentifiers v)
    {
        switch (v)
        {
            case TriangleVertexIdentifiers.A:
                return A;
            case TriangleVertexIdentifiers.B:
                return B;
            case TriangleVertexIdentifiers.C:
                return C;
            default:
                throw new Exception("Coordinate name does not exist");
        }
    }

}
