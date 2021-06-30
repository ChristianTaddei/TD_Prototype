
public abstract class TurnProgressor<T> : Progressor<T> where T : State
{
	AttackPhaseProgressor<T> AttackPhaseProgressor { get; }
	MovePhaseProgressor<T> MovePhaseProgressor { get; }

    private Builder<T> stateBuilder { get; set; }

	public T nextState(T previousState)
	{
        // TODO: builder using polymorphism
        // Builder.Of<T>(previousState);

		// // -units use powers

		AttackPhaseProgressor.progressStateBuilder(stateBuilder);

		MovePhaseProgressor.progressStateBuilder(stateBuilder);

		// // -statuses degenerate

		return stateBuilder.Build();
	}
}
