
using UnityEngine;

// provide common Equals and GetHashCode implementations for all vectors
public abstract class AbstractVector : Vector
{
	public abstract Vector3 FloatRepresentation { get; }

	// Equals and HashCode
	public override bool Equals(object obj)
	{
		return obj is Vector vector &&
			   FloatRepresentation.Equals(vector.FloatRepresentation);
	}

	public override int GetHashCode()
	{
		return -509336368 + FloatRepresentation.GetHashCode();
	}
}
