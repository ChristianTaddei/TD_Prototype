using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfacePath : Path
{
    public List<Vector> Points { get => new List<Vector>(points);}

    public Vector Start { get => points.First(); }
    public Vector End { get => points.Last(); }

    private List<SurfacePoint> points;

    // TODO: internal and requires factory, or public constr?
	public SurfacePath(List<SurfacePoint> points)
    {
        this.points = points;
    }
}
