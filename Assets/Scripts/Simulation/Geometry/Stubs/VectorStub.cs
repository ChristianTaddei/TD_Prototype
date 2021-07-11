using System;
using System.Collections.Generic;
using UnityEngine;

public class VectorStub : Vector
{
	public Vector3 FloatRepresentation => coordinates;

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

	public bool Equals(Vector other)
	{
		if (other != null)
		{
			return this.FloatRepresentation.Equals(other.FloatRepresentation);
		}

		return false;
	}

	public override bool Equals(object obj)
	{
		return this.Equals(obj as VectorStub);
	}

	public static bool operator ==(VectorStub lhs, VectorStub rhs)
	{
		bool isLhsNull = object.ReferenceEquals(lhs, null);
		bool isRhsNull = object.ReferenceEquals(rhs, null);

		if (isLhsNull && isRhsNull)
			return true;
		else if (isLhsNull)
			return false;
		else
			return lhs.Equals(rhs);
	}

	public static bool operator !=(VectorStub lhs, VectorStub rhs)
	{
		return !(lhs == rhs);
	}

	public override int GetHashCode()
	{
		return -509336368 + FloatRepresentation.GetHashCode();
	}
}