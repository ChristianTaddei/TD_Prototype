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
		Mock<Geometry> geometry;

		Mock<Surface> surface;

		[SetUp]
		public void Setup()
		{
			geometry = new Mock<Geometry>();
			pathfinder = new StatelessPathfinder(geometry.Object);

			surface = new Mock<Surface>();
		}

		[TearDown]
		public void TearDown()
		{

		}

		[Test]
		public void directPath_flatSquareOppositeCorners_crossesDiagonalAtMiddle()
		{
			Mock<Vector> start = new Mock<Vector>();
			Mock<Vector> end = new Mock<Vector>();
			Mock<Vector> intersection = new Mock<Vector>();

			Mock<Triangle> a = new Mock<Triangle>();
			Mock<Triangle> b = new Mock<Triangle>();

			surface.Setup(s => s.Contains(It.IsAny<Vector>())).Returns(true);

			surface.Setup(s => s.GetFacesContaining(start.Object)).Returns(new List<Triangle> { a.Object });
			surface.Setup(s => s.GetFacesContaining(end.Object)).Returns(new List<Triangle> { b.Object });
			surface.Setup(s => s.GetFacesContaining(intersection.Object)).Returns(new List<Triangle> { a.Object, b.Object });

			geometry.Setup(g => g.GetTriangleIntersectionToward(start.Object, end.Object)).Returns(intersection.Object);

			Maybe<Path> path = pathfinder.GetDirectPath(surface.Object, start.Object, end.Object);

			Assert.True(path.HasValue());
			Assert.AreEqual(intersection.Object, path.Value.Points[1]);
		}
	}
}
