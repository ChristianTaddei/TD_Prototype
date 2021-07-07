using System.Collections.Generic;
using System.Linq;

public class Path
{
	public Path(List<Vector> points)
	{
		Points = points;
	}

	public List<Vector> Points { get; private set; }

	public Vector Start { get; }
	public Vector End { get; }
}