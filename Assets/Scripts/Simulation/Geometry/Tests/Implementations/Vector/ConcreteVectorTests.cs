using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class ConcreteVectorTests
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

			Vector vector = ConcreteVector.From(vec3);

			Assert.AreEqual(vec3, vector.FloatRepresentation);
		}

		[Test]
		public void from_coords_areEqualTofloatingRepresentationCoords()
		{
			float x = 1, y = 2, z = 3;

			Vector vector = ConcreteVector.From(x, y, z);

			Assert.AreEqual(x, vector.FloatRepresentation.x);
			Assert.AreEqual(y, vector.FloatRepresentation.y);
			Assert.AreEqual(z, vector.FloatRepresentation.z);
		}

		[Test]
		public void copy_vector_isEqualToCopy()
		{
			Mock<Vector> original = new Mock<Vector>();
			original.Setup(v => v.FloatRepresentation).Returns(new Vector3(1, 2, 3));

			Vector copy = ConcreteVector.Copy(original.Object);

			// Cannot use AreEqual neither original.Object.Equals because of mocking
			Assert.True(copy.Equals(original.Object));
		}
	}
}