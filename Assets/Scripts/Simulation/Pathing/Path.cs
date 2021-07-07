using System.Collections.Generic;
using System.Linq;

public interface Path
{
	public List<Vector> Points { get;}

	public Vector Start { get; }
	public Vector End { get; }

	bool Contains(Vector v);
}