using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Interface Interface;
    public Simulation Simulation;

    public SimulationRepresentation simulationRepresentation;

    public InputManager inputManager;

    BoardRepresentation br;

    void Start()
    {
        inputManager = this.gameObject.AddComponent<InputManager>();

        Surface surface = new Surface(10.0f);
        Simulation = new Simulation(surface);

        simulationRepresentation = this.gameObject.AddComponent<SimulationRepresentation>();

        Interface = new Interface(
            new ModifyTerrainCommand(surface)
        );

        // TODO: make into repres & manager
        Board board = new Board(surface);
        br = BoardRepresentation.MakeFrom(board);

        // Register observers/observables
    }

    void Update()
    {
        Interface.Update();
        Simulation.Update();

        br.Sync();
    }
}