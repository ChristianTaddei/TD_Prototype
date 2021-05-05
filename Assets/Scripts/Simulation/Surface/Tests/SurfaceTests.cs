using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class SurfaceTests
    {
        [SetUp]
        public void Setup()
        {
          
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void GetIntersectionTowardItself()
        {
            // This point in in the middle of a face, and there is no intersection in the direction 000
            Maybe<SurfacePoint> noIntersection = 
                TestSurfaceElements.disjointedSurface.GetIntersectionToward(
                    TestSurfaceElements.disjointedPointMiddle,
                    TestSurfaceElements.disjointedPointMiddle);

            Assert.False(noIntersection.HasValue());

            // This point in on an edge, so it actually is already an intesection, even in the direction 000
            Maybe<SurfacePoint> intersection = 
                TestSurfaceElements.disjointedSurface.GetIntersectionToward(
                    TestSurfaceElements.disjointedPoint1,
                    TestSurfaceElements.disjointedPoint1);

            Assert.True(intersection.HasValue());
        }

        [Test]
        public void GetIntersectionTowardPointOnSameFace()
        {
            Assert.True(true);
        }

        [Test]
        public void GetIntersectionTowardExternalPoint()
        {
            Assert.True(true);
        }

        [Test]
        public void GetIntersectionTowardFaceVertex()
        {
            Assert.True(true);
        }
    }
}
