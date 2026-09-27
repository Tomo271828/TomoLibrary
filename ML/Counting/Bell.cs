using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ML.Counting
{
    public static class Bell
    {
        //Bell数をmod 998244353で求める
        //B(n) = 区別できるn個のものを空でない区別できない部分集合に分割する場合の数

        //B(0) ~ B(n)を求めて配列で返す
        public static long[] BellArray(int n)
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
            long[] a = new long[n + 1];
            for (int i = 1; i <= n; i++)
            {
                a[i] = factinv[i];
            }
            FPS fps = new FPS(a);
            fps = fps.Exp(n + 1);
            long[] ret = new long[n + 1];
            for (int i = 0; i <= n; i++)
            {
                ret[i] = fps[i] * fact[i] % mod;
            }
            return ret;
        }
    }
}
