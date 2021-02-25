using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRepresentation : Representation<TowerState>
{
    FixedRepresentation fixedRepresentation;
    AimingRepresentation aimingRepresentation;

    protected override void Start()
    {
        fixedRepresentation = gameObject.AddComponent<FixedRepresentation>();
    }

    public override void Sync()
    {
        fixedRepresentation.Sync();
        //    aimingRepresentation.Sync();

        // HPivot.LookAt(
        //    Vector3.Lerp(
        //        new Vector3(lastTarget.x, HPivot.position.y, lastTarget.z),
        //        new Vector3(nextTarget.x, HPivot.position.y, nextTarget.z),
        //        RepresentationManager.Instance.RepresentationStepElapsedFraction));
    }

    private Transform hPivot;
    private Transform HPivot
    {
        get
        {
            if (hPivot == default)
            {
                hPivot = gameObject.transform.GetComponentsInChildren<Transform>().Where(ct => ct.name == "Tower_Walls").First();
            }

            return hPivot;
        }
    }


    // public override void SetPrevRepresentedState(IState value) // Inherited from interface (not generic...)
    // {
    //     TowerState ts = value as TowerState;
    //     this.position = ts.BoardPosition.Cartesians;
    //     this.direction = ts.Direction;

    //     this.lastTarget = ts.Target;
    // }

    // public override void SetNextRepresentedState(IState value)
    // {
    //     TowerState ts = value as TowerState;
    //     this.position = ts.BoardPosition.Cartesians;
    //     this.direction = ts.Direction;

    //     this.nextTarget = ts.Target;
    // }

    public override void SetPrevRepresentedState(TowerState value)
    {
        if (fixedRepresentation == null) return;

        fixedRepresentation.SetPrevRepresentedState(value);
        // aimingRepresentation.SetPrevRepresentedState(value);
    }

    public override void SetNextRepresentedState(TowerState value)
    {
        if (fixedRepresentation == null) return;

        fixedRepresentation.SetNextRepresentedState(value);
        // aimingRepresentation.SetNextRepresentedState(value);
    }
}
