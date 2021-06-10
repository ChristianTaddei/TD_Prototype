public class HighlightCommand : Command
{
    private RepresentationManager representationManager;

    public HighlightCommand(RepresentationManager representationManager)
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