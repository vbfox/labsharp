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
    /// <para>A class to wrap a matlab engine instance.</para>
    /// <para>Functions are the same that the ones in LibEng but are C#
    /// friendlly and check return codes.</para>
    /// </summary>
    public class Engine : IDisposable
    {
        public class VariableNotFoundException : Exception
        {
            public VariableNotFoundException(string var_name)
                : base(string.Format("Unable to find variable \"{0}\" in matlab workspace",
                var_name))
            {
            }
        }

        IntPtr m_engine;
        public IntPtr NativeObject { get { return m_engine; } }

        bool m_closeOnDispose;

        private void CheckPointer()
        {
            if (m_engine == IntPtr.Zero)
            {
                throw new Exception("Invalid engine pointer");
            }
        }

        public static Engine Open()
        {
            return Open(false);
        }

        public static Engine Open(bool closeOnDispose)
        {
            return new Engine(LibEng.engOpen(), closeOnDispose);
        }

        public static Engine OpenSingleUse()
        {
            return new Engine(LibEng.engOpenSingleUse(), true);
        }

        private Engine(IntPtr engine, bool closeOnDispose)
        {
            if (engine == IntPtr.Zero) throw new Exception("engine pointer is NULL");
            m_engine = engine;
            m_closeOnDispose = closeOnDispose;
        }

        public void Close()
        {
            CheckPointer();
            if (LibEng.engClose(m_engine) != 0)
            {
                throw new Exception("Unable to close this Engine, it may already have been closed");
            }
        }

        public void Eval(string cmd)
        {
            CheckPointer();
            if (LibEng.engEvalString(m_engine, cmd) != 0)
            {
                throw new Exception("Unable to EvalString, it is possible that this engine doesn't exists anymore");
            }
        }

        public bool Visible
        {
            get
            {
                CheckPointer();
                bool value;
                LibEng.engGetVisible(m_engine, out value);
                return value;
            }
            set
            {
                CheckPointer();
                LibEng.engSetVisible(m_engine, value);
            }
        }

        public void OutputToBuffer(IntPtr buffer, int bufferSize)
        {
            CheckPointer();
            LibEng.engOutputBuffer(m_engine, buffer, bufferSize);
        }

        public void DisableOutputToBuffer()
        {
            OutputToBuffer(IntPtr.Zero, 0);
        }

        #region Get/Set MxArray

        public void SetVariable(string var_name, MxArray array)
        {
            CheckPointer();
            if (LibEng.engPutVariable(m_engine, var_name, array.NativeObject) != 0)
            {
                throw new Exception(String.Format("Unable to put variable {0} in Matlab Engine", var_name));
            }
        }

        public MxArray GetVariable(string var_name)
        {
            CheckPointer();
            IntPtr var_ptr = LibEng.engGetVariable(m_engine, var_name);
            if (var_ptr == IntPtr.Zero)
            {
                return null;
            }
            else
            {
                return new MxArray(var_ptr);
            }
        }

        #endregion

        #region SetVariable<TType>
        
        public void SetVariable<TType>(string var_name, TType value)
        {
           using (MxArray array = MxConvert.ToMxArray<TType>(value))
           {
               SetVariable(var_name, array);
           }
        }

        #endregion

        #region GetVariable<TType>

        public TType GetVariable<TType>(string var_name)
        {
            return GetVariable<TType>(var_name, false);
        }

        public TType GetVariable<TType>(string var_name, bool noVectorization)
        {
            using (MxArray array = GetVariable(var_name))
            {
                if (array == null) { throw new VariableNotFoundException(var_name); }
                try
                {
                    return MxConvert.FromMxArray<TType>(array, noVectorization);
                }
                catch (InvalidCastException e)
                {
                    throw new InvalidCastException(string.Format(
                        "Unable to get the variable \"{0}\" as {1}.",
                        var_name, typeof(TType).Name), e);
                }
            }
        }

        public TType GetVariable<TType>(string var_name, TType defaultValue)
        {
            return GetVariable<TType>(var_name, defaultValue, false);
        }

        public TType GetVariable<TType>(string var_name, TType defaultValue, bool noVectorization)
        {
            try
            {
                return GetVariable<TType>(var_name, noVectorization);
            }
            catch (VariableNotFoundException)
            {
                return defaultValue;
            }
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (m_closeOnDispose)
            {
                Close();
            }
        }

        #endregion
    }
}
