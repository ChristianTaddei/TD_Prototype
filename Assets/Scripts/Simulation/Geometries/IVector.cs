
using UnityEngine;

// TODO: could differentiate points and vectors, so that methods
// would know what they should act upon and what they return more 
// precisely. But is it worth? How much additional code for something
// that isnt actually distinct?
public abstract class IVector
{
    public static readonly float EPSILON = 0.001f;

    public abstract Vector3 Position {get;} // Position of the point specified by canonic representation

    public override bool Equals(object obj)
    {
        return obj is IVector vector
             && UnityEngine.Mathf.Abs(this.Position.x - vector.Position.x) < EPSILON
             && UnityEngine.Mathf.Abs(this.Position.y - vector.Position.y) < EPSILON
             && UnityEngine.Mathf.Abs(this.Position.z - vector.Position.z) < EPSILON;
    }

    public override int GetHashCode()
    {
        return -425505606 + Position.GetHashCode();
    }
}
