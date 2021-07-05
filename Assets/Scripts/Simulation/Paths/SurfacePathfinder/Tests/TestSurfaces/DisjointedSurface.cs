using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisjointedSurface
{
    public static readonly ConcreteSurface Surface = new ConcreteSurface();

    public static readonly AbstractFace Face1 =
        Surface.AddFace(
            (CartesianVector)new Vector3(0, 0, 0),
            (CartesianVector)new Vector3(1, 0, 0),
            (CartesianVector)new Vector3(0, 0, 1));

    public static readonly AbstractFace Face2 =
        Surface.AddFace(
            (CartesianVector)new Vector3(1, 0, 1),
            (CartesianVector)new Vector3(2, 0, 1),
            (CartesianVector)new Vector3(1, 0, 2));

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
