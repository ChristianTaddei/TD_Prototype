using UnityEngine;

public class CartesianPoint : IPoint
{
    public Vector3 Coordinates { get => cartesianCoordinates; }

    public CartesianPoint(IPoint point)
    {
        this.cartesianCoordinates = point.Coordinates;
    }

    public CartesianPoint(Vector3 vector)
    {
        this.cartesianCoordinates = vector;
    }

    private readonly Vector3 cartesianCoordinates;

    public static implicit operator CartesianPoint(Vector3 v) => new CartesianPoint(v);
}