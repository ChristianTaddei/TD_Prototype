using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfaceGeometry {
/*	
	public Maybe<SurfacePoint> GetIntersectionToward(SurfacePoint start, SurfacePoint end, List<Face> alreadyVisitedFaces)
	{
		if (start.FloatRepresentation == end.FloatRepresentation)
		{
			return new Maybe<SurfacePoint>.Just(start);
		}

		Triangle flatStartBase = ProjectOnPlane_Oy(start.BarycentricVector.Base);

		VectorialGeometry cg = new VectorialGeometry();
		BarycentricVector endInFlatStartBase = new BarycentricVector(
			flatStartBase,
			cg.Project(new ConcreteVector(end.BarycentricVector.FloatRepresentation), flatStartBase));

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

		HashSet<Face> facesSharingChangedCoordinates = start.Face.GetFacesFromSharedVertices(sharedVertices);
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

		Face nextFace = facesSharingChangedCoordinates
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
			new ConcreteVector(
				new Vector3(triangle.A.FloatRepresentation.x, 0, triangle.A.FloatRepresentation.z)),
			new ConcreteVector(
				new Vector3(triangle.B.FloatRepresentation.x, 0, triangle.B.FloatRepresentation.z)),
			new ConcreteVector(
				new Vector3(triangle.C.FloatRepresentation.x, 0, triangle.C.FloatRepresentation.z)));
	}
*/
}