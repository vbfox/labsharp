using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LabSharp.Tests
{
    [TestFixture]
    public class ReadStruct
    {
        Engine m_eng;

        [SetUp]
        public void Setup()
        {
            m_eng = Engine.Open(false);
            m_eng.Eval("clear val");
        }

        [TearDown]
        public void TearDown()
        {
            m_eng.Eval("clear val");
        }

        [Test]
        public void StructWithDouble()
        {
            m_eng.Eval("val.x = 6.5");
            using (MxArray val = m_eng.GetVariable("val"))
            {
                Assert.AreEqual(6.5, val.GetField<double>(0, "x"));
            }
        }

        [Test]
        public void StructWithComplex()
        {
            m_eng.Eval("val.x = 6.5 + 5.7 * i");
            using (MxArray val = m_eng.GetVariable("val"))
            {
                Assert.IsTrue(new Complex<double>(6.5, 5.7).Equals(
                    val.GetField<Complex<double>>(0, "x")));
            }
        }

        [Test]
        public void StructWithDoubleArray()
        {
            m_eng.Eval("val(1).x = 6.5;val(2).x = 8.7");
            using (MxArray val = m_eng.GetVariable("val"))
            {
                Assert.AreEqual(6.5, val.GetField<double>(0, "x"));
                Assert.AreEqual(8.7, val.GetField<double>(1, "x"));
            }
        }

        [Test]
        public void StructWithComplexArray()
        {
            m_eng.Eval("val(1).x = 6.5+8.18*i;val(2).x = 8.7+98771.6*i");
            using (MxArray val = m_eng.GetVariable("val"))
            {
                Assert.IsTrue(new Complex<double>(6.5, 8.18).Equals(
                    val.GetField<Complex<double>>(0, "x")));
                Assert.IsTrue(new Complex<double>(8.7, 98771.6).Equals(
                    val.GetField<Complex<double>>(1, "x")));
            }
        }

        [Test]
        public void StructWithString()
        {
            m_eng.Eval("val.x = 'hello world'");
            using (MxArray val = m_eng.GetVariable("val"))
            {
                Assert.AreEqual("hello world", val.GetField<string>(0, "x"));
            }
        }

        [Test]
        public void StructWithStringArray()
        {
            m_eng.Eval("val(1).x = 'hello world'; val(2).x = 'how do you do ?'");
            using (MxArray val = m_eng.GetVariable("val"))
            {
                Assert.AreEqual("hello world", val.GetField<string>(0, "x"));
                Assert.AreEqual("how do you do ?", val.GetField<string>(1, "x"));
            }
        }

        [Test]
        public void StructWithStringAndDouble()
        {
            m_eng.Eval("val.x = 'hello world';val.y = 6.55");
            using (MxArray val = m_eng.GetVariable("val"))
            {
                Assert.AreEqual("hello world", val.GetField<string>(0, "x"));
                Assert.AreEqual(6.55, val.GetField<double>(0, "y"));
            }
        }

        [Test]
        public void StructWithStringAndDoubleArray()
        {
            m_eng.Eval("val(1).x = 'hello world';val(1).y = 6.55");
            m_eng.Eval("val(2).x = 'this is a test';val(2).y = 666.00");
            using (MxArray val = m_eng.GetVariable("val"))
            {
                Assert.AreEqual("hello world", val.GetField<string>(0, "x"));
                Assert.AreEqual(6.55, val.GetField<double>(0, "y"));
                Assert.AreEqual("this is a test", val.GetField<string>(1, "x"));
                Assert.AreEqual(666, val.GetField<double>(1, "y"));
            }
        }
    }
}
