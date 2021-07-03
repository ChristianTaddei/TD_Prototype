using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfacePath
{
    public static SurfacePath NO_PATH = new SurfacePath();

    public Vector Start { get => points.First(); }
    public Vector End { get => points.Last(); }

    private List<SurfacePoint> points;
    public List<SurfacePoint> Points { get => new List<SurfacePoint>(points);}

    private SurfacePath()
    {
        this.points = new List<SurfacePoint>();
    }

    public SurfacePath(List<SurfacePoint> points)
    {
        this.points = points;
    }

}
