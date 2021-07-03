
public class AttackThenMoveTurnProgressor : TurnProgressor
{
	public AttackThenMoveTurnProgressor(AttackPhaseProgressor attackPhaseProgressor, MovePhaseProgressor movePhaseProgressor)
		: base(attackPhaseProgressor, movePhaseProgressor) { }

	public override SimulationState nextState(SimulationState previousState)
	{
		SimulationStateBuilder simulationStateBuilder = new SimulationStateBuilder(previousState);

		AttackPhaseProgressor.progressStateBuilder(simulationStateBuilder);
		MovePhaseProgressor.progressStateBuilder(simulationStateBuilder);

		return simulationStateBuilder.Build();
	}
}
