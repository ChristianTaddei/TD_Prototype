using System;
using System.Collections.Generic;

public interface SimulationState
{
	Surface Surface { get; }

	List<Unit> Units { get; }
}