public class Enemy : Unit, MovingUnit , ShootingUnit
{
	public Vector Position { get; }

	public bool HasPathTo(Vector destination)
	{
		throw new System.NotImplementedException();
	}
	
	public bool CanFireAt(Vector target)
	{
		throw new System.NotImplementedException();
	}
}