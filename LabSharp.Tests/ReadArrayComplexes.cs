using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LabSharp.Tests
{
    [TestFixture]
    public class ReadArrayComplexes
    {
        Engine m_eng;
        MxArray m_temp;

        [SetUp]
        public void Setup()
        {
            m_eng = Engine.Open(false);
            m_temp = m_eng.GetVariable("cplx");
        }

        [TearDown]
        public void TearDown()
        {
            if (m_temp != null)
            {
                m_eng.SetVariable("cplx", m_temp);
                m_temp.Destroy();
            }
            else
            {
                m_eng.Eval("clear cplx");
            }
        }

        [Test]
        public void Read1DArrayImplicit()
        {
            m_eng.Eval("cplx = [3.5 + 2.6 * i, 2.8 + 4.9 * i]");
            Complex<double>[,] cplx = (Complex<double>[,])m_eng.GetVariable<Object>("cplx");
            Assert.AreEqual(cplx.Length, 2);
            Assert.AreEqual(cplx[0, 0].RealPart, 3.5, "[0] Real part");
            Assert.AreEqual(cplx[0, 0].ImaginaryPart, 2.6, "[0] Imaginary part");
            Assert.AreEqual(cplx[0, 1].RealPart, 2.8, "[1] Real part");
            Assert.AreEqual(cplx[0, 1].ImaginaryPart, 4.9, "[1] Imaginary part");
        }

        [Test]
        public void Read1DArrayGoodType()
        {
            m_eng.Eval("cplx = [3.5 + 2.6 * i, 2.8 + 4.9 * i]");
            Complex<double>[] cplx =
                (Complex<double>[])m_eng.GetVariable<Complex<double>[]>("cplx");
            Assert.AreEqual(cplx.Length, 2);
            Assert.AreEqual(cplx[0].RealPart, 3.5, "[0] Real part");
            Assert.AreEqual(cplx[0].ImaginaryPart, 2.6, "[0] Imaginary part");
            Assert.AreEqual(cplx[1].RealPart, 2.8, "[1] Real part");
            Assert.AreEqual(cplx[1].ImaginaryPart, 4.9, "[1] Imaginary part");
        }

        [Test]
        public void Read1DArrayOtherType()
        {
            m_eng.Eval("cplx = int16([3 + 2 * i, 2 + 4 * i])");
            Complex<double>[] cplx =
                (Complex<double>[])m_eng.GetVariable<Complex<double>[]>("cplx");
            Assert.AreEqual(cplx.Length, 2);
            Assert.AreEqual(cplx[0].RealPart, 3, "[0] Real part");
            Assert.AreEqual(cplx[0].ImaginaryPart, 2, "[0] Imaginary part");
            Assert.AreEqual(cplx[1].RealPart, 2, "[1] Real part");
            Assert.AreEqual(cplx[1].ImaginaryPart, 4, "[1] Imaginary part");
        }

        [Test]
        public void Read2DArrayImplicit()
        {
            m_eng.Eval("cplx = [3.5 + 2.6 * i, 2.8 + 4.9 * i; 6, 7]");
            Complex<double>[,] cplx = (Complex<double>[,])m_eng.GetVariable<Object>("cplx");
            Assert.AreEqual(cplx.Length, 4);
            Assert.AreEqual(cplx[0, 0].RealPart, 3.5, "[0,0] Real part");
            Assert.AreEqual(cplx[0, 0].ImaginaryPart, 2.6, "[0,0] Imaginary part");
            Assert.AreEqual(cplx[0, 1].RealPart, 2.8, "[0,1] Real part");
            Assert.AreEqual(cplx[0, 1].ImaginaryPart, 4.9, "[0,1] Imaginary part");
            Assert.AreEqual(cplx[1, 0].RealPart, 6, "[1,0] Real part");
            Assert.AreEqual(cplx[1, 0].ImaginaryPart, 0, "[1,0] Imaginary part");
            Assert.AreEqual(cplx[1, 1].RealPart, 7, "[1,0] Real part");
            Assert.AreEqual(cplx[1, 1].ImaginaryPart, 0, "[1,0] Imaginary part");
        }

        [Test]
        public void Read2DArrayGoodType()
        {
            m_eng.Eval("cplx = [3.5 + 2.6 * i, 2.8 + 4.9 * i; 6, 7]");
            Complex<double>[,] cplx =
                (Complex<double>[,])m_eng.GetVariable<Complex<double>[,]>("cplx");
            Assert.AreEqual(cplx.Length, 4);
            Assert.AreEqual(cplx[0, 0].RealPart, 3.5, "[0,0] Real part");
            Assert.AreEqual(cplx[0, 0].ImaginaryPart, 2.6, "[0,0] Imaginary part");
            Assert.AreEqual(cplx[0, 1].RealPart, 2.8, "[0,1] Real part");
            Assert.AreEqual(cplx[0, 1].ImaginaryPart, 4.9, "[0,1] Imaginary part");
            Assert.AreEqual(cplx[1, 0].RealPart, 6, "[1,0] Real part");
            Assert.AreEqual(cplx[1, 0].ImaginaryPart, 0, "[1,0] Imaginary part");
            Assert.AreEqual(cplx[1, 1].RealPart, 7, "[1,0] Real part");
            Assert.AreEqual(cplx[1, 1].ImaginaryPart, 0, "[1,0] Imaginary part");
        }

        [Test]
        public void Read2DArrayOtherType()
        {
            m_eng.Eval("cplx = int16([3 + 2 * i, 2 + 4 * i; 6, 7])");
            Complex<double>[,] cplx =
                (Complex<double>[,])m_eng.GetVariable<Complex<double>[,]>("cplx");
            Assert.AreEqual(cplx.Length, 4);
            Assert.AreEqual(cplx[0, 0].RealPart, 3, "[0,0] Real part");
            Assert.AreEqual(cplx[0, 0].ImaginaryPart, 2, "[0,0] Imaginary part");
            Assert.AreEqual(cplx[0, 1].RealPart, 2, "[0,1] Real part");
            Assert.AreEqual(cplx[0, 1].ImaginaryPart, 4, "[0,1] Imaginary part");
            Assert.AreEqual(cplx[1, 0].RealPart, 6, "[1,0] Real part");
            Assert.AreEqual(cplx[1, 0].ImaginaryPart, 0, "[1,0] Imaginary part");
            Assert.AreEqual(cplx[1, 1].RealPart, 7, "[1,0] Real part");
            Assert.AreEqual(cplx[1, 1].ImaginaryPart, 0, "[1,0] Imaginary part");
        }        
    }
}
