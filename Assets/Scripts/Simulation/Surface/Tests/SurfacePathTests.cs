using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class SurfacePathTests
    {
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void DisjointedFacesNoPath()
        {
            Maybe<SurfacePath> path =
                TestSurfaceElements.disjointedSurface
                    .MakeDirectPath(
                        TestSurfaceElements.disjointedPoint1,
                        TestSurfaceElements.disjointedPoint2);

            Assert.False(path.HasValue());
        }

        [Test]
        public void SameFaceHasDirectPath()
        {
            // TODO: should use different variables?
            Maybe<SurfacePath> path =
                TestSurfaceElements.Square_abcd
                    .MakeDirectPath(
                        TestSurfaceElements.a,
                        TestSurfaceElements.b_abc);

            Assert.True(path.HasValue());
            Assert.AreEqual(
                TestSurfaceElements.a,
                path.Value.Start);
            Assert.AreEqual(
                TestSurfaceElements.b_abc,
                path.Value.End);

            path = TestSurfaceElements.Square_abcd.MakeDirectPath(TestSurfaceElements.b_abc, TestSurfaceElements.c_abc);
            Assert.True(path.HasValue());
            Assert.AreEqual(TestSurfaceElements.b_abc, path.Value.Start);
            Assert.AreEqual(TestSurfaceElements.c_abc, path.Value.End);

            path = TestSurfaceElements.Square_abcd.MakeDirectPath(TestSurfaceElements.a, TestSurfaceElements.c_abc);
            Assert.True(path.HasValue());
            Assert.AreEqual(TestSurfaceElements.a, path.Value.Start);
            Assert.AreEqual(TestSurfaceElements.c_abc, path.Value.End);

            path = TestSurfaceElements.Square_abcd.MakeDirectPath(TestSurfaceElements.b_bcd, TestSurfaceElements.d);
            Assert.True(path.HasValue());
            Assert.AreEqual(TestSurfaceElements.b_bcd, path.Value.Start);
            Assert.AreEqual(TestSurfaceElements.d, path.Value.End);

            path = TestSurfaceElements.Square_abcd.MakeDirectPath(TestSurfaceElements.c_bcd, TestSurfaceElements.d);
            Assert.True(path.HasValue());
            Assert.AreEqual(TestSurfaceElements.c_bcd, path.Value.Start);
            Assert.AreEqual(TestSurfaceElements.d, path.Value.End);

            path = TestSurfaceElements.Square_abcd.MakeDirectPath(TestSurfaceElements.b_bcd, TestSurfaceElements.c_bcd);
            Assert.True(path.HasValue());
            Assert.AreEqual(TestSurfaceElements.b_bcd, path.Value.Start);
            Assert.AreEqual(TestSurfaceElements.c_bcd, path.Value.End);
        }

        [Test]
        public void NeighbourFacesHaveDirectPath()
        {
            Maybe<SurfacePath> path = TestSurfaceElements.Square_abcd.MakeDirectPath(TestSurfaceElements.a, TestSurfaceElements.d);
            Assert.True(path.HasValue());
            Assert.AreEqual(TestSurfaceElements.a, path.Value.Start);
            Assert.AreEqual(TestSurfaceElements.d, path.Value.End);

            SurfacePoint m_bcd = new SurfacePoint(
                TestSurfaceElements.Triangle_bcd,
                new BarycentricVector(
                    TestSurfaceElements.Triangle_bcd.Triangle,
                    new BarycentricCoordinates(0.5f, 0.5f, 0)));

            Assert.AreEqual(m_bcd.Coordinates, path.Value.Points[1].Coordinates);

            // 1 -> intermediary on 2nd face
            // 2 -> multiface point (or auto convert to new face for edge points?), equals
        }
    }
}
