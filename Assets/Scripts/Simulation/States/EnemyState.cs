using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyState : IState, IBoardPosition, IDirection, ITarget, IDestructible
{
    public BoardState BoardState { get; set; }
    public SurfacePoint BoardPosition { get; set; }
    public Vector3 Direction { get; set; }
    public Vector3 Target { get; set; }
    public bool Destroyed { get; set; }

    public Vector3 Position => BoardPosition.GetCartesians(BoardState);

    public Path PathToObjective; // TODO: maybe managed by sim or atk plan
    public Vertex IntermadiaryDestination;
    public bool HasMoved = false;

    public EnemyState()
    {

    }

    public EnemyState(BoardState boardState, SurfacePoint boardPosition, Vector3 direction, Vector3 target, bool destroyed, Path pathToObjective)
    {
        BoardState = boardState;
        BoardPosition = boardPosition;
        Direction = direction;
        Target = target;
        Destroyed = destroyed;

        PathToObjective = pathToObjective;
        // IntermadiaryDestination = intermadiaryDestination;
        // HasMoved = hasMoved;
    }

    public EnemyState(BoardState newBoardState, EnemyState other)
    {
        BoardState = newBoardState;
        BoardPosition = other.BoardPosition;
        Direction = other.Direction;
        Destroyed = other.Destroyed;
        Target = other.Target;

        PathToObjective = other.PathToObjective;
    }


}
