using UnityEngine;
using NUnit.Framework;

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
	}
}