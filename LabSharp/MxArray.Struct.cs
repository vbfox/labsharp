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
        public static MxArray CreateStruct()
        {
            return CreateArray(new int[] { 1 } , ClassID.Struct, Complexity.Real);
        }

        void AssertFieldExists(int field_number)
        {
            if (field_number >= NumberOfFields)
            {
                throw new ArgumentOutOfRangeException("field_number", "This field doesn't exists");
            }
        }

        /// <summary>
        /// Get the name of all fields.
        /// </summary>
        public string[] FieldNames
        {
            get
            {
                AssertClass(ClassID.Struct, "get_FieldNames");
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

        public void RemoveField(int field_number)
        {
            AssertClass(ClassID.Struct, "RemoveField");
            AssertFieldExists(field_number);
            LibMx.mxRemoveField(m_array, field_number);

            // We can't known from what field our childs where coming, remove them all
            ClearChilds();
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
                //FIXME: Put a better Exception type and error message
                throw new Exception(String.Format("Unable to add the field {0}", field_name));
            }
            return index;
        }

        /// <summary>
        /// Get the index of the field named field_name, returns -1 if the field doesn't
        /// exists.
        /// </summary>
        int _GetFieldNumber(string field_name)
        {
            AssertClass(ClassID.Struct, "GetFieldNumber");
            return LibMx.mxGetFieldNumber(m_array, field_name);
        }

        /// <summary>
        /// Get the index of the field named field_name, throw an ArgumentException
        /// if the field doesn't exists.
        /// </summary>
        public int GetFieldNumber(string field_name)
        {
            int index = _GetFieldNumber(field_name);
            if (index < 0)
                throw new ArgumentException(string.Format("The field named {0} doesn't exists", field_name), "field_name");
            return index;
        }

        public string GetFieldName(int field_number)
        {
            AssertClass(ClassID.Struct, "GetFieldName");
            AssertFieldExists(field_number);
            return LibMx.mxGetFieldNameByNumber(m_array, field_number);
        }

        #region GetField

        const string DIRECT_GET_FIELD_EXCEPTION = "To get a field without an index the matrix need to be 1x1";

        public MxArray GetField(string field_name)
        {
            if (NumberOfElements > 1) throw new InvalidOperationException(DIRECT_GET_FIELD_EXCEPTION);
            return GetField(0, field_name);
        }

        public MxArray GetField(int index, string field_name)
        {
            AssertClass(ClassID.Struct, "GetField");

            IntPtr arrayPtr = LibMx.mxGetField(m_array, index, field_name);
            return new MxArray(arrayPtr, this);
        }

        public MxArray GetField(int field_number)
        {
            if (NumberOfElements > 1) throw new InvalidOperationException(DIRECT_GET_FIELD_EXCEPTION);
            return GetField(0, field_number);
        }

        public MxArray GetField(int index, int field_number)
        {
            AssertClass(ClassID.Struct, "GetField");
            AssertFieldExists(field_number);
            
            IntPtr arrayPtr = LibMx.mxGetFieldByNumber(m_array, index, field_number);
            return new MxArray(arrayPtr, this);
        }

        public TType GetField<TType>(string field_name)
        {
            if (NumberOfElements > 1) throw new InvalidOperationException(DIRECT_GET_FIELD_EXCEPTION);
            return GetField<TType>(0, field_name);
        }

        public TType GetField<TType>(int index, string field_name)
        {
            using (MxArray array = GetField(index, field_name))
            {
                return MxConvert.FromMxArray<TType>(array);
            }
        }

        public TType GetField<TType>(int field_number)
        {
            if (NumberOfElements > 1) throw new InvalidOperationException(DIRECT_GET_FIELD_EXCEPTION);
            return GetField<TType>(0, field_number);
        }

        public TType GetField<TType>(int index, int field_number)
        {
            using(MxArray array = GetField(index, field_number))
            {
                return MxConvert.FromMxArray<TType>(array);
            }
        }

        public MxArray GetField(int[] coords, string field_name)
        {
            return GetField(IndexFromCoordinates(coords), field_name);
        }

        public MxArray GetField(int[] coords, int field_number)
        {
            return GetField(IndexFromCoordinates(coords), field_number);
        }

        public TType GetField<TType>(int[] coords, string field_name)
        {
            using (MxArray array = GetField(coords, field_name))
            {
                return MxConvert.FromMxArray<TType>(array);
            }
        }

        public TType GetField<TType>(int[] coords, int field_number)
        {
            using (MxArray array = GetField(coords, field_number))
            {
                return MxConvert.FromMxArray<TType>(array);
            }
        }

        #endregion

        #region SetField

        const string DIRECT_SET_FIELD_EXCEPTION = "To set a field without an index the matrix need to be 1x1";

        public void SetField(int field_number, MxArray value)
        {
            if (NumberOfElements > 1) throw new InvalidOperationException(DIRECT_SET_FIELD_EXCEPTION);
            SetField(0, field_number, value);
        }

        public void SetField(int index, int field_number, MxArray value)
        {
            AssertClass(ClassID.Struct, "SetField");
            AssertFieldExists(field_number);

            if (value.Parent != null)
            {
                // We make a copy so we are sure that we could use this array as we wish.
                // We don't do this otherwise for performance reasons.
                value = value.Clone();
            }

            IntPtr oldFieldValue = LibMx.mxGetFieldByNumber(m_array, index, field_number);
            
            RemoveChilds(oldFieldValue);
            LibMx.mxDestroyArray(oldFieldValue);

            LibMx.mxSetFieldByNumber(m_array, index, field_number, value.m_array);
            value.Parent = this;
        }

        public void SetField<TType>(int field_number, TType value)
        {
            if (NumberOfElements > 1) throw new InvalidOperationException(DIRECT_SET_FIELD_EXCEPTION);
            SetField<TType>(0, field_number, value);
        }

        public void SetField<TType>(int index, int field_number, TType value)
        {
            AssertClass(ClassID.Struct, "SetField");
            using(MxArray array = MxConvert.ToMxArray(value))
            {
                SetField(index, field_number, value);
            }
        }

        public void SetField(string field_name, MxArray value)
        {
            if (NumberOfElements > 1) throw new InvalidOperationException(DIRECT_SET_FIELD_EXCEPTION);
            SetField(0, field_name, value);
        }

        public void SetField(int index, string field_name, MxArray value)
        {
            AssertClass(ClassID.Struct, "SetField");
            int fieldIndex = _GetFieldNumber(field_name);
            if (fieldIndex < 0)
            {
                // If the field doesn't exists we automatically add it
                fieldIndex = AddField(field_name);
            }
            SetField(index, fieldIndex, value);
        }

        public void SetField<TType>(string field_name, TType value)
        {
            if (NumberOfElements > 1) throw new InvalidOperationException(DIRECT_SET_FIELD_EXCEPTION);
            SetField<TType>(0, field_name, value);
        }

        public void SetField<TType>(int index, string field_name, TType value)
        {
            AssertClass(ClassID.Struct, "SetField");
            using (MxArray array = MxConvert.ToMxArray(value))
            {
                SetField(index, field_name, array);
            }
        }

        public void SetField(int[] coords, int field_number, MxArray value)
        {
            SetField(IndexFromCoordinates(coords), field_number, value);
        }

        public void SetField(int[] coords, string field_name, MxArray value)
        {
            SetField(IndexFromCoordinates(coords), field_name, value);
        }

        public void SetField<TType>(int[] coords, int field_number, TType value)
        {
            using (MxArray array = MxConvert.ToMxArray(value))
            {
                SetField(coords, field_number, array);
            }
        }

        public void SetField<TType>(int[] coords, string field_name, TType value)
        {
            using (MxArray array = MxConvert.ToMxArray(value))
            {
                SetField(coords, field_name, array);
            }
        }

        #endregion

        bool IsField(string field_name)
        {
            return _GetFieldNumber(field_name) >= 0;
        }
    }
}
