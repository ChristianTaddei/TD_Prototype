using System;
using System.Collections.Generic;


public class ConcreteFace : AbstractFace
{   
    public override AbstractSurface Surface => surface;

    public override Vector A => svA;
    public override Vector B => svB;
    public override Vector C => svC;

    private int svAIndex;
    private int svBIndex;
    private int svCIndex;

    // public?
    public SurfaceVertex svA => Surface.Vertices[svAIndex];
    public SurfaceVertex svB => Surface.Vertices[svBIndex];
    public SurfaceVertex svC => Surface.Vertices[svCIndex];

    private ConcreteSurface surface;
    
    public ConcreteFace(ConcreteSurface s, ConcreteTriangle t)
    {
        foreach (TriangleVertexIdentifiers vId in Triangle.Vertices)
        {
            bool alreadyInVertices = false;
            for(int i = 0; i < s.Vertices.Count; i++)
            {
                if (s.Vertices[i].Equals(t.GetVertex(vId)))
                {
                    SetVertexId(vId, i);
                    alreadyInVertices = true;
                    break;
                }
            }

            if (!alreadyInVertices)
            {
                SurfaceVertex nsv = new SurfaceVertex(t.GetVertex(vId));
                SetVertexId(vId, s.Vertices.Count);
                s.vertices.Add(nsv);
            }
        }

        this.surface = s;
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

    public override HashSet<TriangleVertexIdentifiers> GetSharedVertices(AbstractFace otherFace)
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

    public override HashSet<AbstractFace> GetFacesFromSharedVertices(HashSet<TriangleVertexIdentifiers> sharedVertices)
    {
        HashSet<AbstractFace> facesSharingVertices = new HashSet<AbstractFace>();

        foreach (AbstractFace candidate in this.Surface.Faces)
        {
            if (this.GetSharedVertices(candidate).SetEquals(sharedVertices)) facesSharingVertices.Add(candidate);
        }

        return facesSharingVertices;
    }

    public override HashSet<AbstractFace> GetFacesFromAtLeastOneSharedVertex(HashSet<TriangleVertexIdentifiers> sharedVertices)
    {
        HashSet<AbstractFace> facesSharingVertices = new HashSet<AbstractFace>();

        foreach (AbstractFace candidate in this.Surface.Faces)
        {
            foreach (TriangleVertexIdentifiers v in this.GetSharedVertices(candidate))
            {
                if (sharedVertices.Contains(v)) facesSharingVertices.Add(candidate);
            }
        }

        return facesSharingVertices;
    }

}
