using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceFactory
{
	private readonly Geometry geometry;

	public SurfaceFactory(Geometry geometry){ // should be passed every time as parameter?
		this.geometry = geometry;
	}

	public Surface MakeSquareSurface(float squareEdge, int subSquaresForSide){
		return new Surface();
	}
	
}
