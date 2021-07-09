using System;
using System.Collections.Generic;

public class GeometryStub : Geometry
{
	// TODO: can all this be automatic? Only missing part is acting on method name 
	// & auto get right param fom actual signature, do as Moq or use reflection?
	public void AddGetTriangleIntersectionTowardStub((Triangle, Vector, Vector) parameters, Vector result)
	{
		preparedGetTriangleIntersectionTowardStubs.Add((parameters.Item1, parameters.Item2, parameters.Item3, result));
	}

	private List<(Triangle, Vector, Vector, Vector)> preparedGetTriangleIntersectionTowardStubs = new List<(Triangle, Vector, Vector, Vector)>();

	public Vector GetTriangleIntersectionToward(Triangle t, Vector currentPoint, Vector finalPoint)
	{
		foreach ((Triangle, Vector, Vector, Vector) tuple in preparedGetTriangleIntersectionTowardStubs)
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