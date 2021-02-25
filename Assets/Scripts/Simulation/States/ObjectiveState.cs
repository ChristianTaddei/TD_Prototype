using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectiveState : IState, IBoardPosition
{
    public BoardState BoardState { get; set; }
    public SurfacePoint BoardPosition { get; set; }

    public Vector3 Position => BoardPosition.GetCartesians(BoardState);

    public ObjectiveState(BoardState boardState, SurfacePoint boardPosition)
    {
        BoardState = boardState;
        BoardPosition = boardPosition;
    }
    
    public ObjectiveState(BoardState newBoardState, ObjectiveState other)
    {
        this.BoardState = newBoardState;
        this.BoardPosition = other.BoardPosition;
    }

}
