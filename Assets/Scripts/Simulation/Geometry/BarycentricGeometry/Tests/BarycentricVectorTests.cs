using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Linq;
using System;

namespace Tests
{
    public class BarycentricVectorTests
    {
        CartesianTriangle regularBase = 
            new CartesianTriangle(
                Vector3.right,
                Vector3.up,
                Vector3.forward);
        CartesianTriangle randomBase = 
            new CartesianTriangle(
                new Vector3(12.3643634f, -2.124f, -55.123f),
                new Vector3(3.0f, 1.5f, -0.124f),
                new Vector3(22.2f, 123.8f, 48.566666666f));

        BarycentricCoordinates originBC = new BarycentricCoordinates(0.0f, 0.0f, 0.0f);
        BarycentricCoordinates aBC = new BarycentricCoordinates(1.0f, 0.0f, 0.0f);
        BarycentricCoordinates aTimesTwoBC = new BarycentricCoordinates(2.0f, 0.0f, 0.0f);
        BarycentricCoordinates centerBC = new BarycentricCoordinates(0.333333f, 0.333333f, 0.333333f);
        BarycentricCoordinates outsiderBC = new BarycentricCoordinates(1.5f, 0.5f, 0.5f);
        BarycentricCoordinates complanarOutsiderBC = new BarycentricCoordinates(2.0f, -0.5f, -0.5f);
        BarycentricCoordinates complanarDirectionBC = new BarycentricCoordinates(1.0f, -0.5f, -0.5f);
        BarycentricCoordinates insiderBC = new BarycentricCoordinates(0.3f, 0.3f, 0.3f);

        BarycentricVector originRegBase, aRegBase, aTimesTwoRegBase, centerRegBase, outsiderRegBase,
            complanarOutsiderRegBase, complanarDirectionRegBase, insiderRegBase, originRandomBase,
            aRandomBase, aTimesTwoRandomBase, centerRandomBase, outsiderRandomBase,
            complanarOutsiderRandomBase, complanarDirectionRandomBase, insiderRandomBase;

        [SetUp]
        public void Setup()
        {
            originRegBase = new BarycentricVector(regularBase, originBC);
            aRegBase = new BarycentricVector(regularBase, aBC);
            aTimesTwoRegBase = new BarycentricVector(regularBase, aTimesTwoBC);
            centerRegBase = new BarycentricVector(regularBase, centerBC);
            outsiderRegBase = new BarycentricVector(regularBase, outsiderBC);
            complanarOutsiderRegBase = new BarycentricVector(regularBase, complanarOutsiderBC);
            complanarDirectionRegBase = new BarycentricVector(regularBase, complanarDirectionBC);
            insiderRegBase = new BarycentricVector(regularBase, insiderBC);

            originRandomBase = new BarycentricVector(randomBase, originBC);
            aRandomBase = new BarycentricVector(randomBase, aBC);
            aTimesTwoRandomBase = new BarycentricVector(randomBase, aTimesTwoBC);
            centerRandomBase = new BarycentricVector(randomBase, centerBC);
            outsiderRandomBase = new BarycentricVector(randomBase, outsiderBC);
            complanarOutsiderRandomBase = new BarycentricVector(randomBase, complanarOutsiderBC);
            complanarDirectionRandomBase = new BarycentricVector(randomBase, complanarDirectionBC);
            insiderRandomBase = new BarycentricVector(randomBase, insiderBC);
        }

        [TearDown]
        public void TearDown()
        {

        }

        private void AssertAreComponentWiseEquals(Vector3 v1, Vector3 v2, float error)
        {
            Assert.AreEqual(v1.x, v2.x, error);
            Assert.AreEqual(v1.y, v2.y, error);
            Assert.AreEqual(v1.z, v2.z, error);
        }

