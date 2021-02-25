using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : InterfaceState
{

    private RepresentationManager representationManager;
    private SimulationManager simulationManager;
    private InputManager inputManager;

    private Vertex start, destination;
    private bool pathChanged = false;

    private List<GameObject> selectionMarkers;

    public FindPath()
    {
        representationManager = GameObject.Find("GameManager").GetComponent<RepresentationManager>();
        simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
    }

    public override void Mount()
    {
        selectionMarkers = new List<GameObject>();
        start = null;
        destination = null;
    }

    public override void Unmount()
    {
        ClearSelectionMarkers();
    }

    public override void Update()
    {
        if (inputManager.LeftClick())
        {
            SurfacePoint boardPosition;
            if (inputManager.TryGetBoardPositionUnderCursor(out boardPosition))
            {
                if (start == null)
                {
                    start = boardPosition.Face.a;
                    List<Vertex> tmp = new List<Vertex>();
                    tmp.Add(start);
                    // selectionMarkers.Add(
                    InterfaceManager.Instance.MakeHighlights(
                        tmp,
                        HighlightSize.Small,
                        Color.green
                // )
                );
                }
                else if (destination == null)
                {
                    destination = boardPosition.Face.a;
                    List<Vertex> tmp = new List<Vertex>();
                    tmp.Add(destination);
                    // selectionMarkers.Add(
                    InterfaceManager.Instance.MakeHighlights(
                        tmp,
                        HighlightSize.Small,
                        Color.red
                // )
                );

                    Path shortestPath = simulationManager.pathFinder.FindShortestPath(
                        SimulationManager.Instance.CurrentState.BoardState, start, tmp);

                    // selectionMarkers.AddRange(
                    //             InterfaceManager.Instance.MakeHighlights(
                    //                 shortestPath.getVertices(),
                    //                 HighlightSize.VerySmall,
                    //                 Color.green
                    //             )
                    //         );

                    InterfaceManager.Instance.MakeHighlights(
                                    shortestPath.getVertices(),
                                    HighlightSize.VerySmall,
                                    Color.green
                            );
                }
                else
                {
                    start = null;
                    destination = null;
                    ClearSelectionMarkers();
                }

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
}