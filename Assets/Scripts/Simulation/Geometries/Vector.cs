
using UnityEngine;

// TODO: could differentiate points and vectors, so that methods
// would know what they should act upon and what they return more 
// precisely. But is it worth? How much additional code for something
// that isnt actually distinct?
public abstract class Vector /* : Summable<Vector>, Crossable<Vector>, Dottable<Vector> */
{
	public abstract Vector3 FloatRepresentation { get; }

	// TODO: remove equals and hashcode, but some test still use it
	public sealed override bool Equals(object otherObject)
	{
		return otherObject is Vector otherPoint
		 && Vector3.Distance(this.FloatRepresentation, otherPoint.FloatRepresentation) < 0.0001f;
	}

	public override int GetHashCode()
	{
		return -509336368 + FloatRepresentation.GetHashCode();
	}
}
