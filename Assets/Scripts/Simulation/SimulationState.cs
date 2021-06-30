public class SimulationState : State
{
    public Surface Surface { get => surface; }

    private Surface surface;

    //private History<Surface> surfacesHistory;

    // generate state after (state, )
    // get state (from id or time)

    public SimulationState(Surface surface)
    {
        this.surface = surface;
    }
}
