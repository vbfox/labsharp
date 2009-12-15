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

            m_eng.Eval("val_ok = all(all(val == [6.65 7.2 154.977; 157.45 789.6 4710.4]))");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }

        [Test]
        public void SingleValue_Cplx()
        {
            m_eng.SetVariable("val", new Complex<double>(6.65, 3));

            m_eng.Eval("val_ok = (val == 6.65 + 3 * i)");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }

        [Test]
        public void Array1D_Cplx()
        {
            m_eng.SetVariable("val", new Complex<double>[]{ 
                new Complex<double>(6.65, 3),
                new Complex<double>(7.2, 68.1),
                new Complex<double>(154.977, 45.7)
                });

            m_eng.Eval("val_ok = all(val == [6.65+3*i 7.2+68.1*i 154.977+45.7*i])");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }

        [Test]
        public void Array2D_Cplx()
        {
            m_eng.SetVariable("val", new Complex<double>[,] { 
                {
                    new Complex<double>(6.65, 3),
                    new Complex<double>(7.2, 68.1),
                    new Complex<double>(154.977, 45.7)
                }, {
                    new Complex<double>(-1.3, 2),
                    new Complex<double>(6.4, -8),
                    new Complex<double>(666, 20)
                }
                });
                

            m_eng.Eval("val_ok = all(all(val == [6.65+i*3 7.2+i*68.1 154.977+i*45.7;"
                + "-1.3+i*2 6.4+i*-8 666+i*20]))");
            Assert.IsTrue(m_eng.GetVariable<bool>("val_ok"));
        }
    }
}
