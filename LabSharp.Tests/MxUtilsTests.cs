using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using LabSharp;

namespace LabSharp.Tests
{
    [TestFixture]
    public class MxUtilsTests
    {
        public MxUtilsTests()
        {
        }

        [Test]
        public void CoordinatesFromIndex_Base()
        {
            Assert.AreEqual(new int[] { 0 }, MxUtils.CoordinatesFromIndex(0, new int[] { 2 }));
            Assert.AreEqual(new int[] { 1 }, MxUtils.CoordinatesFromIndex(1, new int[] { 2 }));
            Assert.AreEqual(new int[] { 9 }, MxUtils.CoordinatesFromIndex(9, new int[] { 10 }));

            Assert.AreEqual(new int[] { 0, 0 }, MxUtils.CoordinatesFromIndex(0, new int[] { 4, 2 }));
            Assert.AreEqual(new int[] { 3, 0 }, MxUtils.CoordinatesFromIndex(3, new int[] { 4, 2 }));
            Assert.AreEqual(new int[] { 1, 1 }, MxUtils.CoordinatesFromIndex(5, new int[] { 4, 2 }));
            Assert.AreEqual(new int[] { 3, 1 }, MxUtils.CoordinatesFromIndex(7, new int[] { 4, 2 }));

            Assert.AreEqual(new int[] { 0, 0, 0 }, MxUtils.CoordinatesFromIndex(0, new int[] { 4, 2, 3 }));
            Assert.AreEqual(new int[] { 2, 0, 1 }, MxUtils.CoordinatesFromIndex(10, new int[] { 4, 2, 3 }));
            Assert.AreEqual(new int[] { 3, 1, 2 }, MxUtils.CoordinatesFromIndex(23, new int[] { 4, 2, 3 }));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CoordinatesFromIndex_NoDimensions()
        {
            MxUtils.CoordinatesFromIndex(0, new int[] { });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CoordinatesFromIndex_NegativeDimension()
        {
            MxUtils.CoordinatesFromIndex(0, new int[] { 3, 5, -1 });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CoordinatesFromIndex_ZeroDimension()
        {
            MxUtils.CoordinatesFromIndex(0, new int[] { 0, 5, 1 });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CoordinatesFromIndex_TooBigIndex()
        {
            MxUtils.CoordinatesFromIndex(24, new int[] { 4, 2, 3 });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CoordinatesFromIndex_NegativeIndex()
        {
            MxUtils.CoordinatesFromIndex(-1, new int[] { 4, 2, 3 });
        }

        [Test]
        public void IndexFromCoordinates_Base()
        {
            Assert.AreEqual(0, MxUtils.IndexFromCoordinates(new int[] { 0 }, new int[] { 2 }));
            Assert.AreEqual(1, MxUtils.IndexFromCoordinates(new int[] { 1 }, new int[] { 2 }));
            Assert.AreEqual(9, MxUtils.IndexFromCoordinates(new int[] { 9 }, new int[] { 10 }));

            Assert.AreEqual(0, MxUtils.IndexFromCoordinates(new int[] { 0, 0 }, new int[] { 4, 2 }));
            Assert.AreEqual(3, MxUtils.IndexFromCoordinates(new int[] { 3, 0 }, new int[] { 4, 2 }));
            Assert.AreEqual(5, MxUtils.IndexFromCoordinates(new int[] { 1, 1 }, new int[] { 4, 2 }));
            Assert.AreEqual(7, MxUtils.IndexFromCoordinates(new int[] { 3, 1 }, new int[] { 4, 2 }));

            Assert.AreEqual(0, MxUtils.IndexFromCoordinates(new int[] { 0, 0, 0 }, new int[] { 4, 2, 3 }));
            Assert.AreEqual(10, MxUtils.IndexFromCoordinates(new int[] { 2, 0, 1 }, new int[] { 4, 2, 3 }));
            Assert.AreEqual(23, MxUtils.IndexFromCoordinates(new int[] { 3, 1, 2 }, new int[] { 4, 2, 3 }));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexFromCoordinates_NegativeCoord()
        {
            MxUtils.IndexFromCoordinates(new int[] { 3, -1, 2 }, new int[] { 4, 2, 3 });
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void IndexFromCoordinates_TooSmallCoordArray()
        {
            MxUtils.IndexFromCoordinates(new int[] { 3, 2 }, new int[] { 4, 2, 3 });
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void IndexFromCoordinates_TooBigCoordArray()
        {
            MxUtils.IndexFromCoordinates(new int[] { 3, 2 ,1 ,1 }, new int[] { 4, 2, 3 });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexFromCoordinates_CoordMoreThanDim()
        {
            MxUtils.IndexFromCoordinates(new int[] { 4, 2, 1 }, new int[] { 4, 2, 3 });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexFromCoordinates_DimZeroValue()
        {
            MxUtils.IndexFromCoordinates(new int[] { 3, 2, 1 }, new int[] { 4, 0, 3 });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexFromCoordinates_DimNegativeValue()
        {
            MxUtils.IndexFromCoordinates(new int[] { 3, 2, 1 }, new int[] { 4, -1, 3 });
        }

        [Test]
        public void IndexAndCoordinatesReciprocity()
        {
            int[] dims = new int[] { 5, 6, 4, 9 };
            for (int i = 0; i < 5*6*4*9; i++ )
            {
                int r = MxUtils.IndexFromCoordinates(MxUtils.CoordinatesFromIndex(i, dims), dims);
                Assert.AreEqual(i, r);
            }
        }
    }
}
