using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnything : InterfaceState
{
    // private SimulationManager simulationManager;
    // private RepresentationManager representationManager;
    // private InputManager inputManager;

    // private List<GameObject> selectionMarkers;

    public SelectAnything()
    {
        // simulationManager = GameObject.Find("GameManager").GetComponent<SimulationManager>();
        // representationManager = GameObject.Find("GameManager").GetComponent<RepresentationManager>();
        // inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
    }

//     public override void Mount()
//     {
//         selectionMarkers = new List<GameObject>();
//     }

//     public override void Unmount()
//     {
//         ClearSelectionMarkers();
//     }

//     public override void Update()
//     {
//         if (inputManager.LeftClick())
//         {
//             ClearSelectionMarkers();
//             BoardPosition boardPosition;
//             IRepresentable token;
//             if (inputManager.TryGetBoardPositionUnderCursor(out boardPosition))
//             {
//                 selectionMarkers.Add(
//                                 representationManager.HighlightBoardPosition(
//                                     boardPosition,
//                                     HighlightSize.VerySmall,
//                                     Color.green
//                                 )
//                             );
//                 /*                
//                                 Debug.Log("Board Position (barycentrics)"
//                                             + boardPosition.Barycentrics.a + " "
//                                             + boardPosition.Barycentrics.b + " "
//                                             + boardPosition.Barycentrics.c);
//                 */
//                 /*
//                                 float threath;
//                                 if (board.Threaths.TryGetValue(hoveredBoardVertex.BoardPosition, out threath))
//                                 {
//                                     Debug.Log("threath: " + threath);
//                                 }
//                 */
//             }
//             else if (inputManager.TryGetTokenUnderCursor(out token))
//             {
//                 if (token is Tower)
//                 {/*
//                     Tower selectedTower = token as Tower;
//                     selectionMarkers.AddRange(
//                         representationManager.HighlightBoardVertices(
//                             selectedTower.CellsInRange,
//                             HighlightSize.VerySmall,
//                             Color.red
//                         )
//                     );
// */
//                 }
 
//                 if (token is Objective)
//                 {
//                     /*
//                     foreach (AttackPlan attackPlan in simulationManager.AttackPlans)
//                     {
//                         if (attackPlan.Objective == token as Objective)
//                         {
//                             foreach (Path pathToObjective in attackPlan.PathsToObjective)
//                             {
//                                 selectionMarkers.AddRange(
//                                     representationManager.HighlightBoardVertices(
//                                         pathToObjective.TraversedVertexs,
//                                         HighlightSize.Small,
//                                         Color.red
//                                     )
//                                 );
//                             }
                           
//                                                         float initialDistance = attackPlan.AllDistances[attackPlan.PathToObjective.GetStart()];
//                                                         foreach (Vertex vertex in attackPlan.AllDistances.Keys)
//                                                         {
//                                                             float currentDistance = attackPlan.AllDistances[vertex];
//                                                             Color color;
//                                                             if (currentDistance > initialDistance)
//                                                             {
//                                                                 color = Color.red;
//                                                             }
//                                                             else 
//                                                             { 
//                                                                 float frac = currentDistance/initialDistance;
//                                                                 color = new Color (frac, 1.0f - frac, 0.0f);
//                                                             }

//                                                             selectionMarkers.Add(
//                                                                 representationManager.HighlightBoardVertex(
//                                                                     vertex,
//                                                                     HighlightSize.VerySmall,
//                                                                     color
//                                                                 )
//                                                             );
//                                                         }
                           
//                         }
//                     } */
//                 }
//             }
//         }

//     }

//     private void ClearSelectionMarkers()
//     {
//         foreach (GameObject selectionMarker in selectionMarkers)
//         {
//             GameObject.Destroy(selectionMarker);
//         }

//         selectionMarkers.Clear();
//     }
}
