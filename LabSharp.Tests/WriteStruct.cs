using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LabSharp.Tests
{
    [TestFixture]
    public class WriteStruct
    {
        Engine m_eng;

        [SetUp]
        public void Setup()
        {
            m_eng = Engine.Open(false);
            m_eng.Eval("clear val");
            m_eng.Eval("clear val_tmp");
        }

        [TearDown]
        public void TearDown()
        {
            m_eng.Eval("clear val");
            m_eng.Eval("clear val_tmp");
        }

        [Test]
        public void WithString()
        {
            using (MxArray arr = MxArray.CreateStruct())
            {
                arr.SetField("title", "hello");
                m_eng.SetVariable("val", arr);
                m_eng.Eval("val_test = val.title");
                Assert.AreEqual("hello", m_eng.GetVariable<string>("val_test"));
            }
        }
    }
}
