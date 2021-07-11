using System.Collections.Generic;
using System.Threading.Tasks;

public class SelfProgressedSimulation : Simulation
{
	public SimulationState CurrentState => currentState;

	private SimulationState currentState;

	public SelfProgressedSimulation(SimulationState initialState)
	{
		this.currentState = initialState;
	}

	public Task<SimulationState> ProgressToNextTurn()
	{
		return Task.Factory.StartNew(executeTurn);
	}

	private SimulationState executeTurn() {
		Surface nextTurnSurface;
		List<Unit> nextTurnUnits;

		// return SimulationState.Factory.From(nextTurnSurface, nextTurnUnits);
		return currentState;
	}
}