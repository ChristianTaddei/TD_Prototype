using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePathState : InterfaceState
{

    private RepresentationManager representationManager;
    // private SimulationManager simulationManager;
    private InputManager inputManager;

    private SurfacePoint start, destination;
    private bool pathChanged = false;

    private List<GameObject> selectionMarkers;

    public MakePathState()
    {
        representationManager = GameObject.Find("GameManager").GetComponent<RepresentationManager>();
        // simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();
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
            Maybe<SurfacePoint> sp = inputManager.GetSurfacePointUnderCursor();
            if (sp.HasValue())
            {
                if (start == null)
                {
                    start = sp.Value;
                    List<SurfacePoint> tmp = new List<SurfacePoint>();
                    tmp.Add(start);
                    selectionMarkers.AddRange(
                            representationManager.HighlightSurfacePoints(
                                tmp,
                                HighlightSize.Small,
                                Color.green
                            )
                        );
                }
                else if (destination == null)
                {
                    destination = sp.Value;
                    List<SurfacePoint> tmp = new List<SurfacePoint>();
                    tmp.Add(destination);
                    selectionMarkers.AddRange(
                            representationManager.HighlightSurfacePoints(
                                tmp,
                                HighlightSize.Small,
                                Color.red
                            )
                        );

                    Maybe<SurfacePath> path = sp.Value.Face.Surface.MakeDirectPath(start, destination);

                    if (path.HasValue())
                    {
                        selectionMarkers.AddRange(
                                    representationManager.HighlightSurfacePoints(
                                        path.Value.Points,
                                        HighlightSize.VerySmall,
                                        Color.green
                                    )
                                );
                    }
                    else
                    {
                        Debug.Log("failed to make path");
                    }
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