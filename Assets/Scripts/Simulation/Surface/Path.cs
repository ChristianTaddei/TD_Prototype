using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfacePath
{
    public IPoint Start { get => points.First(); }
    public IPoint End { get => points.Last(); }

    private List<SurfacePoint> points;

    private SurfacePath()
    {
        points = new List<SurfacePoint>();
    }

    public static bool TryMakeDirectPath(SurfacePoint startPoint, SurfacePoint endPoint, out SurfacePath directPath)
    {
        directPath = new SurfacePath();

        Surface surface;
        if(startPoint.Surface == endPoint.Surface){
            surface = endPoint.Surface;
        } else {
            return false; 
        }

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
