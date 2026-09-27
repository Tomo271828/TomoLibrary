using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.PAC
{
    public static class Combination
    {
        public static int[] NextCombination(int[] arr, int[] count)
        {
            int[] ret = arr;
            for(int i = arr.Length - 1;i >= 0; i--)
            {
                if (ret[i] < count[i] - 1)
                {
                    ret[i] += 1;
                    break;
                }
                else
                {
                    ret[i] = 0;
                }
            }
            return ret;
        }
        public static bool IsLastCombination(int[] arr, int[] count)
        {
            bool ret = true;
            for(int i = 0;i <= arr.Length - 1; i++)
            {
                if (arr[i] != count[i] - 1)
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
    }
}
