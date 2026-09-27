using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.ML;

namespace TomoLibrary.ML.Counting
{
    public static class StirlingNumberSecond
    {
        //第二種スターリング数をmod 998244353で求める
        //S(n, k) = 区別できるn個のものをちょうどk個の区別できない空でない集合に分ける場合の数

        //S(n, 0) ~ S(n, n)を求めて配列で返す
        public static long[] FixedN(int n)
        {
            long mod = 998244353;
            long[] factinv = new long[n + 1];
            long f = 1;
            for (long i = 2; i <= n; i++)
            {
                f *= i;
                f %= mod;
            }
            factinv[n] = Modulo.ModInv(f, mod);
            for (long i = n; i >= 1; i--)
            {
                factinv[i - 1] = factinv[i] * i % mod;
            }
            long[] a = new long[n + 1];
            long[] b = new long[n + 1];
            for (long i = 0; i <= n; i++)
            {
                a[i] = factinv[i] * Modulo.ModPow(i, n, mod) % mod;
                if ((i & 1) == 0)
                {
                    b[i] = factinv[i];
                }
                else
                {
                    b[i] = mod - factinv[i];
                    if (b[i] == mod)
                    {
                        b[i] = 0;
                    }
                }
            }
            long[] c = Convolution.ConvolutionFast(a, b);
            long[] ret = new long[n + 1];
            for (int i = 0; i <= n; i++)
            {
                ret[i] = c[i];
            }
            return ret;
        }
        //S(0, k) ~ S(n, k)を求めて配列で返す
        //k <= nが必要
        //S(i, j) (i < j)に関しては0が返ってくる
        public static long[] FixedK(int n, int k)
        {
            long mod = 998244353;
            long[] fact = new long[n + 2];
            fact[0] = 1;
            for (long i = 1; i <= n + 1; i++)
            {
                fact[i] = fact[i - 1] * i % mod;
            }
            long[] factinv = new long[n + 2];
            factinv[n + 1] = Modulo.ModInv(fact[n + 1], mod);
            for (long i = n + 1; i >= 1; i--)
            {
                factinv[i - 1] = factinv[i] * i % mod;
            }
            long[] a = new long[n - k + 1];
            for (long i = 0; i <= n - k; i++)
            {
                a[i] = factinv[i + 1];
            }
            FPS fps = new FPS(a);
            fps = fps.Pow(k, n - k + 1);
            long[] ret = new long[n + 1];
            for (int i = k; i <= n; i++)
            {
                ret[i] = fact[i] * factinv[k] % mod;
                ret[i] = ret[i] * fps[i - k] % mod;
            }
            return ret;
        }
    }
}
