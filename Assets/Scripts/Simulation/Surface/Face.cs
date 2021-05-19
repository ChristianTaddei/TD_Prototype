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

    public HashSet<Face> GetFacesFromAtLeastOneSharedVertex(HashSet<TriangleVertexIdentifiers> sharedVertices)
    {
        HashSet<Face> facesSharingVertices = new HashSet<Face>();

        foreach (Face candidate in this.Surface.Faces)
        {
            foreach(TriangleVertexIdentifiers v in this.GetSharedVertices(candidate)){
                if(sharedVertices.Contains(v)) facesSharingVertices.Add(candidate);
            }
        }

        return facesSharingVertices;
    }

    public Face(Surface s, Triangle t) : base(t)
    {
        this.Surface = s;
        this.Surface.AddFace(this);
    }
}
