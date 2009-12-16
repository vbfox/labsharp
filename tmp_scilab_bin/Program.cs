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

        static char[] alfa = new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
		    'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
		    'u', 'v', 'w', 'x', 'y', 'z', '_', '#', '!', '$',
		    ' ', '(', ')', ';', ':', '+', '-', '*', '/', '\\',
		    '=', '.', ',', '\'', '[', ']', '%', '|', '&', '<',
		    '>', '~', '^',
        };

        static char[] alfb = new[]
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

        unsafe void c2f(int* id, char* str, int* job, int str_len)
        {
            Func<int, int, int> min = (a, b) => Math.Min(a, b);
            Func<int, int> abs = (i) => Math.Abs(i);

            int i__1, i__2;
            int i__, k, l, i1, ch, ii, ln, idl;
            int[] name = new int[24];
            fixed (int* name__ = name)
            {

                /* Parameter adjustments */
                --id;

                /* Function Body */

                if (*job != 0)
                {
                    i1 = 1;
                    for (l = 1; l <= 6; ++l)
                    {
                        idl = id[l];
                        i__1 = i1 + 3;
                        for (i__ = i1; i__ <= i__1; ++i__)
                        {
                            k = (idl + 128) / 256;
                            if (k < 0)
                            {
                                --k;
                            }
                            ch = idl - (k << 8);
                            idl = k;
                            if (abs(ch) >= 63)
                            {
                                ch = star;
                            }
                            if (ch > 0)
                            {
                                *(char*)&str[i__ - 1] = alfa[ch];
                            }
                            else
                            {
                                *(char*)&str[i__ - 1] = alfb[-ch];
                            }
                        }
                        i1 += 4;
                    }
                }
                else
                {
                    /* Computing MIN */
                    i__1 = 24;
                    i__2 = i_len(str, str_len);
                    ln = min(i__1, i__2);
                    cvstr_(&ln, name__, str, &c__0, str_len);
                    if (ln < 24)
                    {
                        i__1 = 24 - ln;
                        iset_(&i__1, &blank, &name__[ln], &c__1);
                    }
                    i1 = 1;
                    for (l = 1; l <= 6; ++l)
                    {
                        id[l] = 0;
                        for (i__ = 1; i__ <= 4; ++i__)
                        {
                            ii = i1 + 4 - i__;
                            id[l] = (id[l] << 8) + name__[ii - 1];
                        }
                        i1 += 4;
                    }
                }
            }
        }

        i_len

        void Load(Stream s)
        {
            while(s.Position != s.Length-1)
            {
                byte[] buffer = new byte[6];
                s.Read(buffer, 0, 6);

            }
        }

        static void Main(string[] args)
        {
        }
    }
}
