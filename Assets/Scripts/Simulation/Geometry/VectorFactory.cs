using UnityEngine;

public interface VectorFactory<T> where T : Vector
{
	T From(Vector3 vec3);
	T From(float x, float y, float z);

	T Copy(Vector otherVector);
}