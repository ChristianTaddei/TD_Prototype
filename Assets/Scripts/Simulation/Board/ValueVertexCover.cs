using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueVertexCover<T>
{
    /*
    private int boardSize;
    private  values = new Dictionary<BoardPosition, T>();

    public ValueVertexCover(int boardSize)
    {
        this.boardSize = boardSize;
    }

    public Boolean TryGetValue(BoardPosition boardPosition, out T value)
    {
        if(values.ContainsKey(boardPosition)){

        }
        return values[boardPosition.Row, boardPosition.Col];
    }

    public void SetValue(BoardPosition boardPosition, T value)
    {
        values[boardPosition.Row, boardPosition.Col] = value;
    }

    public List<T> AsList()
    {
        List<T> valuesAsList = new List<T>();
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                valuesAsList.Add(values[i, j]);
            }
        }
        return valuesAsList;
    }

    internal bool HasValue(BoardPosition boardPosition)
    {
        return false;
    }
    */
}
