using System.Collections.Generic;

public class SimulationState : State
{
	public Surface Surface { get => surface; }
	public List<Unit> Units { get; set; }

	private Surface surface;

	//private History<Surface> surfacesHistory;

	// generate state after (state, )
	// get state (from id or time)

	public SimulationState()
	{
        Units = new List<Unit>();
	}

	public SimulationState(Surface surface) : this()
	{
		this.surface = surface;
	}


}
