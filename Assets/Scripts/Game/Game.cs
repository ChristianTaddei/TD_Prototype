using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	Interface _interface;
	BoardRepresentation br;

	void Start()
	{
		// Desktop InputManager
		InputManager inputManager = this.gameObject.AddComponent<InputManager>();

		Simulation simulation = new Simulation(new Surface(10.0f));

		SimulationRepresentation simulationRepresentation = this.gameObject.AddComponent<SimulationRepresentation>(); // TODO: new gameobj
		simulationRepresentation.RepresentedSimulation = simulation;

		RaycastMediator raycastMediator = new RaycastMediator(inputManager, simulationRepresentation);

		// Desktop Level Interface
		_interface = new Interface(
			raycastMediator,
		    new ModifyTerrainCommand(simulation.Surface) // TODO: with just sim
		);


		Board board = new Board(simulation.Surface); // TODO: make into repres & surf
		br = BoardRepresentation.MakeFrom(board);

		// Register observers/observables
	}

	void Update()
	{
		_interface.Update();

		br.Sync();
	}
}
