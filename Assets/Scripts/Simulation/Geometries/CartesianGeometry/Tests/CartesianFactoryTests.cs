using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Moq;

namespace Tests
{
	public class CartesianFactoryTests
	{
		CartesianFactory cartesianFactory;

		[SetUp]
		public void Setup()
		{
			cartesianFactory = new CartesianFactory();
		}

		[TearDown]
		public void TearDown()
		{

		}

		// TODO: test on some vectors stored in another file
		[Test]
		public void vectorFromVec3_vec3_floatRepEqualsVec3()
		{
			Vector3 vec3 = new Vector3(1.00000001f, 1315246.23634f, 0.0f);

			Vector v = cartesianFactory.VectorFromVec3(vec3);

			Assert.AreEqual(vec3, v.FloatRepresentation);
		}

		[Test]
		public void vectorFromCoords_coords_floatRepCoordsEqualCoords()
		{
			float c1 = 1.00000001f;
			float c2 = 1315246.23634f;
			float c3 = 0.0f;

			Vector v = cartesianFactory.VectorFromCoordinates(c1, c2, c3);

			Assert.AreEqual(c1, v.FloatRepresentation.x);
			Assert.AreEqual(c2, v.FloatRepresentation.y);
			Assert.AreEqual(c3, v.FloatRepresentation.z);
		}

		// TODO: here keep some vec3 for test (form ext file), somewhere else test all floats?
		[Test]
		public void vectorFromVec3_randomVec3_floatRepEqualsVec3(
			[NUnit.Framework.Random(1)] float randomFloat1,
			[NUnit.Framework.Random(1)] float randomFloat2,
			[NUnit.Framework.Random(1)] float randomFloat3)
		{
			Vector3 vec3 = new Vector3(randomFloat1, randomFloat2, randomFloat3);

			Vector v = cartesianFactory.VectorFromVec3(vec3);

			Assert.AreEqual(vec3, v.FloatRepresentation);
		}

		[Test]
		public void vectorFromCoords_randomCoords_floatRepCoordsEqualCoords(
			[NUnit.Framework.Random(1)] float randomFloat1,
			[NUnit.Framework.Random(1)] float randomFloat2,
			[NUnit.Framework.Random(1)] float randomFloat3)
		{
			Vector v = cartesianFactory.VectorFromCoordinates(randomFloat1, randomFloat2, randomFloat3);

			Assert.AreEqual(randomFloat1, v.FloatRepresentation.x);
			Assert.AreEqual(randomFloat2, v.FloatRepresentation.y);
			Assert.AreEqual(randomFloat3, v.FloatRepresentation.z);
		}

		[Test]
		public void copy_original_copyEqualsOriginal()
		{
			CartesianVector original = new CartesianVector(1, 2, 3);

			Vector copy = cartesianFactory.Copy(original);

			Assert.AreEqual(original, copy);
		}
	}
}
