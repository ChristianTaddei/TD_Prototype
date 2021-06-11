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

		RepresentationFactory representationFactory = new RepresentationFactory(); // TODO: unstaticize

		SimulationRepresentation simulationRepresentation = this.gameObject.AddComponent<SimulationRepresentation>(); // TODO: new gameobj
		simulationRepresentation.RepresentedSimulation = simulation;

		RaycastMediator raycastMediator = new RaycastMediator(inputManager, simulationRepresentation);
		HighlightMediator highlightMediator = new HighlightMediator(representationFactory);

		// Desktop Level Interface
		_interface = new Interface(
			raycastMediator,
	        highlightMediator,
			new ModifyTerrainCommand(simulation.Surface) // TODO: with just sim?
		);

		inputManager.Bind(null, _interface.OnSelect);

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
