using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationStateBuilder : Builder<SimulationState>
{
	// All the content of a real sim state, but without being immutable

	public SimulationStateBuilder(SimulationState previousState)
	{

	}

	public SimulationState Build()
	{
		throw new NotImplementedException();
	}

}
