using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public interface SurfaceFactory
{
	Surface MakeSquareSurface(float squareEdge, int subSquaresForSide);
}
