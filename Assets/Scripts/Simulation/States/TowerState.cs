using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerState : IState, IBoardPosition, IDirection, ITarget, IDestructible
{
    public Vector3 Direction { get; set; }
    public Vector3 Target { get; set; }
    public bool Destroyed { get; set; }
    
    public BoardState BoardState { get; set; }
    public SurfacePoint BoardPosition { get; set; }

    // TODO: euclideanPosition from bstate anb bpos
    public Vector3 Position => BoardPosition.GetCartesians(BoardState);

    public TowerState(BoardState boardState, SurfacePoint boardPosition, Vector3 direction, Vector3 target, bool destroyed)
    {
        BoardState = boardState;
        BoardPosition = boardPosition;
        Direction = direction;
        Target = target;
        Destroyed = destroyed;
    }

    public TowerState(TowerState other)
    {
        BoardState = other.BoardState;
        BoardPosition = other.BoardPosition;
        Direction = other.Direction;
        Target = other.Target;
        Destroyed = other.Destroyed;
    }
}
