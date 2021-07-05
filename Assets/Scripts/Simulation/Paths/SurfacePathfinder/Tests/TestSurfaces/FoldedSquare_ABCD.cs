using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldedSquare_ABCD
{
   /*
        C <<< B 
        |  \  v  
        D --- A 
    */
    public static readonly ConcreteSurface Surface = new ConcreteSurface();

    private static readonly CartesianVector _A = new Vector3(1, 0, 0);
    private static readonly CartesianVector _B = new Vector3(1, 1, 1);
    private static readonly CartesianVector _C = new Vector3(0, 0, 1);
    private static readonly CartesianVector _D = new Vector3(0, 0, 0);

    public static readonly AbstractFace ABC = Surface.AddFace(_A, _B, _C);
    public static readonly AbstractFace ADC = Surface.AddFace(_A, _D, _C);

    public static readonly SurfacePoint ABC_A = new SurfacePoint(
       ABC,
       new BarycentricVector(
           ABC,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint ABC_B = new SurfacePoint(
       ABC,
       new BarycentricVector(
           ABC,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint ABC_C = new SurfacePoint(
       ABC,
       new BarycentricVector(
           ABC,
           new BarycentricCoordinates(0, 0, 1)));

    public static readonly SurfacePoint ADC_A = new SurfacePoint(
       ADC,
       new BarycentricVector(
           ADC,
           new BarycentricCoordinates(1, 0, 0)));

    public static readonly SurfacePoint ADC_D = new SurfacePoint(
       ADC,
       new BarycentricVector(
           ADC,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint ADC_C = new SurfacePoint(
       ADC,
       new BarycentricVector(
           ADC,
           new BarycentricCoordinates(0, 0, 1)));

    public static readonly SurfacePoint CenterOnABC = new SurfacePoint(
                ABC,
                new BarycentricVector(
                    ABC,
                    new BarycentricCoordinates(0.5f, 0.0f, 0.5f)));

    public static readonly SurfacePoint CenterOnADC = new SurfacePoint(
                ADC,
                new BarycentricVector(
                    ADC,
                    new BarycentricCoordinates(0.5f, 0.0f, 0.5f)));

    public static readonly SurfacePoint Barycentre_ABC = new SurfacePoint(
                ABC,
                new BarycentricVector(
                    ABC,
                    new BarycentricCoordinates(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f)));

    public static readonly SurfacePoint PointNotOnEdge_ABC = new SurfacePoint(
                ABC,
                new BarycentricVector(
                    ABC,
                    new BarycentricCoordinates(2.0f / 3.0f, 1.0f / 6.0f, 1.0f / 6.0f)));

    public static readonly SurfacePoint Barycentre_ADC = new SurfacePoint(
                ADC,
                new BarycentricVector(
                    ADC,
                    new BarycentricCoordinates(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f)));
}