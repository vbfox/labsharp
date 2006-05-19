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
            //m_eng.Eval("clear val");
            //m_eng.Eval("clear val_tmp");
        }

        [Test]
        public void WithString()
        {
            using (MxArray arr = MxArray.CreateStruct())
            {
                arr.SetField("title", "hello");
                m_eng.SetVariable("val", arr);
                m_eng.Eval("val_tmp = val.title");
                Assert.AreEqual("hello", m_eng.GetVariable<string>("val_tmp"));
            }
        }

        [Test]
        public void WithStruct()
        {
            using (MxArray arr = MxArray.CreateStruct())
            using (MxArray str = MxArray.CreateStruct())
            {
                arr.SetField("some_field", str);
                str.SetField("some_string", "strucInStruct");
                m_eng.SetVariable("val", arr);
                m_eng.Eval("val_tmp = val.some_field.some_string");
                Assert.AreEqual("strucInStruct", m_eng.GetVariable<string>("val_tmp"));
            }
        }
        [Test]
        public void WithStruct_InvertDestroy()
        {
            using (MxArray str = MxArray.CreateStruct())
            using (MxArray arr = MxArray.CreateStruct())
            {
                arr.SetField("some_field", str);
                str.SetField("some_string", "strucInStruct");
                m_eng.SetVariable("val", arr);
                m_eng.Eval("val_tmp = val.some_field.some_string");
                Assert.AreEqual("strucInStruct", m_eng.GetVariable<string>("val_tmp"));
            }
        }
    }
}
