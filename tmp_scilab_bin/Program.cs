using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LabSharp.Scilab;
using System.Diagnostics;

namespace tmp_scilab_bin
{
    class Program
    {
        /*
        static void Load(Stream s)
        {
            byte[] nameByteBuffer = new byte[6 * 4];
            int[] nameIntBuffer = new int[nameByteBuffer.Length / 4];
            BinaryReader r = new BinaryReader(s);
            while(s.Position != s.Length-1)
            {
                var name = ReadVariableName(r);
                var type = ReadVariableType(r);

                Console.WriteLine("\"{0}\" : {1}", name, type);
                return;
            }
            s.GetLifetimeService
        }
        */

        class ConsoleScilabFileHandler : IBinaryFileHandler
        {
            #region IScilabFileHandler Members

            public void FloatMatrix(string name, int rows, int columns, bool isComplex, BinaryReader reader)
            {
                var type = isComplex ? "Complex" : "double";
                Console.WriteLine("{0}[{1}, {2}] {3}", type, rows, columns, name);
            }

            #endregion
        }

        static void Main(string[] args)
        {
            var stream = new FileStream(@"T:\test.scilab", FileMode.Open);
            var reader = new BinaryFileReader();
            reader.Parse(stream, new ConsoleScilabFileHandler());
            Console.ReadLine();
        }
    }
}
