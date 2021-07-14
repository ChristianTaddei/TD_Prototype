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

	public static IEnumerable<(Vector, Vector)> KnownEqualities
	{
		get => new List<(Vector, Vector)>()
		{
			(new VectorStub(1,2,3), new VectorStub(1,2,3))
		};
	}

	public static IEnumerable<(Vector, Vector)> KnownDisequalities
	{
		get => new List<(Vector, Vector)>()
		{
			(new VectorStub(1,2,3), new VectorStub(2,3,4))
		};
	}
}