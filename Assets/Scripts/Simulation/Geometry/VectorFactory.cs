
using UnityEngine;

public interface VectorFactory 
{
	Vector VectorFromVec3(Vector3 vec3);
	Vector VectorFromCoordinates(float x, float y, float z);

	Vector Copy(Vector otherVector);
}
