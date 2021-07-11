
using System.Collections.Generic;

public class ConcreteSimulationState : SimulationState
{
	public Surface Surface { get; }

	public List<Unit> Units { get; }

	public ConcreteSimulationState(Surface surface, List<Unit> units)
	{
		Surface = surface;
		Units = units;
	}
}
