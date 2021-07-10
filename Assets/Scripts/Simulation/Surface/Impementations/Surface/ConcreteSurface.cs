using System;
using System.Collections.Generic;

public class ConcreteSurface : Surface
{
	public List<Vector> Vertices => throw new NotImplementedException();

	public List<Triangle> Faces => throw new NotImplementedException();

	public void AddFace(Triangle face)
	{
		throw new NotImplementedException();
	}

	public void AddFaces(List<Triangle> faces)
	{
		throw new NotImplementedException();
	}

	public bool Contains(Vector point)
	{
		throw new NotImplementedException();
	}

	public List<Triangle> GetFacesContaining(Vector point)
	{
		throw new NotImplementedException();
	}
}
