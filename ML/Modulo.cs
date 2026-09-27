using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ML
{
    public static class Modulo
    {
        public static long ModPow(long n,long p,long mod)
        {
            long ret = 1;
            long nmod = n % mod;
            while(p > 0)
            {
                if((p & 1) == 1)
                {
                    ret = ret * nmod % mod;
                }
                nmod = nmod * nmod % mod;
                p >>= 1;
            }
            return ret;
        }
        public static long ModInv(long n,long mod)
        {
            long a = n;
            long b = mod;
            long u = 1;
            long v = 0;
            while(b > 0)
            {
                long t = a / b;
                a -= t * b;
                u -= t * v;
                long memo = a;
                a = b;
                b = memo;
                memo = u;
                u = v;
                v = memo;
            }
            u %= mod;
            while(u < 0)
            {
                u += mod;
            }
            return u;
        }
        //long型最大値くらいまで対応、多分遅い
        public static long ModPowBig(long n,long p,long mod)
        {
            UInt128 n128 = (UInt128)n;
            UInt128 mod128 = (UInt128)mod;
            UInt128 ret = 1;
            UInt128 nmod = n128 % mod128;
            while (p > 0)
            {
                if ((p & 1) == 1)
                {
                    ret = (ret * nmod) % mod128;
                }
                nmod = (nmod * nmod) % mod128;
                p >>= 1;
            }
            return (long)ret;
        }
        //x^2 = n(mod p)
        //存在しない場合は-1
        public static long ModSqrt(long n,long mod)
        {
            n %= mod;
            if(n <= 1)
            {
                return n;
            }
            long check = ModPow(n, (mod - 1) / 2, mod);
            if (check != 1)
            {
                return -1;
            }
            if(mod % 8 == 3 || mod % 8 == 7)
            {
                return ModPow(n, (mod + 1) / 4, mod);
            }
            if(mod % 8 == 5)
            {
                long x = ModPow(n, (mod + 3) / 8, mod);
                if((x * x) % mod == n)
                {
                    return x;
                }
                else
                {
                    return (x * ModPow(2, (mod - 1) / 4, mod)) % mod;
                }
            }
            long d = 2;
            long s = 0;
            long t = 0;
            if(mod == 998244353)
            {
                d = 3;
                s = 23;
                t = 119;
            }
            else
            {
                while (true)
                {
                    long c = ModPow(d, (mod - 1) / 2, mod);
                    if(c != 1)
                    {
                        break;
                    }
                    d += 1;
                }
                long mm = mod - 1;
                while(mm % 2 == 0)
                {
                    mm /= 2;
                    s += 1;
                }
                t = mm;
            }
            long a = ModPow(n, t, mod);
            long b = ModPow(d, t, mod);
            long m = 0;
            for(int i = 0;i <= s - 1; i++)
            {
                long pow = ModPow(2, s - 1 - i, mod);
                long tar = a * ModPow(b, m, mod);
                tar %= mod;
                if(ModPow(tar,pow,mod) == mod - 1)
                {
                    m += ModPow(2, i, mod);
                }
            }
            long ret1 = ModPow(n, (t + 1) / 2, mod);
            long ret2 = ModPow(b, m / 2, mod);
            return (ret1 * ret2) % mod;
        }
    }
}
