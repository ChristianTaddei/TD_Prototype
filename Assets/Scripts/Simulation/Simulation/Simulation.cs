using System.Threading.Tasks;

public interface Simulation {
	SimulationState CurrentState { get; }
	Task<SimulationState> ProgressToNextTurn();
}