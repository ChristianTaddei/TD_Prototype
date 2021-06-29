using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationStateBuilder : Builder<SimulationState>
{
	public SimulationStateBuilder(SimulationState previousState)
	{

	}

	// All the content of a real state, but without being immutable

	public SimulationState Build()
	{
		throw new NotImplementedException();
	}

}
