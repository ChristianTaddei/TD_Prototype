using System;
using System.Collections.Generic;
using System.Linq;

public class StatelessPathfinder : Pathfinder
{
	private Geometry geometry;
	private PathFactory pathFactory;

	public StatelessPathfinder(Geometry geometry, PathFactory pathFactory)
	{
		this.geometry = geometry;
		this.pathFactory = pathFactory;
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
			List<Triangle> alreadyVisitedFaces = new List<Triangle>();

			Triangle finalFace = surface.GetFacesContaining(finalPoint).First();

			Vector currentPoint = startPoint;
			Triangle currentFace = surface.GetFacesContaining(currentPoint).First();
			while (currentFace != finalFace)
			{
				Vector intersection = geometry.GetTriangleIntersectionToward(currentFace, currentPoint, finalPoint);
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

			return new Maybe<Path>.Just(pathFactory.PathFromPoints(startPoint, crossingPoints, finalPoint));
		}
		catch (InvalidOperationException e) // TODO: replace first with something safer
		{
			return new Maybe<Path>.Nothing();
		}
	}
}