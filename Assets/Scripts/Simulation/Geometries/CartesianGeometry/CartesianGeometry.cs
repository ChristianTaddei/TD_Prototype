using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartesianGeometry
{
	internal static readonly float FLOAT_REPRESENTATION_EQUALITY_TOLLERANCE = 0.001f;

	// what about "areComplanar" etc methods public while calc ones internal?

	public CartesianVector Sum(CartesianVector v1, CartesianVector v2)
	{
		return new CartesianVector(v1.cartesianCoordinates + v2.cartesianCoordinates);
	}

	public CartesianVector Substract(CartesianVector v1, CartesianVector v2)
	{
		return new CartesianVector(v1.cartesianCoordinates - v2.cartesianCoordinates);
	}

	public float Magnitude(CartesianVector v)
	{
		return v.cartesianCoordinates.magnitude;
	}

	public bool AreCloserThanTollerance(CartesianVector v1, CartesianVector v2)
	{
		return Vector3.Distance(v1.FloatRepresentation, v2.FloatRepresentation) < FLOAT_REPRESENTATION_EQUALITY_TOLLERANCE;
	}

	public float Dot(CartesianVector v1, CartesianVector v2)
	{
		return Vector3.Dot(v1.cartesianCoordinates, v2.cartesianCoordinates);
	}

	public CartesianVector Cross(CartesianVector v1, CartesianVector v2)
	{
		return new CartesianVector(Vector3.Cross(v1.cartesianCoordinates, v2.cartesianCoordinates));
	}

	public bool AreComplanar(CartesianVector a, CartesianVector b, CartesianVector c)
	{
		float mixedProd = Dot(a, Cross(b, c));

		if (System.Math.Abs(mixedProd) <= 0.001f)
		{
			return true;
		}

		return false;
	}

	public CartesianVector Project(CartesianVector v, Triangle triangleDefiningPlane)
	{
		CartesianVector projectedVector;

		CartesianVector Plane_AB = new CartesianVector(triangleDefiningPlane.B.FloatRepresentation - triangleDefiningPlane.A.FloatRepresentation);
		CartesianVector Plane_AC = new CartesianVector(triangleDefiningPlane.C.FloatRepresentation - triangleDefiningPlane.A.FloatRepresentation);
		CartesianVector Plane_n = Cross(Plane_AB, Plane_AC);
		CartesianVector AP = new CartesianVector(v.FloatRepresentation - triangleDefiningPlane.A.FloatRepresentation);

		float squareMag = Magnitude(Plane_n) * Magnitude(Plane_n);
		projectedVector = Substract(v, (new CartesianVector((Dot(AP, Plane_n) / squareMag) * Plane_n.FloatRepresentation)));

		return projectedVector;
	}
}
