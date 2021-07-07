public abstract class AbstractPathfinder : Pathfinder
{
	public abstract Maybe<Path> GetDirectPath(Surface surface, Vector startPoint, Vector endPoint);

	protected Geometry geometry;
	protected PathFactory pathFactory;

	public AbstractPathfinder(Geometry geometry, PathFactory pathFactory)
	{
		this.geometry = geometry;
		this.pathFactory = pathFactory;
	}
}