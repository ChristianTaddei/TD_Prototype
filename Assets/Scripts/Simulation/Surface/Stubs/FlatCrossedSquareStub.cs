using System.Collections.Generic;

public class FlatCrossedSquareStub : Surface
{
	/*
		c ----- d
		|  \  / |
		|   m   |
		|  /  \ |
		a ----- b
	*/
	public static readonly Vector a = new VectorStub(0, 0, 0);
	public static readonly Vector b = new VectorStub(1, 0, 0);
	public static readonly Vector c = new VectorStub(0, 0, 1);
	public static readonly Vector d = new VectorStub(1, 0, 1);
	public static readonly Vector m = new VectorStub(0.5f, 0, 0.5f);

	public bool Contains(Vector point)
	{
		if (point.Equals(a) || point.Equals(b) || point.Equals(c) || point.Equals(d) || point.Equals(m))
		{
			return true;
		}

		return false;
	}

	public static readonly Triangle AB = new TriangleStub(a, b, m);
	public static readonly Triangle AC = new TriangleStub(a, c, m);
	public static readonly Triangle BD = new TriangleStub(b, d, m);
	public static readonly Triangle CD = new TriangleStub(c, d, m);

	public List<Triangle> GetFacesContaining(Vector point)
	{
		List<Triangle> facesContainingPoint = new List<Triangle>();

		if (point.Equals(a))
		{
			facesContainingPoint.Add(AB);
			facesContainingPoint.Add(AC);
		}
		else if (point.Equals(b))
		{
			facesContainingPoint.Add(AB);
			facesContainingPoint.Add(BD);
		}
		else if (point.Equals(c))
		{
			facesContainingPoint.Add(AC);
			facesContainingPoint.Add(CD);
		}
		else if (point.Equals(d))
		{
			facesContainingPoint.Add(BD);
			facesContainingPoint.Add(CD);
		}
		else if (point.Equals(m))
		{
			facesContainingPoint.Add(AB);
			facesContainingPoint.Add(AC);
			facesContainingPoint.Add(BD);
			facesContainingPoint.Add(CD);
		}
		else
		{
			throw new System.Exception("FlatSquareStub.GetFacesContaining unrecognized argument.");
		}

		return facesContainingPoint;
	}

	public List<Vector> Vertices => throw new System.NotImplementedException();

	public List<Triangle> Faces => throw new System.NotImplementedException();

	public void AddFace(Triangle face)
	{
		throw new System.NotImplementedException();
	}

	public void AddFaces(List<Triangle> faces)
	{
		throw new System.NotImplementedException();
	}
}