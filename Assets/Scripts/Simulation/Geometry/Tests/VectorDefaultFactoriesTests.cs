using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;
using System.Reflection;

namespace Tests
{
	public class VectorDefaultFactoriesTests
	{
		MethodInfo FromVec3, FromCoords, Copy;  // Static method, needs to be recovered through reflection

		[OneTimeSetUp]
		public void GetStaticMethods()
		{
			FromVec3 = typeof(Vector).GetMethod("From", new[] { typeof(Vector3) });
			FromCoords = typeof(Vector).GetMethod("From", new[] { typeof(float), typeof(float), typeof(float) });
			Copy = typeof(Vector).GetMethod("Copy", new[] { typeof(Vector) });
		}

		[Test]
		public void defaultFromVec3_isPresent()
		{
			if (FromVec3 == null)
			{
				Assert.Fail("Class does not implement From(Vector3)");
			}
		}

		[Test]
		public void defaultFromCoords_isPresent()
		{
			if (FromCoords == null)
			{
				Assert.Fail("Class does not implement From(float, float, float)");
			}
		}

		[Test]
		public void defaultCopy__isPresent()
		{
			if (Copy == null)
			{
				Assert.Fail("Class does not implement Copy");
			}
		}

		[Test]
		[TestCaseSource(typeof(VectorTestsData), nameof(VectorTestsData.testVec3s))]
		public void fromVec3_testVec3_isEqualToFloatingRepresentation(Vector3 vec3)
		{
			Vector vector = (Vector)FromVec3.Invoke(null, new object[] { vec3 });

			Assert.AreEqual(vec3, vector.FloatRepresentation);
		}

		[Test]
		[TestCaseSource(typeof(VectorTestsData), nameof(VectorTestsData.equalVec3Pairs))]
		public void equals_equalVec3Pair_isEqualToFloatingRepresentation((Vector3, Vector3) vectorPair)
		{
			Vector v1 = (Vector)FromVec3.Invoke(null, new object[] { vectorPair.Item1 });
			Vector v2 = (Vector)FromVec3.Invoke(null, new object[] { vectorPair.Item2 });

			EqualsTestingUtility.TestEqualObjects<Vector>(v1, v2);
		}

		[Test]
		[TestCaseSource(typeof(VectorTestsData), nameof(VectorTestsData.differentVec3Pairs))]
		public void equals_differentVec3Pair_isNotEqualToFloatingRepresentation((Vector3, Vector3) vectorPair)
		{
			Vector v1 = (Vector)FromVec3.Invoke(null, new object[] { vectorPair.Item1 });
			Vector v2 = (Vector)FromVec3.Invoke(null, new object[] { vectorPair.Item2 });

			EqualsTestingUtility.TestUnequalObjects<Vector>(v1, v2);
		}

		[Test]
		[TestCaseSource(typeof(VectorTestsData), nameof(VectorTestsData.testVec3s))]
		public void equals_null_isNotEqualToFloatingRepresentation(Vector3 vec3)
		{
			Vector v = (Vector)FromVec3.Invoke(null, new object[] { vec3 });

			EqualsTestingUtility.TestAgainstNull<Vector>(v);
		}
	}
}