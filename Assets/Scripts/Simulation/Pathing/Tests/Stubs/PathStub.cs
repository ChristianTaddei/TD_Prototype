using System.Collections.Generic;

public class PathStub : Path
{
	public List<Vector> Vertices {get; private set;}

	public Vector Start => throw new System.NotImplementedException();

	public Vector End => throw new System.NotImplementedException();

	internal PathStub(List<Vector> vertices){
		Vertices = vertices;
	}
}