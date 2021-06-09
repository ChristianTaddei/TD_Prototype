﻿using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrainState : InterfaceState
{
    private ModifyTerrainCommand modifyTerrainCommand;

    private List<GameObject> selectionMarkers;

    public float BrushRadius { get; set; } = 2.0f;
    public float HeightChange { get; set; } = 1.0f;

    public ModifyTerrainState(ModifyTerrainCommand modifyTerrainCommand)
    {
        this.modifyTerrainCommand = modifyTerrainCommand;
    }

    public override void Update()
    {
        ClearSelectionMarkers();
        Maybe<SurfacePoint> sp = InputManager.Instance.GetSurfacePointUnderCursor();
        if (sp.HasValue())
        {
            selectionMarkers.Add(
                RepresentationFactory.HighlightSurfacePoint(
                    sp.Value,
                    HighlightSize.VerySmall,
                    Color.green
                )
            );
            if (InputManager.Instance.LeftClick())
            {
                Debug.Log("clicked sp: " + sp.Value.Position);
                
                modifyTerrainCommand.TargetFace = sp.Value.Face;
                modifyTerrainCommand.Execute();
            }
        }
    }

    private void ClearSelectionMarkers()
    {
        foreach (GameObject selectionMarker in selectionMarkers)
        {
            GameObject.Destroy(selectionMarker);
        }

        selectionMarkers.Clear();
    }

    public override void Mount()
    {
        selectionMarkers = new List<GameObject>();
    }

    public override void Unmount()
    {
        ClearSelectionMarkers();
    }
}
