using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face : Triangle
{
    public Surface Surface { get; private set; }

    public HashSet<TriangleVertexIdentifiers> GetSharedVertices(Face otherFace)
    {
        HashSet<TriangleVertexIdentifiers> sharedVertices = new HashSet<TriangleVertexIdentifiers>();

        foreach (TriangleVertexIdentifiers v1 in Vertices)
        {
            foreach (TriangleVertexIdentifiers v2 in Vertices)
            {
                if (this.GetVertex(v1).Position == otherFace.GetVertex(v2).Position) sharedVertices.Add(v1);
            }
        }

        return sharedVertices;
    }

    public HashSet<Face> GetFacesFromSharedVertices(HashSet<TriangleVertexIdentifiers> sharedVertices)
    {
        HashSet<Face> facesSharingVertices = new HashSet<Face>();

        foreach (Face candidate in this.Surface.Faces)
        {
            if (this.GetSharedVertices(candidate).SetEquals(sharedVertices)) facesSharingVertices.Add(candidate);
        }

        return facesSharingVertices;
    }

    public Face(Surface s, Triangle t) : base(t)
    {
        this.Surface = s;
        this.Surface.AddFace(this);
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
