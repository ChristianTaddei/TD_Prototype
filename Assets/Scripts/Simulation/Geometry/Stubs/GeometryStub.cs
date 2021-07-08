public class GeometryStub : Geometry
{
	public Vector GetTriangleIntersectionToward(Triangle t, Vector currentPoint, Vector finalPoint)
	{
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

		throw new System.Exception("Parameters not setup in GeometryStub.GetTriangleIntersectionToward");
	}
}