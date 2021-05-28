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
        // if (InputManager.Instance.TryGetBoardPositionUnderCursor(out clickedBoardPosition))
        // {
        //     if (InputManager.Instance.LeftClick())
        //     {
        //         SimulationManager.Instance.AddToCurrent(new TowerState(
        //              clickedBoardPosition,
        //              Vector3.forward,
        //              Vector3.zero,
        //              false));
        //     }
        //     else
        //     {
        //         towerPlaceholder = RepresentationManager.Instance.HighlightBoardPosition(
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
