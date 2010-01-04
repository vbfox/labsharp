using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabSharp.Scilab
{
    public enum VariableType : int
    {
        FloatMatrix = 1,
        Polynomials = 2,
        SizeImplicitIndices = 129,
        Booleans = 4,
        FloatingSparseMatrix = 5,
        BooleanSparseMatrix = 6,
        MatlabSparseMatrix = 7,
        IntegerMatrix = 8,
        HandleMatrix = 9,
        StringMatrix = 10,
        UncompiledFunction = 11,
        CompiledFunction = 13,
        Library = 14,
        List = 15,
        TList = 16,
        MList = 17,
        Pointers = 128,
        FunctionPointer = 130,
    };

    public class Utils
    {
        static char SCILAB_CODE_BLANK = (char)40;

        static char[] alfa = new char[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
		    'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
		    'u', 'v', 'w', 'x', 'y', 'z', '_', '#', '!', '$',
		    ' ', '(', ')', ';', ':', '+', '-', '*', '/', '\\',
		    '=', '.', ',', '\'', '[', ']', '%', '|', '&', '<',
		    '>', '~', '^',
        };

        static char[] alfb = new char[]
        {
		    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
		    'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
		    'U', 'V', 'W', 'X', 'Y', 'Z', '0', '0', '?', '0',
		    '\t', '0', '0', '0', '0', '0', '0', '0', '0', '$',
		    '0', '0', '0', '"', '{', '}', '0', '0', '0', '`',
		    '0', '@', '0',
        };

        public static int[] StringToVariableName(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            var result = new int[(int)Math.Ceiling(name.Length / 4.0)];

            var strLen = name.Length;

            for (var i = 0; i < result.Length; i++)
            {
                for (var j = 1; j <= 4; j++)
                {
                    var ii = (i * 4) + 4 - j;
                    var scilabCode = (ii >= strLen) ? SCILAB_CODE_BLANK : ConvertAsciiCodeToScilabCode(name[ii]);

                    result[i] = (result[i] << 8) + scilabCode;
                }
            }

            return result;
        }

        public static string VariableNameToString(int[] name)
        {
            if (name == null) throw new ArgumentNullException("name");

            var result = new StringBuilder(name.Length * 4);

            foreach (var current in name)
            {
                var current_ = current;

                for (var x = 0; x < 4; x++)
                {
                    var k = (current_ + 128) / 256;
                    if (k < 0) k -= 1;
                    var currentChar = current_ - (k << 8);
                    current_ = k;

                    var charArray = (currentChar > 0) ? alfa : alfb;
                    currentChar = Math.Abs(currentChar);

                    if (currentChar >= 63)
                    {
                        result.Append('*');
                    }
                    else
                    {
                        result.Append(charArray[currentChar]);
                    }
                }
            }

            return result.ToString();
        }

        static int[] InternalCharactersTableCodesForScilab = new[]
        { 100,101,102,103,104,105,106,107,108,-40,
          110,111,112,113,114,115,116,117,118,119,
          120,121,122,123,124,125,126,127,128,129,
          130,131, 40, 38,-53, 37, 39, 56, 58, 53,
           41, 42, 47, 45, 52, 46, 51, 48,  0,  1,
            2,  3,  4,  5,  6,  7,  8,  9, 44, 43,
           59, 50, 60,-38,-61,-10,-11,-12,-13,-14,
          -15,-16,-17,-18,-19,-20,-21,-22,-23,-24,
          -25,-26,-27,-28,-29,-30,-31,-32,-33,-34,
          -35, 54, 49, 55, 62, 36,-59, 10, 11, 12,
           13, 14, 15, 16, 17, 18, 19, 20, 21, 22,
           23, 24, 25, 26, 27, 28, 29, 30, 31, 32,
           33, 34, 35,-54, 57,-55, 61, 227 };

        static int ConvertAsciiCodeToScilabCode(char scilabChar)
        {
            if (scilabChar < InternalCharactersTableCodesForScilab.Length)
            {
                return InternalCharactersTableCodesForScilab[scilabChar];
            }
            else
            {
                return scilabChar + 100;
            }
        }
    }
}
