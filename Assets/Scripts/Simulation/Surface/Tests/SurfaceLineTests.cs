using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class SurfaceLineTests
    {    
        private Surface disjointedSurface1, disjointedSurface2;
        private Face disjointedFace1, disjointedFace2;
        private SurfacePoint disjointedPoint1, disjointedPoint2;
        
        // A square made of two faces, sharing points b and c.
        private Surface abcd;
        private Face abc1, bcd2;
        private SurfacePoint a1, b1, c1, b2, c2, d2; 


        [SetUp]
        public void Setup()
        {
            disjointedFace1 = new Face(
                (CartesianPoint) new Vector3(0, 1, 0),
                (CartesianPoint) new Vector3(0, 1, 1),
                (CartesianPoint) new Vector3(1, 1, 0)
            );

            disjointedFace2 = new Face(
                (CartesianPoint) new Vector3(0, 2, 0),
                (CartesianPoint) new Vector3(0, 2, 2),
                (CartesianPoint) new Vector3(2, 2, 0)
            );

            disjointedSurface1 = new Surface(disjointedFace1);
            disjointedSurface2 = new Surface(disjointedFace2);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void DisjointedFacesNoLine()
        {
            SurfaceLine surfaceLine;

            Assert.False(
                SurfaceLine.FromPoints(disjointedPoint1, disjointedPoint2, out surfaceLine));
            Assert.True(
                SurfaceLine.FromPoints(a1, b1, out surfaceLine));           
            Assert.True(
                SurfaceLine.FromPoints(b1, c1, out surfaceLine));         
            Assert.True(
                SurfaceLine.FromPoints(a1, d2, out surfaceLine));
        }
    }
}
