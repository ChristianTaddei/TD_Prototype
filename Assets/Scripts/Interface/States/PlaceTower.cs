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
        ClearPlaceholder();
        SurfacePoint clickedBoardPosition;
        if (InputManager.Instance.TryGetBoardPositionUnderCursor(out clickedBoardPosition))
        {
            if (InputManager.Instance.LeftClick())
            {
                SimulationManager.Instance.AddToCurrent(new TowerState(
                     SimulationManager.Instance.Board.InitalState,
                     clickedBoardPosition,
                     Vector3.forward,
                     Vector3.zero,
                     false));
            }
            else
            {
                towerPlaceholder = InterfaceManager.Instance.MakeHighlight(
                            clickedBoardPosition,
                            HighlightSize.Small,
                            Color.green
                        );
            }
        }
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
