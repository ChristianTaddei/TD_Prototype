using System;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    private HashSet<Vertex> neighbours = new HashSet<Vertex>();
    private HashSet<Face> faces = new HashSet<Face>();

    public HashSet<Vertex> Neighbours { get { return new HashSet<Vertex>(neighbours); } }
    public HashSet<Face> Faces { get { return new HashSet<Face>(faces); } }
    
    public Vertex()
    {

    }

    public void AddNeighbour(Vertex otherVertex)
    {
        if (!otherVertex.Equals(this))
        {
            neighbours.Add(otherVertex);
        }
    }

    public void AddFace(Face face){
        faces.Add(face);
    }
}
