using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// A Surface is responsable of maintaining the relation between faces and a list of uniques vertices.
public interface Surface
{
	List<Vector> Vertices { get; }
	List<Triangle> Faces { get; }

	void AddFace(Triangle face);
	void AddFaces(List<Triangle> faces);

    bool Contains(Vector point);
	List<Triangle> GetFacesContaining(Vector point);
}

// In tests:

// All points in Vertices are vertices of a face
// All vertices of faces are in Vertices

// Contains any point inside a Face 
// does not Contain any point outside all Faces

// Implementations (or subinterfaces? or subset of tests that ensure behaviours?):
// connectedSurface -> Faces cannot be disconnected
// stronglyConnectedSurface -> Faces cannot be disconnected or connected by a single vertex
// convexSurface -> 
