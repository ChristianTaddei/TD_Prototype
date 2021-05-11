using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisjointedSurface
{
    public static readonly Surface Surface = new Surface();

    public static readonly Face Face1 =
        Surface.AddFace(
            (CartesianPoint)new Vector3(0, 0, 0),
            (CartesianPoint)new Vector3(1, 0, 0),
            (CartesianPoint)new Vector3(0, 1, 0));

    public static readonly Face Face2 =
        Surface.AddFace(
            (CartesianPoint)new Vector3(1, 1, 0),
            (CartesianPoint)new Vector3(2, 1, 0),
            (CartesianPoint)new Vector3(1, 2, 0));

    public static readonly SurfacePoint PointOn1 =
        new SurfacePoint(
                Face1,
                new BarycentricVector(
                    Face1,
                    new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint PointOn2 =
        new SurfacePoint(
                Face2,
                new BarycentricVector(
                    Face2,
                    new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint BaricentreOf1 =
        new SurfacePoint(
                Face1,
                new BarycentricVector(
                    Face1,
                    new BarycentricCoordinates(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f)));
}
