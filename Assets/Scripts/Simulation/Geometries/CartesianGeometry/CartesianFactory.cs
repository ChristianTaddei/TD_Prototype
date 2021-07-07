using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartesianFactory : VectorFactory
{
	public override Vector VectorFromVec3(Vector3 vec3)
	{
		return new CartesianVector(vec3);
	}

	public override Vector Copy(Vector otherVector)
	{
		return new CartesianVector(
			otherVector.FloatRepresentation.x,
			otherVector.FloatRepresentation.y,
			otherVector.FloatRepresentation.z);
	}
}
