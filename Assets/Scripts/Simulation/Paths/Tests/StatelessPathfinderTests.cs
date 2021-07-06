using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	public class StatelessPathfinderTests
	{
		Pathfinder pathfinder;

		Mock<Surface> surface;

		public void Setup()
		{
			surface = new Mock<Surface>();

			pathfinder = new StatelessPathfinder();
		}

		public void TearDown()
		{

		}

		[Test]
		public void directPath_flatSquareOppositeCorners_crossesDiagonalAtMiddle()
		{
			Mock<Vector> start = new Mock<Vector>();
			start.Setup(v => v.FloatRepresentation).Returns(new Vector3(0, 0, 0));

			Mock<Vector> end = new Mock<Vector>();
			end.Setup(v => v.FloatRepresentation).Returns(new Vector3(1, 0, 1));

			Mock<Vector> middlePoint = new Mock<Vector>();
			middlePoint.Setup(v => v.FloatRepresentation).Returns(new Vector3(0.5f, 0, 0.5f));
			
			// Setup surface to return middlepoint as intersection for getIntersTwrd

			Maybe<Path> path = pathfinder.GetDirectPath(surface.Object, start.Object, end.Object);

			// TODO: avoid more than one -> other tests for hasValue, others for start, others for end
			// Assert.True(path.HasValue());

			// Assert.AreEqual(start.Object.FloatRepresentation, path.Value.Points[0]);
			Assert.AreEqual(middlePoint.Object.FloatRepresentation, path.Value.Points[1]);
			// Assert.AreEqual(end.Object.FloatRepresentation, path.Value.Points[2]);
		}
	}
}
