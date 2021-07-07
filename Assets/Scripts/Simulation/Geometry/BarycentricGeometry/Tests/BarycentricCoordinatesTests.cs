using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    /*
    public class BarycentricCoordinatesTests
    {
        BarycentricCoordinates origin, a, center, outsider, complanarOutsider,
            complanarDirection, insider;

        [SetUp]
        public void Setup()
        {
            origin = new BarycentricCoordinates(0.0f, 0.0f, 0.0f);
            a = new BarycentricCoordinates(1.0f, 0.0f, 0.0f);
            center = new BarycentricCoordinates(0.333333f, 0.333333f, 0.333333f);
            outsider = new BarycentricCoordinates(1.5f, 0.5f, 0.5f);
            complanarOutsider = new BarycentricCoordinates(2.0f, -0.5f, -0.5f);
            complanarDirection = new BarycentricCoordinates(1.0f, -0.5f, -0.5f);
            insider = new BarycentricCoordinates(0.3f, 0.3f, 0.3f);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void CheckSumToOneTest()
        {
            Assert.False(origin.CheckSumToOne());
            Assert.True(a.CheckSumToOne());
            Assert.True(center.CheckSumToOne());
            Assert.False(outsider.CheckSumToOne());
            Assert.True(complanarOutsider.CheckSumToOne());
            Assert.False(complanarDirection.CheckSumToOne());
            Assert.False(insider.CheckSumToOne());
        }

        [Test]
        public void CheckSumToZeroTest()
        {
            Assert.True(origin.CheckSumToZero());
            Assert.False(a.CheckSumToZero());
            Assert.False(center.CheckSumToZero());
            Assert.False(outsider.CheckSumToZero());
            Assert.False(complanarOutsider.CheckSumToZero());
            Assert.True(complanarDirection.CheckSumToZero());
            Assert.False(insider.CheckSumToZero());
        }

        [Test]
        public void CheckInternalTest()
        {
            Assert.True(origin.CheckInternal());
            Assert.True(a.CheckInternal());
            Assert.True(center.CheckInternal());
            Assert.False(outsider.CheckInternal());
            Assert.False(complanarOutsider.CheckInternal());
            Assert.False(complanarDirection.CheckInternal());
            Assert.True(insider.CheckInternal());
        }
    }
    */
}
