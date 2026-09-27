using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;

namespace TomoLibrary.ML
{
    public static class NumberTheory
    {
        public static int Gcd(int x, int y)
        {
            while (x != 0 && y != 0)
            {
                if (x < y)
                {
                    y = y % x;
                }
                else
                {
                    x = x % y;
                }
            }
            return Math.Max(x, y);
        }
        public static long Gcd(long x, long y)
        {
            while (x != 0 && y != 0)
            {
                if (x < y)
                {
                    y = y % x;
                }
                else
                {
                    x = x % y;
                }
            }
            return Math.Max(x, y);
        }
        public static int Lcm(int x, int y)
        {
            return x / Gcd(x, y) * y;
        }
        public static long Lcm(long x, long y)
        {
            return x / Gcd(x, y) * y;
        }
        public static bool IsPrime(int x)
        {
            bool answer = true;
            for (int i = 2; i * i <= x; i++)
            {
                if (x % i == 0)
                {
                    answer = false;
                    break;
                }
            }
            return answer;
        }
        public static bool IsPrime(long x)
        {
            bool answer = true;
            for (long i = 2; i * i <= x; i++)
            {
                if (x % i == 0)
                {
                    answer = false;
                    break;
                }
            }
            return answer;
        }
        public static List<int> PrimeFactorization(int x)
        {
            int n = x;
            int i = 2;
            List<int> answer = new List<int>();
            while (n > 1 && i * i <= n)
            {
                if (n % i == 0)
                {
                    answer.Add(i);
                    n /= i;
                }
                else
                {
                    i += 1;
                }
            }
            if (n != 1)
            {
                answer.Add(n);
            }
            return answer;
        }
        public static List<long> PrimeFactorization(long x)
        {
            long n = x;
            long i = 2;
            List<long> answer = new List<long>();
            while (n > 1 && i * i <= n)
            {
                if (n % i == 0)
                {
                    answer.Add(i);
                    n /= i;
                }
                else
                {
                    i += 1;
                }
            }
            if (n != 1)
            {
                answer.Add(n);
            }
            return answer;
        }
        public static int DivisorCount(int n)
        {
            int count = 0;
            for (int i = 1; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    if (i * i == n)
                    {
                        count += 1;
                    }
                    else
                    {
                        count += 2;
                    }
                }
            }
            return count;
        }
        public static int DivisorCount(long n)
        {
            int count = 0;
            for (long i = 1; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    if (i * i == n)
                    {
                        count += 1;
                    }
                    else
                    {
                        count += 2;
                    }
                }
            }
            return count;
        }
        public static List<int> AllDivisor(int n)
        {
            List<int> ret = new List<int>();
            for (int i = 1; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    if (i * i == n)
                    {
                        ret.Add(i);
                    }
                    else
                    {
                        ret.Add(i);
                        ret.Add(n / i);
                    }
                }
            }
            ret.Sort();
            return ret;
        }
        public static List<long> AllDivisor(long n)
        {
            List<long> ret = new List<long>();
            for (long i = 1; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    if (i * i == n)
                    {
                        ret.Add(i);
                    }
                    else
                    {
                        ret.Add(i);
                        ret.Add(n / i);
                    }
                }
            }
            ret.Sort();
            return ret;
        }
        public static List<int> EratosthenesSieve(int x)
        {
            List<int> ret = new List<int>();
            bool[] check = IsPrimeArray(x);
            for (int i = 2; i <= x; i++)
            {
                if (check[i])
                {
                    ret.Add(i);
                }
            }
            return ret;
        }
        public static List<long> EratosthenesSieve(long x)
        {
            List<long> ret = new List<long>();
            bool[] check = IsPrimeArray(x);
            for (int i = 2; i <= x; i++)
            {
                if (check[i])
                {
                    ret.Add(i);
                }
            }
            return ret;
        }
        //1~xまでの整数に対してその整数が素数であればtrueが、そうでなければfalseが代入されているbool値配列を返す
        public static bool[] IsPrimeArray(long x)
        {
            bool[] ret = new bool[x + 1];
            for (int i = 2; i <= x; i++)
            {
                if ((i & 1) == 1)
                {
                    ret[i] = true;
                }
            }
            if (x >= 2)
            {
                ret[2] = true;
            }
            for (long i = 2; i * i <= x; i++)
            {
                if (ret[i])
                {
                    long index = i * i;
                    while (index <= x)
                    {
                        ret[index] = false;
                        index += i;
                    }
                }
            }
            return ret;
        }
        //ax + by = c の整数解の1つ   Item3には整数解が存在するか否かが入る
        public static (long, long, bool) ExtendedEuclid(long a, long b, long c)
        {
            if (a == 0 && b == 0)
            {
                if (c == 0)
                {
                    return (0, 0, true);
                }
                else
                {
                    return (0, 0, false);
                }
            }
            else if (a * b == 0)
            {
                if (a == 0)
                {
                    //by = c
                    if (Math.Abs(c) % Math.Abs(b) != 0)
                    {
                        return (0, 0, false);
                    }
                    else
                    {
                        return (0, Math.Abs(c) / Math.Abs(b), true);
                    }
                }
                else
                {
                    if (Math.Abs(c) % Math.Abs(a) != 0)
                    {
                        return (0, 0, false);
                    }
                    else
                    {
                        return (0, Math.Abs(c) / Math.Abs(a), true);
                    }
                }
            }
            else
            {
                long gcd = Gcd(Math.Abs(a), Math.Abs(b));
                if (Math.Abs(c) % gcd != 0)
                {
                    return (0, 0, false);
                }
                if (a < 0)
                {
                    a *= -1;
                    b *= -1;
                    c *= -1;
                }
                bool bminus = false;
                if (b < 0)
                {
                    bminus = true;
                    b *= -1;
                }
                (long, long) xy = ExtendedEuclid(a, b);
                long ansx = xy.Item1 * c / gcd;
                long ansy = xy.Item2 * c / gcd;
                if (bminus)
                {
                    ansy *= -1;
                }
                return (ansx, ansy, true);
            }
        }
        static (long, long) ExtendedEuclid(long a, long b)
        {
            if (b == 0)
            {
                return (1, 0);
            }
            else
            {
                (long, long) xy = ExtendedEuclid(b, a % b);
                return (xy.Item2, xy.Item1 - a / b * xy.Item2);
            }
        }
        //x = b1(mod m1), x = b2(mod m2)となる最小の非負整数xを返す 存在しない場合は-1を返す
        public static long ChineseRemainderTheorem(long b1, long m1, long b2, long m2)
        {
            long gcd = Gcd(m1, m2);
            if (b1 % gcd != b2 % gcd)
            {
                return -1;
            }
            (long, long, bool) euclid = ExtendedEuclid(m1, m2, gcd);
            if (euclid.Item3 == false)
            {
                return -1;
            }
            long p = euclid.Item1;
            while (p < 0)
            {
                p += m2;
            }
            long m = m1 * m2 / gcd;
            long memo = (b2 - b1) / gcd * p % (m2 / gcd);
            return (b1 + memo * m1 + m) % m;
        }
        //正整数 n が素数の場合 true, そうでなければ false
        static long[] Bases_MillerRabin = new long[7] { 2, 325, 9375, 28178, 450775, 9780504, 1795265022 };
        static long[] Bases_MillerRabin_Small = new long[3] { 2, 7, 61 };
        public static bool MillerRabin(long n)
        {
            if (n <= 1)
            {
                return false;
            }
            if (n == 2)
            {
                return true;
            }
            if (n % 2 == 0)
            {
                return false;
            }
            long d = n - 1;
            long s = 0;
            long[] target = Bases_MillerRabin;
            if (n < (long)4759123141)
            {
                target = Bases_MillerRabin_Small;
            }
            while (d % 2 == 0)
            {
                s += 1;
                d /= 2;
            }
            for (int i = 0; i <= target.Length - 1; i++)
            {
                long a = target[i];
                if (a >= n)
                {
                    return true;
                }
                long x = Modulo.ModPowBig(a, d, n);
                if (x == 1 || x == n - 1)
                {
                    continue;
                }
                bool end = false;
                for (int j = 0; j <= s - 1; j++)
                {
                    if (x == n - 1)
                    {
                        end = true;
                        break;
                    }
                    UInt128 x128 = (UInt128)x;
                    x128 *= x128;
                    x128 %= (UInt128)n;
                    x = (long)x128;
                }
                if (end == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
