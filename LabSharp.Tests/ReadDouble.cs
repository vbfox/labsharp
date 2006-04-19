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
            m_eng.Eval("val = 6.666");
            double val = (double)m_eng.GetVariable<Object>("val");
            Assert.AreEqual(val, 6.666);
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
        public void Read1DArrayGoodType()
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
        public void Read1DArrayImplicit()
        {
            m_eng.Eval("val = [6.666 7.777 8.888]");
            double[,] val = (double[,])m_eng.GetVariable<Object>("val");
            Assert.AreEqual(val.Length, 3);
            Assert.AreEqual(val[0, 0], 6.666);
            Assert.AreEqual(val[0, 1], 7.777);
            Assert.AreEqual(val[0, 2], 8.888);
        }

        [Test]
        public void Read1DArrayOtherType()
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
        public void Read1DArrayBadType()
        {
            m_eng.Eval("val = 'hello world'");
            double[] val = m_eng.GetVariable<double[]>("val");
        }

        [Test]
        public void Read2DArrayGoodType()
        {
            m_eng.Eval("val = [6.666 7.777 8.888; 9.999 1.111 2.222; 3.333 4.444 5.555]");
            double[,] val = m_eng.GetVariable<double[,]>("val");

            Assert.AreEqual(val[0, 0], 6.666);
            Assert.AreEqual(val[0, 1], 7.777);
            Assert.AreEqual(val[0, 2], 8.888);

            Assert.AreEqual(val[1, 0], 9.999);
            Assert.AreEqual(val[1, 1], 1.111);
            Assert.AreEqual(val[1, 2], 2.222);

            Assert.AreEqual(val[2, 0], 3.333);
            Assert.AreEqual(val[2, 1], 4.444);
            Assert.AreEqual(val[2, 2], 5.555);
        }

        [Test]
        public void Read2DArrayImplicit()
        {
            m_eng.Eval("val = [6.666 7.777 8.888; 9.999 1.111 2.222; 3.333 4.444 5.555]");
            double[,] val = (double[,])m_eng.GetVariable<Object>("val");

            Assert.AreEqual(val[0, 0], 6.666);
            Assert.AreEqual(val[0, 1], 7.777);
            Assert.AreEqual(val[0, 2], 8.888);

            Assert.AreEqual(val[1, 0], 9.999);
            Assert.AreEqual(val[1, 1], 1.111);
            Assert.AreEqual(val[1, 2], 2.222);

            Assert.AreEqual(val[2, 0], 3.333);
            Assert.AreEqual(val[2, 1], 4.444);
            Assert.AreEqual(val[2, 2], 5.555);
        }

        [Test]
        public void Read2DArrayOtherType()
        {
            m_eng.Eval("val = int16([6 7 8; 9 1 2; 3 4 5])");
            double[,] val = m_eng.GetVariable<double[,]>("val");

            Assert.AreEqual(val[0, 0], 6);
            Assert.AreEqual(val[0, 1], 7);
            Assert.AreEqual(val[0, 2], 8);

            Assert.AreEqual(val[1, 0], 9);
            Assert.AreEqual(val[1, 1], 1);
            Assert.AreEqual(val[1, 2], 2);

            Assert.AreEqual(val[2, 0], 3);
            Assert.AreEqual(val[2, 1], 4);
            Assert.AreEqual(val[2, 2], 5);
        }

    }
}
