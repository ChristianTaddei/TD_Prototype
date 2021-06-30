using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    // Could be integration testing, or unit testing with mocked sub progressors
    public class FullSimulationProgressorTest
    {

        [SetUp] 
        public void Setup(){

        }

        [TearDown] 
        public void TearDown(){

        }

        [Test]
        public void OrdersAreExecuted()
        {
            // unit with straight flat path moves of its speed

            // unit without path stands still

            // unit in range shoots

            // unit out of range moves closer
        }

        [Test]
        public void EnemyAdvanceToTarget()
        {
            // enemy with straight flat path moves of its speed
            
            // unit without path stands still
        }

        [Test]
        public void EnemyShootWhenInRange()
        {
            // Assert.True(bar1.Coordinates.IsInTriangle());
        }
    }
}
