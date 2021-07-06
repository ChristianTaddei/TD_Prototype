using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Surface
{
	public List<SurfaceVertex> Vertices { get; }
	public List<Face> Faces { get; }

	// public void AddFace(Face face);
	// public Face AddFace(Vector a, Vector b, Vector c);
}
