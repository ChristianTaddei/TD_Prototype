using System;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrainState : InterfaceState
{
    private Interface _interface;

    private ModifyTerrainCommand modifyTerrainCommand;
    private HighlightCommand highlightCommand;

    // public float BrushRadius { get; set; } = 2.0f;
    public float HeightChange { get; set; } = 1.0f;

    private List<GameObject> selectionMarkers;

    public ModifyTerrainState(Interface _interface, ModifyTerrainCommand modifyTerrainCommand)
    {
        this._interface = _interface;

        this.modifyTerrainCommand = modifyTerrainCommand;
        updateCommand();
    }


    public override void Mount()
    {
        selectionMarkers = new List<GameObject>();

        _interface.OnSelectCommand = modifyTerrainCommand;
        _interface.OnHoverCommand = highlightCommand;
    }

    public override void Update()
    {
        updateCommand();

        ClearSelectionMarkers();
        // Maybe<SurfacePoint> sp = InputManager.GetSurfacePointUnderCursor();
        // if (sp.HasValue())
        // {
        //     selectionMarkers.Add(
        //         RepresentationFactory.HighlightSurfacePoint(
        //             sp.Value,
        //             HighlightSize.VerySmall,
        //             Color.green
        //         )
        //     );
        //     if (InputManager.Instance.LeftClick())
        //     {
        //         Debug.Log("clicked sp: " + sp.Value.Position);

        //         modifyTerrainCommand.TargetFace = sp.Value.Face;
        //         modifyTerrainCommand.Execute();
        //     }
        // }
    }

    public override void Unmount()
    {
        ClearSelectionMarkers();
    }

    private void updateCommand()
    {
        modifyTerrainCommand.HeightChange = HeightChange;
    }

    private void ClearSelectionMarkers()
    {
        foreach (GameObject selectionMarker in selectionMarkers)
        {
            GameObject.Destroy(selectionMarker);
        }

        selectionMarkers.Clear();
    }
}
