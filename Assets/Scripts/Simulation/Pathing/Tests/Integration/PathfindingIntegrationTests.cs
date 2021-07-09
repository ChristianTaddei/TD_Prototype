using NUnit.Framework;
using UnityEngine;

namespace Tests
{
	[TestFixture]
  	[Category("Integration")]
	public class PathingIntegrationTests
	{
		Pathfinder pathfinder;

		Geometry geometry;
		PathFactory pathFactory;

		SurfaceFactory surfaceFactory;

		[SetUp]
		public void Setup()
		{
			// here just use ConcreteVector.From() as needed

			geometry = new ConcreteGeometry();
			pathFactory = new ConcretePathFactory();

			pathfinder = new StatelessPathfinder(geometry, pathFactory);

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
			Vector start = ConcreteVector.From(startVec3);
			Vector3 endVec3 = new Vector3(1, 0, 1);
			Vector end = ConcreteVector.From(endVec3);

			Vector3 intersectionVec3 = new Vector3(0.5f, 0, 0.5f);
			Vector intersection = ConcreteVector.From(intersectionVec3);

			Maybe<Path> path = pathfinder.GetDirectPath(surface, start, end);

			Assert.True(path.Value.Vertices.Contains(intersection));
		}
	}
}
