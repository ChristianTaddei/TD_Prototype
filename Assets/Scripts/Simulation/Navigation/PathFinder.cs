using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;


public class PathFinder
{

    private Surface board;

    public PathFinder(Surface board)
    {
        this.board = board;
    }

    public Path FindShortestPath(BoardState boardState, Vertex source, List<Vertex> destinations, List<Vertex> excludedVertices = default)
    {
        System.DateTime startTime = System.DateTime.Now;

        Dictionary<Vertex, float> distance = new Dictionary<Vertex, float>();
        Dictionary<Vertex, Vertex> previous = new Dictionary<Vertex, Vertex>();
        HashSet<Vertex> unvisitedPositions = new HashSet<Vertex>();

        IEnumerable<Vertex> availableVertices = boardState.VertexStates.Keys;
        if (excludedVertices != default)
        {
            // excludedVertices.ForEach(vertex => availableVertices.Remove(vertex));
            excludedVertices.ForEach(vertex => destinations.Remove(vertex));
        }

        foreach (Vertex vertex in availableVertices)
        {
            distance.Add(vertex, float.MaxValue);
            previous.Add(vertex, null);
            unvisitedPositions.Add(vertex);
        }

        distance[source] = 0;

        while (unvisitedPositions.Count != 0)
        {
            Vertex closestVertex = null;
            float minimumDistance = float.MaxValue;
            foreach (Vertex candidateVertex in unvisitedPositions)
            {
                if (closestVertex != null)
                {
                    minimumDistance = distance[closestVertex];
                }

                if (distance[candidateVertex] <= minimumDistance)
                {
                    closestVertex = candidateVertex;
                }
            }

            if (closestVertex == null)
            {
                Debug.Log("stop");
            }

            if (destinations.Contains(closestVertex))
            {
                List<Vertex> traversedVertexs = new List<Vertex>();
                Vertex currentVertex = closestVertex;
                traversedVertexs.Add(closestVertex);

                while (previous[currentVertex] != null && currentVertex != source)
                {
                    traversedVertexs.Add(previous[currentVertex]);
                    currentVertex = previous[currentVertex];
                }

                System.TimeSpan elapsed = System.DateTime.Now.Subtract(startTime);
                // Debug.Log("search took: " + elapsed.TotalMilliseconds + "ms");
                return new Path(traversedVertexs, 0);
            }

            unvisitedPositions.Remove(closestVertex);

            foreach (Vertex neighbour in closestVertex.Neighbours)
            {
                //if (excludedVertices == default || !excludedVertices.Contains(neighbour))
                {
                    if (unvisitedPositions.Contains(neighbour))
                    {
                        float segmentDistance;
                        if (boardState.Graph.TryGetThreatwiseTraverseCost(closestVertex, neighbour, out segmentDistance))
                        {
                            // if(excludedVertices != default && excludedVertices.Contains(neighbour))
                            //     segmentDistance *= 3.0f;
                            if (excludedVertices != default && excludedVertices.Contains(neighbour))
                            {
                                segmentDistance *= excludedVertices.Where(ev => ev == neighbour).Count();
                            }

                            float tentativeDistance = distance[closestVertex] + segmentDistance;
                            if (tentativeDistance < distance[neighbour])
                            {
                                distance[neighbour] = tentativeDistance;
                                previous[neighbour] = closestVertex;
                            }

                        }
                    }
                }
            }
        }

        return new Path();
    }

    // public Dictionary<Vertex, float> FindAllDistances(Vertex source)
    // {
    //     System.DateTime startTime = System.DateTime.Now;

    //     Dictionary<Vertex, float> distance = new Dictionary<Vertex, float>();
    //     Dictionary<Vertex, Vertex> previous = new Dictionary<Vertex, Vertex>();
    //     HashSet<Vertex> unvisitedPositions = new HashSet<Vertex>();

    //     foreach (Vertex vertex in board.Graph.Vertices)
    //     {
    //         distance.Add(vertex, float.MaxValue);
    //         previous.Add(vertex, null);
    //         unvisitedPositions.Add(vertex);
    //     }

    //     distance[source] = 0;

    //     while (unvisitedPositions.Count != 0)
    //     {
    //         Vertex closestVertex = null;
    //         float minimumDistance = float.MaxValue;
    //         foreach (Vertex candidateVertex in unvisitedPositions)
    //         {
    //             if (closestVertex != null)
    //             {
    //                 minimumDistance = distance[closestVertex];
    //             }

    //             if (distance[candidateVertex] < minimumDistance)
    //             {
    //                 closestVertex = candidateVertex;
    //             }
    //         }

    //         unvisitedPositions.Remove(closestVertex);

    //         if (closestVertex == null) // Some Cells are disconnected from source
    //         {

    //             break;
    //         }

    //         foreach (Vertex neighbour in closestVertex.Neighbours)
    //         {
    //             if (unvisitedPositions.Contains(neighbour))
    //             {
    //                 float segmentDistance;
    //                 board.Graph.TryGetThreatwiseTraverseCost(closestVertex, neighbour, out segmentDistance);
    //                 float tentativeDistance = distance[closestVertex] + segmentDistance;
    //                 if (tentativeDistance < distance[neighbour])
    //                 {
    //                     distance[neighbour] = tentativeDistance;
    //                     previous[neighbour] = closestVertex;
    //                 }
    //             }
    //         }
    //     }

    //     foreach (Vertex disconnectedPosition in unvisitedPositions)
    //     {
    //         distance.Remove(disconnectedPosition);
    //     }

    //     return distance;
    // }
}