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
		public void nextTurn_towerHasEnemyInRange_enemyDestroyed()
		{
			Surface surface = new ConcreteSurface();
			Enemy enemy = new Enemy();
			Tower tower = new Tower();

			Simulation minimalSimulation = new ConcreteSimulation();
			minimalSimulation.ProgressSimulation();

			Assert.False(minimalSimulation.CurrentState.Units.Contains(enemy));
		}
	}
}
