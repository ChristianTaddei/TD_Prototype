using System;
using System.Collections.Generic;

public class SurfacePathfinder : Pathfinder
{
	private SurfaceGeometry surfaceGeometry;

	public SurfacePathfinder(SurfaceGeometry surfaceGeometry){
		this.surfaceGeometry = surfaceGeometry;
	}

	public Path FindPath(Vector start, Vector end)
	{
		List<SurfacePoint> points = new List<SurfacePoint>();

		// start and end to surfacePoints <- needs factory
		// using geometry, find path

		return new SurfacePath(points);
	}

	public Maybe<SurfacePath> MakeDirectPath(SurfacePoint startPoint, SurfacePoint endPoint)
	{
		if (startPoint.Face.Surface !=  endPoint.Face.Surface)
		{
			return new Maybe<SurfacePath>.Nothing();
		}

		List<SurfacePoint> crossingPoints = new List<SurfacePoint>();

		List<AbstractFace> alreadyVisitedFaces = new List<AbstractFace>();
		alreadyVisitedFaces.Add(startPoint.Face);
		int tries = 0;
		SurfacePoint currentPoint = startPoint;
		while (currentPoint.Face != endPoint.Face)
		{
			Maybe<SurfacePoint> intersection = surfaceGeometry.GetIntersectionToward(currentPoint, endPoint, alreadyVisitedFaces);
			if (intersection.HasValue())
			{
				if (intersection.Value.FloatRepresentation == endPoint.FloatRepresentation) break;

				alreadyVisitedFaces.Add(intersection.Value.Face);
				crossingPoints.Add(intersection.Value);
				currentPoint = intersection.Value;
			}
			else
			{
				return new Maybe<SurfacePath>.Nothing();
			}

			tries++;
			if (tries > 1000)
			{
				throw new Exception("Too many tries to get intersection");
			}
		}

		List<SurfacePoint> allPoints = new List<SurfacePoint>();
		allPoints.Add(startPoint);
		allPoints.AddRange(crossingPoints);
		allPoints.Add(endPoint);

		List<SurfacePoint> noDuplicatePositions = new List<SurfacePoint>();
		noDuplicatePositions.Add(allPoints[0]);
		for (int i = 1; i < allPoints.Count; i++)
		{
			SurfacePoint p1 = allPoints[i - 1], p2 = allPoints[i];
			if (!(UnityEngine.Mathf.Abs(p1.FloatRepresentation.x - p2.FloatRepresentation.x) < 0.0001f
				&& UnityEngine.Mathf.Abs(p1.FloatRepresentation.y - p2.FloatRepresentation.y) < 0.0001f
				&& UnityEngine.Mathf.Abs(p1.FloatRepresentation.z - p2.FloatRepresentation.z) < 0.0001f))
			{
				noDuplicatePositions.Add(p2);
			}
		}

		return new Maybe<SurfacePath>.Just(new SurfacePath(noDuplicatePositions));
	}
}
