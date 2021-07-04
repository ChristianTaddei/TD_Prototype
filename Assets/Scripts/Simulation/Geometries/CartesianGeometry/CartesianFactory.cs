using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
