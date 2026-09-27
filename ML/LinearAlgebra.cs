using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;

namespace TomoLibrary.ML
{
    public class LinearAlgebra
    {
        long mod;
        long[,] matrix;
        int h;
        int w;
        bool luCalced;
        LinearAlgebra l;
        LinearAlgebra u;
        long determinant;
        bool determinantCalced;
        public long this[int x,int y] {
            get
            { 
                return ValueAt(x, y); 
            }
            set
            {
                ChangeAt(x, y, value);
            }
        }

        public LinearAlgebra(long[,] map, long mod = 998244353)
        {
            this.mod = mod;
            matrix = map;
            h = map.GetLength(0);
            w = map.GetLength(1);
            for(int i = 0;i <= h - 1; i++)
            {
                for(int j = 0;j <= w - 1; j++)
                {
                    matrix[i, j] %= mod;
                }
            }
            luCalced = false;
            determinantCalced = false;
        }
        public LinearAlgebra(int h,int w,long mod = 998244353)
        {
            this.mod = mod;
            this.h = h;
            this.w = w;
            matrix = new long[h, w];
        }
        //(x, y)要素の取得
        public long ValueAt(int x,int y)
        {
            return matrix[x, y];
        }
        //(x, y)要素を変更
        public void ChangeAt(int x,int y,long value)
        {
            matrix[x, y] = value % mod;
            luCalced = false;
        }
        //入力から直接行列生成
        public static LinearAlgebra Input(int h,int w, long mod = 998244353)
        {
            long[,] map = TomoLibrary.Input.LongMap(h, w);
            return new LinearAlgebra(map, mod);
        }
        //行列を出力
        public void Output()
        {
            TomoLibrary.Output.LongMap(matrix);
        }
        public static LinearAlgebra operator + (LinearAlgebra a, LinearAlgebra b)
        {
            return Plus(a, b);
        }
        public static LinearAlgebra operator - (LinearAlgebra a, LinearAlgebra b)
        {
            return Minus(a, b);
        }
        public static LinearAlgebra operator * (LinearAlgebra a, LinearAlgebra b)
        {
            return Prod(a, b);
        }
        public static LinearAlgebra operator * (LinearAlgebra a, long x)
        {
            return Prod(a, x);
        }
        public static bool operator == (LinearAlgebra a, LinearAlgebra b)
        {
            return IsSame(a, b);
        }
        public static bool operator != (LinearAlgebra a, LinearAlgebra b)
        {
            return !IsSame(a, b);
        }
        public static LinearAlgebra Plus(LinearAlgebra a, LinearAlgebra b)
        {
            if(a.h != b.h || a.w != b.w)
            {
                throw new Exception();
            }
            if(a.mod != b.mod)
            {
                throw new Exception();
            }
            LinearAlgebra ret = new LinearAlgebra(a.h, a.w, a.mod);
            for(int i = 0;i <= a.h - 1; i++)
            {
                for(int j = 0;j <= a.w - 1; j++)
                {
                    ret[i, j] = (a[i, j] + b[i, j]) % a.mod;
                }
            }
            return ret;
        }
        public static LinearAlgebra Minus(LinearAlgebra a, LinearAlgebra b)
        {
            if (a.h != b.h || a.w != b.w)
            {
                throw new Exception();
            }
            if (a.mod != b.mod)
            {
                throw new Exception();
            }
            LinearAlgebra ret = new LinearAlgebra(a.h, a.w, a.mod);
            for (int i = 0; i <= a.h - 1; i++)
            {
                for (int j = 0; j <= a.w - 1; j++)
                {
                    ret[i, j] = a[i, j] - b[i, j];
                    if (ret[i,j] < 0)
                    {
                        ret[i, j] += a.mod;
                    }
                    ret[i, j] %= a.mod;
                }
            }
            return ret;
        }
        public static LinearAlgebra Prod(LinearAlgebra a, LinearAlgebra b)
        {
            if(a.w != b.h)
            {
                throw new Exception();
            }
            if(a.mod != b.mod)
            {
                throw new Exception();
            }
            LinearAlgebra ret = new LinearAlgebra(a.h, b.w, a.mod);
            for(int i = 0;i <= a.h - 1; i++)
            {
                for(int j = 0;j <= b.w - 1; j++)
                {
                    long sum = 0;
                    for(int k = 0;k <= a.w - 1; k++)
                    {
                        sum += a[i, k] * b[k, j];
                        sum %= a.mod;
                    }
                    ret[i, j] = sum;
                }
            }
            return ret;
        }
        public static LinearAlgebra Prod(LinearAlgebra a,long x)
        {
            LinearAlgebra ret = new LinearAlgebra(a.h, a.w, a.mod);
            x %= a.mod;
            for(int i = 0;i <= a.h - 1; i++)
            {
                for(int j = 0;j <= a.w - 1; j++)
                {
                    ret[i, j] = a[i, j] * x;
                    ret[i, j] %= a.mod;
                }
            }
            return ret;
        }
        //modなし行列積  LinearAlgebraクラスは利用しない
        public static long[,] MatrixMul(long[,] a, long[,] b)
        {
            int n = a.GetLength(0);
            long[,] ret = new long[n, n];
            for (int i = 0; i <= n - 1; i++)
            {
                for (int j = 0; j <= n - 1; j++)
                {
                    long sum = 0;
                    for (int k = 0; k <= n - 1; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    ret[i, j] = sum;
                }
            }
            return ret;
        }
        public static bool IsSame(LinearAlgebra a, LinearAlgebra b)
        {
            if(a.h != b.h || a.w != b.w)
            {
                return false;
            }
            if(a.mod != b.mod)
            {
                return false;
            }
            for(int i = 0;i <= a.h - 1; i++)
            {
                for(int j = 0;j <= a.w - 1; j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public LinearAlgebra Copy()
        {
            LinearAlgebra ret = new LinearAlgebra(h, w, mod);
            for(int i = 0;i <= h - 1; i++)
            {
                for(int j = 0;j <= w - 1; j++)
                {
                    ret[i, j] = matrix[i, j];
                }
            }
            return ret;
        }
        public static LinearAlgebra Pow(LinearAlgebra a,long p)
        {
            if(a.h != a.w)
            {
                throw new Exception();
            }
            int n = a.h;
            long m = a.mod;
            LinearAlgebra ret = new LinearAlgebra(n, n, m);
            for(int i = 0;i <= n - 1; i++)
            {
                ret[i, i] = 1;
            }
            LinearAlgebra b = a.Copy();
            while(p > 0)
            {
                if(p % 2 == 1)
                {
                    ret = ret * b;
                }
                b = b * b;
                p /= 2;
            }
            return ret;
        }
        public LinearAlgebra Pow(long p)
        {
            return LinearAlgebra.Pow(this, p);
        }
        public void LU()
        {
            if(h != w)
            {
                throw new IndexOutOfRangeException();
            }
            if (luCalced)
            {
                return;
            }
            int n = h;
            luCalced = true;
            determinantCalced = false;
            LinearAlgebra a = this.Copy();
            l = new LinearAlgebra(n, n, mod);
            u = new LinearAlgebra(n, n, mod);
            for(int i = 0;i <= n - 1; i++)
            {
                if (a[i, i] == 0)
                {
                    bool find = false;
                    for (int j = i + 1; j <= n - 1; j++)
                    {
                        if (a[j, i] != 0)
                        {
                            find = true;
                            for (int k = i; k <= n - 1; k++)
                            {
                                long memo = a[j, k];
                                a[j, k] = a[i, k];
                                a[i, k] = memo;
                                a[i, k] *= -1;
                                a[i, k] += mod;
                                a[i, k] %= mod;
                            }
                        }
                    }
                    if (find == false)
                    {
                        break;
                    }
                }
                for (int j = i;j <= n - 1; j++)
                {
                    l[j, i] = a[j, i];
                }
                u[i, i] = 1;
                long inv = TomoLibrary.ML.Modulo.ModInv(a[i, i], mod);
                for(int j = i + 1;j <= n - 1; j++)
                {
                    u[i, j] = a[i, j] * inv % mod;
                }
                for(int j = i + 1;j <= n - 1; j++)
                {
                    for(int k = i + 1;k <= n - 1; k++)
                    {
                        a[j, k] = a[j, k] - l[j, i] * u[i, k] % mod;
                        while (a[j, k] < 0)
                        {
                            a[j, k] += mod;
                        }
                        a[j, k] %= mod;
                    }
                }
            }
        }
        public long Determinant()
        {
            LU();
            if (determinantCalced)
            {
                return determinant;
            }
            determinantCalced = true;
            long det = 1;
            int n = h;
            for(int i = 0;i <= n - 1; i++)
            {
                det *= l[i, i];
                det %= mod;
            }
            determinant = det;
            return det;
        }
    }
}
