using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVectorialGeometry : ExactGeometry
{
	// internal static readonly float FLOAT_REPRESENTATION_EQUALITY_TOLLERANCE = 0.001f;

	public Vector GetTriangleIntersectionToward(Triangle t, Vector currentPoint, Vector finalPoint)
	{
		throw new System.NotImplementedException();
	}
	
	// what about "areComplanar" etc methods public while calc ones internal?

	// public Vector  Sum(Vector  v1, Vector  v2)
	// {
	// 	return new ConcreteVector(v1.FloatRepresentation + v2.FloatRepresentation);
	// }

	// public Vector  Substract(Vector  v1, Vector  v2)
	// {
	// 	return new ConcreteVector(v1.FloatRepresentation - v2.FloatRepresentation);
	// }

	// public float Magnitude(Vector  v)
	// {
	// 	return v.FloatRepresentation.magnitude;
	// }

	// public bool AreCloserThanTollerance(Vector  v1, Vector  v2)
	// {
	// 	return Vector3.Distance(v1.FloatRepresentation, v2.FloatRepresentation) < FLOAT_REPRESENTATION_EQUALITY_TOLLERANCE;
	// }

	// public float Dot(Vector v1, Vector v2)
	// {
	// 	return Vector3.Dot(v1.FloatRepresentation, v2.FloatRepresentation);
	// }

	// public Vector Cross(Vector v1, Vector v2)
	// {
	// 	return new ConcreteVector(Vector3.Cross(v1.FloatRepresentation, v2.FloatRepresentation));
	// }

	// public bool AreComplanar(Vector a, Vector b, Vector c)
	// {
	// 	float mixedProd = Dot(a, Cross(b, c));

	// 	if (System.Math.Abs(mixedProd) <= 0.001f)
	// 	{
	// 		return true;
	// 	}

	// 	return false;
	// }

	// public Vector Project(Vector v, Triangle triangleDefiningPlane)
	// {
	// 	Vector projectedVector;

	// 	Vector Plane_AB = new ConcreteVector(triangleDefiningPlane.B.FloatRepresentation - triangleDefiningPlane.A.FloatRepresentation);
	// 	Vector Plane_AC = new ConcreteVector(triangleDefiningPlane.C.FloatRepresentation - triangleDefiningPlane.A.FloatRepresentation);
	// 	Vector Plane_n = Cross(Plane_AB, Plane_AC);
	// 	Vector AP = new ConcreteVector(v.FloatRepresentation - triangleDefiningPlane.A.FloatRepresentation);

	// 	float squareMag = Magnitude(Plane_n) * Magnitude(Plane_n);
	// 	projectedVector = Substract(v, (new ConcreteVector((Dot(AP, Plane_n) / squareMag) * Plane_n.FloatRepresentation)));

	// 	return projectedVector;
	// }

}
