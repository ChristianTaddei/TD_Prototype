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
		Mock<PathFactory> pathFactory;
		Mock<Surface> surface;

		[SetUp]
		public void Setup()
		{
			geometry = new Mock<Geometry>();

			// TODO: move to where all tests can use it
			pathFactory = new Mock<PathFactory>();
			pathFactory.Setup(pf => pf.PathFromPoints(It.IsAny<List<Vector>>()))
				.Returns((List<Vector> points) =>
				{
					var path = new Mock<Path>();
					path.Setup(p => p.Points).Returns(points);
					return path.Object;
				});
			pathFactory.Setup(pf => pf.PathFromPoints(It.IsAny<Vector>(), It.IsAny<List<Vector>>(), It.IsAny<Vector>()))
				.Returns((Vector p1, List<Vector> points, Vector p2) =>
				{
					List<Vector> allPoints = new List<Vector>();
					allPoints.Add(p1);
					allPoints.AddRange(points);
					allPoints.Add(p2);

					var path = new Mock<Path>();
					path.Setup(p => p.Points).Returns(allPoints);

					return path.Object;
				});

			pathfinder = new StatelessPathfinder(geometry.Object, pathFactory.Object);

			surface = new Mock<Surface>();
		}

		[TearDown]
		public void TearDown()
		{

		}

		[Test]
		public void getDirectPath_pathCrossOneEdge_pathContainsIntersection()
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

			geometry.Setup(g => g.GetTriangleIntersectionToward(a.Object, start.Object, end.Object)).Returns(intersection.Object);

			Maybe<Path> path = pathfinder.GetDirectPath(surface.Object, start.Object, end.Object);

			// TODO: some verify or stuff to check everything is used and/or nothing feeds correct result?
			Assert.True(path.Value.Points.Contains(intersection.Object));
		}

		[Test]
		public void getDirectPath_pathCrossAtSharedVertex_pathContainsIntersection()
		{
			Mock<Vector> start = new Mock<Vector>();
			Mock<Vector> end = new Mock<Vector>();
			Mock<Vector> intersection = new Mock<Vector>();

			Mock<Triangle> a = new Mock<Triangle>();
			Mock<Triangle> b = new Mock<Triangle>();
			Mock<Triangle> c = new Mock<Triangle>();
			Mock<Triangle> d = new Mock<Triangle>();

			surface.Setup(s => s.Contains(It.IsAny<Vector>())).Returns(true);

			surface.Setup(s => s.GetFacesContaining(start.Object)).Returns(new List<Triangle> { a.Object });
			surface.Setup(s => s.GetFacesContaining(end.Object)).Returns(new List<Triangle> { b.Object });
			surface.Setup(s => s.GetFacesContaining(intersection.Object))
				.Returns(new List<Triangle> { a.Object, b.Object, c.Object, d.Object });

			geometry.Setup(g => g.GetTriangleIntersectionToward(a.Object, start.Object, end.Object)).Returns(intersection.Object);
			geometry.Setup(g => g.GetTriangleIntersectionToward(c.Object, intersection.Object, end.Object)).Returns(intersection.Object);
			geometry.Setup(g => g.GetTriangleIntersectionToward(d.Object, intersection.Object, end.Object)).Returns(intersection.Object);

			Maybe<Path> path = pathfinder.GetDirectPath(surface.Object, start.Object, end.Object);

			Assert.True(path.Value.Points.Contains(intersection.Object));
		}
		[Test]
		public void getDirectPath_pathCrossAtSharedVertex_pathHasNoDuplicates()
		{
			// TODO: duplicate code, extract specific setup methods for this state?
			Mock<Vector> start = new Mock<Vector>();
			Mock<Vector> end = new Mock<Vector>();
			Mock<Vector> intersection = new Mock<Vector>();

			Mock<Triangle> a = new Mock<Triangle>();
			Mock<Triangle> b = new Mock<Triangle>();
			Mock<Triangle> c = new Mock<Triangle>();
			Mock<Triangle> d = new Mock<Triangle>();

			surface.Setup(s => s.Contains(It.IsAny<Vector>())).Returns(true);

			surface.Setup(s => s.GetFacesContaining(start.Object)).Returns(new List<Triangle> { a.Object });
			surface.Setup(s => s.GetFacesContaining(end.Object)).Returns(new List<Triangle> { b.Object });
			surface.Setup(s => s.GetFacesContaining(intersection.Object))
				.Returns(new List<Triangle> { a.Object, b.Object, c.Object, d.Object });

			geometry.Setup(g => g.GetTriangleIntersectionToward(a.Object, start.Object, end.Object)).Returns(intersection.Object);
			geometry.Setup(g => g.GetTriangleIntersectionToward(c.Object, intersection.Object, end.Object)).Returns(intersection.Object);
			geometry.Setup(g => g.GetTriangleIntersectionToward(d.Object, intersection.Object, end.Object)).Returns(intersection.Object);

			Maybe<Path> path = pathfinder.GetDirectPath(surface.Object, start.Object, end.Object);

			Assert.True(hasNoDuplicates(path.Value.Points));
		}

		private bool hasNoDuplicates<T>(List<T> list)
		{
			return list.Count == new HashSet<T>(list).Count;
		}
	}
}
