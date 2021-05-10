using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_abcd
{
    // A square made of two faces.
    public static readonly Surface Surface = new Surface();

    public static readonly Face Triangle_abc = 
        Surface.AddFace(
            (CartesianPoint)new Vector3(0, 0, 0),
            (CartesianPoint)new Vector3(0, 1, 0),
            (CartesianPoint)new Vector3(1, 0, 0));

    public static readonly Face Triangle_bcd = 
        Surface.AddFace(
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
}
