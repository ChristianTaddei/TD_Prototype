using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System;

namespace Tests
{
    public class SurfacePathTests
    {
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void DisjointedFacesNoPath()
        {
            Maybe<SurfacePath> path =
                DisjointedSurface.Surface
                    .MakeDirectPath(
                        DisjointedSurface.PointOn1,
                        DisjointedSurface.PointOn2);

            Assert.False(path.HasValue());
        }

        [Test]
        public void PointsOnSameFace()
        {
            Action<SurfacePoint, SurfacePoint> AssertPathIsJustStartAndEnd =
                (SurfacePoint start, SurfacePoint end) =>
                    {
                        Maybe<SurfacePath> path = start.Face.Surface.MakeDirectPath(start, end);

                        Assert.True(path.HasValue());
                        Assert.AreEqual(2, path.Value.Points.Count);
                        Assert.AreEqual(start, path.Value.Start);
                        Assert.AreEqual(end, path.Value.End);
                    };

            AssertPathIsJustStartAndEnd(Square_ABCD.ABC_A, Square_ABCD.ABC_B);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_D, Square_ABCD.ADC_A);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_C, Square_ABCD.ADC_D);
            AssertPathIsJustStartAndEnd(Square_ABCD.CenterOnABC, Square_ABCD.ABC_B);
            AssertPathIsJustStartAndEnd(Square_ABCD.ABC_C, Square_ABCD.CenterOnABC);
            AssertPathIsJustStartAndEnd(Square_ABCD.Barycentre_ADC, Square_ABCD.ADC_D);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_D, Square_ABCD.Barycentre_ADC);
            AssertPathIsJustStartAndEnd(Square_ABCD.PointNotOnEdge_ABC, Square_ABCD.Barycentre_ABC);
            // TODO: tests on points not on edges
        }

        [Test]
        public void PointsOnNeighbourFaces()
        {
            Action<SurfacePoint, SurfacePoint, SurfacePoint> AssertPathHasOnlyOneInstersection =
                (SurfacePoint start, SurfacePoint intersection, SurfacePoint end) =>
                    {
                        Maybe<SurfacePath> path = start.Face.Surface.MakeDirectPath(start, end);

                        Assert.True(path.HasValue());
                        Assert.AreEqual(3, path.Value.Points.Count);
                        Assert.AreEqual(start, path.Value.Start);
                        Assert.AreEqual(end, path.Value.End);
                        Assert.AreEqual(intersection.Coordinates, path.Value.Points[1].Coordinates);
                    };

            AssertPathHasOnlyOneInstersection(Square_ABCD.ABC_B, Square_ABCD.CenterOnABC, Square_ABCD.ADC_D);
            AssertPathHasOnlyOneInstersection(Square_ABCD.ADC_D, Square_ABCD.CenterOnABC, Square_ABCD.ABC_B);
            AssertPathHasOnlyOneInstersection(Square_ABCD.ABC_B, Square_ABCD.CenterOnADC, Square_ABCD.ADC_D);
            AssertPathHasOnlyOneInstersection(Square_ABCD.ADC_D, Square_ABCD.CenterOnADC, Square_ABCD.ABC_B);

            // TODO: what to do for paths on the other diagonal but with start and end on different faces?

            AssertPathHasOnlyOneInstersection(Rectangle_ACDF.ACB_B, Rectangle_ACDF.m_AC_ACB, Rectangle_ACDF.AFC_F);
            AssertPathHasOnlyOneInstersection(Rectangle_ACDF.AFC_F, Rectangle_ACDF.m_AC_ACB, Rectangle_ACDF.ACB_B);

            SurfacePoint intersection = new SurfacePoint( // TODO: all intersections in rectangle?
                Rectangle_ACDF.ACB,
                new BarycentricVector(
                    Rectangle_ACDF.ACB.Triangle,
                    new BarycentricCoordinates(2.0f / 3.0f, 1.0f / 3.0f, 0.0f)));
            AssertPathHasOnlyOneInstersection(Rectangle_ACDF.AFC_F, intersection, Rectangle_ACDF.m_AB_ACB);
            AssertPathHasOnlyOneInstersection(Rectangle_ACDF.m_AB_ACB, intersection, Rectangle_ACDF.AFC_F);

            intersection = new SurfacePoint(
                Rectangle_ACDF.ACB,
                new BarycentricVector(
                    Rectangle_ACDF.ACB.Triangle,
                    new BarycentricCoordinates(1.0f / 3.0f, 2.0f / 3.0f, 0.0f)));
            AssertPathHasOnlyOneInstersection(Rectangle_ACDF.ACB_B, intersection, Rectangle_ACDF.m_CF_AFC);
            AssertPathHasOnlyOneInstersection(Rectangle_ACDF.m_CF_AFC, intersection, Rectangle_ACDF.ACB_B);

            AssertPathHasOnlyOneInstersection(Rectangle_ACDF.FDC_D, Rectangle_ACDF.m_CF_AFC, Rectangle_ACDF.AFC_A);
        }

        [Test]
        public void PointsOnFarFaces()
        {
            Maybe<SurfacePath> path = Rectangle_ACDF.Surface
                .MakeDirectPath(Rectangle_ACDF.FED_E, Rectangle_ACDF.ACB_B);

            Assert.True(path.HasValue());
            Assert.AreEqual(Rectangle_ACDF.FED_E, path.Value.Start);
            Assert.AreEqual(Rectangle_ACDF.ACB_B, path.Value.End);
            Assert.AreEqual(5, path.Value.Points.Count);

            SurfacePoint intersection = new SurfacePoint(
                Rectangle_ACDF.FED,
                new BarycentricVector(
                    Rectangle_ACDF.FED.Triangle,
                    new BarycentricCoordinates(2.0f / 3.0f, 0.0f, 1.0f / 3.0f)));
            Assert.AreEqual(intersection.Coordinates, path.Value.Points[1].Coordinates);

            Assert.AreEqual(Rectangle_ACDF.m_CF_AFC.Coordinates, path.Value.Points[2].Coordinates);

            intersection = new SurfacePoint(
                 Rectangle_ACDF.ACB,
                 new BarycentricVector(
                     Rectangle_ACDF.ACB.Triangle,
                     new BarycentricCoordinates(1.0f / 3.0f, 2.0f / 3.0f, 0.0f)));
            Assert.AreEqual(intersection.Coordinates, path.Value.Points[3].Coordinates);
        }
    }
}
