using UnityEngine;

// Gives default implementations for shorthands methods
public abstract class AbstractVectorFactory : VectorFactory
{
	public abstract Vector From(Vector3 vec3);

	public Vector From(float x, float y, float z)
	{
		return From(new Vector3(x, y, z));
	}

	public Vector Copy(Vector otherVector)
	{
		return From(
			new Vector3(
				otherVector.FloatRepresentation.x,
				otherVector.FloatRepresentation.y,
				otherVector.FloatRepresentation.z));
	}
}