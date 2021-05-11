using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldedRectangle_ABDE
{
    /*
        D >>> C --- B
        v  \  ^  _| v
        E <<< F >>> A
    */
    public static readonly Surface Surface = new Surface();

    private static readonly CartesianVector _A = new Vector3(2, 0, 2);
    private static readonly CartesianVector _B = new Vector3(2, 1, 1);
    private static readonly CartesianVector _C = new Vector3(1, 1, 1);
    private static readonly CartesianVector _D = new Vector3(0, 1, 0);
    private static readonly CartesianVector _E = new Vector3(0, 0, 1);
    private static readonly CartesianVector _F = new Vector3(1, 0, 0);

    public static readonly Face ACB = Surface.AddFace(_A, _C, _B);
    public static readonly Face AFC = Surface.AddFace(_A, _F, _C);
    public static readonly Face FDC = Surface.AddFace(_F, _D, _C);
    public static readonly Face FED = Surface.AddFace(_F, _E, _D);

    public static readonly SurfacePoint AFC_A = new SurfacePoint(
       AFC,
       new BarycentricVector(
           AFC,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint ACB_B = new SurfacePoint(
        ACB,
        new BarycentricVector(
        ACB,
        new BarycentricCoordinates(0, 0, 1)));

    public static readonly SurfacePoint FDC_C = new SurfacePoint(
       FDC,
       new BarycentricVector(
           FDC,
           new BarycentricCoordinates(0, 0, 1)));

    public static readonly SurfacePoint FDC_D = new SurfacePoint(
       FDC,
       new BarycentricVector(
           FDC,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint FED_E = new SurfacePoint(
       FED,
       new BarycentricVector(
           FED,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint AFC_F = new SurfacePoint(
       AFC,
       new BarycentricVector(
           AFC,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint m_AB_ACB = new SurfacePoint(
       ACB,
       new BarycentricVector(
           ACB,
           new BarycentricCoordinates(0.5f, 0.0f, 0.5f)));

    public static readonly SurfacePoint m_AC_ACB = new SurfacePoint(
       ACB,
       new BarycentricVector(
           ACB,
           new BarycentricCoordinates(0.5f, 0.5f, 0.0f)));

    public static readonly SurfacePoint m_CF_AFC = new SurfacePoint(
       AFC,
       new BarycentricVector(
           AFC,
           new BarycentricCoordinates(0.0f, 0.5f, 0.5f)));

    public static readonly SurfacePoint m_FD_FDC = new SurfacePoint(
       FDC,
       new BarycentricVector(
           FDC,
           new BarycentricCoordinates(0.5f, 0.5f, 0.0f)));
}