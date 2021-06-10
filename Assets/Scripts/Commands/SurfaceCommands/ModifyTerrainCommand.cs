using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModifyTerrainCommand : Command
{
    private readonly Surface surface;

    public Face TargetFace { get; set; }
    public float HeightChange { get; set; }

    public ModifyTerrainCommand(Surface surface)
    {
        this.surface = surface;
    }

    public void Execute()
    {
        // TODO: move to surf
        // int i = surface.vertices.IndexOf(TargetFace.svA);
        // surface.vertices[i] = new SurfaceVertex(
        //     new CartesianVector(
        //         new Vector3(
        //             TargetFace.svA.Position.x,
        //             TargetFace.svA.Position.y + HeightChange,
        //             TargetFace.svA.Position.z)));
    }

    
    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
