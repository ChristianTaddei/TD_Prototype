using System.Collections.Generic;
using UnityEngine;

public class SurfaceVertex : CartesianVector
{
    private List<Face> belongingFaces;

    public SurfaceVertex(Vector v) : base(new Vector3(v.FloatRepresentation.x, v.FloatRepresentation.y, v.FloatRepresentation.z))
    {

    }
}