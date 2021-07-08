using UnityEngine;

public class TriangleStub : Triangle
{
	public Vector A { get; private set; }
	public Vector B { get; private set; }
	public Vector C { get; private set; }

	public TriangleStub(Vector a, Vector b, Vector c)
	{
		A = a; B = b; C = c;
	}
}