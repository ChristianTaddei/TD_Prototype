public class Tower : Unit, ShootingUnit
{
	public Vector Position { get; }
	
	public bool CanFireAt(Vector target)
	{
		throw new System.NotImplementedException();
	}
}