        [Test]
        public void NormalizeTest()
        {
            Assert.Throws<Exception>(delegate { originRegBase.Normalize(); });

            AssertAreComponentWiseEquals(
                aRegBase.Coordinates,
                aRegBase.Normalize().Coordinates,
                0.0001f);
            AssertAreComponentWiseEquals(
                aRandomBase.Coordinates,
                aRandomBase.Normalize().Coordinates,
                0.0001f);
            AssertAreComponentWiseEquals(
                centerRegBase.Coordinates,
                centerRegBase.Normalize().Coordinates,
                0.0001f);
            AssertAreComponentWiseEquals(
                centerRandomBase.Coordinates,
                centerRandomBase.Normalize().Coordinates,
                0.0001f);
        }

        [Test]
        public void IsPointComplanarTest()
        {
            Assert.False(originRegBase.IsPointComplanarToBase());
            Assert.True(aRegBase.IsPointComplanarToBase());
            Assert.False(aTimesTwoRegBase.IsPointComplanarToBase());
            Assert.True(centerRegBase.IsPointComplanarToBase());
            Assert.False(outsiderRegBase.IsPointComplanarToBase());
            Assert.True(complanarOutsiderRegBase.IsPointComplanarToBase());
            Assert.False(complanarDirectionRegBase.IsPointComplanarToBase());
            Assert.False(insiderRegBase.IsPointComplanarToBase());

            Assert.False(originRandomBase.IsPointComplanarToBase());
            Assert.True(aRandomBase.IsPointComplanarToBase());
            Assert.False(aTimesTwoRandomBase.IsPointComplanarToBase());
            Assert.True(centerRandomBase.IsPointComplanarToBase());
            Assert.False(outsiderRandomBase.IsPointComplanarToBase());
            Assert.True(complanarOutsiderRandomBase.IsPointComplanarToBase());
            Assert.False(complanarDirectionRandomBase.IsPointComplanarToBase());
            Assert.False(insiderRandomBase.IsPointComplanarToBase());
        }

        [Test]
        public void IsDirectionComplanarTest()
        {
            Assert.True(originRegBase.IsDirectionComplanarToBase());
            Assert.False(aRegBase.IsDirectionComplanarToBase());
            Assert.False(aTimesTwoRegBase.IsDirectionComplanarToBase());
            Assert.False(centerRegBase.IsDirectionComplanarToBase());
            Assert.False(outsiderRegBase.IsDirectionComplanarToBase());
            Assert.False(complanarOutsiderRegBase.IsDirectionComplanarToBase());
            Assert.True(complanarDirectionRegBase.IsDirectionComplanarToBase());
            Assert.False(insiderRegBase.IsDirectionComplanarToBase());

            Assert.True(originRandomBase.IsDirectionComplanarToBase());
            Assert.False(aRandomBase.IsDirectionComplanarToBase());
            Assert.False(aTimesTwoRandomBase.IsDirectionComplanarToBase());
            Assert.False(centerRandomBase.IsDirectionComplanarToBase());
            Assert.False(outsiderRandomBase.IsDirectionComplanarToBase());
            Assert.False(complanarOutsiderRandomBase.IsDirectionComplanarToBase());
            Assert.True(complanarDirectionRandomBase.IsDirectionComplanarToBase());
            Assert.False(insiderRandomBase.IsDirectionComplanarToBase());
        }

        [Test]
        public void IsPointOnBaseTriangleTest()
        {
            Assert.False(originRegBase.IsPointOnBaseTriangle());
            Assert.True(aRegBase.IsPointOnBaseTriangle());
            Assert.False(aTimesTwoRegBase.IsPointOnBaseTriangle());
            Assert.True(centerRegBase.IsPointOnBaseTriangle());
            Assert.False(outsiderRegBase.IsPointOnBaseTriangle());
            Assert.False(complanarOutsiderRegBase.IsPointOnBaseTriangle());
            Assert.False(complanarDirectionRegBase.IsPointOnBaseTriangle());
            Assert.False(insiderRegBase.IsPointOnBaseTriangle());

            Assert.False(originRandomBase.IsPointOnBaseTriangle());
            Assert.True(aRandomBase.IsPointOnBaseTriangle());
            Assert.False(aTimesTwoRandomBase.IsPointOnBaseTriangle());
            Assert.True(centerRandomBase.IsPointOnBaseTriangle());
            Assert.False(outsiderRandomBase.IsPointOnBaseTriangle());
            Assert.False(complanarOutsiderRandomBase.IsPointOnBaseTriangle());
            Assert.False(complanarDirectionRandomBase.IsPointOnBaseTriangle());
            Assert.False(insiderRandomBase.IsPointOnBaseTriangle());
        }

