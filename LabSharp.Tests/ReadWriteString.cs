using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LabSharp.Tests
{
    [TestFixture]
    public class ReadWriteString
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
        public void ReadBasic()
        {
            m_eng.Eval("val = 'Some test value'");
            Assert.AreEqual(m_eng.GetVariable<string>("val"), "Some test value");
        }

        [Test]
        public void WriteBasic()
        {
            m_eng.SetVariable("val", "A test value");
            m_eng.Eval("val_ok = all(val == 'A test value')");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }

        [Test]
        public void ReadWriteBasic()
        {
            m_eng.SetVariable("val", "An other test value");
            Assert.AreEqual(m_eng.GetVariable<string>("val"), "An other test value");
        }

        [Test]
        public void WriteUTF16()
        {
            m_eng.SetVariable("val", "\u00EC\u00F5");
            m_eng.Eval("val_ok = all(val == [char(hex2dec('EC')) char(hex2dec('F5'))])");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }

        [Test]
        public void ReadUTF16()
        {
            m_eng.Eval("val = [char(hex2dec('F5')) char(hex2dec('EC'))]");
            Assert.AreEqual(m_eng.GetVariable<string>("val"), "\u00F5\u00EC");
        }
    }
}
