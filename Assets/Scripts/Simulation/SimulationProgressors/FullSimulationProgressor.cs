using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Computes the next full state 
public class FullSimulationProgressor : SimulationProgressor
{
	public SimulationState nextState(SimulationState previousState)
	{
		SimulationStateBuilder simulationStateBuilder = new SimulationStateBuilder(previousState);

        // Calls all sub-simulation progressors:

        // -units use powers

        // -units fire
        // -enemy fire

        // -units move
        // -enemy move

        // -statuses degenerate

        return simulationStateBuilder.Build();
	}
}
