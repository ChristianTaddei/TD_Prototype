using System;
using System.Collections.Generic;

public abstract class Triangle // TODO: to interface if can move Vertices and GetVertex away?
{
	public static IEnumerable<TriangleVertexIdentifiers> Vertices =>
		(TriangleVertexIdentifiers[])Enum.GetValues(typeof(TriangleVertexIdentifiers));

	public virtual Vector A { get; }
	public virtual Vector B { get; }
	public virtual Vector C { get; }

	public Vector GetVertex(TriangleVertexIdentifiers v)
	{
		switch (v)
		{
			case TriangleVertexIdentifiers.A:
				return A;
			case TriangleVertexIdentifiers.B:
				return B;
			case TriangleVertexIdentifiers.C:
				return C;
			default:
				throw new Exception("Coordinate name does not exist");
		}
	}
}
