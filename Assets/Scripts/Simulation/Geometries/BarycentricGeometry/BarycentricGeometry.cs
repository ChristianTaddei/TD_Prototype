using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricGeometry : Geometry
{
	private Geometry geometry;

	public BarycentricGeometry()
	{

	}

	public BarycentricGeometry(Geometry geometry)
	{
		this.geometry = geometry;
	}

	public Vector Project(Vector v, Triangle t)
	{
		throw new System.NotImplementedException();
	}
}
