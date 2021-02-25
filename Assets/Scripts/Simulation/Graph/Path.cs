using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    // FIELDS
    private List<Vertex> traversedVertexs = new List<Vertex>();
    private float length;

    public List<Vertex> TraversedVertexs { get => traversedVertexs; set => traversedVertexs = value; }

    // CONSTRUCTORS
    public Path() { }

    public Path(List<Vertex> traversedCells, float length)
    {
        this.TraversedVertexs = traversedCells;
        this.length = length;
    }


    // GET N SET
    public Vertex GetStart()
    {
        if (TraversedVertexs.Count != 0)
        {
            return TraversedVertexs[0];
        }
        else
        {
            return null;
        }
    }

    public Vertex GetDestination()
    {
        if (TraversedVertexs.Count != 0)
        {
            return TraversedVertexs[TraversedVertexs.Count - 1];
        }
        else
        {
            return null;
        }
    }

    public List<Vertex> getVertices()
    {
        return TraversedVertexs;
    }

    public float getLength()
    {
        return length;
    }

    public void setLength(float length)
    {
        this.length = length;
    }
}
