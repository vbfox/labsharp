namespace LabSharp.Tests.Scilab
{
    #region using
    using System.IO;
    using LabSharp.Scilab;
    using Xunit;
    #endregion

    public class BinaryFileReaderTest
    {
        class TestBinaryFileHandler : IBinaryFileHandler
        {
            public delegate void FloatMatrixDelegate(string name, int rows, int columns, bool isComplex, BinaryReader reader);

            public event FloatMatrixDelegate OnFloatMatrix;

            public void FloatMatrix(string name, int rows, int columns, bool isComplex, BinaryReader reader)
            {
                if (OnFloatMatrix != null)
                {
                    OnFloatMatrix(name, rows, columns, isComplex, reader);
                }
            }
        }

        [Fact]
        void CouldReadFloatMatrix()
        {
            // Test with [1 2; 3 4]
            var reader = new BinaryFileReader();
            var stream = new MemoryStream(TestFiles.float_matrix);
            var handler = new TestBinaryFileHandler();

            var floatMatrixCount = 0;
            handler.OnFloatMatrix += (string name, int rows, int columns, bool isComplex, BinaryReader br) =>
                {
                    Assert.Equal("float_matrix", name);
                    Assert.Equal(2, rows);
                    Assert.Equal(2, rows);
                    Assert.Equal(false, isComplex);
                    Assert.NotNull(reader);

                    Assert.Equal(1, br.ReadDouble());
                    Assert.Equal(3, br.ReadDouble());
                    Assert.Equal(2, br.ReadDouble());
                    Assert.Equal(4, br.ReadDouble());
                    floatMatrixCount++;
                };

            reader.Parse(stream, handler);
            Assert.Equal(1, floatMatrixCount);
        }

        [Fact]
        void CouldReadComplexMatrix()
        {
            // Test with [1+1*%i 2+2*%i; 3+3*%i 4+4*%i]
            var reader = new BinaryFileReader();
            var stream = new MemoryStream(TestFiles.complex_matrix);
            var handler = new TestBinaryFileHandler();

            var floatMatrixCount = 0;
            handler.OnFloatMatrix += (string name, int rows, int columns, bool isComplex, BinaryReader br) =>
            {
                Assert.Equal("complex_matrix", name);
                Assert.Equal(2, rows);
                Assert.Equal(2, rows);
                Assert.Equal(true, isComplex);
                Assert.NotNull(reader);
                
                Assert.Equal(1, br.ReadDouble());
                Assert.Equal(3, br.ReadDouble());
                Assert.Equal(2, br.ReadDouble());
                Assert.Equal(4, br.ReadDouble());

                Assert.Equal(1, br.ReadDouble());
                Assert.Equal(3, br.ReadDouble());
                Assert.Equal(2, br.ReadDouble());
                Assert.Equal(4, br.ReadDouble());
                floatMatrixCount++;
            };

            reader.Parse(stream, handler);
            Assert.Equal(1, floatMatrixCount);
        }
    }
}
