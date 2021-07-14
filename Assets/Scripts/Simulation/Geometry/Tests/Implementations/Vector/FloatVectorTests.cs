using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class FloatVectorTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[TearDown]
		public void TearDown()
		{

		}

		[Test]
		public void from_vec3_isEqualToFloatingRepresentation()
		{
			Vector3 vec3 = new Vector3(1, 2, 3);

			Vector vector = FloatVector.From(vec3);

			Assert.AreEqual(vec3, vector.FloatRepresentation);
		}

		[Test]
		public void from_coords_areEqualTofloatingRepresentationCoords()
		{
			float x = 1, y = 2, z = 3;

			Vector vector = FloatVector.From(x, y, z);

			Assert.AreEqual(x, vector.FloatRepresentation.x);
			Assert.AreEqual(y, vector.FloatRepresentation.y);
			Assert.AreEqual(z, vector.FloatRepresentation.z);
		}

		[Test]
		public void copy_vector_isEqualToCopy()
		{
			Vector original = FloatVector.From(1, 2, 3);

			FloatVector copy = FloatVector.Copy(original);

			Assert.AreEqual(original, copy);
		}

		[Test]
		public void equals_VectorFromSameVec3_areEqual()
		{
			Vector3 sharedVec3 = new Vector3(1,3,4);

			FloatVector lhs = FloatVector.From(sharedVec3);
			FloatVector rhs = FloatVector.From(sharedVec3);

			EqualsTestingUtility.TestEqualObjects<Vector>(lhs, rhs);
		}

		[Test]
		public void equals_VectorFromDifferentVec3_areNotEqual()
		{
			Vector3 v1 = new Vector3(1,3,4);
			Vector3 v2 = new Vector3(2,4,2);

			FloatVector lhs = FloatVector.From(v1);
			FloatVector rhs = FloatVector.From(v2);

			EqualsTestingUtility.TestUnequalObjects<Vector>(lhs, rhs);
		}	

		// TODO: can/should test null against null? 

		[Test]
		public void equals_NotNullAgainstNull_areNotEqual()
		{
			FloatVector notNullVector = FloatVector.From(1,2,3);

			EqualsTestingUtility.TestAgainstNull<Vector>(notNullVector);
		}
	}
}

