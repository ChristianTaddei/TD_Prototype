using System.Collections.Generic;
using System.Linq;

public interface Path
{
	List<Vector> Points { get;}

	Vector Start { get; }
	Vector End { get; }

	bool Contains(Vector v);
}