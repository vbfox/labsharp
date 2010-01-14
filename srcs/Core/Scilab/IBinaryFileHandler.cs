using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabSharp.Scilab
{
    public interface IBinaryFileHandler
    {
        void FloatMatrix(string name, int rows, int columns, bool isComplex, BinaryReader reader);
    }
}
