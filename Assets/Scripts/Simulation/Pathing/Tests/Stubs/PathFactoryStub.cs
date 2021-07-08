using System.Collections.Generic;

public class PathFactoryStub : PathFactory
{
	public Path PathFromPoints(Vector startPoint, List<Vector> crossingPoints, Vector finalPoint)
	{
		List<Vector> allPoints = new List<Vector>();
		allPoints.Add(startPoint);
		allPoints.AddRange(crossingPoints);
		allPoints.Add(startPoint);

		return PathFromPoints(allPoints);
	}

	public Path PathFromPoints(List<Vector> points)
	{
		return new PathStub(points);
	}
}