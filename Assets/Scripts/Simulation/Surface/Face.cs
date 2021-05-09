using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face
{
    private ITriangle triangle;

    public Surface Surface { get; private set; }

    public IPoint GetVertex(TriVertexNames n)
    {
        switch (n)
        {
            case TriVertexNames.A:
                return triangle.a;
            case TriVertexNames.B:
                return triangle.b;
            case TriVertexNames.C:
                return triangle.c;
            default:
                throw new Exception("Coordinate name does not exist");
        }
    }

    public HashSet<TriVertexNames> GetSharedVertices(Face otherFace)
    {
        HashSet<TriVertexNames> sharedVertices = new HashSet<TriVertexNames>();

        foreach (TriVertexNames v1 in Enum.GetValues(typeof(TriVertexNames)))
        {
            foreach (TriVertexNames v2 in Enum.GetValues(typeof(TriVertexNames)))
            {
                if (this.GetVertex(v1).Coordinates == otherFace.GetVertex(v2).Coordinates) sharedVertices.Add(v1);
            }
        }

        return sharedVertices;
    }

    public HashSet<Face> GetFacesFromSharedVertices(HashSet<TriVertexNames> sharedVertices)
    {
        HashSet<Face> facesSharingVertices = new HashSet<Face>();

        foreach (Face candidate in this.Surface.Faces)
        {
            if (this.GetSharedVertices(candidate).SetEquals(sharedVertices)) facesSharingVertices.Add(candidate);
        }

        return facesSharingVertices;
    }

    public Face(Surface s, ITriangle t)
    {
        this.triangle = t; // TODO: assign points so that they are alway CCW

        this.Surface = s;
        this.Surface.AddFace(this);
    }

    public Face(Surface s, IPoint a, IPoint b, IPoint c) : this(s, new CartesianTriangle(a, b, c)) { }

    public ITriangle Triangle { get => triangle; }

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
