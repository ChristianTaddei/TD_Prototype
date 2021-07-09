using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class AbstractVectorTests
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
		public void from_vec3_floatRepresentationIsEqual()
		{
			Vector3 original = new Vector3(1,2,3);
			Vector v = Vector.From(original);

			Assert.AreEqual(original, v.FloatRepresentation);
		}

		[Test]
		public void copy_otherVector_areEqual()
		{
			Vector original = new VectorStub(1,2,3);
			Vector copy = Vector.Copy(original);

			Assert.AreEqual(original, copy);
		}
		
		[Test]
		public void equals_sameVector_isEqual()
		{
			Vector v = new VectorStub(1,2,3);

			Assert.AreEqual(v,v);
		}
		
		[Test]
		public void equals_differentVector_notEqual()
		{
			Vector v1 = new VectorStub(1,2,3);
			Vector v2 = new VectorStub(2,3,4);
			
			Assert.AreNotEqual(v1,v2);
		}
	}
}
