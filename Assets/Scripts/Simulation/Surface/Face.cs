using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face 
{
    // public static implicit operator CartesianTriangle(Face f) => new CartesianTriangle();
    
    public IPoint a { get; }
    public IPoint b { get; }
    public IPoint c { get; }

    public Face(IPoint a, IPoint b, IPoint c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    // public List<Face> GetNeighbourFaces()
    // {
    //     List<Face> neighbours = new List<Face>();

    //     neighbours.AddRange(a.Faces);
    //     neighbours.AddRange(b.Faces);
    //     neighbours.AddRange(c.Faces);

    //     return neighbours;
    // }

    // public bool TryGetOppositeOfA(out Face outFace)
    // {

    //     HashSet<Face> commonNeighboursBC = b.Faces;
    //     commonNeighboursBC.IntersectWith(c.Faces);

    //     if (commonNeighboursBC.Count > 0)
    //     {
    //         outFace = commonNeighboursBC.First();
    //         return true;
    //     }

    //     outFace = default;
    //     return false;
    // }

    // public bool TryGetOppositeOfB(out Face outFace)
    // {

    //     HashSet<Face> commonNeighboursAC = a.Faces;
    //     commonNeighboursAC.IntersectWith(c.Faces);

    //     if (commonNeighboursAC.Count > 0)
    //     {
    //         outFace = commonNeighboursAC.First();
    //         return true;
    //     }

    //     outFace = default;
    //     return false;
    // }

    // public bool TryGetOppositeOfC(out Face outFace)
    // {

    //     HashSet<Face> commonNeighboursAB = a.Faces;
    //     commonNeighboursAB.IntersectWith(b.Faces);

    //     if (commonNeighboursAB.Count > 0)
    //     {
    //         outFace = commonNeighboursAB.First();
    //         return true;
    //     }

    //     outFace = default;
    //     return false;
    // }
}
