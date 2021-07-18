using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;
using System.Reflection;

namespace Tests
{
	[TestFixture(typeof(FloatVector))]
	[TestFixture(typeof(MutableVector))]
	public class VectorTests<TVector> where TVector : Vector
	{
		MethodInfo FromVec3, FromCoords, Copy;  // Static method, needs to be recovered through reflection

		[OneTimeSetUp]
		public void GetStaticMethods()
		{
			FromVec3 = typeof(TVector).GetMethod("From", new[] { typeof(Vector3) });
			FromCoords = typeof(TVector).GetMethod("From", new[] { typeof(float), typeof(float), typeof(float) });
			Copy = typeof(TVector).GetMethod("Copy", new[] { typeof(TVector) });
		}

		[Test]
		public void stronglyTypedFromVec3_isHidingDefault()
		{
			if (FromVec3 == null)
			{
				Assert.Fail("Class does not hide default From(Vector3)");
			}
		}

		[Test]
		public void stronglyTypedFromCoords_isHidingDefault()
		{
			if (FromCoords == null)
			{
				Assert.Fail("Class does not hide default From(float, float, float)");
			}
		}

		[Test]
		public void stronglyTypedCopy_isHidingDefault()
		{
			if (Copy == null)
			{
				Assert.Fail("Class does not hide default Copy");
			}
		}

		[Test]
		[TestCaseSource(typeof(VectorTestsData), nameof(VectorTestsData.testVec3s))]
		public void fromVec3_testVec3_isEqualToFloatingRepresentation(Vector3 vec3)
		{
			TVector vector = (TVector)FromVec3.Invoke(null, new object[] { vec3 });

			Assert.AreEqual(vec3, vector.FloatRepresentation);
		}

		[Test]
		[TestCaseSource(typeof(VectorTestsData), nameof(VectorTestsData.equalVec3Pairs))]
		public void equals_equalVec3Pair_isEqualToFloatingRepresentation((Vector3, Vector3) vectorPair)
		{
			TVector v1 = (TVector)FromVec3.Invoke(null, new object[] { vectorPair.Item1 });
			TVector v2 = (TVector)FromVec3.Invoke(null, new object[] { vectorPair.Item2 });

			// Note that equality is tested using default implementation in Vector, not TVector
			EqualsTestingUtility.TestEqualObjects<Vector>(v1, v2);
		}

		[Test]
		[TestCaseSource(typeof(VectorTestsData), nameof(VectorTestsData.differentVec3Pairs))]
		public void equals_differentVec3Pair_isNotEqualToFloatingRepresentation((Vector3, Vector3) vectorPair)
		{
			TVector v1 = (TVector)FromVec3.Invoke(null, new object[] { vectorPair.Item1 });
			TVector v2 = (TVector)FromVec3.Invoke(null, new object[] { vectorPair.Item2 });

			// Note that equality is tested using default implementation in Vector, not TVector
			EqualsTestingUtility.TestUnequalObjects<Vector>(v1, v2);
		}

		[Test]
		[TestCaseSource(typeof(VectorTestsData), nameof(VectorTestsData.testVec3s))]
		public void equals_null_isNotEqualToFloatingRepresentation(Vector3 vec3)
		{
			TVector v = (TVector)FromVec3.Invoke(null, new object[] { vec3 });

			// Note that equality is tested using default implementation in Vector, not TVector
			EqualsTestingUtility.TestAgainstNull<Vector>(v);
		}
	}
}