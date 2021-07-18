using System;
using System.Collections.Generic;
using UnityEngine;

public class VectorStub : Vector
{
	public override Vector3 FloatRepresentation => coordinates;

	private Vector3 coordinates;

	public VectorStub(Vector3 coordinates)
	{
		this.coordinates = coordinates;
	}

	public VectorStub(float x, float y, float z) : this(new Vector3(x, y, z)) { }
}