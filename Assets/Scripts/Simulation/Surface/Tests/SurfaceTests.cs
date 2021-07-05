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

        // [Test]
        // TODO: what is a non-path (start == end) ?
        // public void GetIntersectionTowardItself()
        // {
        //     // This point is in the middle of a face, and there is no intersection in the direction 000
        //     Maybe<SurfacePoint> noIntersection = 
        //         DisjointedSurface.Surface.GetIntersectionToward(
        //             DisjointedSurface.BaricentreOf1,
        //             DisjointedSurface.BaricentreOf1);

        //     Assert.False(noIntersection.HasValue());

        //     // This point in on an edge, so it actually is already an intesection, even in the direction 000
        //     Maybe<SurfacePoint> intersection = 
        //         DisjointedSurface.Surface.GetIntersectionToward(
        //             DisjointedSurface.PointOn1,
        //             DisjointedSurface.PointOn1);

        //     Assert.True(intersection.HasValue());
        // }

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
