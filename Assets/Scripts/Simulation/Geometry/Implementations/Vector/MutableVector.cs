using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutableVector : Vector
{
	public override Vector3 FloatRepresentation => cartesianCoordinates;

	public float x { get => cartesianCoordinates.x; set => cartesianCoordinates.x = value; }
	public float y { get => cartesianCoordinates.y; set => cartesianCoordinates.y = value; }
	public float z { get => cartesianCoordinates.z; set => cartesianCoordinates.z = value; }

	private Vector3 cartesianCoordinates;

	public void Set(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public static new MutableVector From(Vector3 vec3)
	{
		return new MutableVector(vec3);
	}

	public static new MutableVector From(float x, float y, float z)
	{
		return new MutableVector(new Vector3(x, y, z));
	}

	public static new MutableVector Copy(Vector otherVector)
	{
		return new MutableVector(otherVector.FloatRepresentation);
	}

	private MutableVector(Vector3 vector3)
	{
		this.cartesianCoordinates = vector3;
	}
}
