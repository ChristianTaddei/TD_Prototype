using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face // TODO: Face is a triangle
{
    private ITriangle triangle;

    public Surface Surface { get; private set; }

    public IPoint GetVertex(TriangleVertices n)
    {
        switch (n)
        {
            case TriangleVertices.A:
                return triangle.a;
            case TriangleVertices.B:
                return triangle.b;
            case TriangleVertices.C:
                return triangle.c;
            default:
                throw new Exception("Coordinate name does not exist");
        }
    }

    public HashSet<TriangleVertices> GetSharedVertices(Face otherFace)
    {
        HashSet<TriangleVertices> sharedVertices = new HashSet<TriangleVertices>();

        foreach (TriangleVertices v1 in Enum.GetValues(typeof(TriangleVertices)))
        {
            foreach (TriangleVertices v2 in Enum.GetValues(typeof(TriangleVertices)))
            {
                if (this.GetVertex(v1).Coordinates == otherFace.GetVertex(v2).Coordinates) sharedVertices.Add(v1);
            }
        }

        return sharedVertices;
    }

    public HashSet<Face> GetFacesFromSharedVertices(HashSet<TriangleVertices> sharedVertices)
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
        this.triangle = t; // TODO: CCW as requirement or enforced?

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
