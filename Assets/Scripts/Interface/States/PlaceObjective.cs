using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjective : InterfaceState
{
    // private SimulationManager simulationManager;
    // private RepresentationManager representationManager;
    // private InputManager inputManager;

    // GameObject objectivePlaceholder = null;

    public PlaceObjective()
    {
    //     simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();
    //     inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
    //     representationManager = GameObject.Find("GameManager").GetComponent<RepresentationManager>();
    }

    // public override void Mount() { }

    // public override void Update()
    // {
    //     ClearPlaceholder();
    //     BoardPosition clickedBoardPosition;
    //     if (InputManager.Instance.TryGetBoardPositionUnderCursor(out clickedBoardPosition))
    //     {
    //         if (InputManager.Instance.LeftClick())
    //         {
    //             SimulationManager.Instance.AddToCurrent(new ObjectiveState(clickedBoardPosition));
    //         }
    //         else
    //         {
    //             objectivePlaceholder = RepresentationManager.Instance.HighlightBoardPosition(
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
