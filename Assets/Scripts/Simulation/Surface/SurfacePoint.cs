using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfacePoint : IVector
{
    public BarycentricVector BarycentricVector;

    public override Vector3 Position => BarycentricVector.Position;
    public Face Face { get; private set; }

    public SurfacePoint(Face face, BarycentricVector barycentricVector)
    {
        this.Face = face;
        this.BarycentricVector = barycentricVector;
    }

    private SurfacePoint() { }
}
