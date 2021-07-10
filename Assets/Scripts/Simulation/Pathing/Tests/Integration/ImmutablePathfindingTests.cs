using NUnit.Framework;
using UnityEngine;

namespace Tests
{
	// Assert Pathfinding works (just start and end) 
	// Assert all collaborators didnt change

	[TestFixture]
  	[Category("Integration")]
	public class ImmutablePathfindingTests
	{
		ExactPathfinder pathfinder;

		ExactGeometry geometry;
		PathFactory pathFactory;

		SurfaceFactory surfaceFactory;

		[SetUp]
		public void Setup()
		{
			// here just use ConcreteVector.From() as needed

			geometry = new FloatGeometry();
			pathFactory = new ConcretePathFactory();

			pathfinder = new ExactPathfinder(geometry, pathFactory);

			surfaceFactory = new ConcreteSurfaceFactory();
		}

		[TearDown]
		public void TearDown()
		{

		}

		[Test]
		[Ignore("Not all dependencies are implemented")]
		public void getDirectPath_pathCrossOneEdge_pathVerticesContainsIntersection()
		{
			Surface surface = surfaceFactory.MakeSquareSurface(1.0f, 1);

			Vector3 startVec3 = new Vector3(0, 0, 0);
			Vector start = ImmutableVector.From(startVec3);
			Vector3 endVec3 = new Vector3(1, 0, 1);
			Vector end = ImmutableVector.From(endVec3);

			Vector3 intersectionVec3 = new Vector3(0.5f, 0, 0.5f);
			Vector intersection = ImmutableVector.From(intersectionVec3);

			Maybe<Path> path = pathfinder.GetDirectPath(surface, start, end);

			Assert.True(path.Value.Vertices.Contains(intersection));
		}
	}
}
