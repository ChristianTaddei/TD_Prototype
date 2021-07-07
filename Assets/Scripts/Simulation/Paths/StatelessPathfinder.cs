using System.Collections.Generic;

public class StatelessPathfinder : Pathfinder
{
	private Geometry geometry;
	private VectorFactory vectorFactory;

	public StatelessPathfinder(Geometry geometry){
		this.geometry = geometry;
	}

	public Maybe<Path> GetDirectPath(Surface surface, Vector startPoint, Vector endPoint)
	{
		if (!surface.Contains(startPoint) || !surface.Contains(endPoint))
		{
			return new Maybe<Path>.Nothing();
		}

		List<Vector> crossingPoints = new List<Vector>();

		// Vector 

		return new Maybe<Path>.Nothing();
	}
}