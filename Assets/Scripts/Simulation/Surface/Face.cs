using System;
using System.Collections.Generic;


public abstract class Face : Triangle // TODO: interface when triangle is interface
{
    public abstract Surface Surface { get; }

    public abstract HashSet<TriangleVertexIdentifiers> GetSharedVertices(Face otherFace);

    public abstract HashSet<Face> GetFacesFromSharedVertices(HashSet<TriangleVertexIdentifiers> sharedVertices);

    public abstract HashSet<Face> GetFacesFromAtLeastOneSharedVertex(HashSet<TriangleVertexIdentifiers> sharedVertices);
}
