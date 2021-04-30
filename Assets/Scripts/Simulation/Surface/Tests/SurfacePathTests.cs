using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class SurfacePathTests
    {
        private Surface disjointedSurface;
        private Face disjointedFace1, disjointedFace2;
        private SurfacePoint disjointedPoint1, disjointedPoint2;

        // A square made of two faces, sharing points b and c.
        private Surface Square_abcd;
        private Face Triangle_abc, Triangle_bcd;
        private SurfacePoint a, b_abc, c_abc, b_bcd, c_bcd, d;


        [SetUp]
        public void Setup()
        {
            #region Disjointed Faces Surface

            disjointedSurface = new Surface();

            disjointedFace1 = disjointedSurface.AddFace(
                (CartesianPoint)new Vector3(0, 1, 0),
                (CartesianPoint)new Vector3(0, 1, 1),
                (CartesianPoint)new Vector3(1, 1, 0));

            disjointedFace2 = disjointedSurface.AddFace(
                (CartesianPoint)new Vector3(0, 2, 0),
                (CartesianPoint)new Vector3(0, 2, 2),
                (CartesianPoint)new Vector3(2, 2, 0));

            disjointedPoint1 = new SurfacePoint(
                disjointedFace1,
                new BarycentricVector(
                    disjointedFace1.Triangle,
                    new BarycentricCoordinates(1, 0, 0)));

            disjointedPoint2 = new SurfacePoint(
                disjointedFace2,
                new BarycentricVector(
                    disjointedFace1.Triangle,
                    new BarycentricCoordinates(1, 0, 0)));

            #endregion

            #region ABC/BCD Square

            Square_abcd = new Surface();

            Triangle_abc = Square_abcd.AddFace(
                (CartesianPoint)new Vector3(0, 0, 0),
                (CartesianPoint)new Vector3(0, 1, 0),
                (CartesianPoint)new Vector3(1, 0, 0));

            Triangle_bcd = Square_abcd.AddFace(
                (CartesianPoint)new Vector3(0, 1, 0),
                (CartesianPoint)new Vector3(1, 0, 0),
                (CartesianPoint)new Vector3(1, 1, 0));

            a = new SurfacePoint(
                Triangle_abc,
                new BarycentricVector(
                    Triangle_abc.Triangle,
                    new BarycentricCoordinates(1, 0, 0)));

            b_abc = new SurfacePoint(
                Triangle_abc,
                new BarycentricVector(
                    Triangle_abc.Triangle,
                    new BarycentricCoordinates(0, 1, 0)));

            c_abc = new SurfacePoint(
                Triangle_abc,
                new BarycentricVector(
                    Triangle_abc.Triangle,
                    new BarycentricCoordinates(0, 0, 1)));

            b_bcd = new SurfacePoint(
                Triangle_bcd,
                new BarycentricVector(
                    Triangle_bcd.Triangle,
                    new BarycentricCoordinates(1, 0, 0)));

            c_bcd = new SurfacePoint(
                Triangle_bcd,
                new BarycentricVector(
                    Triangle_bcd.Triangle,
                    new BarycentricCoordinates(0, 1, 0)));

            d = new SurfacePoint(
                Triangle_bcd,
                new BarycentricVector(
                    Triangle_bcd.Triangle,
                    new BarycentricCoordinates(0, 0, 1)));

            #endregion
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void DisjointedFacesNoPath()
        {
            SurfacePath path;

            Assert.False(
                disjointedSurface.TryMakeDirectPath(disjointedPoint1, disjointedPoint2, out path));
        }

        [Test]
        public void SameFaceHasDirectPath()
        {
            SurfacePath path;

            // TODO: programmatically: every vertex of same face (both ways), any 2 random points on same face

            Assert.True(
                Square_abcd.TryMakeDirectPath(a, b_abc, out path));
            Assert.AreEqual(a, path.Start);
            Assert.AreEqual(b_abc, path.End);

            Assert.True(
                Square_abcd.TryMakeDirectPath(b_abc, c_abc, out path));
            Assert.AreEqual(b_abc, path.Start);
            Assert.AreEqual(c_abc, path.End);

            Assert.True(
                Square_abcd.TryMakeDirectPath(a, c_abc, out path));
            Assert.AreEqual(a, path.Start);
            Assert.AreEqual(c_abc, path.End);

            Assert.True(
                Square_abcd.TryMakeDirectPath(b_bcd, d, out path));
            Assert.AreEqual(b_bcd, path.Start);
            Assert.AreEqual(d, path.End);

            Assert.True(
                Square_abcd.TryMakeDirectPath(c_bcd, d, out path));
            Assert.AreEqual(c_bcd, path.Start);
            Assert.AreEqual(d, path.End);

            Assert.True(
                Square_abcd.TryMakeDirectPath(b_bcd, c_bcd, out path));
            Assert.AreEqual(b_bcd, path.Start);
            Assert.AreEqual(c_bcd, path.End);
        }
        [Test]

        public void NeighbourFacesHaveDirectPath()
        {
            SurfacePath path;

            Assert.True(
                Square_abcd.TryMakeDirectPath(a, d, out path));
            Assert.AreEqual(a, path.Start);
            Assert.AreEqual(d, path.End);

            SurfacePoint m_bcd = new SurfacePoint(
                Triangle_bcd,
                new BarycentricVector(
                    Triangle_bcd.Triangle,
                    new BarycentricCoordinates(0, 0.5f, 0.5f)));

            Assert.AreEqual(m_bcd, path.Points[1]);

            // 1 -> intermediary on 2nd face
            // 2 -> multiface point (or auto convert to new face for edge points?), equals
        }
    }
}
