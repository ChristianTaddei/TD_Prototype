using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: try with generics -> Factory<T> -> PointFactory<T> : Factory<T extends Point> -> CartesianPointFactory : PointFactory<CartesianPoint> 
// mb factory isnt needed for simple (no exc?) data structures (or it is to ensure immut?)? Avoid using pattern just because?
public class CartesianFactory
{
	public CartesianVector VectorFromVec3(Vector3 vec3)
	{
		return new CartesianVector(vec3);
	}

	public CartesianVector VectorFromCoordinates(float v1, float v2, float v3)
	{
		return new CartesianVector(v1, v2, v3);
	}

	public CartesianVector Copy(CartesianVector otherCV)
	{
		return new CartesianVector(otherCV);
	}
}
