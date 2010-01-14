namespace LabSharp
{
    #region using
    using System;
    using System.IO;
    #endregion

    static class BinaryReaderExtensions
    {
        public static void ReadArray<T>(this BinaryReader reader, T[] destination, Func<BinaryReader, T> readFunc)
        {
            reader.ReadArray(destination, destination.Length, readFunc);
        }

        public static void ReadArray<T>(this BinaryReader reader, T[] destination, int count, Func<BinaryReader, T> readFunc)
        {
            for (var i = 0; i < count; i++)
            {
                destination[i] = readFunc(reader);
            }
        }
    }
}
