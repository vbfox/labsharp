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
    /// Wrapper for the libeng.dll native functions
    /// </summary>
    public static class LibEng
    {
        [DllImport("libeng.dll")]
        public static extern IntPtr engOpen(string startcmd);

        public static IntPtr engOpen()
        {
            return engOpen(null);
        }

        [DllImport("libeng.dll")]
        public static extern IntPtr engOpenSingleUse(string startcmd, IntPtr dcom, out int retstatus);

        public static IntPtr engOpenSingleUse()
        {
            int dummy;
            return engOpenSingleUse(null, IntPtr.Zero, out dummy);
        }

        [DllImport("libeng.dll")]
        public static extern int engClose(IntPtr e);

        [DllImport("libeng.dll")]
        public static extern int engEvalString(IntPtr e, string cmd);

        [DllImport("libeng.dll")]
        public static extern int engSetVisible(IntPtr e, [MarshalAs(UnmanagedType.U1)] bool q);

        [DllImport("libeng.dll")]
        public static extern int engGetVisible(IntPtr e,
            [MarshalAs(UnmanagedType.U1)] out bool visible);

        [DllImport("libeng.dll", CharSet = CharSet.Ansi)]
        public static extern int engOutputBuffer(IntPtr e, IntPtr buffer, int n);

        // OBSOLETED from 6.5
        [DllImport("libeng.dll")]
        public static extern int engPutArray(IntPtr e, IntPtr array);

        // REQUIRES 6.5+
        [DllImport("libeng.dll")]
        public static extern int engPutVariable(IntPtr e, string name, IntPtr array);

        // REQUIRES 6.5+
        [DllImport("libeng.dll")]
        public static extern IntPtr engGetVariable(IntPtr e, string name);

        // OBSOLETED from 6.5
        [DllImport("libeng.dll")]
        public static extern IntPtr engGetArray(IntPtr e, string name);
    }
}