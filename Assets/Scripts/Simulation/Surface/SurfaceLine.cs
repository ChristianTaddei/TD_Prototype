using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceLine 
{
    public SurfacePoint start { get;}
    public SurfacePoint end { get;}

    private List<SurfacePoint> crossingPoints;

    public static bool FromPoints(object disjointedPoint1, object disjointedPoint2, out SurfaceLine surfaceLine)
    {
        surfaceLine = new SurfaceLine();
        return false;
    } 
}
