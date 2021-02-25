using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRepresentation : Representation<EnemyState>
{
    private bool destroyedThisStep = false;

    private AimingRepresentation aimingRepresentation;
    private MovingRepresentation movingRepresentation;

    protected override void Start()
    {
        movingRepresentation = gameObject.AddComponent<MovingRepresentation>();
        aimingRepresentation = gameObject.AddComponent<AimingRepresentation>();
    }

    public override void SetPrevRepresentedState(EnemyState prevState)
    {
        if(movingRepresentation == null) return; // Can be called before start can trigger

        movingRepresentation.prevPosition = prevState.Position;
        movingRepresentation.prevDirection = movingRepresentation.nextDirection;

        if (destroyedThisStep)
        {
            GameObject.Destroy(gameObject);
        }

        destroyedThisStep = prevState.Destroyed;
    }

    public override void SetNextRepresentedState(EnemyState nextState)
    {
        if(movingRepresentation == null) return;

        movingRepresentation.nextPosition = nextState.Position;
        movingRepresentation.nextDirection = movingRepresentation.nextPosition - movingRepresentation.prevPosition;

        if (destroyedThisStep)
        {
            GameObject.Destroy(gameObject);
        }

        destroyedThisStep = nextState.Destroyed;
    }

    public override void Sync()
    {
        movingRepresentation.Sync();
    }

}
