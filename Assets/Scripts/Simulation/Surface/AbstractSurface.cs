using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSurface
{
	public abstract List<SurfaceVertex> Vertices { get; }
	public abstract List<AbstractFace> Faces { get; }

	public abstract void AddFace(AbstractFace face);
	public abstract AbstractFace AddFace(Vector a, Vector b, Vector c);
}
