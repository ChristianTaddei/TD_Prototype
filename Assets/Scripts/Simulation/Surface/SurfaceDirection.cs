using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDirection
{
    private CartesianVector direction { get; }

    public SurfaceDirection(SurfacePoint start, SurfacePoint end)
    {
        this.direction =
            new CartesianVector(
                end.Position - start.Position);
    }
}
