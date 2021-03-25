using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricVector : IVector
{
    public Vector3 Coordinates =>
        barycentricCoordinates.a * _base.a.Coordinates
        + barycentricCoordinates.b * _base.b.Coordinates
        + barycentricCoordinates.c * _base.c.Coordinates;

    public ITriangle Base { get => _base; }

    private readonly ITriangle _base;
    private readonly BarycentricCoordinates barycentricCoordinates;

    public BarycentricVector(ITriangle _base, BarycentricCoordinates coordinates)
    {
        this._base = _base;
        barycentricCoordinates = coordinates;
    }

    private BarycentricVector() { }

    public static bool FromPoint(
        ITriangle _base,
        CartesianPoint cp,
        out BarycentricVector newBarycentricVector,
        bool project = false)
    {
        newBarycentricVector = new BarycentricVector(); //TODO: NO_BAR_VEC
        CartesianVector p = new CartesianVector(cp.Coordinates); // TODO: CV x CP

        CartesianVector a = _base.a.Coordinates;
        CartesianVector b = _base.b.Coordinates;
        CartesianVector c = _base.c.Coordinates;

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
        return barycentricCoordinates.CheckSumToOne();
    }

    public bool IsDirectionComplanarToBase()
    {
        return barycentricCoordinates.CheckSumToZero();
    }

    public bool IsPointOnBaseTriangle()
    {
        return IsPointComplanarToBase() && barycentricCoordinates.CheckInternal();
    }

    public BarycentricVector ChangeBase(ITriangle newBase)
    {
        if (this._base == newBase)
        {
            return new BarycentricVector(newBase, barycentricCoordinates);
        }

        BarycentricVector oldBaseAInNewBase;
        BarycentricVector.FromPoint(newBase, _base.a.Coordinates, out oldBaseAInNewBase);
        BarycentricVector oldBaseBInNewBase;
        BarycentricVector.FromPoint(newBase, _base.b.Coordinates, out oldBaseBInNewBase);
        BarycentricVector oldBaseCInNewBase;
        BarycentricVector.FromPoint(newBase, _base.c.Coordinates, out oldBaseCInNewBase);

        return new BarycentricVector(
            newBase,
            new BarycentricCoordinates(
                oldBaseAInNewBase.barycentricCoordinates.a * this.barycentricCoordinates.a
                + oldBaseBInNewBase.barycentricCoordinates.a * this.barycentricCoordinates.b
                + oldBaseCInNewBase.barycentricCoordinates.a * this.barycentricCoordinates.c,

                oldBaseAInNewBase.barycentricCoordinates.b * this.barycentricCoordinates.a
                + oldBaseBInNewBase.barycentricCoordinates.b * this.barycentricCoordinates.b
                + oldBaseCInNewBase.barycentricCoordinates.b * this.barycentricCoordinates.c,

                oldBaseAInNewBase.barycentricCoordinates.c * this.barycentricCoordinates.a
                + oldBaseBInNewBase.barycentricCoordinates.c * this.barycentricCoordinates.b
                + oldBaseCInNewBase.barycentricCoordinates.c * this.barycentricCoordinates.c
            )
        );
    }

    public BarycentricVector Normalize()
    {
        float Magnitude =
            Math.Abs(barycentricCoordinates.a)
            + Math.Abs(barycentricCoordinates.b)
            + Math.Abs(barycentricCoordinates.c);

        if (Magnitude == 0) throw new Exception();

        return new BarycentricVector(
            _base,
            new BarycentricCoordinates(
                barycentricCoordinates.a / Magnitude,
                barycentricCoordinates.b / Magnitude,
                barycentricCoordinates.c / Magnitude
            )
        );
    }

    // _base is the base of the second operand
    // public static BarycentricVector operator +(BarycentricVector b1, BarycentricVector b2)
    // {
    //     BarycentricVector same_baseB1 = b1.ChangeBase(b2._base);

    //     return new BarycentricVector(b2._base, same_baseB1.barycentricCoordinates + b2.barycentricCoordinates);
    // }

    // public static BarycentricVector operator -(BarycentricVector b1, BarycentricVector b2)
    // {
    //     BarycentricVector same_baseB2 = b2.Change_base(b1._base);
    // STILL USES BASE 2
    //     return new BarycentricVector(b1._base, b1.BarycentricCoordinates - b2.BarycentricCoordinates);
    // }
}
