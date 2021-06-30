using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    // Shared behaviours for turn progressors:
    // order: powers - attack - move - cleanup

	public class TurnProgressorTest
	{
		MockSimulationState initialState;
		class MockSimulationState : State
		{
			public readonly bool attackPhaseHappend = false;
			public readonly bool movePhaseHappend = false;

			public MockSimulationState()
			{
				
			}

			public MockSimulationState(bool attackPhaseHappend, bool movePhaseHappend) : this()
			{
				this.attackPhaseHappend = attackPhaseHappend;
				this.movePhaseHappend = movePhaseHappend;
			}
		}

		class MockSimulationStateBuilder : Builder<MockSimulationState>
		{
			public bool attackPhaseHappend = false;
			public bool movePhaseHappend = false;

			public MockSimulationState Build()
			{
				return new MockSimulationState(attackPhaseHappend, movePhaseHappend);
			}
		}

		MockAttackProgressor attackProgressor;
		class MockAttackProgressor : PhaseProgressor<MockSimulationState>
		{
			public Builder<MockSimulationState> progressStateBuilder(Builder<MockSimulationState> stateBuilder)
			{
                // FIXME: can I have this by construction?
				(stateBuilder as MockSimulationStateBuilder).attackPhaseHappend = true;

				return stateBuilder;
			}
		}

		MockMoveProgressor moveProgressor;
		class MockMoveProgressor : PhaseProgressor<MockSimulationState>
		{
			public Builder<MockSimulationState> progressStateBuilder(Builder<MockSimulationState> stateBuilder)
			{
                // FIXME: can I have this by construction?
				(stateBuilder as MockSimulationStateBuilder).movePhaseHappend = true;

				return stateBuilder;
			}
		}

		public void Setup()
		{
			initialState = new MockSimulationState();
			attackProgressor = new MockAttackProgressor();
			moveProgressor = new MockMoveProgressor();
		}

		public void TearDown()
		{

		}

		[Test]
		public void AttackPhaseHappens<T>(TurnProgressor<T> turnProgressor, T initialState) where T : State
		{
            // T finalState =  turnProgressor.nextState(initialState);

            // Assert.True(finalState.attackPhaseHappend);
		}

		[Test]
		public void AttacksHappensBeforeMoves()
		{

		}
	}
}
