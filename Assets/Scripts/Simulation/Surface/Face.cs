using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face : Triangle
{
    private int svAIndex;
    private int svBIndex;
    private int svCIndex;

    public SurfaceVertex svA => Surface.vertices[svAIndex];
    public SurfaceVertex svB => Surface.vertices[svBIndex];
    public SurfaceVertex svC => Surface.vertices[svCIndex];

    public override Vector A => svA;
    public override Vector B => svB;
    public override Vector C => svC;

    public Surface Surface { get; private set; }

    public Face(Surface s, ConcreteTriangle t)
    {
        foreach (TriangleVertexIdentifiers vId in Triangle.Vertices)
        {
            bool alreadyInVertices = false;
            for(int i = 0; i < s.vertices.Count; i++)
            {
                if (s.vertices[i].Equals(t.GetVertex(vId)))
                {
                    SetVertexId(vId, i);
                    alreadyInVertices = true;
                    break;
                }
            }

            if (!alreadyInVertices)
            {
                SurfaceVertex nsv = new SurfaceVertex(t.GetVertex(vId));
                SetVertexId(vId, s.vertices.Count);
                s.vertices.Add(nsv);
            }
        }

        this.Surface = s;
        this.Surface.AddFace(this);
    }

    public void SetVertexId(TriangleVertexIdentifiers v, int i)
    {
        switch (v)
        {
            case TriangleVertexIdentifiers.A:
                svAIndex = i;
                break;
            case TriangleVertexIdentifiers.B:
                svBIndex = i;
                break;
            case TriangleVertexIdentifiers.C:
                svCIndex = i;
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
                if (this.GetVertex(v1).FloatRepresentation == otherFace.GetVertex(v2).FloatRepresentation) sharedVertices.Add(v1);
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
