using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhaseProgressor : SimulationProgressor
{
	public SimulationState nextState(SimulationState previousState)
	{
		SimulationStateBuilder builder = new SimulationStateBuilder(previousState);

        // align allied shots
        // fire allied weapons

        // align enemy shots
        // fire enemy weapons

        // ballistic calculations

        // apply ballistic results

        return builder.Build();
	}
}
