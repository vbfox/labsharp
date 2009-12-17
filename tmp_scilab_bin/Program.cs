using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

/*
      subroutine cvname(id,str,job)
c     =====================================
c     Scilab internal coding of vars to string 
c     =====================================
      include 'stack.h'
      integer id(nsiz),name(nlgh),ch,blank,star
      character*(*) str
      data blank/40/,star/47/
c
      if(job.ne.0) then 
         i1=1
         do 15 l=1,nsiz
            idl=id(l)
            do 10 i=i1,i1+3
               k=(idl+128)/256
               if(k.lt.0) k=k-1
               ch=idl-256*k
               idl=k
               if(abs(ch).ge.csiz) ch=star
               if(ch.gt.0) then
                  str(i:i)=alfa(ch+1)
               else
                  str(i:i)=alfb(-ch+1)
               endif
 10         continue
            i1=i1+4
 15      continue
      else
         ln=min(nlgh,len(str))
         call cvstr(ln,name,str,0)
         if(ln.lt.nlgh) call iset(nlgh-ln,blank,name(ln+1),1)
         i1=1
         do 30 l=1,nsiz
            id(l)=0
            do 20 i=1,4
               ii=i1+4-i
               id(l)=256*id(l)+name(ii)
 20         continue
            i1=i1+4
 30      continue
      endif
      return
      end
*/
/*
void setScilabCharactersCodes(void)
{
	static char alpha[csiz+1] ={ 
		"0" "1" "2" "3" "4" "5" "6" "7" "8" "9"
		"a" "b" "c" "d" "e" "f" "g" "h" "i" "j"
		"k" "l" "m" "n" "o" "p" "q" "r" "s" "t"
		"u" "v" "w" "x" "y" "z" "_" "#" "!" "$"
		" " "(" ")" ";" ":" "+" "-" "*" "/" "\\"
		"=" "." "," "'" "[" "]" "%" "|" "&" "<"
		">" "~" "^"};
	static char alphb[csiz+1] ={ 
		"0" "1" "2" "3" "4" "5" "6" "7" "8" "9"
		"A" "B" "C" "D" "E" "F" "G" "H" "I" "J"
		"K" "L"	"M" "N" "O" "P" "Q" "R" "S" "T"
		"U" "V" "W"	"X" "Y" "Z" "0" "0" "?" "0"
		"\t" "0" "0" "0" "0" "0" "0" "0" "0" "$"
		"0" "0" "0" "\"" "{" "}" "0" "0" "0" "`"
		"0" "@" "0"};

	int i = 0;
	for (i = 0; i < csiz; i++) 
	{
		*(unsigned char *)&C2F(cha1).alfa[i] = *(unsigned char *)&alpha[i];
		*(unsigned char *)&C2F(cha1).alfb[i] = *(unsigned char *)&alphb[i];
	}
}
*/

namespace tmp_scilab_bin
{
    class Program
    {
        static char blank = (char)40;
        static char star = (char)47;

        static char[] alfa = new char[CSIZ]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
		    'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
		    'u', 'v', 'w', 'x', 'y', 'z', '_', '#', '!', '$',
		    ' ', '(', ')', ';', ':', '+', '-', '*', '/', '\\',
		    '=', '.', ',', '\'', '[', ']', '%', '|', '&', '<',
		    '>', '~', '^',
        };

        static char[] alfb = new char[CSIZ]
        {
		    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
		    'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
		    'U', 'V', 'W', 'X', 'Y', 'Z', '0', '0', '?', '0',
		    '\t', '0', '0', '0', '0', '0', '0', '0', '0', '$',
		    '0', '0', '0', '"', '{', '}', '0', '0', '0', '`',
		    '0', '@', '0',
        };

        static char c__0 = (char)0;
        static char c__1 = (char)1;

        static unsafe int i_len(char* a, int b)
        {
            return b;
        }

        static unsafe void cvstr_(int a, int* b, char* c, char d, int e)
        {

        }

        static unsafe void c2f0(int* id, char* str, int str_len)
        {
            int i__1, i__2;
            int ii, ln;
            int[] name = new int[24];
            fixed (int* name__ = name)
            {

                // Computing MIN
                i__1 = 24;
                i__2 = i_len(str, str_len);
                ln = Math.Min(i__1, i__2);
                cvstr(ln, name__, str, 0, str_len);
                /*if (ln < 24)
                {
                    i__1 = 24 - ln;
                    iset_(&i__1, blank, &name__[ln], c__1);
                }*/
                var currentChar = 1;
                for (var l = 0; l < 6; ++l)
                {
                    id[l] = 0;
                    for (var i__ = 1; i__ <= 4; ++i__)
                    {
                        ii = currentChar + 4 - i__;
                        id[l] = (id[l] << 8) + str[ii - 1];
                    }
                    currentChar += 4;
                }
            }
        }

