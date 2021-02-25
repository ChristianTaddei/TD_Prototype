public interface IRepresentation
{   
    IRepresentable RepresentedObject { get; set;}  

    void Destroy();
}
