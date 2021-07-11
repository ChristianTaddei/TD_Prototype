using UnityEngine;

public class FloatVector : AbstractVector
{
	public override Vector3 FloatRepresentation => cartesianCoordinates;

	private readonly Vector3 cartesianCoordinates;

	public static VectorFactory<FloatVector> Factory = new FloatVectorFactory();

	private class FloatVectorFactory : VectorFactory<FloatVector>
	{
		public FloatVector From(Vector3 vec3)
		{
			return new FloatVector(vec3);
		}

		public FloatVector From(float x, float y, float z)
		{
			return new FloatVector(new Vector3(x, y, z));
		}

		public FloatVector Copy(Vector otherVector)
		{
			return new FloatVector(otherVector.FloatRepresentation);
		}
	}

	private FloatVector(Vector3 vector3)
	{
		this.cartesianCoordinates = vector3;
	}
}
