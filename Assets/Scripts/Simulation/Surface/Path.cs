using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public SurfacePoint start { get; }
    public SurfacePoint end { get; }

    private List<SurfacePoint> crossingPoints;

    private Path()
    {
        crossingPoints = new List<SurfacePoint>();
    }

    public static bool TryMakeDirectPath(SurfacePoint startPoint, SurfacePoint endPoint, out Path directPath)
    {
        directPath = new Path();
        List<SurfacePoint> crossingPoints = new List<SurfacePoint>();

        CartesianVector startToEndDirection = CartesianVector.FromPoints(startPoint, endPoint);

        SurfacePoint currentPoint = startPoint;
        while (currentPoint.Face != endPoint.Face)
        {
            SurfacePoint intersection;
            if (currentPoint.TryGetIntersectionToward(startToEndDirection, out intersection))
            {
                crossingPoints.Add(intersection);
                currentPoint = intersection; // TODO: multi face points
            }
            else
            {
                return false;
            }
        }

        // check last inters + dir crosses end

        return false;
    }
}
