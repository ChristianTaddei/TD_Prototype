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

	public bool Equals(FloatVector other)
	{
		if (other != null)
		{
			return this.FloatRepresentation.Equals(other.FloatRepresentation);
		}

		return false;
	}

	public override bool Equals(object obj)
	{
		return this.Equals(obj as FloatVector);
	}


	public static bool operator ==(FloatVector lhs, FloatVector rhs)
	{
		bool isLhsNull = object.ReferenceEquals(lhs, null);
		bool isRhsNull = object.ReferenceEquals(rhs, null);

		if (isLhsNull && isRhsNull)
			return true;    // TODO: cover with tests (same in stub? or remove stub...)
		else if (isLhsNull)
			return false;   // TODO: cover with tests
		else
			return lhs.Equals(rhs);
	}

	public static bool operator !=(FloatVector lhs, FloatVector rhs)
	{
		return !(lhs == rhs);
	}

	public override int GetHashCode()
	{
		return -509336368 + FloatRepresentation.GetHashCode();
	}

	private FloatVector(Vector3 vector3)
	{
		this.cartesianCoordinates = vector3;
	}
}
