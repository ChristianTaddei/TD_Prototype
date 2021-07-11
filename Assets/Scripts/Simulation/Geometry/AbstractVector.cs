
using UnityEngine;

// provide common Equals and GetHashCode implementations for all vectors
public abstract class AbstractVector : Vector
{
	public abstract Vector3 FloatRepresentation { get; }

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
		return this.Equals(obj as AbstractVector);
	}

	public static bool operator ==(AbstractVector lhs, AbstractVector rhs)
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

	public static bool operator !=(AbstractVector lhs, AbstractVector rhs)
	{
		return !(lhs == rhs);
	}

	public override int GetHashCode()
	{
		return -509336368 + FloatRepresentation.GetHashCode();
	}
}
