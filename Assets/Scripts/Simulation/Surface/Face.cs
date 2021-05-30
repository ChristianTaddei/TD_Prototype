using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face : Triangle
{
    private SurfaceVertex svA;
    private SurfaceVertex svB;
    private SurfaceVertex svC;

    public override IVector A => svA;
    public override IVector B => svB;
    public override IVector C => svC;

    public Surface Surface { get; private set; }

    public Face(Surface s, Triangle t)
    {
        foreach (TriangleVertexIdentifiers vId in Triangle.Vertices)
        {
            bool alreadyInVertices = false;
            foreach (SurfaceVertex sv in s.vertices)
            {
                if (t.GetVertex(vId).Equals(sv))
                {
                    SetVertex(vId, sv);
                    alreadyInVertices = true;
                    break;
                }
            }

            if (!alreadyInVertices)
            {
                SurfaceVertex nsv = new SurfaceVertex(t.GetVertex(vId));
                SetVertex(vId, nsv);
                s.vertices.Add(nsv);
            }
        }

        this.Surface = s;
        this.Surface.AddFace(this);
    }

    public void SetVertex(TriangleVertexIdentifiers v, SurfaceVertex sv)
    {
        switch (v)
        {
            case TriangleVertexIdentifiers.A:
                svA = sv;
                break;
            case TriangleVertexIdentifiers.B:
                svB = sv;
                break;
            case TriangleVertexIdentifiers.C:
                svC = sv;
                break;
            default:
                throw new Exception("Coordinate name does not exist");
        }
    }

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
            foreach (TriangleVertexIdentifiers v in this.GetSharedVertices(candidate))
            {
                if (sharedVertices.Contains(v)) facesSharingVertices.Add(candidate);
            }
        }

        return facesSharingVertices;
    }

}
