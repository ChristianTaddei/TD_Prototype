
using UnityEngine;

public abstract class Vector 
{
	public abstract Vector3 FloatRepresentation { get; }

	public static Vector From(Vector3 vec3)
	{
		return ConcreteVector.From(vec3);
	}

	public static Vector Copy(Vector otherVector)
	{
		return ConcreteVector.Copy(otherVector);
	}

	public override bool Equals(object obj)
	{
		return obj is Vector vector &&
			   FloatRepresentation.Equals(vector.FloatRepresentation);
	}

	public override int GetHashCode()
	{
		return -509336368 + FloatRepresentation.GetHashCode();
	}
}
