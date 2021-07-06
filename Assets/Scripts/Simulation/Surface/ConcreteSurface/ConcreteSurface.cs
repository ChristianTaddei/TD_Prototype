using System;
using System.Collections.Generic;

public class ConcreteSurface : Surface
{	

	public List<Vector> Vertices => new List<Vector>(vertices);
	public List<Triangle> Faces => new List<Triangle>(faces);
	
	// internal to be added from face, invert?
	internal List<Vector> vertices;
	private List<Triangle> faces;

	public ConcreteSurface()
	{
		this.vertices = new List<Vector>();
		this.faces = new List<Triangle>();
	}

	public void AddFace(Face face)
	{
		faces.Add(face);
	}

	public void AddFaces(List<Face> faces)
	{
		throw new NotImplementedException();
	}	

	public bool Contains(Vector startPoint)
	{
		throw new NotImplementedException();
	}

}
