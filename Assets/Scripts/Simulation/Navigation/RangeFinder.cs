using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFinder
{
    private Surface board;
    public RangeFinder(Surface board)
    {
        this.board = board;
    }
/*
    public HashSet<Vertex> FindRangeByCellDistance(Vertex centre, int cellDistance)
    {
        int remainingDistance = cellDistance;
        HashSet<Vertex> allCells = new HashSet<Vertex>();
        HashSet<Vertex> lastRingCells = new HashSet<Vertex>();

        allCells.Add(centre);
        lastRingCells.Add(centre);
        while (remainingDistance > 0)
        {
            HashSet<Vertex> newCells = new HashSet<Vertex>();
            foreach (Vertex lastRingCell in lastRingCells)
            {
                foreach (BoardPosition neighbourBP in lastRingCell.BoardPosition.GetNonDiagonalNeighbours())
                {
                    Vertex neighbourCell;
                    if (board.Cells.TryGetValue(neighbourBP, out neighbourCell))
                    {
                        if (!allCells.Contains(neighbourCell))
                        {
                            newCells.Add(neighbourCell);
                        }
                    }
                }
                allCells.UnionWith(newCells);
            }
            lastRingCells = newCells;
            remainingDistance--;
        }

        return allCells;
    }

    public HashSet<Vertex> FindRangeByXZDistance(Vertex centre, float distance)
    {
        HashSet<Vertex> cellsInRange = new HashSet<Vertex>();
        foreach (Vertex cell in centre.Board.Cells.Values)
        {
            Vector2 centreXYDistance = new Vector2(centre.Position.x, centre.Position.z);
            Vector2 cellXYDistance = new Vector2(cell.Position.x, cell.Position.z);
            if (Vector2.Distance(centreXYDistance, cellXYDistance) < distance)
            {
                cellsInRange.Add(cell);
            }
        }
        return cellsInRange;
    }

    public HashSet<Vertex> FindRangeByDistance(Vertex centre, float distance)
    {
        HashSet<Vertex> cellsInRange = new HashSet<Vertex>();
        foreach (Vertex cell in centre.Board.Cells.Values)
        {
            if (Vector3.Distance(centre.Position, cell.Position) < distance)
            {
                cellsInRange.Add(cell);
            }
        }
        return cellsInRange;
    }
*/
}