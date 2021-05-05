using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSurfaceElements
{
    // TODO: check immutability and/or setup requirements for this to be safe
    public static readonly Surface disjointedSurface = new Surface();
    public static readonly Face disjointedFace1 =
        disjointedSurface.AddFace(
            (CartesianPoint)new Vector3(0, 1, 0),
            (CartesianPoint)new Vector3(0, 1, 1),
            (CartesianPoint)new Vector3(1, 1, 0));
    public static readonly Face disjointedFace2 =
        disjointedSurface.AddFace(
            (CartesianPoint)new Vector3(0, 2, 0),
            (CartesianPoint)new Vector3(0, 2, 2),
            (CartesianPoint)new Vector3(2, 2, 0));
    public static readonly SurfacePoint disjointedPoint1 =
        new SurfacePoint(
                disjointedFace1,
                new BarycentricVector(
                    disjointedFace1.Triangle,
                    new BarycentricCoordinates(1, 0, 0)));
    public static readonly SurfacePoint disjointedPoint2 =
        new SurfacePoint(
                disjointedFace2,
                new BarycentricVector(
                    disjointedFace1.Triangle,
                    new BarycentricCoordinates(1, 0, 0)));
    public static readonly SurfacePoint disjointedPointMiddle =
        new SurfacePoint(
                disjointedFace1,
                new BarycentricVector(
                    disjointedFace1.Triangle,
                    new BarycentricCoordinates(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f)));

    // TODO: if this works make static too

    // A square made of two faces, sharing points b and c.
    private Surface Square_abcd;
    private Face Triangle_abc, Triangle_bcd;
    private SurfacePoint a, b_abc, c_abc, b_bcd, c_bcd, d;

    public void Setup()
    {
        #region ABC/BCD Square

        Square_abcd = new Surface();

        Triangle_abc = Square_abcd.AddFace(
            (CartesianPoint)new Vector3(0, 0, 0),
            (CartesianPoint)new Vector3(0, 1, 0),
            (CartesianPoint)new Vector3(1, 0, 0));

        Triangle_bcd = Square_abcd.AddFace(
            (CartesianPoint)new Vector3(0, 1, 0),
            (CartesianPoint)new Vector3(1, 0, 0),
            (CartesianPoint)new Vector3(1, 1, 0));

        a = new SurfacePoint(
            Triangle_abc,
            new BarycentricVector(
                Triangle_abc.Triangle,
                new BarycentricCoordinates(1, 0, 0)));

        b_abc = new SurfacePoint(
            Triangle_abc,
            new BarycentricVector(
                Triangle_abc.Triangle,
                new BarycentricCoordinates(0, 1, 0)));

        c_abc = new SurfacePoint(
            Triangle_abc,
            new BarycentricVector(
                Triangle_abc.Triangle,
                new BarycentricCoordinates(0, 0, 1)));

        b_bcd = new SurfacePoint(
            Triangle_bcd,
            new BarycentricVector(
                Triangle_bcd.Triangle,
                new BarycentricCoordinates(1, 0, 0)));

        c_bcd = new SurfacePoint(
            Triangle_bcd,
            new BarycentricVector(
                Triangle_bcd.Triangle,
                new BarycentricCoordinates(0, 1, 0)));

        d = new SurfacePoint(
            Triangle_bcd,
            new BarycentricVector(
                Triangle_bcd.Triangle,
                new BarycentricCoordinates(0, 0, 1)));

        #endregion
    }
}
