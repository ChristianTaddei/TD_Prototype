using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	public class SingleThreadAttackPhaseProgressorTest
	{
		public void Setup()
		{
			
		}

		public void TearDown()
		{

		}

		[Test]
		public void nextState_targetInRange_targetHit() 
		{
			SingleThreadAttackPhaseProgressor attackProgressor = new SingleThreadAttackPhaseProgressor(); 

			Mock<Builder<State>> stateBuilder = new Mock<Builder<State>>();
			// stateBuilder.Setup();

			// attackProgressor.progressStateBuilder(stateBuilder);
		}
	}
}
