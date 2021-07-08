using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class VectorStubTests
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
