using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModifyTerrainCommand : Command
{
    private readonly Surface surface;

    public ModifyTerrainCommand(Surface surface)
    {
        this.surface = surface;
    }

    public Face TargetFace { get; set; }

    public void Execute()
    {
        int i = surface.vertices.IndexOf(TargetFace.svA);
        surface.vertices[i] = new SurfaceVertex(
            new CartesianVector(
                new Vector3(
                    TargetFace.svA.Position.x,
                    TargetFace.svA.Position.y + 1,
                    TargetFace.svA.Position.z)));
    }
}
