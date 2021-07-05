using System.Collections.Generic;
using UnityEngine;

public class SurfaceVertex : CartesianVector
{
    private List<ConcreteFace> belongingFaces;

    // FIXME: wont be able to do this when constructors become internal
    public SurfaceVertex(Vector v) : base(new Vector3(v.FloatRepresentation.x, v.FloatRepresentation.y, v.FloatRepresentation.z))
    {

    }
}