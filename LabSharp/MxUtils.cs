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

namespace LabSharp
{
    public static class MxUtils
    {
        /// <summary>
        /// Convert coordinates in an array to the index of the data in matlab.
        /// </summary>
        /// <param name="coordinates">Coordinates to convert</param>
        /// <param name="dimensions">Dimensions of the datas</param>
        /// <returns>Index of the data at specified coordinate in matlab</returns>
        public static int IndexFromCoordinates(int[] coordinates, int[] dimensions)
        {
            if (dimensions.Length == 0) throw new ArgumentOutOfRangeException("dimensions", "Length is 0");
            if (dimensions.Length != coordinates.Length) throw new ArgumentException("Arrays don't have the same size");

            int[] coefs = new int[dimensions.Length];
            int currentCoef = 1;
            for (int i = 0; i < dimensions.Length; i++)
            {
                coefs[i] = currentCoef;
                currentCoef *= dimensions[i];
            }

            int result = 0;
            for (int i = 0; i < dimensions.Length; i++)
            {
                if (dimensions[i] <= 0) throw new ArgumentOutOfRangeException("dimensions", "Dimensions should be > 0");
                if (coordinates[i] < 0) throw new ArgumentOutOfRangeException("coordinates", "Coordinates should be >= 0");
                if (coordinates[i] >= dimensions[i]) throw new ArgumentOutOfRangeException("coordinates", "One coordinate is out of the array");

                result += coordinates[i] * coefs[i];
            }

            return result;
        }

        /// <summary>
        /// Convert an index in matlab to the coordinates in an array of some dimensions.
        /// </summary>
        /// <param name="index">Index in matlab</param>
        /// <param name="dimensions">Dimensions of the datas</param>
        /// <returns>Coordinates of the data at the <paramref name="index"/>.</returns>
        public static int[] CoordinatesFromIndex(int index, int[] dimensions)
        {
            if (dimensions.Length == 0) throw new ArgumentOutOfRangeException("dimensions", "Length is 0");
            if (index < 0) throw new ArgumentOutOfRangeException("index", "Negative index");
            int maxIndex = 1;
            foreach (int dimLength in dimensions)
            {
                if (dimLength == 0) throw new ArgumentOutOfRangeException("dimensions", "One of the dimensions is 0");
                if (dimLength < 0) throw new ArgumentOutOfRangeException("dimensions", "One of the dimensions is negative");
                maxIndex *= dimLength;
            }
            maxIndex--;
            if (index > maxIndex)
            {
                throw new ArgumentOutOfRangeException("index", "Out of the array");
            }

            int[] result = new int[dimensions.Length];

            int[] coefs = new int[dimensions.Length];
            int currentCoef = 1;
            for (int i = 0; i < dimensions.Length; i++)
            {
                coefs[i] = currentCoef;
                currentCoef *= dimensions[i];
            }

            for (int i = dimensions.Length - 1; i >= 0; i--)
            {
                result[i] = Math.DivRem(index, coefs[i], out index);
            }

            if (index > 0)
            {
                throw new ArgumentOutOfRangeException("index", "Out of the array");
            }

            return result;
        }

        public static string DimensionsToString(int[] dimensions)
        {
            StringBuilder s = new StringBuilder();
            bool first = true;
            foreach (int dim in dimensions)
            {
                if (!first)
                {
                    s.Append("x");
                }
                else
                {
                    first = false;
                }
                s.Append(dim);
                
            }
            return s.ToString();
        }
    }
}
