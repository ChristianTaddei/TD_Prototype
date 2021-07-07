using System.Collections.Generic;
using System.Linq;

public interface PathFactory
{
	Path PathFromPoints(Vector startPoint, List<Vector> crossingPoints, Vector finalPoint);
	
	Path PathFromPoints(List<Vector> points);
}