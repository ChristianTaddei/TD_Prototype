using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class TriangleStubTests
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
			TriangleStub t = new TriangleStub(
				new VectorStub(1, 2, 3),
				new VectorStub(3, 4, 5),
				new VectorStub(5, 6, 7));

			Assert.AreEqual(t, t);
			Assert.True(t.Equals(t));
		}

		[Test]
		public void equals_differentTriangle_notEqual()
		{
			TriangleStub t1 = new TriangleStub(
				new VectorStub(1, 2, 3),
				new VectorStub(3, 4, 5),
				new VectorStub(5, 6, 7));
			TriangleStub t2 = new TriangleStub(
				new VectorStub(6, 5, 4),
				new VectorStub(4, 3, 2),
				new VectorStub(2, 1, 0));

			Assert.AreNotEqual(t1, t2);
			Assert.False(t1.Equals(t2));
			Assert.False(t2.Equals(t1));
		}
	}
}
