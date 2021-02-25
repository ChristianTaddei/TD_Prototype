using System;
using System.Collections.Generic;

public class BoardState : IState
{
    public Dictionary<Vertex, VertexState> VertexStates;

    public Graph Graph;

    public BoardState(){
        VertexStates = new Dictionary<Vertex, VertexState>();
        Graph = new Graph(this);
    }

    public BoardState(BoardState other)
    {
        foreach (KeyValuePair<Vertex, VertexState> entry in other.VertexStates)
        {
            this.VertexStates.Add(entry.Key, new VertexState(other.VertexStates[entry.Key]));
        }

        Graph = new Graph(other.Graph);
    }

    public bool Ready { get; internal set; }
}
