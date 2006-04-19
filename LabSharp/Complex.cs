using System;
using System.Collections.Generic;
using System.Text;

namespace LabSharp
{
    public struct Complex<T>
    {
        T m_realPart;

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

        static Complex<TResult> FromRealImaginary<TResult>(TResult real, TResult imaginary)
        {
            Complex<TResult> result = new Complex<TResult>();

            result.RealPart = real;
            result.ImaginaryPart = imaginary;

            return result;
        }
    }
}
