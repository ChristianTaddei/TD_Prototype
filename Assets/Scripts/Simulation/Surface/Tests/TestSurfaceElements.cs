using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSurfaceElements
{
    #region Disjointed Surface
    // TODO: check immutability and/or setup requirements for this to be safe
    public static readonly Surface disjointedSurface = new Surface();

    public static readonly Face disjointedFace1 =
        disjointedSurface.AddFace(
            (CartesianPoint)new Vector3(0, 0, 0),
            (CartesianPoint)new Vector3(1, 0, 0),
            (CartesianPoint)new Vector3(0, 1, 0));

    public static readonly Face disjointedFace2 =
        disjointedSurface.AddFace(
            (CartesianPoint)new Vector3(1, 1, 0),
            (CartesianPoint)new Vector3(2, 1, 0),
            (CartesianPoint)new Vector3(1, 2, 0));

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
                    disjointedFace2.Triangle,
                    new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint disjointedPointMiddle =
        new SurfacePoint(
                disjointedFace1,
                new BarycentricVector(
                    disjointedFace1.Triangle,
                    new BarycentricCoordinates(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f)));
    #endregion

    #region ABC/BCD Square
    // A square made of two faces, sharing points b and c.
    public static readonly Surface Square_abcd = new Surface();

    public static readonly Face Triangle_abc = 
        Square_abcd.AddFace(
            (CartesianPoint)new Vector3(0, 0, 0),
            (CartesianPoint)new Vector3(0, 1, 0),
            (CartesianPoint)new Vector3(1, 0, 0));

    public static readonly Face Triangle_bcd = 
        Square_abcd.AddFace(
            (CartesianPoint)new Vector3(0, 1, 0),
            (CartesianPoint)new Vector3(1, 0, 0),
            (CartesianPoint)new Vector3(1, 1, 0));

    public static readonly SurfacePoint a = new SurfacePoint(
       Triangle_abc,
       new BarycentricVector(
           Triangle_abc.Triangle,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint b_abc = new SurfacePoint(
       Triangle_abc,
       new BarycentricVector(
           Triangle_abc.Triangle,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint c_abc = new SurfacePoint(
       Triangle_abc,
       new BarycentricVector(
           Triangle_abc.Triangle,
           new BarycentricCoordinates(0, 0, 1)));

    public static readonly SurfacePoint b_bcd = new SurfacePoint(
       Triangle_bcd,
       new BarycentricVector(
           Triangle_bcd.Triangle,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint c_bcd = new SurfacePoint(
       Triangle_bcd,
       new BarycentricVector(
           Triangle_bcd.Triangle,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint d = new SurfacePoint(
       Triangle_bcd,
       new BarycentricVector(
           Triangle_bcd.Triangle,
           new BarycentricCoordinates(0, 0, 1)));

    #endregion
}
