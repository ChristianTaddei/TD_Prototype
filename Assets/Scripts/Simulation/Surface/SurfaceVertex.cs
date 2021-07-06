using System.Collections.Generic;
using UnityEngine;

public class SurfaceVertex
{
    private Vector position;
    private List<Face> belongingFaces;

    public SurfaceVertex(Vector v)
    {
        this.position = v;
    }
}