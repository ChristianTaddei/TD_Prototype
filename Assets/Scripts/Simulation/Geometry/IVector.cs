
using UnityEngine;

// TODO: could differentiate points and vectors, so that methods
// would know what they should act upon and what they return more 
// precisely. But is it worth? How much additional code for something
// that isnt actually distinct?
public interface IVector
{
    Vector3 Position {get;} // Position of the point specified by canonic representation
}
