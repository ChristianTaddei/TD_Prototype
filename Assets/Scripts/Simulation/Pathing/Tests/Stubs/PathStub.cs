using System.Collections.Generic;

public class PathStub : Path
{
	public List<Vector> Points {get; private set;}

	public Vector Start => throw new System.NotImplementedException();

	public Vector End => throw new System.NotImplementedException();

	public bool Contains(Vector v)
	{
		return Points.Contains(v);	
	}

	internal PathStub(List<Vector> points){
		Points = points;
	}
}