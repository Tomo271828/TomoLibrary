using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ML
{
    public class Bitset
    {
        int count;
        ulong[] bits;
        ulong[] table;
        public Bitset(int len)
        {
            count = (len + 63) / 64;
            bits = new ulong[count];
            table = new ulong[64];
            for(int i = 0;i <= 63; i++)
            {
                table[i] = (ulong)1 << i;
            }
        }
        //true -> 1   false -> 0
        public void Set0(int pos)
        {
            bits[pos / 64] |= table[pos % 64];
        }
        public void Set1(int pos)
        {
            bits[pos / 64] &= ~table[pos % 64];
        }
        public void Setxor(int pos)
        {
            bits[pos / 64] ^= table[pos % 64];
        }
        public bool Getbit(int pos)
        {
            return (bits[pos / 64] & table[pos % 64]) == 1;
        }
        public int Popcount()
        {
            int ret = 0;
            for(int i = 0;i <= count - 1; i++)
            {
                ret += BitOperations.PopCount(bits[i]);
            }
            return ret;
        }
    }
}
