public interface TriangularGeometry
{
	Maybe<Vector> GetIntersection(Vector startPosition, Vector direction, Triangle triangle);
}