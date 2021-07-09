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

			(geometry as GeometryStub).AddGetTriangleIntersectionTowardStub((A, start, end), intersection);

			// Execute
			Maybe<Path> path = statelessPathfinder.GetDirectPath(flatSquare, start, end);

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

			(geometry as GeometryStub).AddGetTriangleIntersectionTowardStub((AB, start, end), intersection);
			(geometry as GeometryStub).AddGetTriangleIntersectionTowardStub((AC, start, end), intersection);
			(geometry as GeometryStub).AddGetTriangleIntersectionTowardStub((AB, intersection, end), intersection);
			(geometry as GeometryStub).AddGetTriangleIntersectionTowardStub((AC, intersection, end), intersection);
			(geometry as GeometryStub).AddGetTriangleIntersectionTowardStub((BD, intersection, end), end);
			(geometry as GeometryStub).AddGetTriangleIntersectionTowardStub((CD, intersection, end), end);

			// Execute
			Maybe<Path> path = statelessPathfinder.GetDirectPath(flatCrossedSquare, start, end);

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
	}
}
