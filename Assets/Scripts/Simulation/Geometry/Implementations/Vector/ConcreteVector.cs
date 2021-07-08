using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteVector : Vector
{
	public override Vector3 FloatRepresentation =>
		new Vector3(
			cartesianCoordinates.x,
			cartesianCoordinates.y,
			cartesianCoordinates.z
		);

	private readonly Vector3 cartesianCoordinates;

	public static new ConcreteVector From(Vector3 vec3)
	{
		return new ConcreteVector(vec3);
	}

	public static ConcreteVector From(float x, float y, float z)
	{
		return new ConcreteVector(new Vector3(x, y, z));
	}

	public static new ConcreteVector Copy(Vector otherVector)
	{
		return new ConcreteVector(otherVector.FloatRepresentation);
	}

	private ConcreteVector(Vector3 vector3)
	{
		this.cartesianCoordinates = vector3;
	}
}
