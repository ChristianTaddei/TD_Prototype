using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class PathTests
    {
        private Surface disjointedSurface;
        private Face disjointedFace1, disjointedFace2;
        private SurfacePoint disjointedPoint1, disjointedPoint2;

        // A square made of two faces, sharing points b and c.
        private Surface Square_abcd;
        private Face Triangle_abc1, Triangle_bcd2;
        private SurfacePoint a1, b1, c1, b2, c2, d2;


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
                Square_abcd.TryMakeDirectPath(a1, b1, out path));
            Assert.True(
                Square_abcd.TryMakeDirectPath(b1, c1, out path));
            Assert.True(
                Square_abcd.TryMakeDirectPath(a1, c1, out path));
            Assert.True(
                Square_abcd.TryMakeDirectPath(b2, c2, out path));
            Assert.True(
                Square_abcd.TryMakeDirectPath(b2, d2, out path));
            Assert.True(
                Square_abcd.TryMakeDirectPath(c2, d2, out path));
        }
        [Test]

        public void NeighbourFacesHaveDirectPath()
        {
            SurfacePath path;

            Assert.True(
                Square_abcd.TryMakeDirectPath(a1, d2, out path));
        }
    }
}
