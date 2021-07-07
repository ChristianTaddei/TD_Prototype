using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfacePoint : Vector
{
    public BarycentricVector BarycentricVector;

    public Vector3 FloatRepresentation => BarycentricVector.FloatRepresentation;
    public Face Face { get; private set; }

    public SurfacePoint(Face face, BarycentricVector barycentricVector)
    {
        this.Face = face;
        this.BarycentricVector = barycentricVector;
    }

    private SurfacePoint() { }
}
