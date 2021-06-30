
public interface Progressor<T> where T : State
{
    T nextState(T previousState);
}
