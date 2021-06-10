using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SimulationRepresentation : MonoBehaviour
{
    public static SimulationRepresentation Instance;

	public Simulation RepresentedSimulation {get; set;} // FIXME: how to make final when this is mono?

    // private HashSet<IRepresentable> representedObjects;

    public bool SimulationRepresentationRunning { get; set; }
    public bool SimulationRepresentationPaused { get; set; }

    public float SimulationRepresentationStepElapsedFraction { get => SimulationRepresentationStepCurrentTime / SimulationRepresentationStepTotalTime; }

    public float SimulationRepresentationStepTotalTime = 0.5f;
    private float SimulationRepresentationStepCurrentTime = 0.0f;

	void Start()
    {
        Instance = this;
        SimulationRepresentationRunning = false;
        SimulationRepresentationPaused = true;

        // representedObjects = new HashSet<IRepresentable>();
    }

    void Update()
    {

    }
}
