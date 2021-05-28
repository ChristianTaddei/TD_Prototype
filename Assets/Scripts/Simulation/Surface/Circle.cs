using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    public SurfacePoint Centre { get; set; }
    public float Radius { get; set; }

    public Circle(SurfacePoint centre, float radius)
    {
        this.Centre = centre;
        this.Radius = radius;
        
        HashSet<SurfacePoint> coveredCells = new HashSet<SurfacePoint>();

        // recursiveExplore(boardState, Radius, Centre.Face.a, coveredCells);
        // recursiveExplore(boardState, Radius, Centre.Face.b, coveredCells);
        // recursiveExplore(boardState, Radius, Centre.Face.c, coveredCells);

        Cells = coveredCells;
    }

    // public void recursiveExplore(
    //     BoardState boardState,
    //     float maxDistance,
    //     Vertex vertex,
    //     HashSet<Vertex> coveredCells)
    // {
    //     float distance = Vector2.Distance(
    //                             new Vector2(
    //                                 boardState.VertexStates[vertex].Position.x,
    //                                 boardState.VertexStates[vertex].Position.z
    //                                 ),
    //                             new Vector2(
    //                                 Centre.GetCartesians(boardState).x,
    //                                 Centre.GetCartesians(boardState).z
    //                                 )
    //                             );

    //     if (!coveredCells.Contains(vertex)
    //             && distance < Radius)
    //     {
    //         coveredCells.Add(vertex);
    //         foreach (Vertex neighbour in vertex.Neighbours)
    //         {
    //             recursiveExplore(boardState, maxDistance, neighbour, coveredCells);
    //         }
    //     }
    // }
}
