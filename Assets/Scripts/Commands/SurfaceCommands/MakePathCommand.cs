using System.Collections.Generic;
using UnityEngine;

public class MakePathCommand : Command
{
    // public override void Update()
    // {
    //     ClearSelectionMarkers();
    //     BoardPosition boardPosition;
    //     if (input.TryGetBoardPositionUnderCursor(out boardPosition))
    //     {
    //         Circle selection = new Circle(boardPosition, Radius);
    //         HashSet<Vertex> cellsInRange = selection.Cells;

    //         selectionMarkers.AddRange(
    //             SimulationRepresentation.HighlightBoardVertices(
    //                 cellsInRange,
    //                 HighlightSize.VerySmall,
    //                 Color.green
    //             )
    //         );

    //         if (input.LeftClick())
    //         {
    //             simulation.Board.RaiseCells(cellsInRange, HeightChange);
    //             // Simulation.Instance.CurrentStateModified();
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
    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
