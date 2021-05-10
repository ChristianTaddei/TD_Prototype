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
                DisjointedSurface.Surface
                    .MakeDirectPath(
                        DisjointedSurface.PointOn1,
                        DisjointedSurface.PointOn2);

            Assert.False(path.HasValue());
        }

        [Test]
        public void SameFaceHasDirectPath()
        {
            // TODO: should use different variables?
            Maybe<SurfacePath> path =
                Square_abcd.Surface
                    .MakeDirectPath(
                        Square_abcd.a,
                        Square_abcd.b_abc);

            Assert.True(path.HasValue());
            Assert.AreEqual(
                Square_abcd.a,
                path.Value.Start);
            Assert.AreEqual(
                Square_abcd.b_abc,
                path.Value.End);

            path = Square_abcd.Surface.MakeDirectPath(Square_abcd.b_abc, Square_abcd.c_abc);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_abcd.b_abc, path.Value.Start);
            Assert.AreEqual(Square_abcd.c_abc, path.Value.End);

            path = Square_abcd.Surface.MakeDirectPath(Square_abcd.a, Square_abcd.c_abc);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_abcd.a, path.Value.Start);
            Assert.AreEqual(Square_abcd.c_abc, path.Value.End);

            path = Square_abcd.Surface.MakeDirectPath(Square_abcd.b_bcd, Square_abcd.d);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_abcd.b_bcd, path.Value.Start);
            Assert.AreEqual(Square_abcd.d, path.Value.End);

            path = Square_abcd.Surface.MakeDirectPath(Square_abcd.c_bcd, Square_abcd.d);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_abcd.c_bcd, path.Value.Start);
            Assert.AreEqual(Square_abcd.d, path.Value.End);

            path = Square_abcd.Surface.MakeDirectPath(Square_abcd.b_bcd, Square_abcd.c_bcd);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_abcd.b_bcd, path.Value.Start);
            Assert.AreEqual(Square_abcd.c_bcd, path.Value.End);
        }

        [Test]
        public void NeighbourFacesHaveDirectPath()
        {
            Maybe<SurfacePath> path = Square_abcd.Surface.MakeDirectPath(Square_abcd.a, Square_abcd.d);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_abcd.a, path.Value.Start);
            Assert.AreEqual(Square_abcd.d, path.Value.End);

            SurfacePoint m_bcd = new SurfacePoint(
                Square_abcd.Triangle_bcd,
                new BarycentricVector(
                    Square_abcd.Triangle_bcd.Triangle,
                    new BarycentricCoordinates(0.5f, 0.5f, 0)));

            Assert.AreEqual(m_bcd.Coordinates, path.Value.Points[1].Coordinates);

            // 1 -> intermediary on 2nd face
            // 2 -> multiface point (or auto convert to new face for edge points?), equals
        }
    }
}
