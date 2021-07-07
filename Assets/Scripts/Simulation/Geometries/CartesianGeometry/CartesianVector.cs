using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CartesianGeometryTests")]

public class CartesianVector : Vector
{
	public Vector3 FloatRepresentation =>
		new Vector3(
			cartesianCoordinates.x,
			cartesianCoordinates.y,
			cartesianCoordinates.z
		);

	internal readonly Vector3 cartesianCoordinates;

	internal CartesianVector(CartesianVector other)
	{
		this.cartesianCoordinates = other.cartesianCoordinates;
	}

	internal CartesianVector(float x, float y, float z)
	{
		this.cartesianCoordinates = new Vector3(x, y, z);
	}

	internal CartesianVector(Vector3 vector3)
	{
		this.cartesianCoordinates = vector3;
	}

	// FIXME: Without this override test of copy in factory fails. What Equals is it using?
	public override bool Equals(object obj)
	{
		return obj is CartesianVector vector &&
			   FloatRepresentation.Equals(vector.FloatRepresentation) &&
			   cartesianCoordinates.Equals(vector.cartesianCoordinates);
	}

	public override int GetHashCode()
	{
		int hashCode = -831778624;
		hashCode = hashCode * -1521134295 + FloatRepresentation.GetHashCode();
		hashCode = hashCode * -1521134295 + cartesianCoordinates.GetHashCode();
		return hashCode;
	}
}
