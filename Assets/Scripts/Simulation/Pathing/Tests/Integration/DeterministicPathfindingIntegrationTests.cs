using NUnit.Framework;
using UnityEngine;

namespace Tests
{
	// Assert Pathfinding works, with exact expected results

	[TestFixture]
  	[Category("Integration")]
	public class DeterministicPathfindingIntegrationTests
	{
		Pathfinder pathfinder;

		ExactGeometry geometry;

		PathFactory pathFactory;
		SurfaceFactory surfaceFactory;

		[SetUp]
		public void Setup()
		{
			// here just use ConcreteVector.From() as needed

			geometry = new FloatGeometry<FloatVector>(FloatVector.Factory);
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
			Vector3 endVec3 = new Vector3(1, 0, 1);
			
			Vector start = FloatVector.Factory.From(startVec3);
			Vector end = FloatVector.Factory.From(endVec3);

			Vector3 intersectionVec3 = new Vector3(0.5f, 0, 0.5f);
			Vector intersection = FloatVector.Factory.From(intersectionVec3);

			Maybe<Path> path = pathfinder.GetDirectPath(surface, start, end);

			Assert.True(path.Value.Vertices.Contains(intersection));
		}
	}
}
