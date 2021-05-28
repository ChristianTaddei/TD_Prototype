using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrainCommand : Command
{
    private readonly Surface surface;

    public ModifyTerrainCommand(Surface surface)
    {
        this.surface = surface;
    }


    public void Execute(Maybe<SurfacePoint> sp, float heightChange)
    {
        Debug.Log("click");
        // raise face containing
        if (sp.HasValue())
        {
            //
        }

        // later: make circle and raise all vertices in it
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }
}
