using UnityEngine;

public class FloatVector : Vector
{
	public override Vector3 FloatRepresentation => cartesianCoordinates;

	private readonly Vector3 cartesianCoordinates;

	public static new FloatVector From(Vector3 vec3) // Calling the implementation type gives typed From (hiding default factory)
	{
		return new FloatVector(vec3);
	}

	public static new FloatVector From(float x, float y, float z)
	{
		return new FloatVector(new Vector3(x, y, z));
	}

	public static new FloatVector Copy(Vector other)
	{
		return new FloatVector(other.FloatRepresentation);
	}

	private FloatVector(Vector3 vector3)
	{
		this.cartesianCoordinates = vector3;
	}
}
