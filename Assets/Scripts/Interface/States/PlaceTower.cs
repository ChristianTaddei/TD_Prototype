using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : InterfaceState
{
    GameObject towerPlaceholder = null;

    public PlaceTower()
    {

    }

    public override void Update()
    {
        // ClearPlaceholder();
        // BoardPosition clickedBoardPosition;
        // if (Input.Instance.TryGetBoardPositionUnderCursor(out clickedBoardPosition))
        // {
        //     if (Input.Instance.LeftClick())
        //     {
        //         Simulation.Instance.AddToCurrent(new TowerState(
        //              clickedBoardPosition,
        //              Vector3.forward,
        //              Vector3.zero,
        //              false));
        //     }
        //     else
        //     {
        //         towerPlaceholder = SimulationRepresentation.Instance.HighlightBoardPosition(
        //                     clickedBoardPosition,
        //                     HighlightSize.Small,
        //                     Color.green
        //                 );
        //     }
        // }
    }

    private void ClearPlaceholder()
    {
        if (towerPlaceholder != null)
        {
            GameObject.Destroy(towerPlaceholder);
        }
    }

    public override void Unmount()
    {
        ClearPlaceholder();
    }
}
