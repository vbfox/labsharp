using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LabSharp.Tests
{
    [TestFixture]
    public class ReadDoubles
    {
        Engine m_eng;

        [SetUp]
        public void Setup()
        {
            m_eng = Engine.Open(false);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                m_eng.Eval("clear val");
            }
            catch { }
        }

        [Test]
        public void ReadGoodType()
        {
            m_eng.Eval("val = 6.666");
            double val = m_eng.GetVariable<double>("val");
            Assert.AreEqual(val, 6.666);
        }

        [Test]
        public void ReadOtherType()
        {
            m_eng.Eval("val = int32(6)");
            double val = m_eng.GetVariable<double>("val");
            Assert.AreEqual(val, 6);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void ReadBadType()
        {
            m_eng.Eval("val = 'hello world'");
            double val = m_eng.GetVariable<double>("val");
        }

        [Test]
        public void ReadArrayGoodType()
        {
            m_eng.Eval("val = [6.666 7.777 8.888]");
            double[] val = m_eng.GetVariable<double[]>("val");
            Assert.AreEqual(val.Length, 3);
            Assert.AreEqual(val[0], 6.666);
            Assert.AreEqual(val[1], 7.777);
            Assert.AreEqual(val[2], 8.888);

            m_eng.Eval("val = [99.999]");
            val = m_eng.GetVariable<double[]>("val");
            Assert.AreEqual(val.Length, 1);
            Assert.AreEqual(val[0], 99.999);
        }

        [Test]
        public void ReadArrayOtherType()
        {
            m_eng.Eval("val = int32([6 7 8])");
            double[] val = m_eng.GetVariable<double[]>("val");
            Assert.AreEqual(val.Length, 3);
            Assert.AreEqual(val[0], 6);
            Assert.AreEqual(val[1], 7);
            Assert.AreEqual(val[2], 8);

            m_eng.Eval("val = [99]");
            val = m_eng.GetVariable<double[]>("val");
            Assert.AreEqual(val.Length, 1);
            Assert.AreEqual(val[0], 99);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void ReadArrayBadType()
        {
            m_eng.Eval("val = 'hello world'");
            double[] val = m_eng.GetVariable<double[]>("val");
        }

    }
}
