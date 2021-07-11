
using System;
using UnityEngine;

public interface Vector : IEquatable<Vector>
{
	Vector3 FloatRepresentation { get; }
}
