using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartesianVector : IVector
{
    public Vector3 Coordinates =>
        new Vector3(
            cartesianCoordinates.x,
            cartesianCoordinates.y,
            cartesianCoordinates.z
        );

    public float magnitude => cartesianCoordinates.magnitude;

    private readonly Vector3 cartesianCoordinates;

    public CartesianVector(Vector3 cartesianCoordinates)
    {
        this.cartesianCoordinates = cartesianCoordinates;
    }

    public float Dot(CartesianVector v)
    {
        return Vector3.Dot(this.cartesianCoordinates, v.cartesianCoordinates);
    }

    public CartesianVector Cross(CartesianVector v)
    {
        return new CartesianVector(Vector3.Cross(this.cartesianCoordinates, v.cartesianCoordinates));
    }

    public bool isComplanarTo(CartesianVector a, CartesianVector b)
    {
        float mixedProd = this.Dot(a.Cross(b));
        if (System.Math.Abs(mixedProd) <= 0.001f)
        {
            return true;
        }

        return false;
    }

    public static implicit operator CartesianVector(Vector3 v) => new CartesianVector(v);
    // public static implicit operator Vector3(CartesianVector ev) => ev.CartesianCoordinates;

    public static CartesianVector operator +(CartesianVector v1, CartesianVector v2)
    {
        return new CartesianVector(v1.cartesianCoordinates + v2.cartesianCoordinates);
    }

    public static CartesianVector operator -(CartesianVector v1, CartesianVector v2)
    {
        return new CartesianVector(v1.cartesianCoordinates - v2.cartesianCoordinates);
    }
}
