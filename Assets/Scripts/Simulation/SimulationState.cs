using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO: rename and abstract (with BoardState?)
public class SimulationState
{
    // TODO: tryget for all dictionaries
    public BoardState BoardState { get; set; }
    public Dictionary<Enemy, EnemyState> EnemyStates { get; set; }
    public Dictionary<Tower, TowerState> TowerStates { get; set; }
    public Dictionary<Objective, ObjectiveState> ObjectiveStates { get; set; }

    public SimulationState(BoardState boardState)
    {
        this.BoardState = boardState;

        this.EnemyStates = new Dictionary<Enemy, EnemyState>();
        this.TowerStates = new Dictionary<Tower, TowerState>();
        this.ObjectiveStates = new Dictionary<Objective, ObjectiveState>();
    }

    // FIXME: boardstate shallow copy?
    public SimulationState(SimulationState other) : this(other.BoardState) 
    {
        foreach (KeyValuePair<Enemy, EnemyState> entry in other.EnemyStates)
        {
            this.EnemyStates.Add(entry.Key, new EnemyState(other.BoardState, entry.Value));
        }

        foreach (KeyValuePair<Tower, TowerState> entry in other.TowerStates)
        {
            this.TowerStates.Add(entry.Key, new TowerState(entry.Value));
        }

        foreach (KeyValuePair<Objective, ObjectiveState> entry in other.ObjectiveStates)
        {
            // TODO: update this clone with new simstate too
            this.ObjectiveStates.Add(entry.Key, new ObjectiveState(other.BoardState, entry.Value));
        }
    }

    public void AddNew(IState state)
    {
        if (state is EnemyState)
            this.EnemyStates.Add(new Enemy(), state as EnemyState);

        if (state is TowerState)
            this.TowerStates.Add(new Tower(), state as TowerState);

        if (state is ObjectiveState)
            this.ObjectiveStates.Add(new Objective(), state as ObjectiveState);
    }

    internal void RemoveEnemy(Enemy enemy)
    {
        EnemyStates.Remove(enemy);
    }
}