        static unsafe string VariableNameToString(int[] name)
        {
            if (name == null) throw new ArgumentNullException("name");

            var result = new StringBuilder(name.Length * 4);

            foreach(var current in name)
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

        static int[] InternalCharactersTableCodesForScilab = new []
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
            // See modules\string\src\c\getfastcode.c

	        int k = (int)scilabChar ;

            if (k < InternalCharactersTableCodesForScilab.Length) 
	        {
		        return InternalCharactersTableCodesForScilab[k];
	        }
	        else 
	        {
		        return k + 100;
	        }
        }
        const int CSIZ = 63;
        const int EOL = 99;
        const char EXCLAMATION_CHAR = '!';

        static char ConvertScilabCodeToAsciiCode(int scilabCode)
        {
            // See modules\string\src\c\getfastcode.c

	        if (scilabCode == EOL) return EXCLAMATION_CHAR;

	        if (Math.Abs(scilabCode) > CSIZ) 
	        {
		        if (scilabCode > EOL) 
		        {
			        return (char)(scilabCode - (EOL + 1));
		        }
		        else 
		        {
			        return EXCLAMATION_CHAR;
		        }
	        } 
	        else
	        {
		        if (scilabCode < 0) 
		        {
                    return alfb[Math.Abs(scilabCode)];
		        }
		        else 
		        {
			        return alfa[scilabCode];
		        }
	        }
        }

        static unsafe int strlen(char* s)
        {
            int result = 0;
            while (*s++ != '\0') result++;
            return result;
        }

        /* Table of constant values */
        static int cx1 = 1;
        static int c_n1 = -1;
        /*--------------------------------------------------------------------------*/
        static unsafe int cvstr(int n, int* line, char* str, int job, long str_len)
        {
          if (job == 0) asciitocode(n, line, str, cx1, str_len);
          else codetoascii(n, line, str, str_len);
          return 0;
        }

        static unsafe int asciitocode(int n,int * line, char * str,int flagx, long  str_len)
        {
	        if (flagx == 1) 
	        {
		        int j = 0;
		        var nn = n; 
		        while (nn-- > 0)
		        {
			        char current_char = str[j];
			        line[j] = ConvertAsciiCodeToScilabCode(current_char);
			        j++;
		        }
	        } 
	        else 
	        {
		        int l = 0;

		        /* check *n value */
		        if ( (int)strlen(str) > n ) l = (int)strlen(str);
		        else l = n;

		        for (var nn = l - 1 ; nn >= 0; --nn) 
		        {
			        char current_char = str[nn];
			        line[nn] = ConvertAsciiCodeToScilabCode(current_char);
		        }
	        }
	        return 0;
        }

        static unsafe int codetoascii(int n,int * line,char * str, long str_len)
        {
          int j = 0;
          int nn = 0;

          /* check *n value */
          if  (n >= 0) nn = n;
          
          /* conversion code -> ascii */
          while (nn-- > 0)
          {
	          str[j] = ConvertScilabCodeToAsciiCode(line[j]);
	          j++;
          }
          return 0;
        }

        static void Load(Stream s)
        {
            while(s.Position != s.Length-1)
            {
                byte[] buffer = new byte[6*4];
                s.Read(buffer, 0, buffer.Length);
                int[] buffer2 = new int[buffer.Length/4];
                for (int i = 0; i < buffer.Length; i+=4)
                {
                    buffer2[i/4] = BitConverter.ToInt32(buffer, i);
                    Console.WriteLine("0x{0:X8}", buffer2[i/4]);
                }
                string name = VariableNameToString(buffer2);
                Console.WriteLine("[{0}]", name);

                int[] buffer3 = new int[6] { 0x00BEDEAD, 0x00BEDEAD, 0x00BEDEAD, 0x00BEDEAD, 0x00BEDEAD, 0x00BEDEAD };
                unsafe
                {
                    char[] c = name.ToCharArray();
                    fixed(int* pBuffer3 = buffer3)
                    fixed(char* pc = c)
                    {
                        c2f0(pBuffer3, pc, c.Length);
                    }
                }
                foreach (var i in buffer3)
                {
                    Console.WriteLine("0x{0:X8}", i);
                }
                Console.WriteLine("[{0}]", VariableNameToString(buffer3));

                return;
            }
        }

        static void Main(string[] args)
        {
            Load(new FileStream(@"T:\vbfox\666.scilab", FileMode.Open));
            Console.ReadLine();
        }
    }
}
