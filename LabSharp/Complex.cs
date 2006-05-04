using System;
using System.Collections.Generic;
using System.Text;

namespace LabSharp
{
    public struct Complex<T> : IEquatable<Complex<T>>, IEquatable<T>
        where T : struct, IEquatable<T>
    {
        T m_realPart;

        public static implicit operator Complex<T>(T value)
        {
            return new Complex<T>(value, default(T));
        }

        public T RealPart
        {
            get { return m_realPart; }
            set { m_realPart = value; }
        }

        T m_imaginaryPart;

        public T ImaginaryPart
        {
            get { return m_imaginaryPart; }
            set { m_imaginaryPart = value; }
        }

        public Complex(T realPart, T imaginaryPart)
        {
            m_realPart = realPart;
            m_imaginaryPart = imaginaryPart;
        }

        #region IEquatable Members

        public bool Equals(Complex<T> other)
        {
            return (other.m_imaginaryPart.Equals(m_imaginaryPart))
                && (other.m_realPart.Equals(m_realPart));
        }

        bool IEquatable<T>.Equals(T other)
        {
            return (other.Equals(m_realPart)
                && (m_imaginaryPart.Equals(default(T))));
        }

        #endregion
    }
}
