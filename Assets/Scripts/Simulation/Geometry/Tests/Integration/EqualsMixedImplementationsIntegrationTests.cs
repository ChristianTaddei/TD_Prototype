using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Integration")]
	public class EqualsMixedImplementationsIntegrationTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[TearDown]
		public void TearDown()
		{

		}

		// TODO: test all combination of impls -> testCaseSource and/or reflection
		[Test]
		public void floatCopy_mutableVector_isEqualToCopy()
		{
			MutableVector original = MutableVector.From(1, 2, 3);

			FloatVector copy = FloatVector.Copy(original);

			Assert.AreEqual(original, copy); // TODO: check its using mutable.Equals or ==, while below uses float.Equals
		}

		[Test]
		public void mutableCopy_floatVector_isEqualToCopy()
		{
			FloatVector original = FloatVector.From(1, 2, 3);

			MutableVector copy = MutableVector.Copy(original);

			Assert.AreEqual(original, copy);
		}

		[Test]
		public void floatAndMutableFrom_sameCoords_areEquals()
		{
			FloatVector floatVector = FloatVector.From(1, 2, 3);
			MutableVector mutableVector = MutableVector.From(1, 2, 3);

			// TODO: like equalsUtils: make result class that says "Class1.Equals failed (Type1, Type2)"
			Assert.AreEqual(floatVector, mutableVector);
			Assert.AreEqual(mutableVector, floatVector);
		}
	}
}