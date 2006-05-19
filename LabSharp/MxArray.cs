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
using LabSharp.NativeFunctions;

namespace LabSharp
{
    /// <summary>
    /// This class is a direct wrapper for all mx* (libmx.dll) functions of
    /// Matlab.
    /// </summary>
    /// <remarks>This class implements IDisposable, but it isn't auto-called 
    /// by the destructor because mxArrays from mex files shouldn't be destroyed</remarks>
    public partial class MxArray : IDisposable, ICloneable
    {
        public class InvalidClassIDException : Exception
        {
            public InvalidClassIDException(ClassID expected, ClassID actual, string functionName)
                : base(String.Format("Unable to call the function {0} on a \"{1}\" array, it require a \"{2}\" array.", functionName, actual, expected))
            {
            }
        }

        #region Static constructors

        public static MxArray CreateDoubleMatrix(int n, int m, Complexity complexFlag)
        {
            return CreateNumericMatrix(n, m, ClassID.Double, complexFlag);
        }

        public static MxArray CreateNumericMatrix(int n, int m, ClassID classid, Complexity complexFlag)
        {
            return CreateNumericArray(n, new int[] { m }, classid, complexFlag);
        }

        public static MxArray CreateNumericArray(int ndim, int[] dims, ClassID classid, Complexity complexFlag)
        {
            return new MxArray(LibMx.mxCreateNumericArray(ndim, dims, classid, complexFlag));
        }

        public static MxArray CreateString(string str)
        {
            return new MxArray(LibMx.mxCreateString(str));
        }

        #endregion

        IntPtr m_array;
        public IntPtr NativeObject { get { return m_array; } }

        //XXX
        /*
        bool m_doNotDelete = false;
        /// <summary>
        /// <para>
        /// Set this field if you don't want that Dispose or Finalize delete the
        /// unmanaged memory.
        /// </para><para>
        /// This is usefull to set it to true if you have to return the value in
        /// a Mex file.
        /// </para>
        /// </summary>
        /*
        public bool DoNotDelete
        {
            get { return m_doNotDelete; }
            set { m_doNotDelete = value; }
        }*/

        #region Child/Parent management

        List<MxArray> m_childs = new List<MxArray>();

        MxArray m_parent = null;
        /// <summary>
        /// Indicate the parent of the MxArray (Like a struct for the struct elements)
        /// <remarks>
        /// Do not set this value if you don't known what you do, it could lead to memory
        /// leaks, double delete or access to invalid memory.
        /// </remarks>
        /// </summary>
        public MxArray Parent
        {
            get { return m_parent; }
            protected set {
                // Something strange is happenning
                if (m_parent != null) throw new InvalidOperationException("Cannot change parent if it is already set");

                // Attach to the new parent
                m_parent = value;
                m_parent.AddChild(this);
            }
        }

        void RemoveChilds(IntPtr arrayPtr)
        {
            // Code is in 2 part because Destroy change the parent's m_child content
            // (And we can't do that in a foreach)
            List<MxArray> toRemove = new List<MxArray>();
            foreach (MxArray child in m_childs)
            {
                if (child.m_array == arrayPtr)
                {
                    toRemove.Add(child);
                }
            }
            foreach (MxArray arrayToRemove in toRemove)
            {
                arrayToRemove.Destroy();
            }
        }

        void RemoveChild(MxArray child)
        {
            m_childs.Remove(child);
        }

        void AddChild(MxArray child)
        {
            m_childs.Add(child);
        }

        /// <summary>
        /// Call <see cref="Destroy"/> on all our childs (It doesn't remove anything
        /// as they are the childs of this mxArray and their memory will be cleared
        /// with it) and clear the child list.
        /// </summary>
        void ClearChilds()
        {
            foreach (MxArray child in m_childs)
            {
                child.Destroy();
            }
            m_childs.Clear();
        }

        #endregion

        void CheckPointer()
        {
            if (m_array == IntPtr.Zero)
            {
                throw new Exception("Invalid mxArray pointer");
            }
        }

        void AssertClass(ClassID expected, string functionName)
        {
            ClassID classOfThis = Class;
            if (classOfThis != expected)
            {
                throw new InvalidClassIDException(expected, classOfThis, functionName);
            }
        }

        public MxArray(IntPtr array, MxArray parent)
            : this(array)
        {
            Parent = parent;
        }

        public MxArray(IntPtr array)
        {
            m_array = array;
            if (m_array == IntPtr.Zero)
            {
                throw new Exception("Invalid mxArray pointer");
            }
        }

        /// <summary>
        /// Destroy the memory allocated for the array in Matlab.
        /// </summary>
        public void Destroy()
        {
            CheckPointer();
            ClearChilds();

            if (m_parent == null)
            {
                LibMx.mxDestroyArray(m_array);
            }
            else
            {
                m_parent.RemoveChild(this);
            }

            m_array = IntPtr.Zero;
        }

        /// <summary>
        /// Class of the data contained inside the array.
        /// </summary>
        public ClassID Class
        {
            get
            {
                return LibMx.mxGetClassID(m_array);
            }
        }

        public int ColumnCount
        {
            get
            {
                return LibMx.mxGetN(m_array);
            }
        }

        public int RowCount
        {
            get
            {
                return LibMx.mxGetM(m_array);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return LibMx.mxIsEmpty(m_array);
            }
        }

