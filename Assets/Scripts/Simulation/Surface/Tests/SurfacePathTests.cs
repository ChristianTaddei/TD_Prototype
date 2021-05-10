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

            // TODO: Corner Cases, what to do for paths on the other diagonal but with start and end on different faces?

            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.ACB_B, Rectangle_ABDE.m_AC_ACB, Rectangle_ABDE.AFC_F);
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.AFC_F, Rectangle_ABDE.m_AC_ACB, Rectangle_ABDE.ACB_B);

            SurfacePoint intersection = new SurfacePoint( // TODO: intersections in rectangle file
                Rectangle_ABDE.ACB,
                new BarycentricVector(
                    Rectangle_ABDE.ACB.Triangle,
                    new BarycentricCoordinates(2.0f / 3.0f, 1.0f / 3.0f, 0.0f)));
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.AFC_F, intersection, Rectangle_ABDE.m_AB_ACB);
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.m_AB_ACB, intersection, Rectangle_ABDE.AFC_F);

            intersection = new SurfacePoint(
                Rectangle_ABDE.ACB,
                new BarycentricVector(
                    Rectangle_ABDE.ACB.Triangle,
                    new BarycentricCoordinates(1.0f / 3.0f, 2.0f / 3.0f, 0.0f)));
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.ACB_B, intersection, Rectangle_ABDE.m_CF_AFC);
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.m_CF_AFC, intersection, Rectangle_ABDE.ACB_B);

            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.FDC_D, Rectangle_ABDE.m_CF_AFC, Rectangle_ABDE.AFC_A);
        }

        [Test]
        public void PointsOnFarFaces()
        {
            Maybe<SurfacePath> path = Rectangle_ABDE.Surface
                .MakeDirectPath(Rectangle_ABDE.FED_E, Rectangle_ABDE.ACB_B);

            Assert.True(path.HasValue());
            Assert.AreEqual(Rectangle_ABDE.FED_E, path.Value.Start);
            Assert.AreEqual(Rectangle_ABDE.ACB_B, path.Value.End);
            Assert.AreEqual(5, path.Value.Points.Count);

            SurfacePoint intersection = new SurfacePoint(
                Rectangle_ABDE.FED,
                new BarycentricVector(
                    Rectangle_ABDE.FED.Triangle,
                    new BarycentricCoordinates(2.0f / 3.0f, 0.0f, 1.0f / 3.0f)));
            Assert.AreEqual(intersection.Coordinates, path.Value.Points[1].Coordinates);

            Assert.AreEqual(Rectangle_ABDE.m_CF_AFC.Coordinates, path.Value.Points[2].Coordinates);

            intersection = new SurfacePoint(
                 Rectangle_ABDE.ACB,
                 new BarycentricVector(
                     Rectangle_ABDE.ACB.Triangle,
                     new BarycentricCoordinates(1.0f / 3.0f, 2.0f / 3.0f, 0.0f)));
            Assert.AreEqual(intersection.Coordinates, path.Value.Points[3].Coordinates);
        }
    }
}
