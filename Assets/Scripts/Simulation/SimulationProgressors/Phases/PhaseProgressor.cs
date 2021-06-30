
public interface PhaseProgressor<T> where T : State
{
    Builder<T> progressStateBuilder(Builder<T> stateBuilder);
}
