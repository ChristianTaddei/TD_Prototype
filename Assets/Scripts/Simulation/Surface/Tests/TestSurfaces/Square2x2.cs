using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square2x2
{
    /*
        02 --- 12 --- 22
        |   \  |   \  |  
        01 --- 11 --- 21  
        |   \  |   \  |  
        00 --- 10 --- 20  
    */
    public static readonly Surface Surface = new Surface();

    private static readonly CartesianVector _00 = new Vector3(0, 0, 0);
    private static readonly CartesianVector _01 = new Vector3(0, 0, 1);
    private static readonly CartesianVector _02 = new Vector3(0, 0, 2);
    private static readonly CartesianVector _10 = new Vector3(1, 0, 0);
    private static readonly CartesianVector _11 = new Vector3(1, 0, 1);
    private static readonly CartesianVector _12 = new Vector3(1, 0, 2);
    private static readonly CartesianVector _20 = new Vector3(2, 0, 0);
    private static readonly CartesianVector _21 = new Vector3(2, 0, 1);
    private static readonly CartesianVector _22 = new Vector3(2, 0, 2);

    public static readonly Face _00_01_10 = Surface.AddFace(_00, _01, _10);
    public static readonly Face _01_11_10 = Surface.AddFace(_01, _11, _10);
    public static readonly Face _01_02_11 = Surface.AddFace(_01, _02, _11);
    public static readonly Face _02_12_22 = Surface.AddFace(_02, _12, _22);
    public static readonly Face _10_11_20 = Surface.AddFace(_10, _11, _20);
    public static readonly Face _11_21_20 = Surface.AddFace(_11, _21, _20);
    public static readonly Face _11_12_21 = Surface.AddFace(_11, _12, _21);
    public static readonly Face _12_22_21 = Surface.AddFace(_12, _22, _21);

    public static readonly SurfacePoint centre_10_11_20 = new SurfacePoint(
       _10_11_20,
       new BarycentricVector(
           _10_11_20,
           new BarycentricCoordinates(0, 1, 0)));

    public static readonly SurfacePoint _00on_00_01_10 = new SurfacePoint(
       _00_01_10,
       new BarycentricVector(
           _00_01_10,
           new BarycentricCoordinates(1, 0, 0)));
           
    public static readonly SurfacePoint m_01_10 = new SurfacePoint(
        _01_11_10,
        new BarycentricVector(
            _01_11_10,
            new BarycentricCoordinates(.5f, 0, .5f)));
}
