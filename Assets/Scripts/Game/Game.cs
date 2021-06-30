using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	InterfaceManager _interface;
	BoardRepresentation br;

	void Start()
	{
		// Desktop InputManager
		InputManager inputManager = this.gameObject.AddComponent<InputManager>();

		SimulationState simulation = new SimulationState(new Surface(10.0f));

		RepresentationFactory representationFactory = new RepresentationFactory(); 

		SimulationRepresentation simulationRepresentation = this.gameObject.AddComponent<SimulationRepresentation>();
		simulationRepresentation.RepresentedSimulation = simulation;

		RaycastMediator raycastMediator = new RaycastMediator(inputManager);

		HighlightMediator highlightMediator = new HighlightMediator(representationFactory);

		// Desktop Level Interface
		_interface = new InterfaceManager(
			raycastMediator,
	        highlightMediator,
			new ModifyTerrainCommand(simulation.Surface)
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
