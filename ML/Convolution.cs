using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;

namespace TomoLibrary.ML
{
    public static class Convolution
    {
        public static long[] ConvolutionNormal(long[] input_a, long[] input_b)
        {
            List<long> a = new List<long>();
            List<long> b = new List<long>();
            for (int i = 0; i <= input_a.Length - 1; i++)
            {
                a.Add(input_a[i]);
            }
            for (int i = 0; i <= input_b.Length - 1; i++)
            {
                b.Add(input_b[i]);
            }
            List<long> c = ConvolutionNormal(a, b);
            long[] ret = new long[c.Count];
            for (int i = 0; i <= ret.Length - 1; i++)
            {
                ret[i] = c[i];
            }
            return ret;
        }
        public static List<long> ConvolutionNormal(List<long> input_a, List<long> input_b)
        {
            long mod = 998244353;
            List<long> root = Makeroot(mod);
            List<long> invroot = Makeinvroot(root, mod);
            int len_a = input_a.Count;
            int len_b = input_b.Count();
            int len_c = len_a + len_b - 1;
            int n = 1;
            while (n <= len_c)
            {
                n *= 2;
            }
            while (input_a.Count < n)
            {
                input_a.Add(0);
            }
            while (input_b.Count < n)
            {
                input_b.Add(0);
            }
            long log = 1;
            while ((long)1 << (int)log < n)
            {
                log += 1;
            }
            List<long> a = NTT(input_a, log - 1, root);
            List<long> b = NTT(input_b, log - 1, root);
            List<long> dc = new List<long>();
            for (int i = 0; i <= n - 1; i++)
            {
                dc.Add(a[i] * b[i] % mod);
            }
            List<long> c = NTT(dc, log - 1, invroot);
            List<long> ret = new List<long>();
            for (int i = 0; i <= n - 1; i++)
            {
                ret.Add(c[i] * TomoLibrary.ML.Modulo.ModInv(n, mod) % mod);
            }
            return ret;
        }
        static List<long> Makeroot(long mod)
        {
            List<long> ret = new List<long>();
            long r = TomoLibrary.ML.Modulo.ModPow(3, 119, mod);
            for (int i = 0; i < 23; i++)
            {
                ret.Add(r);
                r = r * r % mod;
            }
            ret.Reverse();
            return ret;
        }
        static List<long> Makeinvroot(List<long> root, long mod)
        {
            List<long> ret = new List<long>();
            for (int i = 0; i <= root.Count - 1; i++)
            {
                ret.Add(TomoLibrary.ML.Modulo.ModInv(root[i], mod));
            }
            return ret;
        }
        static List<long> NTT(List<long> a, long depth, List<long> root)
        {
            int n = a.Count;
            long mod = 998244353;
            if (n == 1)
            {
                return a;
            }
            else
            {
                List<long> even = new List<long>();
                List<long> odd = new List<long>();
                for (int i = 0; i <= n - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        even.Add(a[i]);
                    }
                    else
                    {
                        odd.Add(a[i]);
                    }
                }
                List<long> dodd = NTT(odd, depth - 1, root);
                List<long> deven = NTT(even, depth - 1, root);
                long r = root[(int)depth];
                List<long> ret = new List<long>();
                long now = 1;
                for (int i = 0; i <= n - 1; i++)
                {
                    ret.Add((deven[i % (n / 2)] + dodd[i % (n / 2)] * now % mod) % mod);
                    now = now * r % mod;
                }
                return ret;
            }
        }
        //998244353 * 469762049 - 1 まで対応可能
        public static long[] ConvolutionLarge(long[] input_a, long[] input_b)
        {
            List<long> a = new List<long>();
            List<long> b = new List<long>();
            for (int i = 0; i <= input_a.Length - 1; i++)
            {
                a.Add(input_a[i]);
            }
            for (int i = 0; i <= input_b.Length - 1; i++)
            {
                b.Add(input_b[i]);
            }
            List<long> c = ConvolutionLarge(a, b);
            long[] ret = new long[c.Count];
            for (int i = 0; i <= ret.Length - 1; i++)
            {
                ret[i] = c[i];
            }
            return ret;
        }
        public static List<long> ConvolutionLarge(List<long> input_a, List<long> input_b)
        {
            long mod = 998244353;
            long modsp = 469762049;
            long modmodsp = mod * modsp;
            (long, long, bool) extEuc = NumberTheory.ExtendedEuclid(mod, modsp, 1);
            long p = extEuc.Item1;
            List<long> root = Makeroot(mod);
            List<long> invroot = Makeinvroot(root, mod);
            List<long> rootSP = MakerootSP(modsp);
            List<long> invrootSP = Makeinvroot(rootSP, modsp);
            int len_a = input_a.Count;
            int len_b = input_b.Count;
            int len_c = len_a + len_b - 1;
            int n = 1;
            while (n <= len_c)
            {
                n *= 2;
            }
            while (input_a.Count < n)
            {
                input_a.Add(0);
            }
            while (input_b.Count < n)
            {
                input_b.Add(0);
            }
            long log = 1;
            while ((long)1 << (int)log < n)
            {
                log += 1;
            }
            List<long> a = NTT(input_a, log - 1, root);
            List<long> b = NTT(input_b, log - 1, root);
            List<long> asp = NTTSP(input_a, log - 1, rootSP);
            List<long> bsp = NTTSP(input_b, log - 1, rootSP);
            List<long> dc = new List<long>();
            List<long> dcsp = new List<long>();
            for (int i = 0; i <= n - 1; i++)
            {
                dc.Add(a[i] * b[i] % mod);
                dcsp.Add(asp[i] * bsp[i] % modsp);
            }
            List<long> c = NTT(dc, log - 1, invroot);
            List<long> csp = NTTSP(dcsp, log - 1, invrootSP);
            List<long> ret = new List<long>();
            List<long> retsp = new List<long>();
            for (int i = 0; i <= n - 1; i++)
            {
                ret.Add(c[i] * TomoLibrary.ML.Modulo.ModInv(n, mod) % mod);
                retsp.Add(csp[i] * TomoLibrary.ML.Modulo.ModInv(n, modsp) % modsp);
            }
            List<long> answer = new List<long>();
            for (int i = 0; i <= n - 1; i++)
            {
                long add = ret[i] + mod * (retsp[i] - ret[i]) * p;
                long addadd = Math.Abs(add) / modmodsp + 1;
                add += addadd * modmodsp;
                add %= modmodsp;
                answer.Add(add);
            }
            return answer;
        }
        static List<long> NTTSP(List<long> a, long depth, List<long> root)
        {
            int n = a.Count;
            long mod = 469762049;
            if (n == 1)
            {
                return a;
            }
            else
            {
                List<long> even = new List<long>();
                List<long> odd = new List<long>();
                for (int i = 0; i <= n - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        even.Add(a[i]);
                    }
                    else
                    {
                        odd.Add(a[i]);
                    }
                }
                List<long> dodd = NTTSP(odd, depth - 1, root);
                List<long> deven = NTTSP(even, depth - 1, root);
                long r = root[(int)depth];
                List<long> ret = new List<long>();
                long now = 1;
                for (int i = 0; i <= n - 1; i++)
                {
                    ret.Add((deven[i % (n / 2)] + dodd[i % (n / 2)] * now % mod) % mod);
                    now = now * r % mod;
                }
                return ret;
            }
        }
        static List<long> MakerootSP(long mod)
        {
            List<long> ret = new List<long>();
            long r = TomoLibrary.ML.Modulo.ModPow(3, 7, mod);
            for (int i = 0; i < 26; i++)
            {
                ret.Add(r);
                r = r * r % mod;
            }
            ret.Reverse();
            return ret;
        }
        public static long[] ConvolutionFast(long[] a, long[] b)
        {
            int n = 1;
            long mod = 998244353;
            while (n < (int)(a.Length + b.Length - 1))
            {
                n <<= 1;
            }
            int[] a2 = new int[n];
            int[] b2 = new int[n];
            for (int i = 0; i <= a.Length - 1; i++)
            {
                a2[i] = (int)(a[i] % mod);
            }
            for (int i = 0; i <= b.Length - 1; i++)
            {
                b2[i] = (int)(b[i] % mod);
            }
            NTTFast(a2, n, false);
            NTTFast(b2, n, false);
            for (int i = 0; i <= n - 1; i++)
            {
                a2[i] = (int)((long)a2[i] * (long)b2[i] % mod);
            }
            b2 = null;
            GC.Collect();
            NTTFast(a2, n, true);
            long[] ret = new long[a.Length + b.Length - 1];
            for (int i = 0; i <= a.Length + b.Length - 2; i++)
            {
                ret[i] = (long)a2[i];
            }
            return ret;
        }
        static void NTTFast(int[] a, int n, bool invert)
        {
            int k = 0;
            for (int i = 1; i < n; i++)
            {
                int bit = n >> 1;
                while ((k & bit) != 0)
                {
                    k ^= bit;
                    bit >>= 1;
                }
                k |= bit;
                if (i < k)
                {
                    (a[i], a[k]) = (a[k], a[i]);
                }
            }
            long r = 3;
            long mod = 998244353;
            for (int len = 2; len <= n; len <<= 1)
            {
                long wlen = TomoLibrary.ML.Modulo.ModPow(r, (mod - 1) / len, mod);
                if (invert)
                {
                    wlen = TomoLibrary.ML.Modulo.ModInv(wlen, mod);
                }
                for (int i = 0; i <= n - 1; i += len)
                {
                    long w = 1;
                    for (int j = 0; j <= len / 2 - 1; ++j)
                    {
                        long u = a[i + j];
                        long v = (long)a[i + j + len / 2] * w % mod;
                        a[i + j] = (int)((u + v) % mod);
                        a[i + j + len / 2] = (int)((u - v + mod) % mod);
                        w = wlen * w % mod;
                    }
                }
            }
            if (invert)
            {
                long invn = TomoLibrary.ML.Modulo.ModInv(n, mod);
                for (int i = 0; i <= a.Length - 1; i++)
                {
                    a[i] = (int)((long)a[i] * invn % mod);
                }
            }
        }
    }
}
