using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrain : InterfaceState
{
    private SimulationManager simulationManager;
    private RepresentationManager representationManager;
    private InputManager inputManager;

    private List<GameObject> selectionMarkers;

    public float Radius { get; set; }
    public float HeightChange { get; set; }

    public ModifyTerrain()
    {
        simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();
        representationManager = GameObject.Find("GameManager").GetComponent<RepresentationManager>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
    }

    public override void Update()
    {
        ClearSelectionMarkers();
        SurfacePoint boardPosition;
        if (inputManager.TryGetBoardPositionUnderCursor(out boardPosition))
        {
            Circle selection = new Circle(SimulationManager.Instance.CurrentState.BoardState, boardPosition, Radius);
            HashSet<Vertex> cellsInRange = selection.Cells;

            selectionMarkers.AddRange(
                InterfaceManager.Instance.MakeHighlights(
                    cellsInRange,
                    HighlightSize.VerySmall,
                    Color.green
                )
            );

            if (inputManager.LeftClick())
            {
                SimulationManager.Instance.ChangeCellsHeight(cellsInRange, HeightChange);
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
