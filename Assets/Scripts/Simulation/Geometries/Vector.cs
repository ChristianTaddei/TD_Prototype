
using UnityEngine;

// TODO: could differentiate points and vectors, so that methods
// would know what they should act upon and what they return more 
// precisely. But is it worth? How much additional code for something
// that isnt actually distinct?
public abstract class Vector /* : Summable<Vector>, Crossable<Vector>, Dottable<Vector> */
{
	private static readonly float FLOAT_REPRESENTATION_EQUALITY_TOLLERANCE = 0.001f;

	public abstract Vector3 FloatRepresentation { get; }

	public sealed override bool Equals(object otherObject)
	{
		return otherObject is Vector otherPoint
			 && Vector3.Distance(this.FloatRepresentation, otherPoint.FloatRepresentation) < FLOAT_REPRESENTATION_EQUALITY_TOLLERANCE;
	}

	public override int GetHashCode()
	{
		return -425505606 + FloatRepresentation.GetHashCode();
	}
}
