using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    public static IEnumerable<TriangleVerticesIdentifier> Vertices =>
        (TriangleVerticesIdentifier[])Enum.GetValues(typeof(TriangleVerticesIdentifier));

    private readonly IPoint _A;
    private readonly IPoint _B;
    private readonly IPoint _C;

    public IPoint A { get => _A; }
    public IPoint B { get => _B; }
    public IPoint C { get => _C; }

    public Triangle(IPoint A, IPoint B, IPoint C)
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

    public IPoint GetVertex(TriangleVerticesIdentifier v)
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
