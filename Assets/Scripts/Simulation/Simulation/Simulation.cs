public interface Simulation {
	SimulationState CurrentState { get; }
	void ProgressSimulation();
}