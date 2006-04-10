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

namespace LabSharp
{
    /// <summary>
    /// Data type of matlab Matrix elements.
    /// </summary>
    public enum ClassID
    {
        Unknown,
        Cell,
        Struct,
        Logical,
        Char,
        Sparse,
        Double,
        Single,
        Int8,
        UInt8,
        Int16,
        UInt16,
        Int32,
        UInt32,
        Int64,
        UInt64,
        Function,
        Opaque,
        Object
    }
}
