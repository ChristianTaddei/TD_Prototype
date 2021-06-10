public class HighlightCommand : Command
{
    private SimulationRepresentation representationManager;

    public HighlightCommand(SimulationRepresentation representationManager)
    {
        this.representationManager = representationManager;
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}