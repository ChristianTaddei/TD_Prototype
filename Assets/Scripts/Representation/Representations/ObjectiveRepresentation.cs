using System;
using UnityEngine;

public class ObjectiveRepresentation : Representation<ObjectiveState>
{
    FixedRepresentation fixedRepresentation;

    protected override void Start()
    {
        fixedRepresentation = gameObject.AddComponent<FixedRepresentation>();
    }

    public override void SetPrevRepresentedState(ObjectiveState value)
    {
        if (fixedRepresentation != null)
            fixedRepresentation.SetPrevRepresentedState(value);
    }

    public override void SetNextRepresentedState(ObjectiveState value)
    {
        if (fixedRepresentation != null)
            fixedRepresentation.SetNextRepresentedState(value);
    }

    public override void Sync()
    {
        fixedRepresentation.Sync();
    }


}
