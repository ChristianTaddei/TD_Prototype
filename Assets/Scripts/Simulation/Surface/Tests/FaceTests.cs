using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class FaceTests
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
        public void SingleFaceNoNeighbours()
        {

        }

        [Test]
        public void FacesShareVertices()
        {
            // Assert.AreEqual(
            //     new HashSet<TriangleVertices>(new TriangleVertices[] { TriangleVertices.B, TriangleVertices.C }),
            //     Square_ABCD.ABC.GetSharedVertices(Square_ABCD.BCD));

            // Assert.AreEqual(
            //     new HashSet<TriangleVertices>(new TriangleVertices[] { TriangleVertices.A, TriangleVertices.B }),
            //     Square_ABCD.BCD.GetSharedVertices(Square_ABCD.ABC));
        }

        [Test]
        public void GetFaceFromSharedVertices()
        {
            // HashSet<Face> facesSharingVertices =
            //     Square_ABCD.ABC.GetFacesFromSharedVertices(
            //         new HashSet<TriangleVertices>(new TriangleVertices[] { TriangleVertices.B, TriangleVertices.C }));

            // Assert.AreEqual(1, facesSharingVertices.Count);
            // Assert.True(facesSharingVertices.Contains(Square_ABCD.BCD));
        }
    }
}
