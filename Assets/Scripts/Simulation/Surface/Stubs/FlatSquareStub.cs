using System.Collections.Generic;

public class FlatSquareStub : Surface
{

	public static readonly Vector a = new VectorStub(0, 0, 0);
	public static readonly Vector b = new VectorStub(1, 0, 0);
	public static readonly Vector c = new VectorStub(0, 0, 1);
	public static readonly Vector d = new VectorStub(1, 0, 1);
	public static readonly Vector centre = new VectorStub(0.5f, 0, 0.5f);

	public bool Contains(Vector point)
	{
		if (point.Equals(a) || point.Equals(b) || point.Equals(c) || point.Equals(d) || point.Equals(centre))
		{
			return true;
		}

		return false;
	}

	public static readonly Triangle A = new TriangleStub(a, b, c);
	public static readonly Triangle B = new TriangleStub(b, c, d);

	public List<Triangle> GetFacesContaining(Vector point)
	{
		List<Triangle> facesContainingPoint = new List<Triangle>();

		if (point.Equals(a))
		{
			facesContainingPoint.Add(A);
		}
		else if (point.Equals(b))
		{
			facesContainingPoint.Add(A);
			facesContainingPoint.Add(B);
		}
		else if (point.Equals(c))
		{
			facesContainingPoint.Add(A);
			facesContainingPoint.Add(B);
		}
		else if (point.Equals(d))
		{
			facesContainingPoint.Add(B);
		}
		else if (point.Equals(centre))
		{
			facesContainingPoint.Add(A);
			facesContainingPoint.Add(B);
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