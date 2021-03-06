﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathToBorder : InterfaceState
{
    private RepresentationManager representationManager;

    private Vertex start;
    private bool pathChanged = false;

    //private List<Token> pathMarkers = new List<Token>();

    public FindPathToBorder(RepresentationManager representationManager)
    {
        this.representationManager = representationManager;
    }

    public void Mount()
    {
        start = null;
    }

    public void Unmount()
    {
        clearPath();
    }

    public void OnCellSelected(Vertex selectedCell)
    {
        start = selectedCell;
        pathChanged = true;
    }

    public void Update()
    {
/*
        if (start != null && pathChanged)
        {
            clearPath();
            Path shortestPath = board.ComputeShortestPath(start.BoardPosition, board.Border);

            foreach (BoardPosition pathPosition in shortestPath.getBoardPositions())
            {

                BoardVertex pathCell;
                if (board.Cells.TryGetValue(pathPosition, out pathCell))
                {
                    Unit go = new Unit(pathCell);
                    pathMarkers.Add(go);

                    representationManager.MakeRepresentation(go);
                }

            }

            pathChanged = false;
        }
*/
    }

    private void clearPath()
    {
        //foreach (Token pathMarker in pathMarkers)
       //{
            //GameObject.Destroy(pathMarker.Representation);
        //}

        //pathMarkers.Clear();
    }
}
