using System.Collections.Generic;

public class GeometryStub : Geometry
{
	public void AddGetTriangleIntersectionTowardStub((Triangle, Vector, Vector) parameters, Vector intersection)
	{
		setupStubs.Add((parameters.Item1, parameters.Item2, parameters.Item3, intersection));
	}

	private List<(Triangle, Vector, Vector, Vector)> setupStubs = new List<(Triangle, Vector, Vector, Vector)>();

	public Vector GetTriangleIntersectionToward(Triangle t, Vector currentPoint, Vector finalPoint)
	{
		// TODO: triangle is the same if vertices are in a different order?
		TriangleStub Triangle_000_100_001
		= new TriangleStub(
			new VectorStub(0, 0, 0),
			new VectorStub(1, 0, 0),
			new VectorStub(0, 0, 1));

		if (t.Equals(Triangle_000_100_001)
			&& currentPoint.Equals(new VectorStub(0, 0, 0))
			&& finalPoint.Equals(new VectorStub(1, 0, 1)))
		{
			return new VectorStub(0.5f, 0, 0.5f);
		}

		foreach ((Triangle, Vector, Vector, Vector) tuple in setupStubs)
		{
			if (
				tuple.Item1.Equals(t) &&
				tuple.Item2.Equals(currentPoint) &&
				tuple.Item3.Equals(finalPoint)
				)
			{
				return tuple.Item4;
			}
		}

		throw new System.Exception("Parameters not setup in GeometryStub.GetTriangleIntersectionToward");
	}
}