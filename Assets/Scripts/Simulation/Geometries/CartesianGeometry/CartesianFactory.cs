using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartesianFactory
{
	private readonly CartesianGeometry cartesianGeometry;

	public CartesianFactory(CartesianGeometry cartesianGeometry)
	{
		this.cartesianGeometry = cartesianGeometry;
	}

	public CartesianVector VectorFromVec3(Vector3 vec3)
	{
		return new CartesianVector(vec3);
	}

	public virtual CartesianVector VectorFromCoordinates(float v1, float v2, float v3)
	{
		return new CartesianVector(v1, v2, v3);
	}
}
