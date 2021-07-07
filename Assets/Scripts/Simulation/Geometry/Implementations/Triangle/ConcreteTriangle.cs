// generic triangle implementation
public class ConcreteTriangle : Triangle
{
	public Vector A { get => _A; }
	public Vector B { get => _B; }
	public Vector C { get => _C; }

	private readonly Vector _A;
	private readonly Vector _B;
	private readonly Vector _C;

	public ConcreteTriangle(Vector A, Vector B, Vector C)
	{
		_A = A;
		_B = B;
		_C = C;
	}
}
