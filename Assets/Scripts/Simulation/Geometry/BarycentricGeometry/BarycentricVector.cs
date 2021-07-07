using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A base and 3 cooridnates specifying a point in space
public class BarycentricVector // : Vector
{
	// public Vector3 FloatRepresentation =>
	// 	BarycentricCoordinates.a * _base.A.FloatRepresentation
	// 	+ BarycentricCoordinates.b * _base.B.FloatRepresentation
	// 	+ BarycentricCoordinates.c * _base.C.FloatRepresentation;

	// private readonly Triangle _base;
	// public Triangle Base { get => _base; }

	// public readonly BarycentricCoordinates BarycentricCoordinates;

	// public BarycentricVector(Triangle _base, BarycentricCoordinates coordinates)
	// {
	// 	this._base = _base;
	// 	BarycentricCoordinates = coordinates;
	// }
/*
	public BarycentricVector(Triangle _base, ConcreteVector p) // TODO: project by default, want warning or something?
	{
		this._base = _base;
		VectorialGeometry cartesianGeometry = new VectorialGeometry(); // TODO: created in barGeom

		ConcreteVector a = new ConcreteVector(_base.A.FloatRepresentation);
		ConcreteVector b = new ConcreteVector(_base.B.FloatRepresentation);
		ConcreteVector c = new ConcreteVector(_base.C.FloatRepresentation);

		ConcreteVector ap = cartesianGeometry.Substract(p, a);

		ConcreteVector ab = cartesianGeometry.Substract(b, a);
		ConcreteVector ac = cartesianGeometry.Substract(c, a);

		ConcreteVector projectedP = p;
		if (!cartesianGeometry.AreComplanar(ap, ab, ac))
		{
			projectedP = cartesianGeometry.Project(p, _base);
		}


		ConcreteVector bc = cartesianGeometry.Substract(c, b);
		ConcreteVector ca = cartesianGeometry.Substract(a, c);

		ConcreteVector apP = cartesianGeometry.Substract(projectedP, a);
		ConcreteVector bpP = cartesianGeometry.Substract(projectedP, b);
		ConcreteVector cpP = cartesianGeometry.Substract(projectedP, c);

		ConcreteVector n = cartesianGeometry.Cross(ab, ac);
		ConcreteVector n_a = cartesianGeometry.Cross(bc, bpP);
		ConcreteVector n_b = cartesianGeometry.Cross(ca, cpP);
		ConcreteVector n_c = cartesianGeometry.Cross(ab, apP);

		float squareMag = cartesianGeometry.Magnitude(n) * cartesianGeometry.Magnitude(n);

		BarycentricCoordinates barycentricCoordinates =
			new BarycentricCoordinates(
				cartesianGeometry.Dot(n, n_a) / squareMag,
				cartesianGeometry.Dot(n, n_b) / squareMag,
				cartesianGeometry.Dot(n, n_c) / squareMag
			);

		this.BarycentricCoordinates = barycentricCoordinates;
	}

	private BarycentricVector() { }

	// public static implicit operator Vector3(BarycentricVector bv) => bv.Coordinates;

	public bool IsPointComplanarToBase()
	{
		return BarycentricCoordinates.CheckSumToOne();
	}

	public bool IsDirectionComplanarToBase()
	{
		return BarycentricCoordinates.CheckSumToZero();
	}

	public bool IsPointOnBaseTriangle()
	{
		return IsPointComplanarToBase() && BarycentricCoordinates.CheckInternal();
	}

	public BarycentricVector ChangeBase(Triangle newBase)
	{
		if (this._base == newBase)
		{
			return new BarycentricVector(newBase, BarycentricCoordinates);
		}

		bool allSuccess = true;
		BarycentricVector oldBaseAInNewBase = new BarycentricVector(newBase, new ConcreteVector(_base.A.FloatRepresentation));
		BarycentricVector oldBaseBInNewBase = new BarycentricVector(newBase, new ConcreteVector(_base.B.FloatRepresentation));
		BarycentricVector oldBaseCInNewBase = new BarycentricVector(newBase, new ConcreteVector(_base.C.FloatRepresentation));

		if (!allSuccess)
		{
			// base change should always be possible (needs projection probably)
			throw new Exception("ChangeBase -> FromPoint failed");
		}

		return new BarycentricVector(
			newBase,
			new BarycentricCoordinates(
				oldBaseAInNewBase.BarycentricCoordinates.a * this.BarycentricCoordinates.a
				+ oldBaseBInNewBase.BarycentricCoordinates.a * this.BarycentricCoordinates.b
				+ oldBaseCInNewBase.BarycentricCoordinates.a * this.BarycentricCoordinates.c,

				oldBaseAInNewBase.BarycentricCoordinates.b * this.BarycentricCoordinates.a
				+ oldBaseBInNewBase.BarycentricCoordinates.b * this.BarycentricCoordinates.b
				+ oldBaseCInNewBase.BarycentricCoordinates.b * this.BarycentricCoordinates.c,

				oldBaseAInNewBase.BarycentricCoordinates.c * this.BarycentricCoordinates.a
				+ oldBaseBInNewBase.BarycentricCoordinates.c * this.BarycentricCoordinates.b
				+ oldBaseCInNewBase.BarycentricCoordinates.c * this.BarycentricCoordinates.c
			)
		);
	}

	public BarycentricVector Normalize()
	{
		float Magnitude =
			Math.Abs(BarycentricCoordinates.a)
			+ Math.Abs(BarycentricCoordinates.b)
			+ Math.Abs(BarycentricCoordinates.c);

		if (Magnitude == 0) throw new Exception();

		return new BarycentricVector(
			_base,
			new BarycentricCoordinates(
				BarycentricCoordinates.a / Magnitude,
				BarycentricCoordinates.b / Magnitude,
				BarycentricCoordinates.c / Magnitude
			)
		);
	}

	public static BarycentricVector operator +(BarycentricVector b1, BarycentricVector b2)
	{
		BarycentricVector same_baseB2 = b2.ChangeBase(b1._base);

		return new BarycentricVector(b1._base, b1.BarycentricCoordinates + same_baseB2.BarycentricCoordinates);
	}

	public static BarycentricVector operator -(BarycentricVector b1, BarycentricVector b2)
	{
		BarycentricVector same_baseB2 = b2.ChangeBase(b1._base);
		return new BarycentricVector(b1._base, b1.BarycentricCoordinates - same_baseB2.BarycentricCoordinates);
	}
*/
}
