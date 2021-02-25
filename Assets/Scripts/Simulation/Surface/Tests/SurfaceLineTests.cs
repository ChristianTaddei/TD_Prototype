using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class SurfaceLineTests
    {
        private SurfacePoint disjointedPoint1, disjointedPoint2;

        [SetUp]
        public void Setup()
        {

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

        }
    }
}
