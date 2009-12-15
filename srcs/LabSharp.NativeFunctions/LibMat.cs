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
    /// Wrapper for the libmat.dll native functions
    /// </summary>
    public static class LibMat
    {
        [DllImport("libmat.dll")]
        public static extern int matClose(IntPtr mat);

        [DllImport("libmat.dll")]
        public static extern IntPtr matOpen(string filename, string mode);

        [DllImport("libmat.dll")]
        public static extern int matPutArray(IntPtr mat, IntPtr mtx);

        // R13 only
        [DllImport("libmat.dll")]
        public static extern int matPutVariable(IntPtr mat, IntPtr mtx);

        [DllImport("libmat.dll")]
        public static extern IntPtr matGetArray(IntPtr mat, string name);

        [DllImport("libmat.dll")]
        public static extern IntPtr matGetArrayHeader(IntPtr mat, string name);

        [DllImport("libmat.dll")]
        public static extern IntPtr matGetNextArrayHeader(IntPtr mat);

        [DllImport("libmat.dll")]
        public static extern IntPtr matGetNextArray(IntPtr mat, IntPtr mtx);

        [DllImport("libmat.dll")]
        public static extern int matDeleteArray(IntPtr mat, string name);
    }
}