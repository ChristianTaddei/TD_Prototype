using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
	[TestFixture]
	[Category("Integration")]
	public class MinimalSimulationIntegrationTests
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
		// [Ignore("Not all dependencies are implemented")]
		public void nextTurn_towerHasEnemyInRange_enemyDestroyed()
		{
			Surface surface = new ConcreteSurface();
			Unit enemy = new Enemy();
			Unit tower = new Tower();

			SimulationState firstTurn = new ConcreteSimulationState( // TODO: could have personal factory that takes enemy/allies
				surface,
				new List<Unit>() { tower, enemy });

			SelfProgressedSimulation minimalSimulation = new SelfProgressedSimulation(firstTurn);

			Task<SimulationState> firstTurnExectution = minimalSimulation.ProgressToNextTurn();
			SimulationState secondTurn = firstTurnExectution.Result;

			// Assert.False(minimalSimulation.CurrentState.Units.Contains(enemy));
			Assert.AreEqual(firstTurn, secondTurn);
		}
	}
}
