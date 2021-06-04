using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InterfaceManager InterfaceManager;
    public SimulationManager SimulationManager;

    BoardRepresentation br;

    void Start()
    {
        // TODO: add components here to have correct order

        Surface surface = new Surface(10.0f);
        SimulationManager = new SimulationManager(surface);

        InterfaceManager = new InterfaceManager(
            new ModifyTerrainCommand(surface)
        );

        // TODO: make into repres & manager
        Board board = new Board(surface);
        br = BoardRepresentation.MakeFrom(board);

        // Register observers/observables
    }

    void Update()
    {
        InterfaceManager.Update();
        SimulationManager.Update();

        br.Sync();
    }
}
