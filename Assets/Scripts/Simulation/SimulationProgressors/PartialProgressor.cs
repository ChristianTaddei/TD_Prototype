
public interface PartialProgressor<T> where T : State
{
    Builder<T> progressStateBuilder(Builder<T> stateBuilder);
}
