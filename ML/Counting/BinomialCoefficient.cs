using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ML.Counting
{
    public class BinomialCoefficient
    {
        long[] fact;
        long[] inv;
        long[] fact_inv;
        long mod;
        public BinomialCoefficient(long n,long mod)
        {
            fact = new long[n + 5];
            fact_inv = new long[n + 5];
            inv = new long[n + 5];
            fact[0] = 1;
            fact[1] = 1;
            fact_inv[0] = 1;
            fact_inv[1] = 1;
            inv[1] = 1;
            this.mod = mod;
            for(int i = 2;i <= n + 4; i++)
            {
                fact[i] = fact[i - 1] * i % mod;
                inv[i] = (mod - inv[mod % i] * (mod / i)) % mod;
                fact_inv[i] = fact_inv[i - 1] * inv[i] % mod;
            }
        }
        public long Combination(long n,long k)
        {
            if(n < k || n < 0 || k < 0)
            {
                return 0;
            }
            long ret = fact_inv[k] * fact_inv[n - k] % mod * fact[n] % mod;
            while(ret < 0)
            {
                ret += mod;
            }
            return ret;
        }
        //nが大きい場合 O(k)で計算
        public long CombinationLargeN(long n,long k)
        {
            if (n < k || n < 0 || k < 0)
            {
                return 0;
            }
            long ret = 1;
            for(long i = n;i >= n - k + 1; i--)
            {
                ret *= i % mod;
                ret %= mod;
            }
            ret *= fact_inv[k];
            if(ret < 0)
            {
                ret += (ret * -1 / mod + 1) * mod;
            }
            return ret % mod;
        }
        //nPk
        public long Permutation(long n, long k)
        {
            if (n < k || n < 0 || k < 0)
            {
                return 0;
            }
            long ret = fact[n] * fact_inv[n - k] % mod;
            while (ret < 0)
            {
                ret += mod;
            }
            return ret;
        }
    }
}
