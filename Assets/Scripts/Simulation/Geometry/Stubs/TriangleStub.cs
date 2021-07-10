using UnityEngine;

public class TriangleStub : AbstractTriangle
{
	public override Vector A => _A;
	public override Vector B => _B;
	public override Vector C => _C;

	private Vector _A;
	private Vector _B;
	private Vector _C;

	public TriangleStub(Vector a, Vector b, Vector c)
	{
		_A = a; _B = b; _C = c;
	}
}