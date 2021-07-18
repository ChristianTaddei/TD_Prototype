using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	public class EqualsMixedImplementationsIntegrationTests
	{
		[Test]
		public void equals_equalVectorsOfDifferentType_areEqual(
			[ValueSource(
				typeof(MixedImplementationTestsData),
		 		nameof(MixedImplementationTestsData.mixedImplementationEqualVectors))]
				Vector v1,
			[ValueSource(
				typeof(MixedImplementationTestsData),
		 		nameof(MixedImplementationTestsData.mixedImplementationEqualVectors))]
				Vector v2)
		{
			EqualsTestingUtility.TestEqualObjects<Vector>(v1, v2);
		}

		[Test]
		public void equals_differentVectorsOfDifferentType_areNotEqual(
			[ValueSource(
				typeof(MixedImplementationTestsData),
		 		nameof(MixedImplementationTestsData.mixedImplementationDifferentVectors))]
				Vector v1,
			[ValueSource(
				typeof(MixedImplementationTestsData),
		 		nameof(MixedImplementationTestsData.mixedImplementationDifferentVectors))]
				Vector v2)
		{
			if(v1 != v2) // given the combinatorial approach it is necessary to skip reference equals vectors 
				EqualsTestingUtility.TestUnequalObjects<Vector>(v1, v2);
		}
	}
}