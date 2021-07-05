using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModifyTerrainCommand : Command
{
    private readonly ConcreteSurface surface;

    public AbstractFace TargetFace { get; set; }
    public float HeightChange { get; set; }

    public ModifyTerrainCommand(ConcreteSurface surface)
    {
        this.surface = surface;
    }

    public void Execute()
    {
        surface.Raise(TargetFace, HeightChange);
    }

	public void Undo()
	{
		throw new System.NotImplementedException();
	}
}
