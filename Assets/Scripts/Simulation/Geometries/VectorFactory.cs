
using UnityEngine;

public abstract class VectorFactory
{
	public abstract Vector VectorFromVec3(Vector3 vec3);

	public Vector VectorFromCoordinates(float x, float y, float z)
	{
		return VectorFromVec3(new Vector3(x, y, z));
	}

	public abstract Vector Copy(Vector otherVector);
}
