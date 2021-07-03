
using UnityEngine;

// TODO: could differentiate points and vectors, so that methods
// would know what they should act upon and what they return more 
// precisely. But is it worth? How much additional code for something
// that isnt actually distinct?
public abstract class Vector /* : Summable<Vector>, Crossable<Vector>, Dottable<Vector> */
{
	public abstract Vector3 FloatRepresentation { get; }
}
