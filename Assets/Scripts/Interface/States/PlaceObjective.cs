using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjective : InterfaceState
{
    // private Simulation simulation;
    // private SimulationRepresentation SimulationRepresentation;
    // private Input input;

    // GameObject objectivePlaceholder = null;

    public PlaceObjective()
    {
    //     simulation = GameObject.Find("Game").GetComponent<Simulation>();
    //     input = GameObject.Find("Game").GetComponent<Input>();
    //     SimulationRepresentation = GameObject.Find("Game").GetComponent<Representation>();
    }

    // public override void Mount() { }

    // public override void Update()
    // {
    //     ClearPlaceholder();
    //     BoardPosition clickedBoardPosition;
    //     if (Input.Instance.TryGetBoardPositionUnderCursor(out clickedBoardPosition))
    //     {
    //         if (Input.Instance.LeftClick())
    //         {
    //             Simulation.Instance.AddToCurrent(new ObjectiveState(clickedBoardPosition));
    //         }
    //         else
    //         {
    //             objectivePlaceholder = SimulationRepresentation.Instance.HighlightBoardPosition(
    //                         clickedBoardPosition,
    //                         HighlightSize.Small,
    //                         Color.green
    //                     );
    //         }
    //     }
    // }
    
    // private void ClearPlaceholder()
    // {
    //     if (objectivePlaceholder != null)
    //     {
    //         GameObject.Destroy(objectivePlaceholder);
    //     }
    // }

    // public override void Unmount()
    // {
    //     ClearPlaceholder();
    // }
}
