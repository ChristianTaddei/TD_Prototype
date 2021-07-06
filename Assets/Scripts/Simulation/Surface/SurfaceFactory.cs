using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceFactory
{
	public SurfaceFactory(){ 
	}

	public Surface MakeSquareSurface(float squareEdge, int subSquaresForSide){
		return new ConcreteSurface();

		// add faces & verts to make square
	}
	
}
