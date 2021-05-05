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
                    disjointedFace2.Triangle,
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
            Assert.False(
                disjointedSurface.MakeDirectPath(disjointedPoint1, disjointedPoint2).HasValue());
        }

        [Test]
        public void SameFaceHasDirectPath()
        {
            // TODO: should use different variables?
            Maybe<SurfacePath> path = Square_abcd.MakeDirectPath(a, b_abc);
            Assert.True(path.HasValue());
            Assert.AreEqual(a, path.Value.Start);
            Assert.AreEqual(b_abc, path.Value.End);

            path = Square_abcd.MakeDirectPath(b_abc, c_abc);
            Assert.True(path.HasValue());
            Assert.AreEqual(b_abc, path.Value.Start);
            Assert.AreEqual(c_abc, path.Value.End);

            path = Square_abcd.MakeDirectPath(a, c_abc);
            Assert.True(path.HasValue());
            Assert.AreEqual(a, path.Value.Start);
            Assert.AreEqual(c_abc, path.Value.End);

            path = Square_abcd.MakeDirectPath(b_bcd, d);
            Assert.True(path.HasValue());
            Assert.AreEqual(b_bcd, path.Value.Start);
            Assert.AreEqual(d, path.Value.End);

            path = Square_abcd.MakeDirectPath(c_bcd, d);
            Assert.True(path.HasValue());
            Assert.AreEqual(c_bcd, path.Value.Start);
            Assert.AreEqual(d, path.Value.End);

            path = Square_abcd.MakeDirectPath(b_bcd, c_bcd);
            Assert.True(path.HasValue());
            Assert.AreEqual(b_bcd, path.Value.Start);
            Assert.AreEqual(c_bcd, path.Value.End);
        }
        
        [Test]
        public void NeighbourFacesHaveDirectPath()
        {
            Maybe<SurfacePath> path = Square_abcd.MakeDirectPath(a, d);
            Assert.True(path.HasValue());
            Assert.AreEqual(a, path.Value.Start);
            Assert.AreEqual(d, path.Value.End);

            // SurfacePoint m_bcd = new SurfacePoint(
            //     Triangle_bcd,
            //     new BarycentricVector(
            //         Triangle_bcd.Triangle,
            //         new BarycentricCoordinates(0, 0.5f, 0.5f)));

            // Assert.AreEqual(m_bcd, path.Points[1]);

            // 1 -> intermediary on 2nd face
            // 2 -> multiface point (or auto convert to new face for edge points?), equals
        }
    }
}
