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
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace LabSharp
{
    public partial class MxArray
    {
        public bool IsClass(ClassID classid)
        {
            return Class == classid;
        }

        public bool IsCell
        {
            get
            {
                return IsClass(ClassID.Cell);
            }
        }

        public bool IsChar
        {
            get
            {
                return IsClass(ClassID.Char);
            }
        }

        public bool IsDouble
        {
            get
            {
                return IsClass(ClassID.Double);
            }
        }

        public bool IsInt8
        {
            get
            {
                return IsClass(ClassID.Int8);
            }
        }

        public bool IsInt16
        {
            get
            {
                return IsClass(ClassID.Int16);
            }
        }

        public bool IsInt32
        {
            get
            {
                return IsClass(ClassID.Int32);
            }
        }

        public bool IsInt64
        {
            get
            {
                return IsClass(ClassID.Int64);
            }
        }

        public bool IsUInt8
        {
            get
            {
                return IsClass(ClassID.UInt8);
            }
        }

        public bool IsUInt16
        {
            get
            {
                return IsClass(ClassID.UInt16);
            }
        }

        public bool IsUInt32
        {
            get
            {
                return IsClass(ClassID.UInt32);
            }
        }

        public bool IsUInt64
        {
            get
            {
                return IsClass(ClassID.UInt64);
            }
        }

        public bool IsLogical
        {
            get
            {
                return IsClass(ClassID.Logical);
            }
        }

        public bool IsSingle
        {
            get
            {
                return IsClass(ClassID.Single);
            }
        }

        public bool IsStruct
        {
            get
            {
                return IsClass(ClassID.Struct);
            }
        }
    }
}
