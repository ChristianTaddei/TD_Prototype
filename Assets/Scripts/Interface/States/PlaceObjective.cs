using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjective : InterfaceState
{
    private SimulationManager simulationManager;
    private RepresentationManager representationManager;
    private InputManager inputManager;

    GameObject objectivePlaceholder = null;

    public PlaceObjective()
    {
        simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
        representationManager = GameObject.Find("GameManager").GetComponent<RepresentationManager>();
    }

    public override void Mount() { }

    public override void Update()
    {
        ClearPlaceholder();
        SurfacePoint hoveredBoardPosition;
        if (InputManager.Instance.TryGetBoardPositionUnderCursor(out hoveredBoardPosition))
        {
            if (InputManager.Instance.LeftClick())
            {
                SimulationManager.Instance.AddToCurrent(new ObjectiveState(
                    SimulationManager.Instance.CurrentState.BoardState,
                    hoveredBoardPosition)
                );
            }
            else
            {
                objectivePlaceholder = InterfaceManager.Instance.MakeHighlight(
                            hoveredBoardPosition,
                            HighlightSize.Small,
                            Color.green
                        );
            }
        }
    }
    
    private void ClearPlaceholder()
    {
        if (objectivePlaceholder != null)
        {
            GameObject.Destroy(objectivePlaceholder);
        }
    }

    public override void Unmount()
    {
        ClearPlaceholder();
    }
}
