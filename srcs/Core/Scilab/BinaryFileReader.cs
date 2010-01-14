using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace LabSharp.Scilab
{
    /// <summary>
    /// Low level event based parser for scilab binary files.
    /// </summary>
    public class BinaryFileReader
    {
        public BinaryFileReader()
        {
        }

        public void Parse(Stream stream, IBinaryFileHandler handler)
        {
            var parser = new Parser(stream, handler);
            parser.Parse();
        }

        class Parser
        {
            Stream m_stream;
            BinaryReader m_reader;
            IBinaryFileHandler m_handler;

            public Parser(Stream stream, IBinaryFileHandler handler)
            {
                Debug.Assert(stream != null);
                Debug.Assert(stream.CanRead);
                Debug.Assert(stream.CanSeek);
                Debug.Assert(handler != null);

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

                m_reader.ReadArray(m_nameBuffer, r => r.ReadInt32());

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
}