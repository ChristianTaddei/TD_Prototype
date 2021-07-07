public interface Geometry
{
	Vector Project(Vector v, Triangle t);
	Vector GetTriangleIntersectionToward(Vector currentPoint, Vector finalPoint);
}