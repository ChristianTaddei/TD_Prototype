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
                new List<TriVertexNames>(new TriVertexNames[]{TriVertexNames.B, TriVertexNames.C}),
                TestSurfaceElements.Triangle_abc.GetSharedVertices( TestSurfaceElements.Triangle_bcd));
        }
    }
}
