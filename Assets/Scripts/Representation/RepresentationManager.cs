using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public enum HighlightSize
{
    Small,
    Large,
    VerySmall,
}

public class RepresentationManager : MonoBehaviour
{
    public static RepresentationManager Instance;

    private Dictionary<Type, GameObject> loadedPrefabs = new Dictionary<Type, GameObject>();

    private HashSet<IRepresentable> representedObjects;
    private BoardRepresentation boardRepresentation = null;

    public bool RepresentationRunning { get; set; }
    public bool RepresentationPaused { get; set; }

    public float RepresentationStepElapsedFraction { get => representationStepCurrentTime / representationStepTotalTime; }

    [HideInInspector] public float RepresentationSpeed = 1.0f;
    private float representationStepTotalTime;
    private float representationStepCurrentTime = 0.0f;

    void Start()
    {
        Instance = this;
        RepresentationRunning = false;
        RepresentationPaused = true;

        representedObjects = new HashSet<IRepresentable>();
    }

    void Update()
    {
        SimulationState currState = SimulationManager.Instance.CurrentState;

        if(boardRepresentation == null){
            boardRepresentation = BoardRepresentation.MakeFrom(SimulationManager.Instance.Board);
        }

        if (!RepresentationPaused)
        {
            representationStepTotalTime = SimulationManager.SimulationStepDuration / RepresentationSpeed;
            representationStepCurrentTime += Time.deltaTime;
        }

        if (RepresentationRunning)
        {
            if (representationStepCurrentTime >= representationStepTotalTime)
            {
                representationStepCurrentTime -= representationStepTotalTime;

                if (SimulationManager.Instance.ReadyStates.Count > 1)
                {
                    SimulationState prevState = SimulationManager.Instance.CurrentState;
                    SimulationManager.Instance.GoToNextState();
                    SimulationState nextState = SimulationManager.Instance.CurrentState;
                    StartTransition(prevState, nextState);
                }
            }
        }
        else
        {
            StartTransition(SimulationManager.Instance.CurrentState, SimulationManager.Instance.CurrentState);
        }
    }

    public void StartTransition(SimulationState prevState, SimulationState nextState)
    {
        boardRepresentation.SetPrevRepresentedState(prevState.BoardState);

        foreach (IRepresentable representable in representedObjects)
        {
            if ((prevState.EnemyStates.ContainsKey(representable as Enemy)
            || prevState.TowerStates.ContainsKey(representable as Tower)
            || prevState.ObjectiveStates.ContainsKey(representable as Objective))
                && representable.Representation != null)
            {
                representable.Representation.Destroy();
                representable.Representation = null;
            }

            representedObjects.Remove(representable);
        }

        List<IRepresentable> representableKeys = new List<IRepresentable>();
        representableKeys.AddRange(prevState.EnemyStates.Keys);
        representableKeys.AddRange(prevState.TowerStates.Keys);
        representableKeys.AddRange(prevState.ObjectiveStates.Keys);

        List<IRepresentable> newRepresentables = new List<IRepresentable>();
        foreach (IRepresentable representable in representableKeys)
        {
            if (representable.Representation == null)
            {
                newRepresentables.Add(representable);
            }
        }

        foreach (IRepresentable representable in newRepresentables)
        {
            RepresentationFactory.MakeRepresentationFor(representable);
        }

        SetAllLastRepresentedStates(prevState);
        SetAllNextRepresentedStates(nextState);
    }

    private void ClearAllRepresentations()
    {
        foreach (IRepresentable representable in representedObjects)
        {
            if (representable.Representation != null)
            {
                representable.Representation.Destroy();
                representable.Representation = null;
            }
        }
        representedObjects.Clear();
    }

    private void SetAllLastRepresentedStates(SimulationState state)
    {
        foreach (KeyValuePair<Enemy, EnemyState> entry in state.EnemyStates)
        {
            Enemy representable = entry.Key;

            if (representable.Representation == null) continue;

            representable.Representation.SetPrevRepresentedState(entry.Value);
        }

        foreach (KeyValuePair<Tower, TowerState> entry in state.TowerStates)
        {
            Tower representable = entry.Key;

            if (representable.Representation == null) continue;

            representable.Representation.SetPrevRepresentedState(entry.Value);
        }

        foreach (KeyValuePair<Objective, ObjectiveState> entry in state.ObjectiveStates)
        {
            Objective representable = entry.Key;

            if (representable.Representation == null) continue;

            representable.Representation.SetPrevRepresentedState(entry.Value);
        }
    }

    private void SetAllNextRepresentedStates(SimulationState state)
    {
        foreach (KeyValuePair<Enemy, EnemyState> entry in state.EnemyStates)
        {
            Enemy representable = entry.Key;

            if (representable.Representation == null) continue;

            representable.Representation.SetNextRepresentedState(entry.Value);
        }

        foreach (KeyValuePair<Tower, TowerState> entry in state.TowerStates)
        {
            Tower representable = entry.Key;

            if (representable.Representation == null) continue;

            representable.Representation.SetNextRepresentedState(entry.Value);
        }

        foreach (KeyValuePair<Objective, ObjectiveState> entry in state.ObjectiveStates)
        {
            Objective representable = entry.Key;

            if (representable.Representation == null) continue;

            representable.Representation.SetNextRepresentedState(entry.Value);
        }
    }
}
