
using System;
using UnityEngine;

// provide common Equals and GetHashCode implementations for all vectors
// and default factory
public abstract class Vector : IEquatable<Vector>
{
	public abstract Vector3 FloatRepresentation { get; }

	public static Vector From(Vector3 vector3) // Default factory -> force override (check in tests)
	{ 
		return FloatVector.From(vector3);
	}

	public static Vector From(float x, float y, float z) // Default factory shorthand -> force override (same as def. fac)
	{ 
		return FloatVector.From(new Vector3(x, y, z));
	}

	public static Vector Copy(Vector other) // Default factory shorthand
	{ 
		return FloatVector.From(other.FloatRepresentation);
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
		return this.Equals(obj as Vector);
	}

	public static bool operator ==(Vector lhs, Vector rhs)
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

	public static bool operator !=(Vector lhs, Vector rhs)
	{
		return !(lhs == rhs);
	}

	public override int GetHashCode()
	{
		return -509336368 + FloatRepresentation.GetHashCode();
	}
}
