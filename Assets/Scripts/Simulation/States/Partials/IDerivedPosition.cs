using UnityEngine;

public interface IDerivedPosition : IPartialState 
{
    Vector3 GetPosition(BoardState boardState);
}
