using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	InterfaceManager _interface;
	BoardRepresentation br;

	void Start()
	{
		// Geometry cartesianGeometry = new VectorialGeometry();
		// Geometry barycentricGeometry = new BarycentricGeometry(cartesianGeometry);

		// SurfaceFactory surfaceFactory = new SurfaceFactory();

		// Surface gameSurface = surfaceFactory.MakeSquareSurface(10.0f, 10);

		// Pathfinder statelessPathFinder = new StatelessPathfinder(cartesianGeometry);

		// Simulation gameSimulation = new Simulation(gameSurface);

		// UnitFactory unitFactory = new UnitFactory();

		// RepresentationFactory representationFactory = new RepresentationFactory(); 

		// SimulationRepresentation simulationRepresentation = representationFactory.GetRepresentation(gameSimulation);

		// gameSimulation.AddEnemy(unitFactory.GetEnemy(), new Vector3(1,0,1));
	}

	void Update()
	{
		_interface.Update();

		br.Sync();
	}
}
