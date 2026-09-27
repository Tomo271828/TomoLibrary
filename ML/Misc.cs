using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;

namespace TomoLibrary.ML
{
    public static class Misc
    {
        //閉区間[aMin,aMax]と[bMin,bMax]に共通部分が存在するかどうか判定する
        public static bool CommonPart(int aMin, int aMax, int bMin, int bMax)
        {
            bool ret = true;
            if (aMax < bMin || bMax < aMin)
            {
                ret = false;
            }
            return ret;
        }
        public static bool CommonPart(long aMin, long aMax, long bMin, long bMax)
        {
            bool ret = true;
            if (aMax < bMin || bMax < aMin)
            {
                ret = false;
            }
            return ret;
        }
        public static bool CommonPart(float aMin, float aMax, float bMin, float bMax)
        {
            bool ret = true;
            if (aMax < bMin || bMax < aMin)
            {
                ret = false;
            }
            return ret;
        }
        //閉区間aと閉区間bの共通部分を返す
        //存在しない場合はnullを返す
        public static int[] GetCommonPart(int[] a, int[] b)
        {
            if (CommonPart(a[0], a[1], b[0], b[1]) == false)
            {
                return null;
            }
            int[] ret = new int[2] { Math.Max(a[0], b[0]), Math.Min(a[1], b[1]) };
            return ret;
        }
        public static long[] GetCommonPart(long[] a, long[] b)
        {
            if (CommonPart(a[0], a[1], b[0], b[1]) == false)
            {
                return null;
            }
            long[] ret = new long[2] { Math.Max(a[0], b[0]), Math.Min(a[1], b[1]) };
            return ret;
        }
        //[(ai + b) / m] の i = 0 から i = N - 1 の総和を求める
        public static long FloorSum(long n, long m, long a, long b)
        {
            long answer = 0;
            long mul = 1;
            while (true)
            {
                long a1 = a / m;
                long a2 = a % m;
                long s = n * (n - 1) / 2 * a1;
                long b1 = b / m;
                long b2 = b % m;
                if (a2 == 0)
                {
                    answer += mul * (s + b1 * n);
                    break;
                }
                long k = (a2 * (n - 1) + b2) / m;
                answer += mul * (s + n * (k + b1));
                long nn = k;
                long nm = a2;
                long na = m;
                long nb = m + a2 - b2 - 1;
                n = nn;
                m = nm;
                a = na;
                b = nb;
                mul *= -1;
            }
            return answer;
        }
        //x^(1/k) の整数部分を求める (x >= 0, k >= 1)
        public static ulong KthRoot(ulong x, ulong k)
        {
            if (x <= 1)
            {
                return x;
            }
            if (k > 64)
            {
                return 1;
            }
            if (k == 1)
            {
                return x;
            }
            UInt128 min = 1;
            UInt128 max = x;
            while (max - min > 1)
            {
                UInt128 mid = (max + min) / 2;
                bool over = false;
                UInt128 mul = 1;
                for (ulong i = 0; i <= k - 1; i++)
                {
                    // mul * mid > x だと over
                    if (mul > x / mid)
                    {
                        over = true;
                        break;
                    }
                    mul *= mid;
                    if (mul > x)
                    {
                        break;
                    }
                }
                if (over || mul > x)
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }
            return (ulong)min;
        }
    }
}
