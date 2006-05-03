using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LabSharp.Tests
{
    [TestFixture]
    public class WriteDouble
    {
        Engine m_eng;
        MxArray m_temp, m_tempbool;

        [SetUp]
        public void Setup()
        {
            m_eng = Engine.Open(false);
            m_temp = m_eng.GetVariable("val");
            m_tempbool = m_eng.GetVariable("val_ok");
        }

        [TearDown]
        public void TearDown()
        {
            if (m_temp != null)
            {
                m_eng.SetVariable("val", m_temp);
                m_temp.Destroy();
                m_temp = null;
            }
            else
            {
                m_eng.Eval("clear val");
            }
            if (m_tempbool != null)
            {
                m_eng.SetVariable("val_ok", m_tempbool);
                m_tempbool.Destroy();
                m_tempbool = null;
            }
            else
            {
                m_eng.Eval("clear val_ok");
            }
        }

        [Test]
        public void SingleValue()
        {
            m_eng.SetVariable("val", (double)6.65);

            m_eng.Eval("val_ok = (val == 6.65)");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }

        [Test]
        public void Array1D()
        {
            m_eng.SetVariable("val", new double[] { 6.65, 7.2, 154.977 });

            m_eng.Eval("val_ok = all(val == [6.65 7.2 154.977])");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }

        [Test]
        public void Array2D()
        {
            m_eng.SetVariable("val", new double[,]
                { { 6.65, 7.2, 154.977 }, { 157.45, 789.6, 4710.4 } });

            m_eng.Eval("val_ok = all(val == [6.65 7.2 154.977; 157.45 789.6 4710.4])");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }
    }
}
