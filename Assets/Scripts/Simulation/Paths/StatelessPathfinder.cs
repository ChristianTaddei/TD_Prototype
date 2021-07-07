using System;
using System.Collections.Generic;
using System.Linq;

public class StatelessPathfinder : Pathfinder
{
	private Geometry geometry;

	public StatelessPathfinder(Geometry geometry)
	{
		this.geometry = geometry;
	}

	public Maybe<Path> GetDirectPath(Surface surface, Vector startPoint, Vector finalPoint)
	{
		try
		{
			if (!surface.Contains(startPoint) || !surface.Contains(finalPoint))
			{
				return new Maybe<Path>.Nothing();
			}

			List<Vector> crossingPoints = new List<Vector>();
			crossingPoints.Add(startPoint);

			List<Triangle> alreadyVisitedFaces = new List<Triangle>();

			Triangle finalFace = surface.GetFacesContaining(finalPoint).First();

			Vector currentPoint = startPoint;
			Triangle currentFace = surface.GetFacesContaining(currentPoint).First();
			while (currentFace != finalFace)
			{
				Vector intersection = geometry.GetTriangleIntersectionToward(currentPoint, finalPoint);
				alreadyVisitedFaces.Add(currentFace);

				List<Triangle> candidatesForNextFace = surface.GetFacesContaining(intersection);
				candidatesForNextFace.RemoveAll(t => alreadyVisitedFaces.Contains(t));

				if (!crossingPoints.Contains(intersection))
				{
					crossingPoints.Add(intersection);
				}

				currentPoint = intersection;
				currentFace = candidatesForNextFace.First();
			}

			if (!crossingPoints.Contains(finalPoint))
			{
				crossingPoints.Add(finalPoint);
			}

			return new Maybe<Path>.Just(new Path(crossingPoints));
		}
		catch (InvalidOperationException e) // TODO: replace first with something safer
		{
			return new Maybe<Path>.Nothing();
		}
	}
}