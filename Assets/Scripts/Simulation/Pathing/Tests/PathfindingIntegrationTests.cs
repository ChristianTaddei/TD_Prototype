using NUnit.Framework;
using UnityEngine;

namespace Tests
{
	[TestFixture]
  	[Category("Integration")]
	public class PathingIntegrationTests
	{
		Pathfinder pathfinder;

		VectorFactory vectorFactory;

		Geometry geometry;
		PathFactory pathFactory;

		SurfaceFactory surfaceFactory;

		[SetUp]
		public void Setup()
		{
			vectorFactory = new ConcreteVectorFactory();

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
		public void getDirectPath_pathCrossOneEdge_pathContainsIntersection()
		{
			Surface surface = surfaceFactory.MakeSquareSurface(1.0f, 1);

			Vector3 startVec3 = new Vector3(0, 0, 0);
			Vector start = vectorFactory.VectorFromVec3(startVec3);
			Vector3 endVec3 = new Vector3(1, 0, 1);
			Vector end = vectorFactory.VectorFromVec3(endVec3);

			Vector3 intersectionVec3 = new Vector3(0.5f, 0, 0.5f);
			Vector intersection = vectorFactory.VectorFromVec3(intersectionVec3);

			Maybe<Path> path = pathfinder.GetDirectPath(surface, start, end);

			Assert.True(path.Value.Contains(intersection));
		}
	}
}
