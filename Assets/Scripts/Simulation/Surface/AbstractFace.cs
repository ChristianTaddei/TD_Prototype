using System;
using System.Collections.Generic;


public abstract class AbstractFace : Triangle // TODO: interface when triangle is interface
{
    public abstract AbstractSurface Surface { get; }

    public abstract HashSet<TriangleVertexIdentifiers> GetSharedVertices(AbstractFace otherFace);

    public abstract HashSet<AbstractFace> GetFacesFromSharedVertices(HashSet<TriangleVertexIdentifiers> sharedVertices);

    public abstract HashSet<AbstractFace> GetFacesFromAtLeastOneSharedVertex(HashSet<TriangleVertexIdentifiers> sharedVertices);
}
