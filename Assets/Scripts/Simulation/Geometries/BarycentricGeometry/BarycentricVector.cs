using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A base and 3 cooridnates specifying a point in space
public class BarycentricVector : Vector
{
    public override Vector3 FloatRepresentation =>
        BarycentricCoordinates.a * _base.A.FloatRepresentation
        + BarycentricCoordinates.b * _base.B.FloatRepresentation
        + BarycentricCoordinates.c * _base.C.FloatRepresentation;

    private readonly Triangle _base;
    public Triangle Base { get => _base; }

    public readonly BarycentricCoordinates BarycentricCoordinates;

    public BarycentricVector(Triangle _base, BarycentricCoordinates coordinates)
    {
        this._base = _base;
        BarycentricCoordinates = coordinates;
    }

    public BarycentricVector(Triangle _base, CartesianVector p) // TODO: project by default, want warning or something?
    {
        this._base = _base;

        CartesianVector a = _base.A.FloatRepresentation;
        CartesianVector b = _base.B.FloatRepresentation;
        CartesianVector c = _base.C.FloatRepresentation;

        if (!(CartesianVector.areComplanar(p - a, b - a, c - a)))
        {
            p = p.Project(_base);
        }

        CartesianVector n = (b - a).Cross(c - a);
        CartesianVector n_a = (c - b).Cross(p - b);
        CartesianVector n_b = (a - c).Cross(p - c);
        CartesianVector n_c = (b - a).Cross(p - a);

        float squareMag = n.magnitude * n.magnitude;

        BarycentricCoordinates barycentricCoordinates =
            new BarycentricCoordinates(
                n.Dot(n_a) / squareMag,
                n.Dot(n_b) / squareMag,
                n.Dot(n_c) / squareMag
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

        // TODO: can find components of old base in new base algebrically?
        bool allSuccess = true;
        BarycentricVector oldBaseAInNewBase = new BarycentricVector(newBase, _base.A.FloatRepresentation);
        BarycentricVector oldBaseBInNewBase = new BarycentricVector(newBase, _base.B.FloatRepresentation);
        BarycentricVector oldBaseCInNewBase = new BarycentricVector(newBase, _base.C.FloatRepresentation);

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
}
