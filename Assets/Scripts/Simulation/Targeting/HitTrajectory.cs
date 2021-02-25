using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrajectory 
{
    private Vector3 firePoint;
    private Vector3 target;

    public HitTrajectory(Vector3 firePoint, Vector3 target)
    {
        FirePoint = firePoint;
        Target = target;
    }

    public Vector3 FirePoint { get => firePoint; set => firePoint = value; }
    public Vector3 Target { get => target; set => target = value; }

    public Vector3 Position => firePoint;
}
