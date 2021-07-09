using System.Collections.Generic;
using System.Linq;

public interface Path
{
	List<Vector> Vertices { get;}

	Vector Start { get; }
	Vector End { get; }
}