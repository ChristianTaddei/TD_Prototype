using System.Collections.Generic;
using UnityEngine;

public class SurfaceVertex : CartesianVector
{
    private List<Face> belongingFaces;


    public SurfaceVertex(IVector v) : base(new Vector3(v.Position.x, v.Position.y, v.Position.z))
    {

    }
}