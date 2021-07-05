using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfacePoint : Vector
{
    public BarycentricVector BarycentricVector;

    public override Vector3 FloatRepresentation => BarycentricVector.FloatRepresentation;
    public AbstractFace Face { get; private set; }

    public SurfacePoint(AbstractFace face, BarycentricVector barycentricVector)
    {
        this.Face = face;
        this.BarycentricVector = barycentricVector;
    }

    private SurfacePoint() { }
}
