using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

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
        public void SameFaceHasDirectPath()
        {
            // TODO: should use different variables?
            Maybe<SurfacePath> path =
                Square_ABCD.Surface
                    .MakeDirectPath(
                        Square_ABCD.A,
                        Square_ABCD.B_ABC);

            Assert.True(path.HasValue());
            Assert.AreEqual(
                Square_ABCD.A,
                path.Value.Start);
            Assert.AreEqual(
                Square_ABCD.B_ABC,
                path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.B_ABC, Square_ABCD.C_ABC);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.B_ABC, path.Value.Start);
            Assert.AreEqual(Square_ABCD.C_ABC, path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.A, Square_ABCD.C_ABC);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.A, path.Value.Start);
            Assert.AreEqual(Square_ABCD.C_ABC, path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.B_BCD, Square_ABCD.D);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.B_BCD, path.Value.Start);
            Assert.AreEqual(Square_ABCD.D, path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.C_BCD, Square_ABCD.D);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.C_BCD, path.Value.Start);
            Assert.AreEqual(Square_ABCD.D, path.Value.End);

            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.B_BCD, Square_ABCD.C_BCD);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.B_BCD, path.Value.Start);
            Assert.AreEqual(Square_ABCD.C_BCD, path.Value.End);
        }

        [Test]
        public void NeighbourFacesHaveDirectPath()
        {
            #region ABCD -> A to D
            Maybe<SurfacePath> path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.A, Square_ABCD.D);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.A, path.Value.Start);
            Assert.AreEqual(Square_ABCD.D, path.Value.End);

            SurfacePoint m_a = new SurfacePoint(
                Square_ABCD.BCD,
                new BarycentricVector(
                    Square_ABCD.BCD.Triangle,
                    new BarycentricCoordinates(0.5f, 0.5f, 0)));

            Assert.AreEqual(m_a.Coordinates, path.Value.Points[1].Coordinates);
            #endregion

            #region ABCD -> D to A
            path = Square_ABCD.Surface.MakeDirectPath(Square_ABCD.D, Square_ABCD.A);
            Assert.True(path.HasValue());
            Assert.AreEqual(Square_ABCD.D, path.Value.Start);
            Assert.AreEqual(Square_ABCD.A, path.Value.End);

            Assert.AreEqual(m_a.Coordinates, path.Value.Points[1].Coordinates);
            #endregion

            #region ACDF -> B to E (start from CW triangle)
            path = Rectangle_ACDF.Surface.MakeDirectPath(Rectangle_ACDF.BCD_B, Rectangle_ACDF.CDE_E);
            Assert.True(path.HasValue());
            Assert.AreEqual(Rectangle_ACDF.BCD_B, path.Value.Start);
            Assert.AreEqual(Rectangle_ACDF.CDE_E, path.Value.End);

            SurfacePoint m_b = new SurfacePoint(
                Rectangle_ACDF.BCD,
                new BarycentricVector(
                    Rectangle_ACDF.BCD.Triangle,
                    new BarycentricCoordinates(0.0f, 0.5f, 0.5f)));

            Assert.AreEqual(m_b.Coordinates, path.Value.Points[1].Coordinates);
            #endregion

            #region ACDF -> C to F
            path = Rectangle_ACDF.Surface.MakeDirectPath(Rectangle_ACDF.CDE_C, Rectangle_ACDF.F);
            Assert.True(path.HasValue());
            Assert.AreEqual(Rectangle_ACDF.CDE_C, path.Value.Start);
            Assert.AreEqual(Rectangle_ACDF.F, path.Value.End);

            SurfacePoint m_c = new SurfacePoint(
                Rectangle_ACDF.CDE,
                new BarycentricVector(
                    Rectangle_ACDF.CDE.Triangle,
                    new BarycentricCoordinates(0.0f, 0.5f, 0.5f)));

            Assert.AreEqual(m_c.Coordinates, path.Value.Points[1].Coordinates);
            #endregion

            #region ACDF -> m_b to A (does not start from vertex)
            path = Rectangle_ACDF.Surface.MakeDirectPath(m_b, Rectangle_ACDF.A);
            Assert.True(path.HasValue());
            Assert.AreEqual(m_b, path.Value.Start);
            Assert.AreEqual(Rectangle_ACDF.A, path.Value.End);

            SurfacePoint intersectionOn_ABC_a = new SurfacePoint(
                Rectangle_ACDF.ABC,
                new BarycentricVector(
                    Rectangle_ACDF.ABC.Triangle,
                    new BarycentricCoordinates(0.0f, 1.0f / 3.0f, 2.0f / 3.0f)));

            Assert.AreEqual(intersectionOn_ABC_a.Coordinates, path.Value.Points[1].Coordinates);
            #endregion
        }

        [Test]
        public void FarFacesHaveDirectPath()
        {
            Maybe<SurfacePath> path = Rectangle_ACDF.Surface.MakeDirectPath(Rectangle_ACDF.A, Rectangle_ACDF.F);
            Assert.True(path.HasValue());
            Assert.AreEqual(Rectangle_ACDF.A, path.Value.Start);
            Assert.AreEqual(Rectangle_ACDF.F, path.Value.End);

            SurfacePoint intersectionOn_ABC_a = new SurfacePoint(
                Rectangle_ACDF.ABC,
                new BarycentricVector(
                    Rectangle_ACDF.ABC.Triangle,
                    new BarycentricCoordinates(0.0f, 1.0f / 3.0f, 2.0f / 3.0f)));

            SurfacePoint intersectionOn_BCD_b = new SurfacePoint(
                Rectangle_ACDF.BCD,
                new BarycentricVector(
                    Rectangle_ACDF.BCD.Triangle,
                    new BarycentricCoordinates(0.0f, 0.5f, 0.5f)));

            SurfacePoint intersectionOn_DEF_f = new SurfacePoint(
                Rectangle_ACDF.DEF,
                new BarycentricVector(
                    Rectangle_ACDF.DEF.Triangle,
                    new BarycentricCoordinates(2.0f / 3.0f, 1.0f / 3.0f, 0.0f)));

            Assert.AreEqual(intersectionOn_ABC_a.Coordinates, path.Value.Points[1].Coordinates);
            Assert.AreEqual(intersectionOn_BCD_b.Coordinates, path.Value.Points[2].Coordinates);
            Assert.AreEqual(intersectionOn_DEF_f.Coordinates, path.Value.Points[3].Coordinates);
        }
    }
}
