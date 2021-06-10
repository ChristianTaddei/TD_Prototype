public class RaycastMediator
{
	public RaycastMediator(InputManager inputManager, SimulationRepresentation simulationRepresentation)
	{
		InputManager = inputManager;
		SimulationRepresentation = simulationRepresentation;
	}

	public InputManager InputManager { get; }
	public SimulationRepresentation SimulationRepresentation { get; }
}
