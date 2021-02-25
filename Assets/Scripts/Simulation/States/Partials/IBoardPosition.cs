using UnityEngine;

public interface IBoardPosition : IPartialState
{
    BoardState BoardState { get; set; }
    SurfacePoint BoardPosition { get; set; }
    Vector3 Position { get; }
}
