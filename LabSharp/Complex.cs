/*
 * Lab# - Matlab interaction library for .Net
 * 
 * Copyright (C) 2005 Julien Roncaglia
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

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
