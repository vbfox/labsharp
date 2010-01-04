using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LabSharp.Scilab;
using System.Diagnostics;

namespace tmp_scilab_bin
{
    interface IScilabFileHandler
    {
        void FloatMatrix(string name, int rows, int columns, bool isComplex, BinaryReader reader);
    }

    /// <summary>
    /// Low level event based parser for scilab binary files.
    /// </summary>
    class ScilabFileReader
    {
        public ScilabFileReader()
        {
        }

        public void Parse(Stream stream, IScilabFileHandler handler)
        {
            var parser = new Parser(stream, handler);
            parser.Parse();
        }

        class Parser
        {
            Stream m_stream;
            BinaryReader m_reader;
            IScilabFileHandler m_handler;

            public Parser(Stream stream, IScilabFileHandler handler)
            {
                m_stream = stream;
                m_handler = handler;

                m_reader = new BinaryReader(stream);
            }

            [ThreadStatic]
            static int[] m_nameBuffer;

            const int NAME_LENGTH = 6;

            string ReadVariableName()
            {
                if (m_nameBuffer == null) m_nameBuffer = new int[NAME_LENGTH];

                for (var i = 0; i < NAME_LENGTH; i++)
                {
                    m_nameBuffer[i] = m_reader.ReadInt32();
                }

                return Utils.VariableNameToString(m_nameBuffer).TrimEnd();
            }

            VariableType ReadVariableType()
            {
                return (VariableType)m_reader.ReadInt32();
            }

            void ParseFloatMatrix(string name)
            {
                var rows = m_reader.ReadInt32();
                var columns = m_reader.ReadInt32();
                var isComplex = m_reader.ReadInt32() != 0;

                var positionBefore = m_stream.Position;
                long size = (long)rows * (long)columns;
                if (isComplex) size *= 2;
                size *= sizeof(double);

                m_handler.FloatMatrix(name, rows, columns, isComplex, m_reader);

                m_stream.Position = positionBefore + size;
            }

            void ReadVariable()
            {
                var name = ReadVariableName();
                var type = ReadVariableType();

                switch (type)
                {
                    case VariableType.FloatMatrix:
                        ParseFloatMatrix(name);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }

            public void Parse()
            {
                while (m_stream.Position < m_stream.Length - 1)
                {
                    ReadVariable();
                    //break;
                }
            }
        }
    }

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

        class ConsoleScilabFileHandler : IScilabFileHandler
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
            var stream = new FileStream(@"C:\test.scilab", FileMode.Open);
            var reader = new ScilabFileReader();
            reader.Parse(stream, new ConsoleScilabFileHandler());
            Console.ReadLine();
        }
    }
}
