using System;
using UnityEngine;

public class BarycentricCoordinates
{
    private readonly float _a, _b, _c;

    public float a { get => _a; }
    public float b { get => _b; }
    public float c { get => _c; }

    public BarycentricCoordinates(float a, float b, float c)
    {
        this._a = a;
        this._b = b;
        this._c = c;

        // CooridnatesSumToOne();
    }


    public bool CheckSumToOne()
    {
        if (a + b + c > 1.0001f
            || a + b + c < 0.9999f)
        {
            // Debug.LogWarning("Barycentric does not sum to 1: "
            //     + a + " " + b + " " + c);
            return false;
        }

        return true;
    }

    public bool CheckSumToZero()
    {
        if (a + b + c > 0.0001f
            || a + b + c < -0.0001f)
        {
            // Debug.LogWarning("Barycentric does not sum to 0: "
            //     + a + " " + b + " " + c);
            return false;
        }

        return true;
    }

    public bool CheckInternal(/*bool verbose = false*/)
    {
        if (a > 1.0f
            || a < 0.0f
            || b > 1.0f
            || b < 0.0f
            || c > 1.0f
            || c < 0.0f)
        {
            // if (verbose)
            // {
            //     Debug.LogWarning("Barycentric coordinates outside the triangle:"
            //         + a + " " + b + " " + c);
            // }
            return false;
        }

        return true;
    }

    // Test? or keep in vector only?
    public static BarycentricCoordinates operator +(BarycentricCoordinates b1, BarycentricCoordinates b2)
    {
        return new BarycentricCoordinates(b1.a + b2.a, b1.b + b2.b, b1.c + b2.c);
    }
    public static BarycentricCoordinates operator -(BarycentricCoordinates b1, BarycentricCoordinates b2)
    {
        return new BarycentricCoordinates(b1.a - b2.a, b1.b - b2.b, b1.c - b2.c);
    }
}