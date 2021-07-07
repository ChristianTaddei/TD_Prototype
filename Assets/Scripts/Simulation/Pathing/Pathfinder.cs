public interface Pathfinder
{
	Maybe<Path> GetDirectPath(Surface surface, Vector startPoint, Vector endPoint);
}