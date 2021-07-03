using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	// This knows units, while actual file does not, right?

	public class AttackThenMoveTurnProgressorTests
	{
		public void Setup()
		{

		}

		public void TearDown()
		{

		}
/*
		[Test]
		public void nextState_anyState_attackProgCalled()
		{
			// setup
			Mock<SimulationState> simState = new Mock<SimulationState>();
			simState.SetupProperty(ss => ss.Units);

			Mock<AttackPhaseProgressor> attackProg = new Mock<AttackPhaseProgressor>();
			attackProg.Setup(ap => ap.progressStateBuilder(simState.Object)).Returns(stateBuilder.Object);

			Mock<MovePhaseProgressor> moveProg = new Mock<MovePhaseProgressor>();

			AttackThenMoveTurnProgressor progressor = new AttackThenMoveTurnProgressor(
				attackProg.Object,
				moveProg.Object
			);

			// run
			progressor.nextState(simState.Object);

			// check
			Assert.AreEqual(0, simState.Object.Units.Count);
		}
*/
	}
}
