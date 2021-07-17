using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class MutableVectorTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[TearDown]
		public void TearDown()
		{

		}

		// TODO: actually test set methods and property setter
		[Test]
		public void isMutable()
		{ 
			Vector3 initial = new Vector3(1,3,4);

			MutableVector v = (MutableVector) MutableVector.From(initial);
			v.x = 12;

			Assert.AreNotEqual(initial, v.FloatRepresentation);
		}

		[Test]
		public void from_vec3_isEqualToFloatingRepresentation()
		{
			Vector3 vec3 = new Vector3(1, 2, 3);

			Vector vector = MutableVector.From(vec3);

			Assert.AreEqual(vec3, vector.FloatRepresentation);
		}

		[Test]
		public void from_coords_areEqualTofloatingRepresentationCoords()
		{
			float x = 1, y = 2, z = 3;

			Vector vector = MutableVector.From(x, y, z);

			Assert.AreEqual(x, vector.FloatRepresentation.x);
			Assert.AreEqual(y, vector.FloatRepresentation.y);
			Assert.AreEqual(z, vector.FloatRepresentation.z);
		}

		[Test]
		public void copy_vector_isEqualToCopy()
		{
			Vector original = new VectorStub(1, 3, 4);

			Vector copy = MutableVector.Copy(original);

			Assert.AreEqual(original, copy);
		}
	}
}