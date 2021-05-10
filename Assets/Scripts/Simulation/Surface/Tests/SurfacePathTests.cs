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
                Square_ABCD.Surface
                    .MakeDirectPath(
                        Square_ABCD.A,
                        Square_ABCD.B_ABC);

            Assert.True(path.HasValue());
            Assert.AreEqual(
                Square_ABCD.A,
                path.Value.Start);
            Assert.AreEqual(
                Square_ABCD.B_ABC,
                path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.B_ABC, Square_ABCD.C_ABC);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.B_ABC, path.Value.Start);
            Assert.AreEqual(Square_ABCD.C_ABC, path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.A, Square_ABCD.C_ABC);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.A, path.Value.Start);
            Assert.AreEqual(Square_ABCD.C_ABC, path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.B_BCD, Square_ABCD.D);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.B_BCD, path.Value.Start);
            Assert.AreEqual(Square_ABCD.D, path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.C_BCD, Square_ABCD.D);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.C_BCD, path.Value.Start);
            Assert.AreEqual(Square_ABCD.D, path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.B_BCD, Square_ABCD.C_BCD);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.B_BCD, path.Value.Start);
            Assert.AreEqual(Square_ABCD.C_BCD, path.Value.End);
        }

        [Test]
        public void NeighbourFacesHaveDirectPath()
        {
            Maybe<SurfacePath> path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.A, Square_ABCD.D);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.A, path.Value.Start);
            Assert.AreEqual(Square_ABCD.D, path.Value.End);

            SurfacePoint m_bcd = new SurfacePoint(
                Square_ABCD.BCD,
                new BarycentricVector(
                    Square_ABCD.BCD.Triangle,
                    new BarycentricCoordinates(0.5f, 0.5f, 0)));

            Assert.AreEqual(m_bcd.Coordinates, path.Value.Points[1].Coordinates);

            // 1 -> intermediary on 2nd face
            // 2 -> multiface point (or auto convert to new face for edge points?), equals
        }
    }
}
