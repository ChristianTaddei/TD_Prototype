using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteSurface : AbstractSurface
{
	// internal to be added from face, invert?
	internal List<SurfaceVertex> vertices;
	private List<AbstractFace> faces;

	public override List<SurfaceVertex> Vertices => new List<SurfaceVertex>(vertices);
	public override List<AbstractFace> Faces => new List<AbstractFace>(faces);

	public ConcreteSurface()
	{
		this.vertices = new List<SurfaceVertex>();
		this.faces = new List<AbstractFace>();
	}

	public override void AddFace(AbstractFace face)
	{
		faces.Add(face);
	}

	public override AbstractFace AddFace(Vector a, Vector b, Vector c)
	{
		ConcreteFace newFace = new ConcreteFace(this, new ConcreteTriangle(a, b, c));
		return newFace;
	}

	// TODO: move away
	public void Raise(AbstractFace targetFace, float heightChange)
	{
		// TODO: avoid this casts
		if (targetFace is ConcreteFace)
		{
			ConcreteFace concreteTargetFace = targetFace as ConcreteFace;
			int i = vertices.IndexOf(concreteTargetFace.svA);
			vertices[i] = new SurfaceVertex(
				new CartesianVector(
					new Vector3(
						concreteTargetFace.svA.FloatRepresentation.x,
						concreteTargetFace.svA.FloatRepresentation.y + heightChange,
						concreteTargetFace.svA.FloatRepresentation.z)));
		}


	}

	// public void Raise(Face targetFace)
	// {
	//     List<SurfaceVertex> verticesToRaise = new List<SurfaceVertex>();
	//     verticesToRaise.Add(targetFace.A);
	// }

	public Maybe<SurfacePoint> GetSurfacePoint(int triangleIndex, Vector3 position)
	{
		AbstractFace faceContainingPoint = faces[triangleIndex];
		BarycentricVector vector =
				new BarycentricVector(faceContainingPoint,
				new CartesianVector(position));

		if (!vector.IsPointOnBaseTriangle())
		{
			return new Maybe<SurfacePoint>.Nothing();
		}

		return new Maybe<SurfacePoint>.Just(new SurfacePoint(faceContainingPoint, vector));
	}

	public Maybe<SurfacePoint> GetSurfacePoint(Vector3 point)
	{
		foreach (ConcreteFace face in faces)
		{
			BarycentricVector bc = new BarycentricVector(face, new CartesianVector(point));
			if (bc.IsPointOnBaseTriangle())
			{
				return new Maybe<SurfacePoint>.Just(new SurfacePoint(face, bc));
			}
		}

		return new Maybe<SurfacePoint>.Nothing();
	}

	public Maybe<SurfacePath> MakeDirectPath(SurfacePoint startPoint, SurfacePoint endPoint)
	{

		if (startPoint.Face.Surface != this || endPoint.Face.Surface != this)
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
			Maybe<SurfacePoint> intersection = GetIntersectionToward(currentPoint, endPoint, alreadyVisitedFaces);
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

	public Maybe<SurfacePoint> GetIntersectionToward(SurfacePoint start, SurfacePoint end, List<AbstractFace> alreadyVisitedFaces)
	{
		if (start.FloatRepresentation == end.FloatRepresentation)
		{
			return new Maybe<SurfacePoint>.Just(start);
		}

		Triangle flatStartBase = ProjectOnPlane_Oy(start.BarycentricVector.Base);

		CartesianGeometry cg = new CartesianGeometry();
		BarycentricVector endInFlatStartBase = new BarycentricVector(
			flatStartBase,
			cg.Project(new CartesianVector(end.BarycentricVector.FloatRepresentation), flatStartBase));

		BarycentricVector flatEndInStartBase = new BarycentricVector(
			start.BarycentricVector.Base,
			endInFlatStartBase.BarycentricCoordinates);

		BarycentricVector startToFlatEnd = flatEndInStartBase - start.BarycentricVector;

		float coefficient = float.MaxValue;
		foreach (TriangleVertexIdentifiers c in Triangle.Vertices)
		{
			if (start.BarycentricVector.BarycentricCoordinates.GetCoordinate(c) == 0.0f)
			{
				if (startToFlatEnd.BarycentricCoordinates.GetCoordinate(c) >= 0.0f)
				{
					continue;
				}
			}

			float denominator = startToFlatEnd.BarycentricCoordinates.GetCoordinate(c);
			if (Mathf.Abs(denominator) <= 0.0001f) // FIXME: epsilon
			{
				continue;
			}
			else
			{
				float partialCoefficient;
				partialCoefficient = -start.BarycentricVector.BarycentricCoordinates.GetCoordinate(c) / denominator;
				if (partialCoefficient >= 0.0f && partialCoefficient < coefficient)
				{
					coefficient = partialCoefficient;
				}
			}
		}

		Func<float, float> snapIfZero = (float initialValue) =>
		{
			if (Mathf.Abs(initialValue) <= 0.0001f)
			{
				return 0.0f;
			}

			return initialValue;
		};

		BarycentricCoordinates intersectionCoordinates = new BarycentricCoordinates(
		   snapIfZero((start.BarycentricVector.BarycentricCoordinates + coefficient * startToFlatEnd.BarycentricCoordinates).a),
		   snapIfZero((start.BarycentricVector.BarycentricCoordinates + coefficient * startToFlatEnd.BarycentricCoordinates).b),
		   snapIfZero((start.BarycentricVector.BarycentricCoordinates + coefficient * startToFlatEnd.BarycentricCoordinates).c)
		);

		BarycentricVector intersectionVector =
			new BarycentricVector(
				start.Face,
				intersectionCoordinates);

		intersectionVector = intersectionVector.Normalize();

		// if (!intersectionVector.IsPointOnBaseTriangle())
		// {
		//     Debug.Log("Before change, not norm");
		// }

		HashSet<TriangleVertexIdentifiers> sharedVertices = new HashSet<TriangleVertexIdentifiers>(Triangle.Vertices);
		foreach (TriangleVertexIdentifiers c in Triangle.Vertices)
		{
			if (intersectionCoordinates.GetCoordinate(c) != 1.0f)
			{
				sharedVertices.Add(c);
			}
		}

		HashSet<AbstractFace> facesSharingChangedCoordinates = start.Face.GetFacesFromSharedVertices(sharedVertices);
		facesSharingChangedCoordinates.Remove(start.Face);
		if (facesSharingChangedCoordinates.Count == 0)
		{
			facesSharingChangedCoordinates = start.Face.GetFacesFromAtLeastOneSharedVertex(sharedVertices);
			facesSharingChangedCoordinates.Remove(start.Face);
		}

		if (facesSharingChangedCoordinates.Count == 0
			|| facesSharingChangedCoordinates
				.Where(face => intersectionVector.ChangeBase(face).IsPointOnBaseTriangle())
				.Count() == 0)
		{
			return new Maybe<SurfacePoint>.Nothing();
		}

		AbstractFace nextFace = facesSharingChangedCoordinates
			.Where(face => intersectionVector.ChangeBase(face).IsPointOnBaseTriangle()
				&& !alreadyVisitedFaces.Contains(face))
			.OrderBy(face => Mathf.Min(Mathf.Min(
				Vector3.Distance(face.A.FloatRepresentation, flatEndInStartBase.FloatRepresentation),
				Vector3.Distance(face.B.FloatRepresentation, flatEndInStartBase.FloatRepresentation)),
				Vector3.Distance(face.C.FloatRepresentation, flatEndInStartBase.FloatRepresentation)))
			.First();

		intersectionVector = intersectionVector.ChangeBase(nextFace);
		intersectionVector = intersectionVector.Normalize();

		// if (!intersectionVector.IsPointOnBaseTriangle())
		// {
		//     Debug.Log("After change, not norm");
		// }

		return new Maybe<SurfacePoint>.Just(new SurfacePoint(nextFace, intersectionVector));
	}

	private static Triangle ProjectOnPlane_Oy(Triangle triangle)
	{
		return new ConcreteTriangle(
			new CartesianVector(
				new Vector3(triangle.A.FloatRepresentation.x, 0, triangle.A.FloatRepresentation.z)),
			new CartesianVector(
				new Vector3(triangle.B.FloatRepresentation.x, 0, triangle.B.FloatRepresentation.z)),
			new CartesianVector(
				new Vector3(triangle.C.FloatRepresentation.x, 0, triangle.C.FloatRepresentation.z)));
	}

	// Surface made of squares
	public ConcreteSurface(float edgeSize) : this()
	{
		for (int i = 0; i < edgeSize; i++)
		{
			for (int j = 0; j < edgeSize; j++)
			{
				addSquareAt(new Vector3(i, 0, j));
			}
		}
	}

	public void addSquareAt(Vector3 point)
	{
		CartesianVector _A = new CartesianVector(point + new Vector3(1, 0, 0));
		CartesianVector _B = new CartesianVector(point + new Vector3(1, 0, 1));
		CartesianVector _C = new CartesianVector(point + new Vector3(0, 0, 1));
		CartesianVector _D = new CartesianVector(point + new Vector3(0, 0, 0));

		AbstractFace ACB = AddFace(_A, _C, _B);
		AbstractFace ADC = AddFace(_A, _D, _C);
	}

	public ConcreteSurface(float edgeSize, float maxH) : this()
	{
		for (int i = 0; i < edgeSize; i++)
		{
			for (int j = 0; j < edgeSize; j++)
			{
				addTiltedSquareAt(new Vector3(i, (i + j) / 2.0f, j));
			}
		}
	}

	public void addTiltedSquareAt(Vector3 point)
	{
		CartesianVector _A = new CartesianVector(point + new Vector3(1, .5f, 0));
		CartesianVector _B = new CartesianVector(point + new Vector3(1, 1, 1));
		CartesianVector _C = new CartesianVector(point + new Vector3(0, .5f, 1));
		CartesianVector _D = new CartesianVector(point + new Vector3(0, 0, 0));

		AbstractFace ACB = AddFace(_A, _C, _B);
		AbstractFace ADC = AddFace(_A, _D, _C);
	}
}
