using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class TriangleEqualsTests
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
		public void equals_sameTriangle_isEqual()
		{
			Triangle t = new TriangleStub(
				new VectorStub(1, 2, 3),
				new VectorStub(3, 4, 5),
				new VectorStub(5, 6, 7));

			Assert.AreEqual(t, t);
		}

		[Test]
		public void equals_differentTriangle_notEqual()
		{
			Triangle t1 = new TriangleStub(
				new VectorStub(1, 2, 3),
				new VectorStub(3, 4, 5),
				new VectorStub(5, 6, 7));
			Triangle t2 = new TriangleStub(
				new VectorStub(6, 5, 4),
				new VectorStub(4, 3, 2),
				new VectorStub(2, 1, 0));

			Assert.AreNotEqual(t1, t2);
		}

		// test hashcode?
	}
}
