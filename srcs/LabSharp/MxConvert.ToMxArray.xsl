<?xml version="1.0"?>
<!-- We use 2 languages and have to mix indent of both, in this case vim isn't so smart -->
<!-- vim: set nosi ai indentexpr= et ts=4 sw=4: -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="text" encoding="UTF-8" indent="yes" />
<xsl:preserve-space elements="*" />
<xsl:template match="/MxConvert">/*
 * Lab# - Matlab interaction library for .Net
 * 
 * Copyright (C) 2006 Julien Roncaglia
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

/*
 * THIS CODE IS GENERATED BY MxConvert.xsl FROM MxConvert.xml, 
 * DO NOT EDIT DIRECTLY.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace LabSharp
{
    public static partial class MxConvert
	{
		static MxArray _ToMxArray&lt;TType&gt;(TType value)
		{
			Type genericType = typeof(TType);
            Type convertFromDataType;
            bool isArray;
            bool isComplexType;
			ExtractTypeInfos(genericType, out convertFromDataType, out isArray, out isComplexType);
            
            <xsl:apply-templates select="Convert" mode="if-on-type" />
                throw new Exception("Boom!"); //FIXME
        }

        <xsl:apply-templates select="Convert" mode="mxarray-from-functions" />

	}
}
</xsl:template>

<xsl:template match="Convert" mode="if-on-type">
            if (convertFromDataType == typeof(<xsl:value-of select="@csharpType" />))
            {
                if (isArray)
                {
                    
                    if (isComplexType)
                    {
                         return _MxArrayFrom<xsl:value-of select="@name" />Array_Cplx(
                            (Array)(Object)value);
                    }
                    else
                    {
                         return _MxArrayFrom<xsl:value-of select="@name" />Array(
                            (Array)(Object)value);
                    }
                }
                else
                {
                    if (isComplexType)
                    {
                        return _MxArrayFrom<xsl:value-of select="@name" />_Cplx(
                            (Complex&lt;<xsl:value-of select="@csharpType" />&gt;)(Object)value);
                    }
                    else
                    {
                        return _MxArrayFrom<xsl:value-of select="@name" />(
                            (<xsl:value-of select="@csharpType" />)(Object)value);
                    }            
                }
            }
            else
</xsl:template>

<xsl:template match="Convert" mode="mxarray-from-functions">
        unsafe static MxArray _MxArrayFrom<xsl:value-of select="@name" />(
            <xsl:value-of select="@csharpType" /> value)
        {
            MxArray result = MxArray.CreateArray(new int[] { 1 },
                ClassID.<xsl:value-of select="@matlabType" />, Complexity.Real);
            <xsl:value-of select="@csharpType" />* pr;
            pr = (<xsl:value-of select="@csharpType" />*)result.RealElements;
            *pr = value;

            return result;
        }

        static MxArray _MxArrayFrom<xsl:value-of select="@name" />Array(
            Array value)
        {
            int count = value.Length;
            int[] arraydims = MxUtils.GetArrayDimensions(value);
            int[] dims;
            if (value.Rank == 1)
            {
                dims = new int[] {1, count};
            }
            else
            {
                dims = (int[])arraydims.Clone();
            }
            MxArray result = MxArray.CreateArray(dims,
                ClassID.<xsl:value-of select="@matlabType" />, Complexity.Real);
            unsafe
            {
                <xsl:value-of select="@csharpType" />* pr;
                pr = (<xsl:value-of select="@csharpType" />*)result.RealElements;

                for(int i = 0; i &lt; count; i++)
                {
                    *pr++ = (<xsl:value-of select="@csharpType" />)
                        value.GetValue(MxUtils.CoordinatesFromIndex(i, arraydims));
                }
            }

            return result;
        }

        unsafe static MxArray _MxArrayFrom<xsl:value-of select="@name" />_Cplx(
            Complex&lt;<xsl:value-of select="@csharpType" />&gt; value)
        {
            MxArray result = MxArray.CreateArray(new int[] { 1 },
                ClassID.<xsl:value-of select="@matlabType" />, Complexity.Complex);
            <xsl:value-of select="@csharpType" />* pr, pi;

            pr = (<xsl:value-of select="@csharpType" />*)result.RealElements;
            pi = (<xsl:value-of select="@csharpType" />*)result.ImaginaryElements;

            *pr = value.RealPart;
            *pi = value.ImaginaryPart;

            return result;
        }

        static MxArray _MxArrayFrom<xsl:value-of select="@name" />Array_Cplx(
            Array value)
        {
            int count = value.Length;
            int[] arraydims = MxUtils.GetArrayDimensions(value);
            int[] dims;
            if (value.Rank == 1)
            {
                dims = new int[] {1, count};
            }
            else
            {
                dims = (int[])arraydims.Clone();
            }
            MxArray result = MxArray.CreateArray(dims,
                ClassID.<xsl:value-of select="@matlabType" />, Complexity.Complex);
            unsafe
            {
                <xsl:value-of select="@csharpType" />* pr, pi;
                pr = (<xsl:value-of select="@csharpType" />*)result.RealElements;
                pi = (<xsl:value-of select="@csharpType" />*)result.ImaginaryElements;

                Complex&lt;<xsl:value-of select="@csharpType" />&gt; currentValue;
                for(int i = 0; i &lt; count; i++)
                {
                    currentValue = (Complex&lt;<xsl:value-of select="@csharpType" />&gt;)
                         value.GetValue(MxUtils.CoordinatesFromIndex(i, arraydims));
                    *pr++ = currentValue.RealPart;
                    *pi++ = currentValue.ImaginaryPart;
                }
            }

            return result;
        }
</xsl:template>

</xsl:stylesheet>
