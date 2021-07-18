using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;
using System.Reflection;

namespace Tests
{
	public class MixedImplementationTestsData
	{
		public static IEnumerable<Vector> mixedImplementationEqualVectors
		{
			get => new List<Vector>()
				{
					FloatVector.From(1,2,3),
					MutableVector.From(1,2,3)
				};
		}

		public static IEnumerable<Vector> mixedImplementationDifferentVectors
		{
			get => new List<Vector>()
				{
					FloatVector.From(1,2,3),
					MutableVector.From(2,3,4)
				};
		}
	}
}