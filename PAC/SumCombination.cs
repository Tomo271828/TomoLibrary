using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.PAC
{
    public static class SumCombination
    {
        public static int[] NextSumCombination(int[] arr,int sum)
        {
            int n = arr.Length;
            int[] ret = new int[n];
            int s = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                ret[i] = arr[i];
                s += arr[i];
            }
            for(int i = n - 1;i >= 0; i--)
            {
                if(s < sum)
                {
                    ret[i] += 1;
                    break;
                }
                else
                {
                    s -= ret[i];
                    ret[i] = 0;
                }
            }
            return ret;
        }
        public static bool IsLastSumCombination(int[] arr,int sum)
        {
            if (arr[0] == sum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
