using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LabSharp.Tests
{
    [TestFixture]
    public class ReadNonArrayComplexes
    {
        Engine m_eng;

        [SetUp]
        public void Setup()
        {
            m_eng = Engine.Open(false);
        }

        [Test]
        public void ReadComplexGoodType()
        {
            m_eng.Eval("cplx = 3 + 2 * i");
            Complex<double> cplx = m_eng.GetVariable<Complex<double>>("cplx");
            Assert.AreEqual(cplx.RealPart, 3, "Real part");
            Assert.AreEqual(cplx.ImaginaryPart, 2, "Imaginary part");
        }

        [Test]
        [Ignore]
        public void ReadComplexOtherType()
        {

        }

        [Test]
        [Ignore]
        public void ReadComplexBadType()
        {

        }

        [Test]
        [Ignore]
        public void ReadRealAsComplexGoodType()
        {

        }

        [Test]
        [Ignore]
        public void ReadRealAsComplexOtherType()
        {

        }

        [Test]
        [Ignore]
        public void ReadRealAsComplexBadType()
        {

        }
    }
}
