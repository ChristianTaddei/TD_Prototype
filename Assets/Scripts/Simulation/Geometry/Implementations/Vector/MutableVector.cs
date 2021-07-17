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
	
	public bool Equals(MutableVector other)
	{
		if (other != null)
		{
			return this.FloatRepresentation.Equals(other.FloatRepresentation);
		}

		return false;
	}

	public override bool Equals(object obj)
	{
		return this.Equals(obj as MutableVector);
	}

	public static bool operator ==(MutableVector lhs, MutableVector rhs)
	{
		bool isLhsNull = object.ReferenceEquals(lhs, null);
		bool isRhsNull = object.ReferenceEquals(rhs, null);

		if (isLhsNull && isRhsNull)
			return true;    // TODO: cover with tests (same in stub? or remove stub...)
		else if (isLhsNull)
			return false;   // TODO: cover with tests
		else
			return lhs.Equals(rhs);
	}

	public static bool operator !=(MutableVector lhs, MutableVector rhs)
	{
		return !(lhs == rhs);
	}

	public override int GetHashCode()
	{
		return -509336368 + FloatRepresentation.GetHashCode();
	}

	private MutableVector(Vector3 vector3)
	{
		this.cartesianCoordinates = vector3;
	}
}
