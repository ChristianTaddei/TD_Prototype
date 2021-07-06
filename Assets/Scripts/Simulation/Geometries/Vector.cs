
using UnityEngine;

public abstract class Vector /* : Summable<Vector>, Crossable<Vector>, Dottable<Vector> */
{
	public abstract Vector3 FloatRepresentation { get; }

	// TODO: copy method of factory fails if this is not overridden, what Equals is is using?
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
