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
            Assert.AreEqual(
                new HashSet<TriVertexNames>(new TriVertexNames[] { TriVertexNames.B, TriVertexNames.C }),
                TestSurfaceElements.Triangle_abc.GetSharedVertices(TestSurfaceElements.Triangle_bcd));

            Assert.AreEqual(
                new HashSet<TriVertexNames>(new TriVertexNames[] { TriVertexNames.A, TriVertexNames.B }),
                TestSurfaceElements.Triangle_bcd.GetSharedVertices(TestSurfaceElements.Triangle_abc));
        }

        [Test]
        public void GetFaceFromSharedVertices()
        {
            HashSet<Face> facesSharingVertices =
                TestSurfaceElements.Triangle_abc.GetFacesFromSharedVertices(
                    new HashSet<TriVertexNames>(new TriVertexNames[] { TriVertexNames.A, TriVertexNames.B }));

            Assert.AreEqual(1, facesSharingVertices.Count);
            Assert.True(facesSharingVertices.Contains(TestSurfaceElements.Triangle_bcd));
        }
    }
}
