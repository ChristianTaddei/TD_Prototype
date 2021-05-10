using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle_ACDF
{
    // A rectangle made of four faces (two squares side by side).
    /*
        B --- D --- F  // TODO:make all CCW
        |  \  |  \  |
        A --- C --- E
    */
    public static readonly Surface Surface = new Surface();

    private static readonly CartesianPoint _A = new Vector3(0, 0, 0);
    private static readonly CartesianPoint _B = new Vector3(0, 1, 0);
    private static readonly CartesianPoint _C = new Vector3(1, 0, 0);
    private static readonly CartesianPoint _D = new Vector3(1, 1, 0);
    private static readonly CartesianPoint _E = new Vector3(2, 0, 0);
    private static readonly CartesianPoint _F = new Vector3(2, 1, 0);

    public static readonly Face ABC = Surface.AddFace(_A, _B, _C);
    public static readonly Face BCD = Surface.AddFace(_B, _C, _D);
    public static readonly Face CDE = Surface.AddFace(_C, _D, _E);
    public static readonly Face DEF = Surface.AddFace(_D, _E, _F);

    public static readonly SurfacePoint A = new SurfacePoint(
       ABC,
       new BarycentricVector(
           ABC.Triangle,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint BCD_B = new SurfacePoint(
       BCD,
       new BarycentricVector(
           BCD.Triangle,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint CDE_C = new SurfacePoint(
       CDE,
       new BarycentricVector(
           CDE.Triangle,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint CDE_E = new SurfacePoint(
       CDE,
       new BarycentricVector(
           CDE.Triangle,
           new BarycentricCoordinates(0, 0, 1)));

    public static readonly SurfacePoint F = new SurfacePoint(
       DEF,
       new BarycentricVector(
           DEF.Triangle,
           new BarycentricCoordinates(0, 0, 1)));
}
