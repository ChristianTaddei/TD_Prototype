using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	[HideInInspector] public Interface Interface;
	[HideInInspector] public Simulation Simulation;

	[HideInInspector] public RepresentationManager representationManager;

	[HideInInspector] public InputManager inputManager;

	BoardRepresentation br;

	void Start()
	{
		inputManager = this.gameObject.AddComponent<InputManager>();

		Surface surface = new Surface(10.0f);
		Simulation = new Simulation(surface);

		representationManager = this.gameObject.AddComponent<RepresentationManager>();

		Interface = new Interface(
		    new ModifyTerrainCommand(surface),
		    new HighlightCommand(representationManager)
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
