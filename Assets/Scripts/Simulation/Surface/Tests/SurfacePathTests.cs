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
            AssertPathIsJustStartAndEnd(Square_ABCD.ABC_A, Square_ABCD.ABC_B);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_D, Square_ABCD.ADC_A);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_C, Square_ABCD.ADC_D);
            AssertPathIsJustStartAndEnd(Square_ABCD.CenterOnABC, Square_ABCD.ABC_B);
            AssertPathIsJustStartAndEnd(Square_ABCD.ABC_C, Square_ABCD.CenterOnABC);
            AssertPathIsJustStartAndEnd(Square_ABCD.Barycentre_ADC, Square_ABCD.ADC_D);
            AssertPathIsJustStartAndEnd(Square_ABCD.ADC_D, Square_ABCD.Barycentre_ADC);
            AssertPathIsJustStartAndEnd(Square_ABCD.PointNotOnEdge_ABC, Square_ABCD.Barycentre_ABC);
            // TODO: tests points not on edges
        }

        [Test]
        public void PointsOnNeighbourFaces()
        {


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
        }

        [Test]
        public void PointsOnNonComplanarFaces()
        {
            /*
                C <<< B 
                |  \  v  
                D --- A 
            */

            // AssertPathIsJustStartAndEnd(FoldedSquare_ABCD.ADC_C, FoldedSquare_ABCD.ABC_A); // Corner
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
    }
}
