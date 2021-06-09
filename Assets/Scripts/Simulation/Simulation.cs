public class Simulation
{
    public Surface Surface { get => surface; }

    private Surface surface;

    //private History<Surface> surfacesHistory;

    // generate state after (state, )
    // get state (from id or time)

    public Simulation(Surface surface)
    {
        this.surface = surface;
    }

    public void Update()
    {

    }
}
