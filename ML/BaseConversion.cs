using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;

namespace TomoLibrary.ML
{
    public static class BaseConversion
    {
        public static int ToBase10(string str, int k)
        {
            int ret = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                ret += (int)(str[i] - '0') * (int)Math.Pow(k, str.Length - 1 - i);
            }
            return ret;
        }
        public static long ToBase10(string str, long k)
        {
            long ret = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                ret += (long)(str[i] - '0') * (long)Math.Pow(k, str.Length - 1 - i);
            }
            return ret;
        }
        public static int ToBase10(List<int> str, int k)
        {
            int ret = 0;
            int mul = 1;
            for (int i = 0; i <= str.Count - 1; i++)
            {
                ret += str[str.Count - 1 - i] * mul;
                mul *= k;
            }
            return ret;
        }
        public static long ToBase10(List<int> str, long k)
        {
            long ret = 0;
            long mul = 1;
            for (int i = 0; i <= str.Count - 1; i++)
            {
                ret += (long)str[str.Count - 1 - i] * mul;
                mul *= k;
            }
            return ret;
        }
        public static int Digit(int n)
        {
            return n.ToString().Length;
        }
        public static int Digit(long n)
        {
            return n.ToString().Length;
        }
        public static List<int> Conversion(int n, int k)
        {
            List<int> ret = new List<int>();
            if (n == 0)
            {
                ret.Add(0);
                return ret;
            }
            int x = n;
            while (x > 0)
            {
                ret.Add(x % k);
                x /= k;
            }
            ret.Reverse();
            return ret;
        }
        public static List<int> Conversion(int n, int k, int len)
        {
            List<int> ret = new List<int>();
            int x = n;
            while (x > 0)
            {
                ret.Add(x % k);
                x /= k;
            }
            while (ret.Count < len)
            {
                ret.Add(0);
            }
            ret.Reverse();
            return ret;
        }
        public static List<int> Conversion(long n, int k)
        {
            List<int> ret = new List<int>();
            if (n == 0)
            {
                ret.Add(0);
                return ret;
            }
            long x = n;
            while (x > 0)
            {
                ret.Add((int)(x % k));
                x /= k;
            }
            ret.Reverse();
            return ret;
        }
        public static List<int> Conversion(long n, int k, int len)
        {
            List<int> ret = new List<int>();
            long x = n;
            while (x > 0)
            {
                ret.Add((int)(x % k));
                x /= k;
            }
            while (ret.Count < len)
            {
                ret.Add(0);
            }
            ret.Reverse();
            return ret;
        }
    }
}
