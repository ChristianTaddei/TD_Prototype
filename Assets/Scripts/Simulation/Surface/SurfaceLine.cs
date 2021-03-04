using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceLine
{
    public SurfacePoint start { get; }
    public SurfacePoint end { get; }

    private List<SurfacePoint> crossingPoints;

    private SurfaceLine()
    {
        crossingPoints = new List<SurfacePoint>();
    }

    public static bool FromPoints(SurfacePoint startPoint, SurfacePoint endPoint, out SurfaceLine surfaceLine)
    {
        surfaceLine = new SurfaceLine();

        // use 2d geometry for dir & checks?
        SurfaceDirection dir = new SurfaceDirection(startPoint, endPoint);

        // currentPoint = start
        // while currentPoint.Face != end.Face
        //      currentPoint = currentPoint.GetIntersectionToward(dir)
        //      crossingPoint.Add(currentPoint)

        // check last inters + dir crosses end

        return false;
    }
}
