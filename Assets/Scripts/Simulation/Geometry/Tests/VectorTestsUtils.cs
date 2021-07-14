using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture(typeof(FloatVector))]
	// public class VectorAndFactory_Tests
	public class Vector_Tests<TVector> where TVector : Vector
	{

		// TFactory factory = new TFactory();

		[Test]
		[TestCaseSource("Vec3s")]
		public void from123_vec3_isEqualToFloatingRepresentation(Vector3 vec3)
		{
			// Reflection required to access static methods
			// TODO: check all overload exist and are correctly typed
			Vector vector = (Vector)typeof(TVector).GetMethod("From", new[] { typeof(Vector3) }).Invoke(null, new object[] { vec3 });

			Assert.AreEqual(vec3, vector.FloatRepresentation);
		}

		public static IEnumerable<Vector3> Vec3s
		{
			get => new List<Vector3>()
				{
					new Vector3(1,2,3),
					new Vector3(2,3,4)
				};
		}
		/*
				public void from_coords_areEqualTofloatingRepresentationCoords()
				{
					float x = 1, y = 2, z = 3;

					Vector vector = FloatVector.Factory.From(x, y, z);

					Assert.AreEqual(x, vector.FloatRepresentation.x);
					Assert.AreEqual(y, vector.FloatRepresentation.y);
					Assert.AreEqual(z, vector.FloatRepresentation.z);
				}


				public void copy_vector_isEqualToCopy()
				{
					Vector original = FloatVector.Factory.From(1, 2, 3);

					Vector copy = FloatVector.Factory.Copy(original);

					Assert.AreEqual(original, copy);
				}


				public void equals_VectorFromSameVec3_areEqual()
				{
					Vector3 sharedVec3 = new Vector3(1, 3, 4);

					FloatVector lhs = (FloatVector)FloatVector.Factory.From(sharedVec3);
					FloatVector rhs = (FloatVector)FloatVector.Factory.From(sharedVec3);

					EqualsTestingUtility.TestEqualObjects<Vector>(lhs, rhs);
				}

				public void equals_VectorFromDifferentVec3_areNotEqual()
				{
					Vector3 v1 = new Vector3(1, 3, 4);
					Vector3 v2 = new Vector3(2, 4, 2);

					FloatVector lhs = (FloatVector)FloatVector.Factory.From(v1);
					FloatVector rhs = (FloatVector)FloatVector.Factory.From(v2);

					EqualsTestingUtility.TestUnequalObjects<Vector>(lhs, rhs);
				}

				// TODO: can/should test null against null? 


				public void equals_NotNullAgainstNull_areNotEqual()
				{
					FloatVector notNullVector = (FloatVector)FloatVector.Factory.From(1, 2, 3);

					EqualsTestingUtility.TestAgainstNull<Vector>(notNullVector);
				}
		*/
	}
}