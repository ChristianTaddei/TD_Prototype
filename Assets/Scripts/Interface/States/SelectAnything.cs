using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnything : InterfaceState
{
    // private Simulation simulation;
    // private SimulationRepresentation SimulationRepresentation;
    // private Input input;

    // private List<GameObject> selectionMarkers;

    public SelectAnything()
    {
        // simulation = GameObject.Find("Game").GetComponent<Simulation>();
        // SimulationRepresentation = GameObject.Find("Game").GetComponent<Representation>();
        // input = GameObject.Find("Game").GetComponent<Input>();
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
//         if (input.LeftClick())
//         {
//             ClearSelectionMarkers();
//             BoardPosition boardPosition;
//             IRepresentable token;
//             if (input.TryGetBoardPositionUnderCursor(out boardPosition))
//             {
//                 selectionMarkers.Add(
//                                 SimulationRepresentation.HighlightBoardPosition(
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
//             else if (input.TryGetTokenUnderCursor(out token))
//             {
//                 if (token is Tower)
//                 {/*
//                     Tower selectedTower = token as Tower;
//                     selectionMarkers.AddRange(
//                         SimulationRepresentation.HighlightBoardVertices(
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
//                     foreach (AttackPlan attackPlan in simulation.AttackPlans)
//                     {
//                         if (attackPlan.Objective == token as Objective)
//                         {
//                             foreach (Path pathToObjective in attackPlan.PathsToObjective)
//                             {
//                                 selectionMarkers.AddRange(
//                                     SimulationRepresentation.HighlightBoardVertices(
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
//                                                                 SimulationRepresentation.HighlightBoardVertex(
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
