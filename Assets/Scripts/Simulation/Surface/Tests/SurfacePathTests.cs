using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class SurfacePathTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        # region Common assertions
        // TODO: when going for noncomplanars we need toleance, where to define it?
        Action<SurfacePoint, SurfacePoint> AssertAreSamePosition =
            (SurfacePoint a, SurfacePoint b) =>
                {
                    Assert.True(UnityEngine.Mathf.Abs(a.Position.x - b.Position.x) < 0.0001f);
                    Assert.True(UnityEngine.Mathf.Abs(a.Position.y - b.Position.y) < 0.0001f);
                    Assert.True(UnityEngine.Mathf.Abs(a.Position.z - b.Position.z) < 0.0001f);
                };

        Action<SurfacePoint, SurfacePoint, SurfacePoint> AssertPathHasOnlyOneInstersection =
            (SurfacePoint start, SurfacePoint intersection, SurfacePoint end) =>
                {
                    Maybe<SurfacePath> path = start.Face.Surface.MakeDirectPath(start, end);

                    Assert.True(path.HasValue());
                    Assert.AreEqual(3, path.Value.Points.Count);
                    Assert.AreEqual(start, path.Value.Start);
                    Assert.AreEqual(end, path.Value.End);
                    Assert.AreEqual(intersection.Position, path.Value.Points[1].Position);
                };

        Action<SurfacePoint, SurfacePoint> AssertPathIsJustStartAndEnd =
            (SurfacePoint start, SurfacePoint end) =>
                {
                    Maybe<SurfacePath> path = start.Face.Surface.MakeDirectPath(start, end);

                    Assert.True(path.HasValue());
                    Assert.AreEqual(2, path.Value.Points.Count);
                    Assert.AreEqual(start, path.Value.Start);
                    Assert.AreEqual(end, path.Value.End);
                };
        #endregion

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
            AssertPathIsJustStartAndEnd(Square_ABCD.ACB_A, Square_ABCD.ACB_B);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_D, Square_ABCD.ADC_A);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_C, Square_ABCD.ADC_D);
            AssertPathIsJustStartAndEnd(Square_ABCD.CenterOnACB, Square_ABCD.ACB_B);
            AssertPathIsJustStartAndEnd(Square_ABCD.ACB_C, Square_ABCD.CenterOnACB);
            AssertPathIsJustStartAndEnd(Square_ABCD.Barycentre_ADC, Square_ABCD.ADC_D);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_D, Square_ABCD.Barycentre_ADC);
            AssertPathIsJustStartAndEnd(Square_ABCD.PointNotOnEdge_ACB, Square_ABCD.Barycentre_ACB);
            // TODO: tests points not on edges
        }

        [Test]
        public void PointsOnNearFaces()
        {
            AssertPathHasOnlyOneInstersection(Square_ABCD.ACB_B, Square_ABCD.CenterOnACB, Square_ABCD.ADC_D);
            AssertPathHasOnlyOneInstersection(Square_ABCD.ADC_D, Square_ABCD.CenterOnACB, Square_ABCD.ACB_B);
            AssertPathHasOnlyOneInstersection(Square_ABCD.ACB_B, Square_ABCD.CenterOnADC, Square_ABCD.ADC_D);
            AssertPathHasOnlyOneInstersection(Square_ABCD.ADC_D, Square_ABCD.CenterOnADC, Square_ABCD.ACB_B);

            // TODO: Corner Cases, what to do for paths on the other diagonal but with start and end on different faces?

            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.ACB_B, Rectangle_ABDE.m_AC_ACB, Rectangle_ABDE.AFC_F);
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.AFC_F, Rectangle_ABDE.m_AC_ACB, Rectangle_ABDE.ACB_B);

            SurfacePoint intersection = new SurfacePoint( // TODO: intersections in rectangle file
                Rectangle_ABDE.ACB,
                new BarycentricVector(
                    Rectangle_ABDE.ACB,
                    new BarycentricCoordinates(2.0f / 3.0f, 1.0f / 3.0f, 0.0f)));
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.AFC_F, intersection, Rectangle_ABDE.m_AB_ACB);
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.m_AB_ACB, intersection, Rectangle_ABDE.AFC_F);

            intersection = new SurfacePoint(
                Rectangle_ABDE.ACB,
                new BarycentricVector(
                    Rectangle_ABDE.ACB,
                    new BarycentricCoordinates(1.0f / 3.0f, 2.0f / 3.0f, 0.0f)));
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.ACB_B, intersection, Rectangle_ABDE.m_CF_AFC);
            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.m_CF_AFC, intersection, Rectangle_ABDE.ACB_B);

            AssertPathHasOnlyOneInstersection(Rectangle_ABDE.FDC_D, Rectangle_ABDE.m_CF_AFC, Rectangle_ABDE.AFC_A);
        }

        [Test]
        public void PointsOnFarFaces()
        {

            #region Bottom left to top right
            Maybe<SurfacePath> path = Rectangle_ABDE.Surface
                .MakeDirectPath(Rectangle_ABDE.FED_E, Rectangle_ABDE.ACB_B);

            Assert.True(path.HasValue());
            Assert.AreEqual(Rectangle_ABDE.FED_E, path.Value.Start);
            Assert.AreEqual(Rectangle_ABDE.ACB_B, path.Value.End);
            Assert.AreEqual(5, path.Value.Points.Count);

            SurfacePoint intersection = new SurfacePoint(
                Rectangle_ABDE.FED,
                new BarycentricVector(
                    Rectangle_ABDE.FED,
                    new BarycentricCoordinates(2.0f / 3.0f, 0.0f, 1.0f / 3.0f)));
            Assert.AreEqual(intersection.Position, path.Value.Points[1].Position);

            Assert.AreEqual(Rectangle_ABDE.m_CF_AFC.Position, path.Value.Points[2].Position);

            intersection = new SurfacePoint(
                 Rectangle_ABDE.ACB,
                 new BarycentricVector(
                     Rectangle_ABDE.ACB,
                     new BarycentricCoordinates(1.0f / 3.0f, 2.0f / 3.0f, 0.0f)));
            Assert.AreEqual(intersection.Position, path.Value.Points[3].Position);
            #endregion
        }

        [Test]
        public void PointsOnNonComplanarNearFaces()
        {
            AssertPathIsJustStartAndEnd(FoldedSquare_ABCD.ABC_B, FoldedSquare_ABCD.ABC_A);
            AssertPathIsJustStartAndEnd(FoldedSquare_ABCD.ABC_C, FoldedSquare_ABCD.ABC_B);

            AssertPathHasOnlyOneInstersection(FoldedSquare_ABCD.ADC_D, FoldedSquare_ABCD.CenterOnABC, FoldedSquare_ABCD.ABC_B);
            AssertPathHasOnlyOneInstersection(FoldedSquare_ABCD.ADC_D, FoldedSquare_ABCD.CenterOnADC, FoldedSquare_ABCD.ABC_B);
            AssertPathHasOnlyOneInstersection(FoldedSquare_ABCD.ABC_B, FoldedSquare_ABCD.CenterOnABC, FoldedSquare_ABCD.ADC_D);
            AssertPathHasOnlyOneInstersection(FoldedSquare_ABCD.ABC_B, FoldedSquare_ABCD.CenterOnADC, FoldedSquare_ABCD.ADC_D);

            Maybe<SurfacePath> path = FoldedRectangle_ABDE.Surface
                .MakeDirectPath(FoldedRectangle_ABDE.FED_E, FoldedRectangle_ABDE.FDC_C);

            Assert.True(path.HasValue());
            Assert.AreEqual(FoldedRectangle_ABDE.FED_E, path.Value.Start);
            Assert.AreEqual(FoldedRectangle_ABDE.FDC_C, path.Value.End);
            Assert.AreEqual(3, path.Value.Points.Count);

            AssertAreSamePosition(FoldedRectangle_ABDE.m_FD_FDC, path.Value.Points[1]);
        }

        [Test]
        public void PointsOnNonComplanarFarFaces()
        {
            Maybe<SurfacePath> path = FoldedRectangle_ABDE.Surface
                    .MakeDirectPath(FoldedRectangle_ABDE.FED_E, FoldedRectangle_ABDE.ACB_B);

            Assert.True(path.HasValue());
            Assert.AreEqual(FoldedRectangle_ABDE.FED_E, path.Value.Start);
            Assert.AreEqual(FoldedRectangle_ABDE.ACB_B, path.Value.End);
            Assert.AreEqual(5, path.Value.Points.Count);

            SurfacePoint intersection = new SurfacePoint(
                FoldedRectangle_ABDE.FED,
                new BarycentricVector(
                    FoldedRectangle_ABDE.FED,
                    new BarycentricCoordinates(2.0f / 3.0f, 0.0f, 1.0f / 3.0f)));

            AssertAreSamePosition(intersection, path.Value.Points[1]);
            AssertAreSamePosition(FoldedRectangle_ABDE.m_CF_AFC, path.Value.Points[2]);
            // points[3]
        }

        // TODO: auto project, so check proj on high surfaces{

        [Test]
        public void LargeSquareLongDiagonalBackUp()
        {
            SurfacePoint start = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(3.122f, 0, 2.05f), out start));
            // Assert.AreEqual(new Vector3(3.1f, 0, 2.1f), start.Position);

            SurfacePoint end = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(0.8f, 0, 5.1f), out end));
            // Assert.AreEqual(new Vector3(0.8f, 0, 5.1f), end.Position);

            Maybe<SurfacePath> path = LargeSquare.Surface.MakeDirectPath(start, end);
            Assert.True(path.HasValue());
            // Assert.AreEqual(8, path.Value.Points.Count);

            Assert.AreEqual(new HashSet<SurfacePoint>(path.Value.Points).Count, path.Value.Points.Count);
            bool duplicates = false;
            foreach (SurfacePoint p1 in path.Value.Points)
            {
                foreach (SurfacePoint p2 in path.Value.Points)
                {
                    if (
                        p1 != p2 &&
                        UnityEngine.Mathf.Abs(p1.Position.x - p2.Position.x) < 0.0001f
                        && UnityEngine.Mathf.Abs(p1.Position.y - p2.Position.y) < 0.0001f
                        && UnityEngine.Mathf.Abs(p1.Position.z - p2.Position.z) < 0.0001f)
                    {
                        duplicates = true;
                    }
                }
            }
            Assert.False(duplicates);

        }

        [Test]
        public void CornerCases()
        {
            // AssertPathIsJustStartAndEnd(Square_ABCD.ADC_C, Square_ABCD.ACB_A);

            SurfacePoint start = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(1.0f, 0.0f, 5.2f), out start));

            SurfacePoint end = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(1.0f, 0, 0.8f), out end));

            Maybe<SurfacePath> path = LargeSquare.Surface.MakeDirectPath(start, end);

            Assert.True(path.HasValue());
            Assert.AreEqual(7, path.Value.Points.Count);
            Assert.AreEqual(start, path.Value.Start);
            Assert.AreEqual(end, path.Value.End);

            Vector3 intersection1 = new Vector3(1.0f, 0, 5.0f);
            Vector3 intersection2 = new Vector3(1.0f, 0, 4.0f);
            Vector3 intersection3 = new Vector3(1.0f, 0, 3.0f);
            Vector3 intersection4 = new Vector3(1.0f, 0, 2.0f);
            Vector3 intersection5 = new Vector3(1.0f, 0, 1.0f);

            Assert.AreEqual(intersection1, path.Value.Points[1].Position);
            Assert.AreEqual(intersection2, path.Value.Points[2].Position);
            Assert.AreEqual(intersection3, path.Value.Points[3].Position);
            Assert.AreEqual(intersection4, path.Value.Points[4].Position);
            Assert.AreEqual(intersection5, path.Value.Points[5].Position);

            start = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(1.0f, 0.0f, 5.0f), out start));

            end = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(1.0f, 0, 1.0f), out end));

            path = LargeSquare.Surface.MakeDirectPath(start, end);

            Assert.True(path.HasValue());
            Assert.AreEqual(5, path.Value.Points.Count);
            Assert.AreEqual(start, path.Value.Start);
            Assert.AreEqual(end, path.Value.End);

            Assert.AreEqual(intersection1, path.Value.Points[0].Position);
            Assert.AreEqual(intersection2, path.Value.Points[1].Position);
            Assert.AreEqual(intersection3, path.Value.Points[2].Position);
            Assert.AreEqual(intersection4, path.Value.Points[3].Position);
            Assert.AreEqual(intersection5, path.Value.Points[4].Position);

            start = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(7.0f, 0.0f, 3.0f), out start));

            end = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(7.0f, 0, 9.0f), out end));

            path = LargeSquare.Surface.MakeDirectPath(start, end);

            Assert.True(path.HasValue());
            Assert.AreEqual(7, path.Value.Points.Count);
            Assert.AreEqual(start, path.Value.Start);
            Assert.AreEqual(end, path.Value.End);
        }

        [Test]
        public void CornerCases2()
        {
            SurfacePoint start = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(3.0f, 0.0f, 2.0f), out start));

            SurfacePoint end = default;
            Assert.True(LargeSquare.Surface.TryGetSurfacePointFromPosition(new Vector3(7.0f, 0, 9.0f), out end));

            Maybe<SurfacePath> path = LargeSquare.Surface.MakeDirectPath(start, end);

            Assert.True(path.HasValue());
            Assert.AreEqual(21, path.Value.Points.Count);
            Assert.AreEqual(start, path.Value.Start);
            Assert.AreEqual(end, path.Value.End);
        }

        [Test]
        public void PathFromVertexInAllDirections()
        {
            // For the 6 faces containing centre, ensure it reaches all 5 extern points not on that face
            // should be enough for 1 face? who cares which coordinates...

            // Diagonals
            //11->00
            AssertPathHasOnlyOneInstersection(
                Square2x2.centre_10_11_20,
                Square2x2.m_01_10,
                Square2x2._00on_00_01_10);

            //11->02
            AssertPathIsJustStartAndEnd(
                Square2x2.centre_10_11_20,
                Square2x2._02on_01_02_11);

            //11->22
            AssertPathHasOnlyOneInstersection(
                Square2x2.centre_10_11_20,
                Square2x2.m_12_21,
                Square2x2._22on_12_22_21);

            //11->20
            AssertPathIsJustStartAndEnd(
                Square2x2.centre_10_11_20,
                Square2x2._20on_10_11_20);

            // Verticals
            //11->12
            AssertPathIsJustStartAndEnd(
                Square2x2.centre_10_11_20,
                Square2x2._12on_02_12_11);

            //11->10
            AssertPathIsJustStartAndEnd(
                Square2x2.centre_10_11_20,
                Square2x2._10on_00_01_10);

            //Horizontals
            //11->01
            AssertPathIsJustStartAndEnd(
                Square2x2.centre_10_11_20,
                Square2x2._01on_00_01_10);

            //11->21
            AssertPathIsJustStartAndEnd(
                Square2x2.centre_10_11_20,
                Square2x2._21on_11_21_20);
        }
    }
}
