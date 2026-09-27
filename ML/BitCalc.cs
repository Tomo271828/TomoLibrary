using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ML
{
    public static class BitCalc
    {
        public static int Popcount(int x)
        {
            int popcount = 0;
            while(x > 0)
            {
                if(x % 2 == 1)
                {
                    popcount += 1;
                }
                x /= 2;
            }
            return popcount;
        }
        public static int Popcount(long x)
        {
            int popcount = 0;
            while (x > 0)
            {
                if (x % 2 == 1)
                {
                    popcount += 1;
                }
                x /= 2;
            }
            return popcount;
        }
        public static int[] GrayCode(int n)
        {
            int[] ret = new int[1 << n];
            for(int i = 0;i <= (1 << n) - 1; i++)
            {
                ret[i] = (i ^ (i >> 1));
            }
            return ret;
        }
    }
}
