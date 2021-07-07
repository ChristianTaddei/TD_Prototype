using System.Collections.Generic;

public class ConcretePathFactory : PathFactory
{
	public Path PathFromPoints(Vector startPoint, List<Vector> crossingPoints, Vector finalPoint)
	{
		List<Vector> allPoints = new List<Vector>();
		allPoints.Add(startPoint);
		allPoints.AddRange(crossingPoints);
		allPoints.Add(finalPoint);

		return new ConcretePath(allPoints);
	}

	public Path PathFromPoints(List<Vector> points)
	{
		return new ConcretePath(points);
	}
}