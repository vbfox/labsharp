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
            m_eng.Eval("clear val_ok");
        }

        [TearDown]
        public void TearDown()
        {
            m_eng.Eval("clear val");
            m_eng.Eval("clear val_ok");
        }

        [Test]
        public void StructWithDouble()
        {
            m_eng.Eval("val.x = 6.5");
            MxArray val = m_eng.GetVariable("val");
            Assert.AreEqual(6.5, val.GetField<double>(0, "x"));
        }
    }
}
