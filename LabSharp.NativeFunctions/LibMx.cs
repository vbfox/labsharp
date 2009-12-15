/*
 * Lab# - Matlab interaction library for .Net
 * 
 * Copyright (C) 2004 Emanuele Ruffaldi
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
using System.Runtime.InteropServices;

namespace LabSharp.NativeFunctions
{
    /// <summary>
    /// Wrapper for the libmx.dll native functions
    /// </summary>
    public static class LibMx
    {
        #region mxArray creation

        // Creates a Matrix with the specified dimensions
        [DllImport("libmx.dll")]
        public static extern IntPtr mxCreateDoubleMatrix(int n, int m, Complexity c);

        // Creates a Matrix with the specified dimensions
        [DllImport("libmx.dll")]
        public static extern IntPtr mxCreateNumericMatrix(int n, int m, ClassID classid, Complexity c);

        // Creates a Multidimensional Matrix with the specific type
        [DllImport("libmx.dll")]
        public static extern IntPtr mxCreateNumericArray(int ndim, int[] dims, ClassID classid, Complexity flag);

        // Creates a Matrix with the specified dimensions
        [DllImport("libmx.dll")]
        public static extern IntPtr mxCreateString(string str);

        #endregion

        [DllImport("libmx.dll")]
        public static extern int mxGetString(IntPtr array_ptr, StringBuilder buf, int buflen);

        #region Direct Memory Management

        [DllImport("libmx.dll")]
        public static extern void mxFree(IntPtr pa);

        [DllImport("libmx.dll")]
        public static extern IntPtr mxMalloc(uint n);

        [DllImport("libmx.dll")]
        public static extern IntPtr mxCalloc(uint n, uint size);

        [DllImport("libmx.dll")]
        public static extern IntPtr mxRealloc(IntPtr ptr, uint n);

        #endregion

        // Destroy a Matrix
        [DllImport("libmx.dll")]
        public static extern void mxDestroyArray(IntPtr pa);

        // Sets the name of a Matrix
        // OBSOLETED from 6.5
        [DllImport("libmx.dll")]
        public static extern int mxSetName(IntPtr array_ptr, string name);

        // Gets the name of a Matrix
        // OBSOLETED from 6.5
        [DllImport("libmx.dll")]
        public static extern string mxGetName(IntPtr array_ptr);

        // Get the raw pointer to the matrix data as a double *
        [DllImport("libmx.dll")]
        public static extern IntPtr mxGetPr(IntPtr array_ptr);

        // Gets the raw pointer to the matrix data as a void *
        [DllImport("libmx.dll")]
        public static extern IntPtr mxGetData(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        public static extern void mxSetData(IntPtr array_ptr, IntPtr data_ptr);

        [DllImport("libmx.dll")]
        public static extern IntPtr mxGetImagData(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        public static extern void mxSetImagData(IntPtr array_ptr, IntPtr data_ptr);

        [DllImport("libmx.dll")]
        public static extern double mxGetScalar(IntPtr array_ptr);

        // Gets the number of columns
        [DllImport("libmx.dll")]
        public static extern int mxGetN(IntPtr array_ptr);

        // Gets the number of rows
        [DllImport("libmx.dll")]
        public static extern int mxGetM(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        public static extern int mxGetNumberOfElements(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        public static extern int mxGetNumberOfDimensions(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        public static extern IntPtr mxGetDimensions(IntPtr array_ptr);

        #region Struct functions (Fields)

        [DllImport("libmx.dll")]
        public static extern int mxGetNumberOfFields(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        public static extern int mxAddField(IntPtr array_ptr, string field_name);

        [DllImport("libmx.dll")]
        public static extern void mxRemoveField(IntPtr array_ptr, int field_number);

        [DllImport("libmx.dll")]
        public static extern int mxGetFieldNumber(IntPtr array_ptr, string field_name);

        [DllImport("libmx.dll")]
        public static extern IntPtr mxGetFieldByNumber(IntPtr array_ptr, int index, int field_number);

        [DllImport("libmx.dll")]
        public static extern IntPtr mxGetField(IntPtr array_ptr, int index, string field_name);

        [DllImport("libmx.dll")]
        public static extern string mxGetFieldNameByNumber(IntPtr array_ptr, int field_number);

        [DllImport("libmx.dll")]
        public static extern void mxSetFieldByNumber(IntPtr array_ptr, int index, int field_number, IntPtr value);

        [DllImport("libmx.dll")]
        public static extern void mxSetField(IntPtr array_ptr, int index, string field_name, IntPtr value);

        #endregion

        [DllImport("libmx.dll")]
        public static extern int mxGetElementSize(IntPtr array_ptr);

        // Gets The type of the matrix data
        [DllImport("libmx.dll")]
        public static extern ClassID mxGetClassID(IntPtr array_ptr);

        // Gets The type name of the matrix data
        [DllImport("libmx.dll")]
        public static extern string mxGetClassName(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mxIsEmpty(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mxIsNumeric(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mxIsComplex(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mxIsSparse(IntPtr array_ptr);

        [DllImport("libmx.dll")]
        public static extern IntPtr mxDuplicateArray(IntPtr array_ptr);

        #region Numeric constants & tests

        [DllImport("libmx.dll")]
        public static extern double mxGetNaN();

        [DllImport("libmx.dll")]
        public static extern double mxGetInf();

        [DllImport("libmx.dll")]
        public static extern double mxGetEps();

        [DllImport("libmx.dll")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mxIsFinite(double value);

        [DllImport("libmx.dll")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mxIsInf(double value);

        #endregion
    }
}