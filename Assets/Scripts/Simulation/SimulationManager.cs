using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance;

    private RepresentationManager representationManager;

    // Currently not depending on state (updated on currentState modified and all states can access them)
    public Surface Board { get; private set; }
    public HashSet<AttackPlan> AttackPlans { get; private set; }

    public PathFinder pathFinder; // used by interface also
    public RangeFinder rangeFinder;

    public SimulationState CurrentState { get => ReadyStates[0]; set { ReadyStates[0] = value; CurrentStateModified(); } }
    public SimulationState LastState { get => ReadyStates[ReadyStates.Count - 1]; }
    public volatile List<SimulationState> ReadyStates = new List<SimulationState>();
    public volatile int statesCount = 0;

    public const float SimulationStepDuration = 0.5f;
    private Thread computeNextState = null;
    public Thread updateBoard = null;

    void Start()
    {
        Instance = this;

        representationManager = this.GetComponent<RepresentationManager>();

        Board = new Surface(30);
        pathFinder = new PathFinder(Board);
        rangeFinder = new RangeFinder(Board);

        AttackPlans = new HashSet<AttackPlan>();

        SimulationState initialState = new SimulationState(Board.InitalState);
        ReadyStates.Add(initialState);
        CurrentStateModified();

       
    }

    internal void AddToCurrent(IState state)
    {
        CurrentState.AddNew(state);
        CurrentStateModified();
    }

    void Update()
    {
        if (!RepresentationManager.Instance.RepresentationRunning) return;

        if (updateBoard != null && !updateBoard.IsAlive)
        {
            if ((computeNextState == null || !computeNextState.IsAlive))
            {
                statesCount = ReadyStates.Count;
                computeNextState = new Thread(() => RunStep(LastState));
                computeNextState.Start();
            }
        }
    }

    public void GoToNextState()
    {
        ReadyStates.Remove(CurrentState);
    }

    public void CurrentStateModified()
    {
        if (computeNextState != null) computeNextState.Abort();

        SimulationState newCurrentState = CurrentState;
        ReadyStates.Clear();
        ReadyStates.Add(newCurrentState);

        if (updateBoard != null) updateBoard.Abort();
        updateBoard = new Thread(() =>
            {
                CurrentState.BoardState.Graph.updateDistances();
                CurrentState.BoardState.Graph.UpdateThreats(CurrentState.TowerStates);
                CurrentState.BoardState.Graph.updateThreatwiseDistances();

                AttackPlans.Clear();
                AttackPlan.alreadyUsedVertices.Clear();
                foreach (Objective objective in SimulationManager.Instance.CurrentState.ObjectiveStates.Keys)
                {
                    AttackPlan attackPlan = new AttackPlan(CurrentState, objective);
                    AttackPlans.Add(attackPlan);
                }
            });
        updateBoard.Start();
    }


    private void RunStep(SimulationState initialSimState)
    {
        // System.DateTime startTime = System.DateTime.Now;

        SimulationState cleanInitialState = removeDestroyedEnemies(initialSimState);
        SimulationState taggedInitialState = shootTowers(cleanInitialState);
        SimulationState finalState = moveEnemies(taggedInitialState);

        foreach (AttackPlan attackPlan in AttackPlans)
        {
            foreach (Path pathToObjective in attackPlan.PathsToObjective)
            {
                Vertex spawningVertex = pathToObjective.GetStart();

                Vector3 vertexPosition = initialSimState.BoardState.VertexStates[spawningVertex].Position;
                bool positionTaken = finalState.EnemyStates.Values.Any(otherEnemyState =>
                    Vector3.Distance(vertexPosition, otherEnemyState.BoardPosition.GetCartesians(initialSimState.BoardState)) < 0.9);

                if (!positionTaken)
                {
                    EnemyState newEnemyState = new EnemyState(
                        initialSimState.BoardState,
                        new SurfacePoint(initialSimState.BoardState, spawningVertex),
                        initialSimState.BoardState.VertexStates[pathToObjective.TraversedVertexs[1]].Position - vertexPosition,
                        initialSimState.BoardState.VertexStates[pathToObjective.GetDestination()].Position,
                        false,
                        pathToObjective);
                    finalState.AddNew(newEnemyState);
                }

            }
        }

        // System.TimeSpan elapsed = System.DateTime.Now.Subtract(startTime);
        // Debug.Log("RunStep took " + elapsed.TotalMilliseconds + "ms with " + finalState.EnemyStates.Values.Count() + " enemies.");

        ReadyStates.Add(finalState);
    }

    public void ChangeCellsHeight(HashSet<Vertex> cellsInRange, float heighChange)
    {
        foreach (Vertex vertex in cellsInRange)
        {
            VertexState newVertexState = new VertexState(CurrentState.BoardState.VertexStates[vertex]);
            newVertexState.Position = new Vector3(
                newVertexState.Position.x,
                newVertexState.Position.y + heighChange,
                newVertexState.Position.z);

            CurrentState.BoardState.VertexStates[vertex] = newVertexState;
        }
        CurrentStateModified();
    }

    private SimulationState removeDestroyedEnemies(SimulationState initialSimState)
    {
        SimulationState cleanInitialState = new SimulationState(initialSimState);

        foreach (KeyValuePair<Enemy, EnemyState> kvp in initialSimState.EnemyStates)
        {
            Enemy enemy = kvp.Key;
            EnemyState initialEnemyState = kvp.Value;

            if (initialEnemyState.Destroyed)
            {
                cleanInitialState.EnemyStates.Remove(enemy);
            }
        }

        return cleanInitialState;
    }

    private SimulationState shootTowers(SimulationState initialSimState)
    {

        SimulationState nextSimulationState = new SimulationState(initialSimState);

        foreach (KeyValuePair<Tower, TowerState> kvp in initialSimState.TowerStates)
        {
            Tower tower = kvp.Key;
            TowerState initialTowerState = kvp.Value;
            TowerState finalTowerState = new TowerState(initialTowerState);

            IEnumerable<KeyValuePair<Enemy, EnemyState>> enemyKvps = nextSimulationState.EnemyStates;

            if (enemyKvps.Count() > 0)
            {
                KeyValuePair<Enemy, EnemyState> closestEnemyKvp = enemyKvps
                           .Aggregate((kvp1, kvp2) =>
                               Vector3.Distance(kvp1.Value.BoardPosition.GetCartesians(initialSimState.BoardState), initialTowerState.BoardPosition.GetCartesians(initialSimState.BoardState))
                               < Vector3.Distance(kvp2.Value.BoardPosition.GetCartesians(initialSimState.BoardState), initialTowerState.BoardPosition.GetCartesians(initialSimState.BoardState))
                               ? kvp1 : kvp2);

                if (Vector3.Distance(closestEnemyKvp.Value.BoardPosition.GetCartesians(initialSimState.BoardState), initialTowerState.BoardPosition.GetCartesians(initialSimState.BoardState)) < Tower.Range)
                {
                    EnemyState newEnemyState = new EnemyState(nextSimulationState.BoardState, closestEnemyKvp.Value);
                    newEnemyState.Destroyed = true;
                    nextSimulationState.EnemyStates[closestEnemyKvp.Key] = newEnemyState;

                    finalTowerState.Target = (closestEnemyKvp.Value).BoardPosition.GetCartesians(initialSimState.BoardState);
                    nextSimulationState.TowerStates[tower] = finalTowerState;
                }
            }
        }

        return nextSimulationState;
    }

    private SimulationState moveEnemies(SimulationState initialSimState)
    {
        SimulationState finalState = new SimulationState(initialSimState);

        foreach (KeyValuePair<Enemy, EnemyState> kvp in initialSimState.EnemyStates)
        {
            kvp.Value.HasMoved = false;
        }

        foreach (KeyValuePair<Enemy, EnemyState> kvp in initialSimState.EnemyStates)
        {
            Enemy enemy = kvp.Key;
            EnemyState initialEnemyState = kvp.Value;
            EnemyState newEnemyState = new EnemyState(finalState.BoardState, initialEnemyState);

            Vertex intermediaryDestination = Enemy.FindIntermediaryDestination(initialSimState.BoardState, initialEnemyState);
            if (intermediaryDestination != null)
            {
                newEnemyState.BoardPosition = Enemy.ComputeNextBP(Board, finalState, initialEnemyState, intermediaryDestination);
                newEnemyState.HasMoved = true;
            }

            finalState.EnemyStates[enemy] = newEnemyState;
        }

        return finalState;
    }
}
