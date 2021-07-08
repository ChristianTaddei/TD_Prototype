using System;
using System.Collections.Generic;

public abstract class Triangle 
{
	Vector A { get; }
	Vector B { get; }
	Vector C { get; }

	public override bool Equals(object obj)
	{
		return obj is Triangle triangle &&
			   EqualityComparer<Vector>.Default.Equals(A, triangle.A) &&
			   EqualityComparer<Vector>.Default.Equals(B, triangle.B) &&
			   EqualityComparer<Vector>.Default.Equals(C, triangle.C);
	}

	public override int GetHashCode()
	{
		int hashCode = 793064651;
		hashCode = hashCode * -1521134295 + EqualityComparer<Vector>.Default.GetHashCode(A);
		hashCode = hashCode * -1521134295 + EqualityComparer<Vector>.Default.GetHashCode(B);
		hashCode = hashCode * -1521134295 + EqualityComparer<Vector>.Default.GetHashCode(C);
		return hashCode;
	}
}
