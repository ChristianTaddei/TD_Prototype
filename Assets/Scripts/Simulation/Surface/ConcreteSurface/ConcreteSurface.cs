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
