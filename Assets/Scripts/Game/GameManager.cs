using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputManager inputManager;
    private InterfaceManager interfaceManager;
    private SimulationManager simulationManager;
    private RepresentationManager representationManager;

    private GameState gameState;

    public GameState GameState { get => gameState; set => changeGameState(value); }

    private void changeGameState(GameState nextState)
    {
        leaveState(GameState);
        gameState = nextState;
        enterState(nextState);
    }

    private void enterState(GameState nextState)
    {
        switch (nextState)
        {
            case GameState.EDIT:
                // simulationManager.SimulationRunning = false;
                //interfaceManager.ShowEditInterface();
                break;
            case GameState.PLAY:
                // simulationManager.SimulationRunning = true;
                //interfaceManager.ShowPlayInterface();
                break;
            case GameState.PAUSE:
                // simulationManager.SimulationRunning = false;
                //interfaceManager.ShowPlayInterface();
                break;
            default:
                break;
        }
    }

    private void leaveState(GameState prevState)
    {
        switch (prevState)
        {
            case GameState.EDIT:
                break;
            default:
                break;
        }
    }

    private void InitializeGame()
    {

    }

    void Start()
    {
        inputManager = this.GetComponent<InputManager>();
        interfaceManager = this.GetComponent<InterfaceManager>();
        simulationManager = this.GetComponent<SimulationManager>();
        representationManager = this.GetComponent<RepresentationManager>();

        GameState = GameState.EDIT;
    }


    void Update()
    {

    }
}