        [Test]
        public void FromPointTest()
        {
            Assert.Throws<Exception>(
               () => BarycentricVector.FromPoint(regularBase, new Vector3(1, 2, 3))
            );
            Assert.Throws<Exception>(
               () => BarycentricVector.FromPoint(randomBase, new Vector3(1, 2, 3))
            );

            Assert.AreEqual(
               new Vector3(1, 0, 0),
               BarycentricVector.FromPoint(regularBase, new Vector3(1, 0, 0)).Coordinates
            );

            Vector3 cartesianRegularBaseCentre = new Vector3(1, 1, 1) / 3.0f;
            AssertAreComponentWiseEquals(
                new Vector3(1, 0, 0),
                BarycentricVector.FromPoint(regularBase, new Vector3(1, 0, 0)).Coordinates,
                0.001f
             );

            Vector3 cartesianRandomBaseCentre =
                randomBase.a.Coordinates / 3.0f +
                randomBase.b.Coordinates / 3.0f +
                randomBase.c.Coordinates / 3.0f;
            AssertAreComponentWiseEquals(
                cartesianRandomBaseCentre,
                BarycentricVector.FromPoint(randomBase, cartesianRandomBaseCentre).Coordinates,
                0.001f
            );
        }

        [Test]
        public void DifferentBasesSamePointTest()
        {

        }

        [Test]
        public void ChangeBaseDoesNotChangePointTest()
        {
            Action<BarycentricVector, CartesianTriangle> assertionsToTest = (bv, otherBase) =>
            {
                Assert.AreEqual( // Change base to itself
                    bv.Coordinates,
                    bv.ChangeBase(bv.Base).Coordinates);
                Assert.AreEqual( // Change to other base
                    bv.Coordinates,
                    bv.ChangeBase(otherBase).Coordinates);
                Assert.AreEqual( // To other base and back
                    bv.Coordinates,
                    bv.ChangeBase(otherBase).ChangeBase(bv.Base).Coordinates);
            };

            CartesianTriangle otherRegBase =
                new CartesianTriangle(
                    new Vector3(0, 1, 0),
                    new Vector3(0, 0, 1),
                    new Vector3(1, 0, 0)
                );

            assertionsToTest(aRegBase, otherRegBase);

            assertionsToTest(centerRegBase, otherRegBase);

            // what happens when using planes that contains origin?
            // assertionsToTest(originRegBase, otherRegBase);
            // assertionsToTest(originRegBase, randomBase);
            // assertionsToTest(originRandomBase, otherRegBase);
            // assertionsToTest(originRandomBase, regularBase);

            // change base only for points that also are complanar to other base
            // until projection implemented

            // assertionsToTest(originRegBase, randomBase);
            // assertionsToTest(aRegBase, randomBase);
            // assertionsToTest(aTimesTwoRegBase, randomBase);
            // assertionsToTest(centerRegBase, randomBase);
            // assertionsToTest(outsiderRegBase, randomBase);
            // assertionsToTest(complanarOutsiderRegBase, randomBase);
            // assertionsToTest(complanarDirectionRegBase, randomBase);
            // assertionsToTest(insiderRegBase, randomBase);

            // assertionsToTest(originRandomBase, regularBase);
            // assertionsToTest(aRandomBase, regularBase);
            // assertionsToTest(aTimesTwoRandomBase, regularBase);
            // assertionsToTest(centerRandomBase, regularBase);
            // assertionsToTest(outsiderRandomBase, regularBase);
            // assertionsToTest(complanarOutsiderRegBase, regularBase);
            // assertionsToTest(complanarOutsiderRandomBase, regularBase);
            // assertionsToTest(insiderRandomBase, regularBase);
        }
    }
}
