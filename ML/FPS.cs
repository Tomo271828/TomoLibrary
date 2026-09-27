using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;

namespace TomoLibrary.ML
{
    public class FPS
    {
        long mod = 998244353;
        long[] arr;
        long n;
        public FPS(long[] arr)
        {
            if(arr.Length == 0)
            {
                this.arr = new long[1] { 0 };
            }
            else
            {
                this.arr = arr;
            }
            AllMod();
            Reduce0();
        }
        public FPS(long x)
        {
            if(x < 0)
            {
                x += ((Math.Abs(x) - 1) / mod + 1) * mod;
            }
            x %= mod;
            this.arr = new long[1] { x };
            this.n = 1;
        }
        void AllMod()
        {
            for(int i = 0;i <= arr.Length - 1; i++)
            {
                if (arr[i] < 0)
                {
                    arr[i] += ((Math.Abs(arr[i]) - 1) / mod + 1) * mod;
                }
                arr[i] %= mod;
            }
        }
        void Reduce0()
        {
            int index = arr.Length - 1;
            while (arr[index] == 0)
            {
                index -= 1;
                if(index == -1)
                {
                    break;
                }
            }
            n = index + 1;
        }
        //x^indexの項の係数を返す
        public long this[int index]
        {
            get
            {
                return index < arr.Length ? arr[index] : 0;
            }
        }
        //リストの長さがnのときnを返す
        public int Length
        {
            get
            {
                return (int)n;
            }
        }
        public long[] GetArray()
        {
            return arr.ToArray();
        }
        public long[] GetArray(int len)
        {
            long[] ret = new long[len];
            for(int i = 0;i <= len - 1; i++)
            {
                ret[i] = i < arr.Length ? arr[i] : 0;
            }
            return ret;
        }
        public static FPS operator + (FPS a, FPS b)
        {
            return FPS.Add(a, b);
        }
        public static FPS operator - (FPS a, FPS b)
        {
            return FPS.Sub(a, b);
        }
        public static FPS operator * (FPS a, FPS b)
        {
            return FPS.Mul(a, b);
        }
        public static FPS operator * (FPS a,long x)
        {
            return FPS.Mul(a, x);
        }
        //x^(index-1)の項までのFPSを返す
        public FPS Reduce(int index)
        {
            if(index <= 0)
            {
                return new FPS(new long[1] { 0 });
            }
            long[] ret = new long[index];
            for(int i = 0;i <= index - 1; i++)
            {
                ret[i] = this[i];
            }
            return new FPS(ret);
        }
        //破壊的
        public FPS ReduceInplace(int index)
        {
            n = index;
            return this;
        }
        //x^size倍する
        public FPS Shift(int size)
        {
            if(n + size < 0)
            {
                return new FPS(new long[1] { 0 });
            }
            long[] ret = new long[n + size];
            for (int i = (int)n - 1; i >= 0; i--)
            {
                if (i + size < 0)
                {
                    break;
                }
                ret[i + size] = arr[i];
            }
            return new FPS(ret);
        }
        public static FPS Add(FPS a,FPS b)
        {
            long mod = 998244353;
            int n = Math.Max(a.Length, b.Length);
            long[] ret = new long[n];
            for(int i = 0;i <= n - 1; i++)
            {
                long set = 0;
                if(i < a.Length)
                {
                    set += a[i];
                }
                if(i < b.Length)
                {
                    set += b[i];
                }
                set %= mod;
                ret[i] = set;
            }
            return new FPS(ret);
        }
        public static FPS Sub(FPS a, FPS b)
        {
            long mod = 998244353;
            int n = Math.Max(a.Length, b.Length);
            long[] ret = new long[n];
            for (int i = 0; i <= n - 1; i++)
            {
                long set = 0;
                if (i < a.Length)
                {
                    set += a[i];
                }
                if (i < b.Length)
                {
                    set -= b[i];
                }
                set += mod;
                set %= mod;
                ret[i] = set;
            }
            return new FPS(ret);
        }
        public static FPS Mul(FPS a, FPS b)
        {
            long[] ret = TomoLibrary.ML.Convolution.ConvolutionFast(a.GetArray(), b.GetArray());
            return new FPS(ret);
        }
        public static FPS Mul(FPS a,long x)
        {
            long mod = 998244353;
            x %= mod;
            int n = (int)a.n;
            long[] ret = new long[n];
            for(int i = 0;i <= n - 1; i++)
            {
                ret[i] = a[i] * x;
                ret[i] %= mod;
            }
            return new FPS(ret);
        }
        //逆元をx^(n-1)の項まで求める
        public FPS Inv(int n)
        {
            FPS g = new FPS(TomoLibrary.ML.Modulo.ModInv(arr[0], mod));
            FPS two = new FPS(2);
            FPS f;
            int loop = 0;
            int pow = 1;
            int red = 2;
            while(pow < n)
            {
                loop += 1;
                pow *= 2;
            }
            for(int i = 0;i <= loop - 1; i++)
            {
                f = this.Reduce(red);
                g = g * (two - f * g);
                g = g.Reduce(red);
                red *= 2;
            }
            long[] ret = new long[n];
            for(int i = 0;i <= n - 1; i++)
            {
                ret[i] = g[i];
            }
            return new FPS(ret);
        }
        //微分
        public FPS Diff()
        {
            if(arr.Length == 1)
            {
                return new FPS(0);
            }
            int n = arr.Length - 1;
            long[] ret = new long[n];
            for(long i = 1;i <= n; i++)
            {
                ret[i - 1] = arr[(int)i] * i;
                ret[i - 1] %= mod;
            }
            return new FPS(ret);
        }
        //積分
        public FPS Integral()
        {
            int n = arr.Length;
            long[] ret = new long[n + 1];
            for(long i = 0;i <= n - 1; i++)
            {
                ret[i + 1] = arr[(int)i] * Modulo.ModInv(i + 1, mod);
                ret[i + 1] %= mod;
            }
            return new FPS(ret);
        }
        //log f(x)をx^(n-1)の項まで求める
        public FPS Log(int n)
        {
            FPS f = this.Reduce(n + 1);
            FPS fdiff = f.Diff();
            FPS finv = f.Inv(n);
            FPS g = fdiff * finv;
            g = g.Integral();
            return g.Reduce(n);
        }
        //f(x)^mをx^(n-1)の項まで求める
        public FPS Pow(long m,int n)
        {
            if(m == 0)
            {
                return new FPS(new long[1] { 1 });
            }
            int k = -1;
            for(int i = 0;i <= this.n; i++)
            {
                if (this[i] != 0)
                {
                    k = i;
                    break;
                }
            }
            if(k == -1)
            {
                return new FPS(new long[1] { 0 });
            }
            if(k != 0 && (long)n <= m)
            {
                return new FPS(new long[1] { 0 });
            }
            if((long)n - (long)k * m <= 0)
            {
                return new FPS(new long[1] { 0 });
            }
            FPS g = this.Shift(k * -1);
            long c = g[0];
            g *= TomoLibrary.ML.Modulo.ModInv(c, mod);
            g = g.Log((int)((long)n - (long)k * m));
            g *= m % mod;
            g = g.Exp((int)((long)n - (long)k * m));
            g = g.Shift((int)((long)k * m));
            c = TomoLibrary.ML.Modulo.ModPow(c, m, mod);
            g *= c;
            return g;
        }
        //e^f(x)をx^(n-1)の項まで求める
        public FPS Exp(int n)
        {
            FPS g = new FPS(1);
            FPS one = new FPS(1);
            int loop = 0;
            int pow = 1;
            int red = 2;
            while (pow < n)
            {
                loop += 1;
                pow *= 2;
            }
            for (int i = 0; i <= loop - 1; i++)
            {
                FPS f = this.Reduce(red);
                g = g * (f + one - g.Log(red));
                g = g.Reduce(red);
                red *= 2;
            }
            long[] ret = new long[n];
            for (int i = 0; i <= n - 1; i++)
            {
                ret[i] = g[i];
            }
            return new FPS(ret);
        }
        //Sqrt(f(x))をx^(n-1)の項まで求める 存在しない場合はnullを返す
        public FPS Sqrt(int n)
        {
            long root = 0;
            if(this.n == 1)
            {
                root = Modulo.ModSqrt(arr[0], mod);
                if(root == -1)
                {
                    return null;
                }
                return new FPS(root);
            }
            int notzero = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                if (arr[i] != 0)
                {
                    notzero = i;
                    if(i % 2 == 1)
                    {
                        return null;
                    }
                    break;
                }
            }
            FPS f = this.Shift(-notzero);
            root = Modulo.ModSqrt(f[0], mod);
            if(root == -1)
            {
                return null;
            }
            int n2 = n - notzero / 2;
            FPS g = new FPS(root);
            long inv2 = Modulo.ModInv(2, mod);
            int loop = 0;
            int pow = 1;
            int red = 2;
            while (pow < n2)
            {
                loop += 1;
                pow *= 2;
            }
            for (int i = 0; i <= loop - 1; i++)
            {
                FPS inv = g.Inv(red);
                g = (g + f.Reduce(red) * inv) * inv2;
                g = g.Reduce(red);
                red *= 2;
            }
            long[] ret = new long[n2];
            for (int i = 0; i <= n2 - 1; i++)
            {
                ret[i] = g[i];
            }
            FPS ans = new FPS(ret);
            return ans.Shift(notzero / 2);
        }
        //ベルヌーイ数 mod 998244353 を0番目からN番目まで求める
        public static long[] BernoulliNumber(int n)
        {
            long mod = 998244353;
            long[] fact = new long[n + 2];
            fact[0] = 1;
            for (long i = 1; i <= n + 1; i++)
            {
                fact[i] = fact[i - 1] * i;
                fact[i] %= mod;
            }
            long[] arr = new long[n + 1];
            for (int i = 0; i <= n; i++)
            {
                arr[i] = Modulo.ModInv(fact[i + 1], mod);
            }
            FPS fps = new FPS(arr);
            fps = fps.Inv(n + 1);
            arr = fps.GetArray(n + 1);
            for (int i = 0; i <= n; i++)
            {
                arr[i] *= fact[i];
                arr[i] %= mod;
            }
            return arr;
        }
    }
}
