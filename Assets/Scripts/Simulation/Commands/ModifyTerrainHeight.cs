using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrainHeight : Command
{
    public float BrushRadius { get; set; }
    public SurfacePoint BrushCenter { get; set; }
    public float HeightChange { get; set; }

    private List<GameObject> selectionMarkers;
    private readonly Surface surface;

    public ModifyTerrainHeight(Surface surface)
    {
        this.surface = surface;
    }

    public void Execute()
    {
        // for(Vertex v in new Brush(BrushCenter, BrushRadius).Vertices){

        // }
    }

    // public override void Update()
    // {
    //     ClearSelectionMarkers();
    //     BoardPosition boardPosition;
    //     if (inputManager.TryGetBoardPositionUnderCursor(out boardPosition))
    //     {
    //         Circle selection = new Circle(boardPosition, Radius);
    //         HashSet<Vertex> cellsInRange = selection.Cells;

    //         selectionMarkers.AddRange(
    //             representationManager.HighlightBoardVertices(
    //                 cellsInRange,
    //                 HighlightSize.VerySmall,
    //                 Color.green
    //             )
    //         );

    //         if (inputManager.LeftClick())
    //         {
    //             simulationManager.Board.RaiseCells(cellsInRange, HeightChange);
    //             // SimulationManager.Instance.CurrentStateModified();
    //         }
    //     }
    // }

    // private void ClearSelectionMarkers()
    // {
    //     foreach (GameObject selectionMarker in selectionMarkers)
    //     {
    //         GameObject.Destroy(selectionMarker);
    //     }

    //     selectionMarkers.Clear();
    // }

    // public override void Mount()
    // {
    //     selectionMarkers = new List<GameObject>();
    // }

    // public override void Unmount()
    // {
    //     ClearSelectionMarkers();
    // }
}
