using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;
using System;
using System.Reflection;
using System.Linq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class VectorEqualsTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[TearDown]
		public void TearDown()
		{

		}

		// [Test]
		// [TestCaseSource(typeof(VectorStub), "KnownEqualities")] 
		// public void vectorStubEquals_operandsInEqualities_areEqual((Vector, Vector) operands)
		// {
		// 	VectorStub lhs = new VectorStub(operands.Item1.FloatRepresentation);
		// 	VectorStub rhs = new VectorStub(operands.Item2.FloatRepresentation);

		// 	EqualsTestingUtility.TestEqualObjects<VectorStub>(lhs, rhs);
		// }
	}
}

