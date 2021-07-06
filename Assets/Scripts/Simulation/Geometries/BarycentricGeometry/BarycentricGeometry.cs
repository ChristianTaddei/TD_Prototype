using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricGeometry : Geometry
{
	private Geometry geometry;

	public BarycentricGeometry(Geometry geometry)
	{
		this.geometry = geometry;
	}
}
