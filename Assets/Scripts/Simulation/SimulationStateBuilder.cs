using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using UnityEngine;

public class SimulationStateBuilder : Builder<SimulationState>
{
	// All the content of a real sim state, but without being immutable
	private List<Unit> units;

	public SimulationStateBuilder(SimulationState previousState)
	{

	}

	public SimulationState Build()
	{
		throw new NotImplementedException();
	}

	public void HitUnit(Unit unitInRange)
	{
		throw new NotImplementedException();
	}
}
