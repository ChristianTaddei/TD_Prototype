using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square_ABCD
{
    // A square made of two faces.
    public static readonly Surface Surface = new Surface();

    public static readonly Face ABC = 
        Surface.AddFace(
            (CartesianPoint)new Vector3(0, 0, 0),
            (CartesianPoint)new Vector3(0, 1, 0),
            (CartesianPoint)new Vector3(1, 0, 0));

    public static readonly Face BCD = 
        Surface.AddFace(
            (CartesianPoint)new Vector3(0, 1, 0),
            (CartesianPoint)new Vector3(1, 0, 0),
            (CartesianPoint)new Vector3(1, 1, 0));

    public static readonly SurfacePoint A = new SurfacePoint(
       ABC,
       new BarycentricVector(
           ABC.Triangle,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint B_ABC = new SurfacePoint(
       ABC,
       new BarycentricVector(
           ABC.Triangle,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint C_ABC = new SurfacePoint(
       ABC,
       new BarycentricVector(
           ABC.Triangle,
           new BarycentricCoordinates(0, 0, 1)));

    public static readonly SurfacePoint B_BCD = new SurfacePoint(
       BCD,
       new BarycentricVector(
           BCD.Triangle,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint C_BCD = new SurfacePoint(
       BCD,
       new BarycentricVector(
           BCD.Triangle,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint D = new SurfacePoint(
       BCD,
       new BarycentricVector(
           BCD.Triangle,
           new BarycentricCoordinates(0, 0, 1)));
}
