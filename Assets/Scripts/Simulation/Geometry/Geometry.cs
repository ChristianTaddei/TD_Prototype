public interface Geometry
{
	// Vector Project(Vector v, Triangle t);
	Vector GetTriangleIntersectionToward(Triangle t, Vector currentPoint, Vector finalPoint);
}