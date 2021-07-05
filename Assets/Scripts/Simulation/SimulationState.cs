using System.Collections.Generic;

public class SimulationState : State
{
	public ConcreteSurface Surface { get => surface; }
	public List<Unit> Units { get; set; }

	private ConcreteSurface surface;

	//private History<Surface> surfacesHistory;

	// generate state after (state, )
	// get state (from id or time)

	public SimulationState()
	{
        Units = new List<Unit>();
	}

	public SimulationState(ConcreteSurface surface) : this()
	{
		this.surface = surface;
	}


}
