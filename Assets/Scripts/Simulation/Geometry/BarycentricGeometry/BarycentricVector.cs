using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricVector : IVector
{
    public Vector3 Coordinates =>
        BarycentricCoordinates.a * _base.A.Coordinates
        + BarycentricCoordinates.b * _base.B.Coordinates
        + BarycentricCoordinates.c * _base.C.Coordinates;

    public Triangle Base { get => _base; }

    private readonly Triangle _base;
    public readonly BarycentricCoordinates BarycentricCoordinates;

    public BarycentricVector(Triangle _base, BarycentricCoordinates coordinates)
    {
        this._base = _base;
        BarycentricCoordinates = coordinates;
    }

    private BarycentricVector() { }

    public static bool FromPoint(
        Triangle _base,
        CartesianPoint cp,
        out BarycentricVector newBarycentricVector,
        bool project = false)
    {
        newBarycentricVector = new BarycentricVector(); //TODO: Monad
        CartesianVector p = new CartesianVector(cp.Coordinates); // TODO: CV x CP

        CartesianVector a = _base.A.Coordinates;
        CartesianVector b = _base.B.Coordinates;
        CartesianVector c = _base.C.Coordinates;

        CartesianVector n = (b - a).Cross(c - a);
        CartesianVector n_a = (c - b).Cross(p - b);
        CartesianVector n_b = (a - c).Cross(p - c);
        CartesianVector n_c = (b - a).Cross(p - a);

        if (!(p - a).isComplanarTo(b - a, c - a))
        {
            if (project)
            {
                // TODO: project
                return true;
            }
            else
            {
                return false;
            }
        }

        float squareMag = n.magnitude * n.magnitude;

        BarycentricCoordinates barycentricCoordinates =
            new BarycentricCoordinates(
                n.Dot(n_a) / squareMag,
                n.Dot(n_b) / squareMag,
                n.Dot(n_c) / squareMag
            );

        newBarycentricVector = new BarycentricVector(_base, barycentricCoordinates);
        return true;
    }

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
        BarycentricVector oldBaseAInNewBase;
        allSuccess &= BarycentricVector.FromPoint(newBase, _base.A.Coordinates, out oldBaseAInNewBase);
        BarycentricVector oldBaseBInNewBase;
        allSuccess &= BarycentricVector.FromPoint(newBase, _base.B.Coordinates, out oldBaseBInNewBase);
        BarycentricVector oldBaseCInNewBase;
        allSuccess &= BarycentricVector.FromPoint(newBase, _base.C.Coordinates, out oldBaseCInNewBase);

        if(!allSuccess) {
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
