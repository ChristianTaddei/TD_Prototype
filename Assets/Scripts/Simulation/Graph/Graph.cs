using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public BoardState boardState;

    // vertex additional values
    private Dictionary<Vertex, float> sampledThreaths;

    // edges weights
    private AdjacencyMatrix<float> traverseCost;
    private AdjacencyMatrix<float> threathwiseTraverseCost;

    public Graph(BoardState boardState)
    {
        this.boardState = boardState;   
    }

    public Graph(Graph other)
    {
        this.boardState = other.boardState;

        foreach (KeyValuePair<Vertex, float> entry in other.sampledThreaths)
        {
            this.sampledThreaths.Add(entry.Key, entry.Value);
        }

        throw new NotImplementedException();
    }

    public bool TryGetTraverseCost(Vertex start, Vertex destination, out float distance)
    {
        return traverseCost.TryGetValue(start, destination, out distance);
    }

    internal bool TryGetThreatwiseTraverseCost(Vertex start, Vertex destination, out float segmentDistance)
    {
        return threathwiseTraverseCost.TryGetValue(start, destination, out segmentDistance);
    }

    public void updateDistances()
    {
        Func<Vertex, Vertex, float> computeMovementCost =
        (a, b) =>
        {
            Vector3 ab = boardState.VertexStates[b].Position - boardState.VertexStates[a].Position;
            float distance = ab.magnitude;
            Vector3 xzProjection = new Vector3(ab.x, 0, ab.z);
            Vector3 yProjection = new Vector3(0, ab.y, 0);
            float tan = yProjection.magnitude / xzProjection.magnitude;

            if (ab.y > 0)
            {
                return (1 + (tan / 0.05f)) * distance;
            }
            else
            {
                return xzProjection.magnitude;
            }
        };

        traverseCost = new AdjacencyMatrix<float>(boardState.VertexStates.Keys, computeMovementCost);
    }

    public void UpdateThreats(Dictionary<Tower, TowerState> towerStates)
    {
        sampledThreaths = new Dictionary<Vertex, float>();
        foreach (Vertex vertex in boardState.VertexStates.Keys)
        {
            sampledThreaths.Add(vertex, 0);
        }

        foreach (TowerState tower in towerStates.Values)
        {
            foreach (Vertex threatenedCell in new Circle(boardState, tower.BoardPosition, Tower.Range).Cells)
            {
                float oldThreath;
                sampledThreaths.TryGetValue(threatenedCell, out oldThreath);
                sampledThreaths[threatenedCell] = oldThreath + 1;
            }
        }
    }

    public void updateThreatwiseDistances()
    {
        Func<Vertex, Vertex, float> computeThreatwiseCost = (v1, v2) =>
        {
            Vector3.Distance(boardState.VertexStates[v1].Position, boardState.VertexStates[v2].Position);
            float threath = 0, otherThreath = 0, distance = 0;
            sampledThreaths.TryGetValue(v1, out threath);
            sampledThreaths.TryGetValue(v2, out otherThreath);

            traverseCost.TryGetValue(v1, v2, out distance);

            float averageThreath = (threath + otherThreath) / 2.0f;
            if (averageThreath == 0) averageThreath = 0.05f;

            float threathWiseDistance = averageThreath * distance;
            return threathWiseDistance;
        };

        threathwiseTraverseCost = new AdjacencyMatrix<float>(boardState.VertexStates.Keys, computeThreatwiseCost);
    }
}