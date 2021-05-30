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

    public override bool Equals(object obj)
    {
        return obj is SurfacePoint point &&
               base.Equals(obj) &&
               Position.Equals(point.Position) &&
               EqualityComparer<BarycentricVector>.Default.Equals(BarycentricVector, point.BarycentricVector) &&
               Position.Equals(point.Position) &&
               EqualityComparer<Face>.Default.Equals(Face, point.Face);
    }
}
