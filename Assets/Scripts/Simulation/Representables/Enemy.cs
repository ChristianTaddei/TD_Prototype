using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// TODO: move to package representation, move logic away (in sim) and rename representableKey or smt
public class Enemy : Representable<EnemyRepresentation>
{
    public override string PrefabString => "enemy";

    private static float moveSpeed = .3f;
    private static float searchRange = 6.0f;

    public Enemy()
    {

    }

    public static Vertex FindIntermediaryDestination(BoardState boardState, EnemyState initialEnemyState)
    {
        Circle circle = new Circle(boardState, initialEnemyState.BoardPosition, searchRange);
        List<Vertex> possibleDests = circle.Cells
            .Where(v => initialEnemyState.PathToObjective.TraversedVertexs.Contains(v)).ToList();

        if (possibleDests.Count == 0)
        {
            Debug.Log("failed find intermed dest");
            return null;
        }

        Vertex dest = possibleDests.Aggregate((v1, v2) =>
            initialEnemyState.PathToObjective.TraversedVertexs.IndexOf(v1) > initialEnemyState.PathToObjective.TraversedVertexs.IndexOf(v2)
                ? v1 : v2
        );

        Vector3 a = SimulationManager.Instance.Board.InitalState.VertexStates[dest].Position;
        return dest;
    }

    public static SurfacePoint ComputeNextBP(Surface board, SimulationState simState, EnemyState initialEnemyState, Vertex intermediaryDestination)
    {
        // Vector3 movementVector = (
        //     simState.BoardState.VertexStates[intermediaryDestination].Position
        //     - initialEnemyState.BoardPosition.GetCartesians(simState.BoardState))
        //     .normalized * moveSpeed;

        // BoardPosition destination = SimulationManager.Instance.Board.SumBarAndCart(
        //     simState.BoardState,
        //     initialEnemyState.BoardPosition,
        //     movementVector);

        Vector3 targetPos2d = make2d(simState.BoardState.VertexStates[intermediaryDestination].Position);
        Vector3 start2d = make2d(initialEnemyState.BoardPosition.GetCartesians(simState.BoardState));
        Vector3 destination2d = start2d + ((targetPos2d - start2d).normalized * moveSpeed);
        SurfacePoint destination = board.MakeBPFrom2d(destination2d);

        bool willHit = simState.EnemyStates.Values.Any(otherEnemyState =>
            otherEnemyState.HasMoved
            && Vector3.Distance(destination.GetCartesians(simState.BoardState), otherEnemyState.BoardPosition.GetCartesians(simState.BoardState)) < 0.9
            && otherEnemyState != initialEnemyState);

        if (!willHit)
        {
            return destination;
        }

        return initialEnemyState.BoardPosition;
    }

    static Vector3 make2d(Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }
}
