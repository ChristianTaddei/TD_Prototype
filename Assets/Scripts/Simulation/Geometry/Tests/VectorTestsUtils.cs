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
	public class Vector_Tests<TVector> where TVector : Vector
	{

		MethodInfo FromVec3; // Static method, needs to be recovered through reflection

		// TODO: consider if comments below are worth changing tests

		// those tests does not teach much on how Vectors are used -> fine, that's in integration testing?

		// using internal (exposed to tests) constructor: 
		// -faliure of From does not cascade on other tests

		// can all test that are not testing equals use something else?
		// -vector3 equals?

		[OneTimeSetUp]
		public void GetStaticMethods()
		{
			FromVec3 = typeof(TVector).GetMethod("From", new[] { typeof(Vector3) });
		}

		[Test]
		public void fromVec3_isOverridden()
		{
			if (FromVec3 == null)
			{
				Assert.Fail("Class does not override From(Vector3)");
			}
		}

		[Test]
		[TestCaseSource("testVec3s")]
		public void fromVec3_testVec3_isEqualToFloatingRepresentation(Vector3 vec3)
		{
			Vector vector = (Vector)FromVec3.Invoke(null, new object[] { vec3 });

			Assert.AreEqual(vec3, vector.FloatRepresentation);
		}

		[Test]
		[TestCaseSource("equalVec3Pairs")]
		public void equals_equalVec3Pair_isEqualToFloatingRepresentation((Vector3, Vector3) vectorPair)
		{
			TVector v1 = (TVector)FromVec3.Invoke(null, new object[] { vectorPair.Item1 });
			TVector v2 = (TVector)FromVec3.Invoke(null, new object[] { vectorPair.Item2 });

			EqualsTestingUtility.TestEqualObjects<TVector>(v1, v2);
		}

		[Test]
		[TestCaseSource("differentVec3Pairs")]
		public void equals_differentVec3Pair_isNotEqualToFloatingRepresentation((Vector3, Vector3) vectorPair)
		{
			TVector v1 = (TVector)FromVec3.Invoke(null, new object[] { vectorPair.Item1 });
			TVector v2 = (TVector)FromVec3.Invoke(null, new object[] { vectorPair.Item2 });

			EqualsTestingUtility.TestUnequalObjects<TVector>(v1, v2);
		}

		[Test]
		[TestCaseSource("testVec3s")]
		public void equals_null_isNotEqualToFloatingRepresentation(Vector3 vec3)
		{
			TVector v = (TVector)FromVec3.Invoke(null, new object[] { vec3 });

			EqualsTestingUtility.TestAgainstNull<TVector>(v);
		}

		public static IEnumerable<Vector3> testVec3s
		{
			get => new List<Vector3>()
				{
					new Vector3(1,2,3),
					new Vector3(2,3,4)
				};
		}

		public static IEnumerable<(Vector3, Vector3)> equalVec3Pairs
		{
			get => new List<(Vector3, Vector3)>()
				{
					(new Vector3(1,2,3), new Vector3(1,2,3)),
					(new Vector3(2,3,4), new Vector3(2,3,4)),
				};
		}

		public static IEnumerable<(Vector3, Vector3)> differentVec3Pairs
		{
			get => new List<(Vector3, Vector3)>()
				{
					(new Vector3(1,2,3), new Vector3(3,4,5)),
					(new Vector3(2,3,4), new Vector3(5,5,5)),
				};
		}
	}
}