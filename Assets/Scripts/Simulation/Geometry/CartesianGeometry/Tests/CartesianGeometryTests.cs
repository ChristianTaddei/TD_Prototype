using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Moq;

namespace Tests
{  
	public class ConcreteGeometryTests 
	{
		ConcreteGeometry ConcreteGeometry;

		[SetUp]
		public void Setup()
		{
			ConcreteGeometry = new ConcreteGeometry();
		}

		[TearDown]
		public void TearDown()
		{

		}

		// TODO: test all
		
		[Test]
		public void areCloserThanTollerance_vectorsCloser_true()
		{
			ConcreteVector v1 = new ConcreteVector(1, 0, 0); 
			ConcreteVector v2 = new ConcreteVector(1.0000001f, 0, 0);

			bool areCloser = ConcreteGeometry.AreCloserThanTollerance(v1, v2);

			Assert.True(areCloser);
		}

		[Test]
		public void areCloserThanTollerance_vectorsFurther_false()
		{

			ConcreteVector v1 = new ConcreteVector(1, 0, 0);
			ConcreteVector v2 = new ConcreteVector(1.1f, 0, 0);

			bool areCloser = ConcreteGeometry.AreCloserThanTollerance(v1, v2);

			Assert.False(areCloser);
		}

		[Test]
		public void project_predefinedValues_predefinedResults()
		{
			ConcreteTriangle Plane_Oz = new ConcreteTriangle(
				new ConcreteVector(0, 0, 0),
				new ConcreteVector(1, 0, 0),
				new ConcreteVector(0, 1, 0));

			ConcreteVector cv = new ConcreteVector(0, 0, 1);

			Assert.AreEqual(new Vector3(0, 0, 0), ConcreteGeometry.Project(cv, Plane_Oz).FloatRepresentation);

			ConcreteTriangle ShiftedPlane_Oz = new ConcreteTriangle(
				new ConcreteVector(0, 0, 1),
				new ConcreteVector(1, 0, 1),
				new ConcreteVector(0, 1, 1));

			cv = new ConcreteVector(0, 0, 0);

			Assert.AreEqual(new Vector3(0, 0, 1), ConcreteGeometry.Project(cv, ShiftedPlane_Oz).FloatRepresentation);

			ConcreteTriangle ShiftedPlane_Oy = new ConcreteTriangle(
				new ConcreteVector(1, 1, 0),
				new ConcreteVector(0, 1, 1),
				new ConcreteVector(0, 1, 0));

			cv = new ConcreteVector(3, 3, 3);

			Assert.AreEqual(new Vector3(3, 1, 3), ConcreteGeometry.Project(cv, ShiftedPlane_Oy).FloatRepresentation);

			cv = new ConcreteVector(-3, -3, -3);

			Assert.AreEqual(new Vector3(-3, 1, -3), ConcreteGeometry.Project(cv, ShiftedPlane_Oy).FloatRepresentation);

			ConcreteTriangle Plane_45degs = new ConcreteTriangle(
				new ConcreteVector(0, 0, 0),
				new ConcreteVector(1, 0, 1),
				new ConcreteVector(0, 1, 1));

			cv = new ConcreteVector(0.5f, 0.5f, 1.0f);
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), ConcreteGeometry.Project(cv, Plane_45degs).FloatRepresentation);

			cv = new ConcreteVector(-0.5f, -0.5f, 2.0f);
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), ConcreteGeometry.Project(cv, Plane_45degs).FloatRepresentation);

			cv = new ConcreteVector(0.0f, 0.0f, 1.5f);
			Assert.AreEqual(new Vector3(0.5f, 0.5f, 1.0f), ConcreteGeometry.Project(cv, Plane_45degs).FloatRepresentation);
		}
	}
}
