using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class StatelessPathfinderTests
	{
		// unit under test - use implementation
		Pathfinder statelessPathfinder;

		// collaborators - use stubs and fill them as needed
		Geometry geometry;
		PathFactory pathFactory;

		[SetUp]
		public void Setup()
		{
			geometry = new GeometryStub();
			pathFactory = new PathFactoryStub();

			statelessPathfinder = new StatelessPathfinder(geometry, pathFactory);
		}

		[TearDown]
		public void TearDown()
		{

		}

		[Test]
		public void getDirectPath_pathCrossOneEdge_pathContainsIntersection()
		{
			FlatSquareStub flatSquare = new FlatSquareStub();

			// Setup
			Vector start = FlatSquareStub.a;
			Vector end = FlatSquareStub.d;

			Vector intersection = FlatSquareStub.centre;

			Triangle A = FlatSquareStub.A;
			Triangle B = FlatSquareStub.B;

			// Execute
			Maybe<Path> path = statelessPathfinder.GetDirectPath(flatSquare, start, end);

			Assert.AreEqual(intersection, path.Value.Points[1]);

			// TODO: check equals in concrete impl tests
			// Assert.AreEqual(intersection, ConcreteVector.From(0.5f, 0, 0.5f));
			// Assert.AreEqual(ConcreteVector.From(0.5f, 0, 0.5f), intersection);
		}

		// [Test]
		// public void getDirectPath_pathCrossAtSharedVertex_pathContainsIntersection()
		// {
		// 	// sepatation interface abstract -> to be sure not to use any impl in this tests
		// 	// stub classes -> made using Mock<> but already setup trivial calls and has Equals support?

		// 	Mock<Vector> start = new Mock<Vector>();
		// 	Mock<Vector> end = new Mock<Vector>();
		// 	Mock<Vector> intersection = new Mock<Vector>();

		// 	Mock<Triangle> a = new Mock<Triangle>();
		// 	Mock<Triangle> b = new Mock<Triangle>();
		// 	Mock<Triangle> c = new Mock<Triangle>();
		// 	Mock<Triangle> d = new Mock<Triangle>();

		// 	surface.Setup(s => s.Contains(It.IsAny<Vector>())).Returns(true);

		// 	surface.Setup(s => s.GetFacesContaining(start.Object)).Returns(new List<Triangle> { a.Object });
		// 	surface.Setup(s => s.GetFacesContaining(end.Object)).Returns(new List<Triangle> { b.Object });
		// 	surface.Setup(s => s.GetFacesContaining(intersection.Object))
		// 		.Returns(new List<Triangle> { a.Object, b.Object, c.Object, d.Object });

		// 	geometry.Setup(g => g.GetTriangleIntersectionToward(a.Object, start.Object, end.Object)).Returns(intersection.Object);
		// 	geometry.Setup(g => g.GetTriangleIntersectionToward(c.Object, intersection.Object, end.Object)).Returns(intersection.Object);
		// 	geometry.Setup(g => g.GetTriangleIntersectionToward(d.Object, intersection.Object, end.Object)).Returns(intersection.Object);

		// 	Maybe<Path> path = statelessPathfinder.GetDirectPath(surface.Object, start.Object, end.Object);

		// 	Assert.True(path.Value.Contains(intersection.Object));
		// }

		// [Test]
		// public void getDirectPath_pathCrossAtSharedVertex_pathHasNoDuplicates()
		// {
		// 	// TODO: duplicate code, extract specific setup methods for this state?
		// 	Mock<Vector> start = new Mock<Vector>();
		// 	Mock<Vector> end = new Mock<Vector>();
		// 	Mock<Vector> intersection = new Mock<Vector>();

		// 	Mock<Triangle> a = new Mock<Triangle>();
		// 	Mock<Triangle> b = new Mock<Triangle>();
		// 	Mock<Triangle> c = new Mock<Triangle>();
		// 	Mock<Triangle> d = new Mock<Triangle>();

		// 	surface.Setup(s => s.Contains(It.IsAny<Vector>())).Returns(true);

		// 	surface.Setup(s => s.GetFacesContaining(start.Object)).Returns(new List<Triangle> { a.Object });
		// 	surface.Setup(s => s.GetFacesContaining(end.Object)).Returns(new List<Triangle> { b.Object });
		// 	surface.Setup(s => s.GetFacesContaining(intersection.Object))
		// 		.Returns(new List<Triangle> { a.Object, b.Object, c.Object, d.Object });

		// 	geometry.Setup(g => g.GetTriangleIntersectionToward(a.Object, start.Object, end.Object)).Returns(intersection.Object);
		// 	geometry.Setup(g => g.GetTriangleIntersectionToward(c.Object, intersection.Object, end.Object)).Returns(intersection.Object);
		// 	geometry.Setup(g => g.GetTriangleIntersectionToward(d.Object, intersection.Object, end.Object)).Returns(intersection.Object);

		// 	Maybe<Path> path = statelessPathfinder.GetDirectPath(surface.Object, start.Object, end.Object);

		// 	Assert.True(hasNoDuplicates(path.Value.Points));
		// }

		// private bool hasNoDuplicates<T>(List<T> list)
		// {
		// 	return list.Count == new HashSet<T>(list).Count;
		// }
	}
}
