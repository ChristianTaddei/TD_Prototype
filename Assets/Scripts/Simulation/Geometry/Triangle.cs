using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    public static IEnumerable<TriangleVerticesIdentifier> Vertices =>
        (TriangleVerticesIdentifier[])Enum.GetValues(typeof(TriangleVerticesIdentifier));

    private readonly IVector _A;
    private readonly IVector _B;
    private readonly IVector _C;

    public IVector A { get => _A; }
    public IVector B { get => _B; }
    public IVector C { get => _C; }

    public Triangle(IVector A, IVector B, IVector C)
    {
        _A = A;
        _B = B;
        _C = C;
    }

    public Triangle(Triangle t)
    {
        _A = t._A;
        _B = t._B;
        _C = t._C;
    }

    public IVector GetVertex(TriangleVerticesIdentifier v)
    {
        switch (v)
        {
            case TriangleVerticesIdentifier.A:
                return A;
            case TriangleVerticesIdentifier.B:
                return B;
            case TriangleVerticesIdentifier.C:
                return C;
            default:
                throw new Exception("Coordinate name does not exist");
        }
    }

}