        /// <summary>
        /// Size in bytes of each elements of the array.
        /// </summary>
        public int ElementSize
        {
            get
            {
                int elementSize = LibMx.mxGetElementSize(m_array);
                if (elementSize == 0)
                {
                    throw new Exception("Unable to get element size");
                }
                return elementSize;
            }
        }

        public int NumberOfDimensions
        {
            get
            {
                return LibMx.mxGetNumberOfDimensions(m_array);
            }
        }

        public int[] Dimensions
        {
            get
            {
                int count = NumberOfDimensions;
                int[] result = new int[count];
                unsafe
                {
                    int* dims = (int*)LibMx.mxGetDimensions(m_array).ToPointer();
                    for (int i = 0; i < count; i++)
                    {
                        result[i] = *dims++;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Total number of elements in all dimensions.
        /// </summary>
        public int NumberOfElements
        {
            get
            {
                return LibMx.mxGetNumberOfElements(m_array);
            }
        }

        #region Unsafe getters

        /// <summary>
        /// Really low level and dangerous access, especially the pointer to the
        /// SetFunction that should have been allocated with mxCalloc...
        /// 
        /// If the size allocated is other than NumberOfElements * ElementSize 
        /// there is also a risk of problems.
        /// </summary>
        unsafe public void* RealElements
        {
            get
            {
                CheckPointer();
                return LibMx.mxGetData(m_array).ToPointer();
            }
            set
            {
                CheckPointer();
                void* realElements = RealElements;
                if (realElements != null)
                {
                    LibMx.mxFree(new IntPtr(realElements));
                }
                LibMx.mxSetData(m_array, new IntPtr(value));
            }
        }

        unsafe public void* ImaginaryElements
        {
            get
            {
                CheckPointer();
                return LibMx.mxGetImagData(m_array).ToPointer();
            }
            set
            {
                CheckPointer();
                void* imagElements = ImaginaryElements;
                if (imagElements != null)
                {
                    LibMx.mxFree(new IntPtr(imagElements));
                }
                LibMx.mxSetImagData(m_array, new IntPtr(value));
            }
        }

        #endregion

        public int[] CoordinatesFromIndex(int index)
        {
            return MxUtils.CoordinatesFromIndex(index, Dimensions);
        }
        
        public int IndexFromCoordinates(int[] coordinates)
        {
            return MxUtils.IndexFromCoordinates(coordinates, Dimensions);
        }

        #region Conversion vers les types de base

        public string StringValue
        {
            get
            {
                if (!IsChar)
                {
                    throw new Exception("Can't GetString on ClassID other than \"char\".");
                }
                int buflen = RowCount * ColumnCount * ElementSize + 1;
                StringBuilder str = new StringBuilder(buflen);
                if (LibMx.mxGetString(m_array, str, buflen) == 1)
                {
                    throw new Exception("Unable to get the mxArray as a string.");
                }
                return str.ToString();
            }
        }

        public double ScalarValue
        {
            get
            {
                return LibMx.mxGetScalar(m_array);
            }
        }

        #endregion

        public bool IsComplex
        {
            get
            {
                CheckPointer();
                return LibMx.mxIsComplex(m_array);
            }
        }

        public bool IsNumeric
        {
            get
            {
                CheckPointer();
                return LibMx.mxIsNumeric(m_array);
            }
        }

        public bool IsSparse
        {
            get
            {
                CheckPointer();
                return LibMx.mxIsSparse(m_array);
            }
        }

        #region Static methods (Working on doubles : Nan, Inf, ...)

        public static double NaN
        {
            get
            {
                return LibMx.mxGetNaN();
            }
        }

        public static double GetInf
        {
            get
            {
                return LibMx.mxGetInf();
            }
        }

        public static bool IsFinite(double value)
        {
            return LibMx.mxIsFinite(value);
        }

        public static bool IsInf(double value)
        {
            return LibMx.mxIsInf(value);
        }

        public static double Eps
        {
            get
            {
                return LibMx.mxGetEps();
            }
        }

        #endregion

        #region IDisposable Members

        ~MxArray()
        {
            Dispose(false);
        }

        void Dispose(bool disposing)
        {
            if (m_array != IntPtr.Zero)
            {
                Destroy();
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Clone methods

        object ICloneable.Clone()
        {
            return (object)Clone();
        }

        public MxArray Clone()
        {
            CheckPointer();
            return new MxArray(LibMx.mxDuplicateArray(m_array));
        }

        #endregion

        /// <summary>
        /// Convert the object to a simple string, this only works for single
        /// values or char arrays.
        /// </summary>
        /// <returns>
        /// The string representation of the object if it is simple enough,
        /// null otherwise.
        /// </returns>
        string ToSimpleString()
        {
            //TODO: Allow for numeric arrays of less than 10 elements like matlab
            //TODO: Allow for complex number
            if (IsNumeric && (NumberOfElements == 1) && !IsComplex)
            {
                return ScalarValue.ToString();
            }
            else if (IsChar)
            {
                return StringValue;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            if (m_array == IntPtr.Zero) return "Invalidated or deleted mxArray";

            StringBuilder result = new StringBuilder();

            result.Append(MxUtils.DimensionsToString(Dimensions));
            result.Append(" ");
            result.Append(Class.ToString());
            if (IsComplex)
            {
                result.Append(" (complex)");
            }
            result.Append(" mxArray");

            string simpleString = ToSimpleString();
            if (simpleString != null)
            {
                result.Append(" (");
                result.Append(simpleString);
                result.Append(")");
            }

            return result.ToString();
        }    
    }
}
