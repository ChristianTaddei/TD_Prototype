using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Factory
{
	Vector VectorFromVec3(Vector3 vec3);

	Vector VectorFromCoordinates(float v1, float v2, float v3);
}
