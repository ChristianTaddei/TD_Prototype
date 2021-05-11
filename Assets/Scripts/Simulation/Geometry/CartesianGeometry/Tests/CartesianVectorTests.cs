using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class CartesianVectorTests
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
        public void TestProject()
        {
            Triangle Plane_Oz = new Triangle(
                new CartesianVector(new Vector3(0, 0, 0)), // TODO: this should be automatic, other way rather than specific CV constructor?
                new CartesianVector(new Vector3(1, 0, 0)),
                new CartesianVector(new Vector3(0, 1, 0)));

            CartesianVector cv = new Vector3(0, 0, 1);

            Assert.AreEqual(new Vector3(0, 0, 0), cv.Project(Plane_Oz).Position);

            Triangle ShiftedPlane_Oz = new Triangle(
                new CartesianVector(new Vector3(0, 0, 1)),
                new CartesianVector(new Vector3(1, 0, 1)),
                new CartesianVector(new Vector3(0, 1, 1)));

            cv = new Vector3(0,0,0);
           
            Assert.AreEqual(new Vector3(0, 0, 1), cv.Project(ShiftedPlane_Oz).Position);
            
            Triangle ShiftedPlane_Oy = new Triangle(
                new CartesianVector(new Vector3(1, 1, 0)),
                new CartesianVector(new Vector3(0, 1, 1)),
                new CartesianVector(new Vector3(0, 1, 0)));

            cv = new Vector3(3,3,3);
           
            Assert.AreEqual(new Vector3(3, 1, 3), cv.Project(ShiftedPlane_Oy).Position);
            
            cv = new Vector3(-3,-3,-3);

            Assert.AreEqual(new Vector3(-3, 1, -3), cv.Project(ShiftedPlane_Oy).Position);

            Triangle Plane_45degs = new Triangle(
                new CartesianVector(new Vector3(0, 0, 0)),
                new CartesianVector(new Vector3(1, 0, 1)),
                new CartesianVector(new Vector3(0, 1, 1)));

            cv = new Vector3(0.5f, 0.5f, 1.0f);
            Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cv.Project(Plane_45degs).Position);

            cv = new Vector3(-0.5f, -0.5f, 2.0f);
            Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cv.Project(Plane_45degs).Position);

            cv = new Vector3(0.0f, 0.0f, 1.5f);
            Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cv.Project(Plane_45degs).Position);
        }
    }
}
