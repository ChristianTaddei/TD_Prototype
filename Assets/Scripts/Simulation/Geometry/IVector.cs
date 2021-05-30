
using UnityEngine;

// TODO: could differentiate points and vectors, so that methods
// would know what they should act upon and what they return more 
// precisely. But is it worth? How much additional code for something
// that isnt actually distinct?
public abstract class IVector
{
    public abstract Vector3 Position {get;} // Position of the point specified by canonic representation

    public override bool Equals(object obj)
    {
        return obj is IVector vector
             && UnityEngine.Mathf.Abs(this.Position.x - vector.Position.x) < 0.0001f
             && UnityEngine.Mathf.Abs(this.Position.y - vector.Position.y) < 0.0001f
             && UnityEngine.Mathf.Abs(this.Position.z - vector.Position.z) < 0.0001f;
    }

    // TODO: what if some vectors are equals but different hashcodes?
    public override int GetHashCode()
    {
        return -425505606 + Position.GetHashCode();
    }
}
