
using System;
using UnityEngine;

public class ConcreteVectorFactory : AbstractVectorFactory
{
	public override Vector VectorFromVec3(Vector3 vec3)
	{
		return new ConcreteVector(vec3);
	}

	public override Vector Copy(Vector otherVector)
	{
		return new ConcreteVector(
			otherVector.FloatRepresentation.x,
			otherVector.FloatRepresentation.y,
			otherVector.FloatRepresentation.z);
	}
}
