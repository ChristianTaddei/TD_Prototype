using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmutableVector : AbstractVector
{
	public override Vector3 FloatRepresentation => cartesianCoordinates;

	private readonly Vector3 cartesianCoordinates;

	public static new ImmutableVector From(Vector3 vec3)
	{
		return new ImmutableVector(vec3);
	}

	public static ImmutableVector From(float x, float y, float z)
	{
		return new ImmutableVector(new Vector3(x, y, z));
	}

	public static new ImmutableVector Copy(Vector otherVector)
	{
		return new ImmutableVector(otherVector.FloatRepresentation);
	}

	private ImmutableVector(Vector3 vector3)
	{
		this.cartesianCoordinates = vector3;
	}
}
