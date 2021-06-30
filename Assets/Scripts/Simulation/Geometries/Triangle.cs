using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Triangle
{
    public static IEnumerable<TriangleVertexIdentifiers> Vertices =>
        (TriangleVertexIdentifiers[])Enum.GetValues(typeof(TriangleVertexIdentifiers));

    public abstract IVector A { get; }
    public abstract IVector B { get; }
    public abstract IVector C { get; }


    public IVector GetVertex(TriangleVertexIdentifiers v)
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
