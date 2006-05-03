using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LabSharp.Tests
{
    [TestFixture]
    public class ReadBool
    {
        Engine m_eng;
        MxArray m_temp;

        [SetUp]
        public void Setup()
        {
            m_eng = Engine.Open(false);
            m_temp = m_eng.GetVariable("val");
        }

        [TearDown]
        public void TearDown()
        {
            if (m_temp != null)
            {
                m_eng.SetVariable("val", m_temp);
                m_temp.Destroy();
            }
            else
            {
                m_eng.Eval("clear val");
            }
        }

        [Test]
        public void ReadImplicit()
        {
            m_eng.Eval("val = true");
            bool val = (bool)m_eng.GetVariable<Object>("val");
            Assert.AreEqual(val, true);

            m_eng.Eval("val = false");
            val = (bool)m_eng.GetVariable<Object>("val");
            Assert.AreEqual(val, false);
        }

        [Test]
        public void ReadGoodType()
        {
            m_eng.Eval("val = true");
            bool val = m_eng.GetVariable<bool>("val");
            Assert.AreEqual(val, true);

            m_eng.Eval("val = false");
            val = m_eng.GetVariable<bool>("val");
            Assert.AreEqual(val, false);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void ReadBadType()
        {
            m_eng.Eval("val = 'hello world'");
            bool val = m_eng.GetVariable<bool>("val");
        }

        [Test]
        public void Read1DArrayGoodType()
        {
            m_eng.Eval("val = [true false true]");
            bool[] val = m_eng.GetVariable<bool[]>("val");
            Assert.AreEqual(val.Length, 3);
            Assert.AreEqual(val[0], true);
            Assert.AreEqual(val[1], false);
            Assert.AreEqual(val[2], true);

            m_eng.Eval("val = [false true true true]");
            val = m_eng.GetVariable<bool[]>("val");
            Assert.AreEqual(val.Length, 4);
            Assert.AreEqual(val[0], false);
            Assert.AreEqual(val[1], true);
            Assert.AreEqual(val[2], true);
            Assert.AreEqual(val[3], true);
        }

        [Test]
        public void Read1DArrayImplicit()
        {
            m_eng.Eval("val = [true false false]");
            bool[,] val = (bool[,])m_eng.GetVariable<Object>("val");
            Assert.AreEqual(val.Length, 3);
            Assert.AreEqual(val[0, 0], true);
            Assert.AreEqual(val[0, 1], false);
            Assert.AreEqual(val[0, 2], false);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void Read1DArrayBadType()
        {
            m_eng.Eval("val = 'hello world'");
            bool[] val = m_eng.GetVariable<bool[]>("val");
        }

        [Test]
        public void Read2DArrayGoodType()
        {
            m_eng.Eval("val = [true true false; false true false; true false false]");
            bool[,] val = m_eng.GetVariable<bool[,]>("val");

            Assert.AreEqual(val[0, 0], true);
            Assert.AreEqual(val[0, 1], true);
            Assert.AreEqual(val[0, 2], false);

            Assert.AreEqual(val[1, 0], false);
            Assert.AreEqual(val[1, 1], true);
            Assert.AreEqual(val[1, 2], false);

            Assert.AreEqual(val[2, 0], true);
            Assert.AreEqual(val[2, 1], false);
            Assert.AreEqual(val[2, 2], false);
        }

        [Test]
        public void Read2DArrayImplicit()
        {
            m_eng.Eval("val = [true true false; false true false; true false false]");
            bool[,] val = (bool[,])m_eng.GetVariable<Object>("val");

            Assert.AreEqual(val[0, 0], true);
            Assert.AreEqual(val[0, 1], true);
            Assert.AreEqual(val[0, 2], false);

            Assert.AreEqual(val[1, 0], false);
            Assert.AreEqual(val[1, 1], true);
            Assert.AreEqual(val[1, 2], false);

            Assert.AreEqual(val[2, 0], true);
            Assert.AreEqual(val[2, 1], false);
            Assert.AreEqual(val[2, 2], false);
        }
    }
}
