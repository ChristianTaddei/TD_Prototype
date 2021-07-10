using UnityEngine;

public interface VectorFactory
{
	Vector From(Vector3 vec3);
	Vector From(float x, float y, float z);

	Vector Copy(Vector otherVector);
}