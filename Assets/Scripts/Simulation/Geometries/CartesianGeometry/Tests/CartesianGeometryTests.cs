using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
	public class CartesianGeometryTests
	{
		CartesianGeometry cartesianGeometry;

		[SetUp]
		public void Setup()
		{
			cartesianGeometry = new CartesianGeometry();
		}

		[TearDown]
		public void TearDown()
		{

		}

		// TODO: to geometry too
		[Test]
		public void equals_vectorsCloserThanTollerance_vectorsEqual()
		{

		}

		[Test]
		public void equals_vectorsFurtherThanTollerance_vectorsNotEqual()
		{

		}

		// TODO: creation from vec3 respect certain rules -> factory tests
		// (does not moves fRep more than x, magnitude within y? in this case 0 and 0)

		// TODO: to cart geometry tests (+ other geom methods)
		[Test]
		public void TestProject()
		{
			CartesianTriangle Plane_Oz = new CartesianTriangle(
				new CartesianVector(new Vector3(0, 0, 0)), // TODO: this should be automatic, other way rather than specific CV constructor?
				new CartesianVector(new Vector3(1, 0, 0)),
				new CartesianVector(new Vector3(0, 1, 0)));

			CartesianVector cv = new CartesianVector(new Vector3(0, 0, 1));

			Assert.AreEqual(new Vector3(0, 0, 0), cartesianGeometry.Project(cv, Plane_Oz).FloatRepresentation);

			CartesianTriangle ShiftedPlane_Oz = new CartesianTriangle(
				new CartesianVector(new Vector3(0, 0, 1)),
				new CartesianVector(new Vector3(1, 0, 1)),
				new CartesianVector(new Vector3(0, 1, 1)));

			cv = new CartesianVector(new Vector3(0, 0, 0));

			Assert.AreEqual(new Vector3(0, 0, 1), cartesianGeometry.Project(cv, ShiftedPlane_Oz).FloatRepresentation);

			CartesianTriangle ShiftedPlane_Oy = new CartesianTriangle(
				new CartesianVector(new Vector3(1, 1, 0)),
				new CartesianVector(new Vector3(0, 1, 1)),
				new CartesianVector(new Vector3(0, 1, 0)));

			cv = new CartesianVector(new Vector3(3, 3, 3));

			Assert.AreEqual(new Vector3(3, 1, 3), cartesianGeometry.Project(cv, ShiftedPlane_Oy).FloatRepresentation);

			cv = new CartesianVector(new Vector3(-3, -3, -3));

			Assert.AreEqual(new Vector3(-3, 1, -3), cartesianGeometry.Project(cv, ShiftedPlane_Oy).FloatRepresentation);

			CartesianTriangle Plane_45degs = new CartesianTriangle(
				new CartesianVector(new Vector3(0, 0, 0)),
				new CartesianVector(new Vector3(1, 0, 1)),
				new CartesianVector(new Vector3(0, 1, 1)));

			cv = new CartesianVector(new Vector3(0.5f, 0.5f, 1.0f));
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cartesianGeometry.Project(cv, Plane_45degs).FloatRepresentation);

			cv = new CartesianVector(new Vector3(-0.5f, -0.5f, 2.0f));
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cartesianGeometry.Project(cv, Plane_45degs).FloatRepresentation);

			cv = new CartesianVector(new Vector3(0.0f, 0.0f, 1.5f));
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cartesianGeometry.Project(cv, Plane_45degs).FloatRepresentation);
		}
	}
}
