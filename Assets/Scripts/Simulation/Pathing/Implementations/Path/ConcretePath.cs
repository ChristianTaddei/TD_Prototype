using System.Collections.Generic;
using System.Linq;

public class ConcretePath : Path
{
	internal ConcretePath(List<Vector> points)
	{
		Vertices = points;
	}

	public List<Vector> Vertices { get; }

	public Vector Start { get; }
	public Vector End { get; }

	public bool Contains(Vector v)
	{
		throw new System.NotImplementedException();
	}
}