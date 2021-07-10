using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;

namespace Tests
{
	[TestFixture]
	[Category("Unit")]
	public class PathfinderTests
	{
		// unit under test - use implementation
		Pathfinder Pathfinder;

		// collaborators - use stubs and fill them as needed
		ExactGeometry geometry;
		PathFactory pathFactory;

		[SetUp]
		public void Setup()
		{
			geometry = new ExactGeometryStub();
			pathFactory = new PathFactoryStub();

			Pathfinder = new ExactPathfinder(geometry, pathFactory);
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

			(geometry as ExactGeometryStub).AddGetTriangleIntersectionTowardStub((A, start, end), intersection);

			// Execute
			Maybe<Path> path = Pathfinder.GetDirectPath(flatSquare, start, end);

			Assert.True(path.Value.Vertices.Contains(intersection));
		}

		[Test]
		public void getDirectPath_pathCrossAtSharedVertex_pathContainsIntersection()
		{
			FlatCrossedSquareStub flatCrossedSquare = new FlatCrossedSquareStub();

			// Setup
			Vector start = FlatCrossedSquareStub.a;
			Vector end = FlatCrossedSquareStub.d;

			Vector intersection = FlatCrossedSquareStub.m;

			Triangle AB = FlatCrossedSquareStub.AB;
			Triangle AC = FlatCrossedSquareStub.AC;
			Triangle BD = FlatCrossedSquareStub.BD;
			Triangle CD = FlatCrossedSquareStub.CD;

			(geometry as ExactGeometryStub).AddGetTriangleIntersectionTowardStub((AB, start, end), intersection);
			(geometry as ExactGeometryStub).AddGetTriangleIntersectionTowardStub((AC, start, end), intersection);
			(geometry as ExactGeometryStub).AddGetTriangleIntersectionTowardStub((AB, intersection, end), intersection);
			(geometry as ExactGeometryStub).AddGetTriangleIntersectionTowardStub((AC, intersection, end), intersection);
			(geometry as ExactGeometryStub).AddGetTriangleIntersectionTowardStub((BD, intersection, end), end);
			(geometry as ExactGeometryStub).AddGetTriangleIntersectionTowardStub((CD, intersection, end), end);

			// Execute
			Maybe<Path> path = Pathfinder.GetDirectPath(flatCrossedSquare, start, end);

			Assert.True(path.Value.Vertices.Contains(intersection));
		}

		[Test]
		public void getDirectPath_pathCrossAtSharedVertex_pathHasNoDuplicates()
		{
			
		}

		private bool hasNoDuplicates<T>(List<T> list)
		{
			return list.Count == new HashSet<T>(list).Count;
		}


		// test points on different surface no path

		// test no direct pact no path
	}
}
