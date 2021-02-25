using System;
using UnityEngine;

public class FixedRepresentation : PartialRepresentation<IBoardPosition>
{
    protected Vector3 position = Vector3.zero;
    // protected Vector3 direction = Vector3.forward; 

    public override void SetNextRepresentedState(IBoardPosition value)
    {
        position = value.Position;
    }

    public override void SetPrevRepresentedState(IBoardPosition value)
    {
        position = value.Position;
    }

    public override void Sync(){
        this.transform.position = this.position;
    }

    protected override void Start()
    {
        // throw new NotImplementedException();
    }
}