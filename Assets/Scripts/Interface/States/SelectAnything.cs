using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnything : InterfaceState
{
    private SimulationManager simulationManager;
    private RepresentationManager representationManager;
    private InputManager inputManager;

    private List<GameObject> selectionMarkers;

    public SelectAnything()
    {
        simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();
        representationManager = GameObject.Find("GameManager").GetComponent<RepresentationManager>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
    }

    public override void Mount()
    {
        selectionMarkers = new List<GameObject>();
    }

    public override void Unmount()
    {
        ClearSelectionMarkers();
    }

    public override void Update()
    {
        if (inputManager.LeftClick())
        {
            ClearSelectionMarkers();
            SurfacePoint boardPosition;
            IRepresentable token;
            if (inputManager.TryGetBoardPositionUnderCursor(out boardPosition))
            {
                selectionMarkers.Add(
                                InterfaceManager.Instance.MakeHighlight(
                                    boardPosition,
                                    HighlightSize.VerySmall,
                                    Color.green
                                )
                            );
            }
            else if (inputManager.TryGetTokenUnderCursor(out token))
            {
                if (token is Tower)
                {
                    Tower selectedTower = token as Tower;
                    // selectionMarkers.AddRange(
                    //     InterfaceManager.Instance.MakeHighlights(
                    //         new Circle(
                    //             (SimulationManager.Instance.CurrentState.TowerStates[selectedTower] as TowerState).BoardPosition,
                    //             Tower.Range).Cells,
                    //         HighlightSize.VerySmall,
                    //         Color.red
                    //     )
                    // );
                }
                if (token is Enemy)
                {
                    Enemy selectedEnemy = token as Enemy;
                    selectionMarkers.AddRange(
                        InterfaceManager.Instance.MakeHighlights(
                            (SimulationManager.Instance.CurrentState.EnemyStates[selectedEnemy] as EnemyState).PathToObjective.TraversedVertexs,
                            HighlightSize.VerySmall,
                            Color.red
                        )
                    );
                }
                if (token is Objective)
                {
                    foreach (AttackPlan attackPlan in simulationManager.AttackPlans)
                    {
                        if (attackPlan.TargetObjective.Equals(token as Objective))
                        {
                            foreach (Path pathToObjective in attackPlan.PathsToObjective)
                            {
                                selectionMarkers.AddRange(
                                    InterfaceManager.Instance.MakeHighlights(
                                        pathToObjective.TraversedVertexs,
                                        HighlightSize.Small,
                                        Color.red
                                    )
                                );
                            }

                            // foreach (Path pathToObjective in attackPlan.PathsToObjective)
                            // {
                            //     float initialDistance = attackPlan.AllDistances[pathToObjective.GetStart()];
                            //     foreach (Vertex vertex in attackPlan.AllDistances.Keys)
                            //     {
                            //         float currentDistance = attackPlan.AllDistances[vertex];
                            //         Color color;
                            //         if (currentDistance > initialDistance)
                            //         {
                            //             color = Color.red;
                            //         }
                            //         else
                            //         {
                            //             float frac = currentDistance / initialDistance;
                            //             color = new Color(frac, 1.0f - frac, 0.0f);
                            //         }

                            //         selectionMarkers.Add(
                            //             InterfaceManager.Instance.MakeHighlight(
                            //                 vertex,
                            //                 HighlightSize.VerySmall,
                            //                 color
                            //             )
                            //         );
                            //     }
                            // }
                        }
                    }
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
