using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VertexState : IState
{
    public Vector3 Position { get; set; }

    public VertexState(Vector3 position)
    {
        Position = position;
    }

    public VertexState(VertexState other){
            Position = other.Position;
    }
}
