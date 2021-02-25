using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterfaceState
{
    public virtual void Mount(){}
    public virtual void Unmount(){}
    public virtual void OnSelection(IRepresentable simulationObject){}
    public virtual void OnBoardVertexSelected(Vertex boardVertex){}
    public virtual void Update(){}
}
