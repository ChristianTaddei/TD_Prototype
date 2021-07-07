using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class MakePathState : InterfaceState
// {
//     private InterfaceManager _interface;

//     private SurfacePoint start, destination;
//     private bool pathChanged = false;

//     private BindOverrideCommand bindingOverride;

//     private List<GameObject> selectionMarkers;

//     public MakePathState(InterfaceManager _interface)
//     {
//         this._interface = _interface;
//     }

//     public override void Mount()
//     {
//         selectionMarkers = new List<GameObject>();
//         start = null;
//         destination = null;

//         bindingOverride = new BindOverrideCommand();
//         bindingOverride.Execute();
//     }

//     public override void Unmount()
//     {
//         ClearSelectionMarkers();

//         bindingOverride.Undo();
//     }

//     public override void Update()
//     {
//         // if (input.LeftClick())
//         // {
//         //     Maybe<SurfacePoint> sp = input.GetSurfacePointUnderCursor();
//         //     if (sp.HasValue())
//         //     {
//         //         if (start == null)
//         //         {
//         //             start = sp.Value;
//         //             List<SurfacePoint> tmp = new List<SurfacePoint>();
//         //             tmp.Add(start);
//         //             selectionMarkers.AddRange(
//         //                     RepresentationFactory.HighlightSurfacePoints(
//         //                         tmp,
//         //                         HighlightSize.Small,
//         //                         Color.green
//         //                     )
//         //                 );
//         //         }
//         //         else if (destination == null)
//         //         {
//         //             destination = sp.Value;
//         //             List<SurfacePoint> tmp = new List<SurfacePoint>();
//         //             tmp.Add(destination);
//         //             selectionMarkers.AddRange(
//         //                     RepresentationFactory.HighlightSurfacePoints(
//         //                         tmp,
//         //                         HighlightSize.Small,
//         //                         Color.red
//         //                     )
//         //                 );

//         //             Maybe<SurfacePath> path = sp.Value.Face.Surface.MakeDirectPath(start, destination);

//         //             if (path.HasValue())
//         //             {
//         //                 selectionMarkers.AddRange(
//         //                             RepresentationFactory.HighlightSurfacePoints(
//         //                                 path.Value.Points,
//         //                                 HighlightSize.VerySmall,
//         //                                 Color.green
//         //                             )
//         //                         );
//         //             }
//         //             else
//         //             {
//         //                 Debug.Log("failed to make path");
//         //             }
//         //         }
//         //         else
//         //         {
//         //             start = null;
//         //             destination = null;
//         //             ClearSelectionMarkers();
//         //         }

//         //     }
//         // }
//     }

//     private void ClearSelectionMarkers()
//     {
//         foreach (GameObject selectionMarker in selectionMarkers)
//         {
//             GameObject.Destroy(selectionMarker);
//         }

//         selectionMarkers.Clear();
//     }
// }