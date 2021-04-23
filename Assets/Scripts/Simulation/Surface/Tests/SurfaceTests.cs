using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class SurfaceTests
    {
        private Surface disjointedSurface;
        private Face disjointedFace1, disjointedFace2;
        private SurfacePoint disjointedPoint1, disjointedPoint2;

        // A square made of two faces, sharing points b and c.
        private Surface abcd;
        private Face abc1, bcd2;
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
        public void Test()
        {
            Assert.True(true);
        }
    }
}
