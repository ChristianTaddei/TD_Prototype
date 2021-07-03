
public abstract class TurnProgressor : Progressor<SimulationState>
{
	protected AttackPhaseProgressor AttackPhaseProgressor { get; }
	protected MovePhaseProgressor MovePhaseProgressor { get; }

	protected TurnProgressor(AttackPhaseProgressor attackPhaseProgressor, MovePhaseProgressor movePhaseProgressor)
	{
		AttackPhaseProgressor = attackPhaseProgressor;
		MovePhaseProgressor = movePhaseProgressor;
	}

	public abstract SimulationState nextState(SimulationState previousState);
}
