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
	public partial class MxArray
	{
        /// <summary>
        /// Get the name of all fields.
        /// </summary>
        public string[] FieldNames
        {
            get
            {
                AssertClass(ClassID.Struct, "get_Fields");
                int count = NumberOfFields;
                string[] result = new string[count];

                for (int i = 0; i < count; i++)
                {
                    result[i] = GetFieldName(i);
                }

                return result;
            }
        }

        public int NumberOfFields
        {
            get
            {
                AssertClass(ClassID.Struct, "NumberOfFields");
                return LibMx.mxGetNumberOfFields(m_array);
            }
        }

        void AssertFieldExists(int field_number)
        {
            if (field_number >= NumberOfFields)
            {
                throw new ArgumentOutOfRangeException("field_number", "This field doesn't exists");
            }
        }

        public void RemoveField(int field_number)
        {
            AssertClass(ClassID.Struct, "RemoveField");
            AssertFieldExists(field_number);
            LibMx.mxRemoveField(m_array, field_number);
        }

        public void RemoveField(string field_name)
        {
            RemoveField(GetFieldNumber(field_name));
        }

        public int AddField(string field_name)
        {
            AssertClass(ClassID.Struct, "AddField");
            int index = LibMx.mxAddField(m_array, field_name);
            if (index < 0)
            {
                throw new Exception(String.Format("Unable to add the field {0}", field_name));
            }
            return index;
        }

        public int GetFieldNumber(string field_name)
        {
            AssertClass(ClassID.Struct, "GetFieldNumber");
            return LibMx.mxGetFieldNumber(m_array, field_name);
        }

        public string GetFieldName(int field_number)
        {
            AssertClass(ClassID.Struct, "GetFieldName");
            AssertFieldExists(field_number);
            return LibMx.mxGetFieldNameByNumber(m_array, field_number);
        }

        public MxArray GetField(int index, string field_name)
        {
            AssertClass(ClassID.Struct, "GetField");
            return new MxArray(LibMx.mxGetField(m_array, index, field_name));
        }

        public MxArray GetField(int index, int field_number)
        {
            AssertClass(ClassID.Struct, "GetField");
            AssertFieldExists(field_number);
            return new MxArray(LibMx.mxGetFieldByNumber(m_array, index, field_number));
        }

        public void SetField(int index, int field_number, MxArray value)
        {
            AssertClass(ClassID.Struct, "SetField");
            AssertFieldExists(field_number);
            // From MATLAB documentation :
            //   This function does not free any memory allocated for existing data that it
            //   displaces. To free existing memory, call mxFree on the pointer returned by
            //   mxGetField before you call mxSetField.
            IntPtr oldFieldValue = LibMx.mxGetFieldByNumber(m_array, index, field_number);
            LibMx.mxFree(oldFieldValue);

            LibMx.mxSetFieldByNumber(m_array, index, field_number, value.m_array);
        }

        public void SetField(int index, string field_name, MxArray value)
        {
            AssertClass(ClassID.Struct, "SetField");
            SetField(index, GetFieldNumber(field_name), value);
        }

        public MxArray GetField(int[] coords, string field_name)
        {
            return GetField(IndexFromCoordinates(coords), field_name);
        }

        public MxArray GetField(int[] coords, int field_number)
        {
            return GetField(IndexFromCoordinates(coords), field_number);
        }

        public void SetField(int[] coords, int field_number, MxArray value)
        {
            SetField(IndexFromCoordinates(coords), field_number, value);
        }

        public void SetField(int[] coords, string field_name, MxArray value)
        {
            SetField(IndexFromCoordinates(coords), field_name, value);
        }
	}
}
