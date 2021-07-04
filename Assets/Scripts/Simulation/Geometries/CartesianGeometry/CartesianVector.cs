using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CartesianGeometryTests")]

public class CartesianVector : Vector
{
	public override Vector3 FloatRepresentation =>
		new Vector3(
			cartesianCoordinates.x,
			cartesianCoordinates.y,
			cartesianCoordinates.z
		);

	internal readonly Vector3 cartesianCoordinates;

	internal CartesianVector(CartesianVector otherVector)
	{
		this.cartesianCoordinates = otherVector.cartesianCoordinates;
	}

	internal CartesianVector(float x, float y, float z)
	{
		this.cartesianCoordinates = new Vector3(x, y, z);
	}

	public CartesianVector(Vector3 cartesianCoordinates)
	{
		this.cartesianCoordinates = cartesianCoordinates;
	}

	// TODO: This gtg too, but its used a lot in tests....
	public static implicit operator CartesianVector(Vector3 v) => new CartesianVector(v);
}
