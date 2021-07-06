using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Moq;

namespace Tests
{  
	public class CartesianGeometryTests 
	{
		VectorialGeometry cartesianGeometry;

		[SetUp]
		public void Setup()
		{
			cartesianGeometry = new VectorialGeometry();
		}

		[TearDown]
		public void TearDown()
		{

		}

		// TODO: test all
		
		[Test]
		public void areCloserThanTollerance_vectorsCloser_true()
		{
			CartesianVector v1 = new CartesianVector(1, 0, 0); 
			CartesianVector v2 = new CartesianVector(1.0000001f, 0, 0);

			bool areCloser = cartesianGeometry.AreCloserThanTollerance(v1, v2);

			Assert.True(areCloser);
		}

		[Test]
		public void areCloserThanTollerance_vectorsFurther_false()
		{

			CartesianVector v1 = new CartesianVector(1, 0, 0);
			CartesianVector v2 = new CartesianVector(1.1f, 0, 0);

			bool areCloser = cartesianGeometry.AreCloserThanTollerance(v1, v2);

			Assert.False(areCloser);
		}

		[Test]
		public void project_predefinedValues_predefinedResults()
		{
			ConcreteTriangle Plane_Oz = new ConcreteTriangle(
				new CartesianVector(0, 0, 0),
				new CartesianVector(1, 0, 0),
				new CartesianVector(0, 1, 0));

			CartesianVector cv = new CartesianVector(0, 0, 1);

			Assert.AreEqual(new Vector3(0, 0, 0), cartesianGeometry.Project(cv, Plane_Oz).FloatRepresentation);

			ConcreteTriangle ShiftedPlane_Oz = new ConcreteTriangle(
				new CartesianVector(0, 0, 1),
				new CartesianVector(1, 0, 1),
				new CartesianVector(0, 1, 1));

			cv = new CartesianVector(0, 0, 0);

			Assert.AreEqual(new Vector3(0, 0, 1), cartesianGeometry.Project(cv, ShiftedPlane_Oz).FloatRepresentation);

			ConcreteTriangle ShiftedPlane_Oy = new ConcreteTriangle(
				new CartesianVector(1, 1, 0),
				new CartesianVector(0, 1, 1),
				new CartesianVector(0, 1, 0));

			cv = new CartesianVector(3, 3, 3);

			Assert.AreEqual(new Vector3(3, 1, 3), cartesianGeometry.Project(cv, ShiftedPlane_Oy).FloatRepresentation);

			cv = new CartesianVector(-3, -3, -3);

			Assert.AreEqual(new Vector3(-3, 1, -3), cartesianGeometry.Project(cv, ShiftedPlane_Oy).FloatRepresentation);

			ConcreteTriangle Plane_45degs = new ConcreteTriangle(
				new CartesianVector(0, 0, 0),
				new CartesianVector(1, 0, 1),
				new CartesianVector(0, 1, 1));

			cv = new CartesianVector(0.5f, 0.5f, 1.0f);
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cartesianGeometry.Project(cv, Plane_45degs).FloatRepresentation);

			cv = new CartesianVector(-0.5f, -0.5f, 2.0f);
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cartesianGeometry.Project(cv, Plane_45degs).FloatRepresentation);

			cv = new CartesianVector(0.0f, 0.0f, 1.5f);
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), cartesianGeometry.Project(cv, Plane_45degs).FloatRepresentation);
		}
	}
}
