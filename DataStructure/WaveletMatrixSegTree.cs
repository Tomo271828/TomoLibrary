using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.DataStructure.SegTree;

namespace TomoLibrary.DataStructure
{
    public class WaveletMatrixSegTree<T>
    {
        int h;
        int n;
        long[] a;
        long[] fa;
        int[][] dat;
        SegmentTree<T>[] sts;
        ISegmentTree<T> operations;
        T e;
        int l1;
        int r1;
        int l2;
        int r2;
        public WaveletMatrixSegTree(ISegmentTree<T> operations, long[] arr)
        {
            h = 0;
            n = arr.Length;
            e = operations.E();
            this.operations = operations;
            for (int i = 0; i <= n - 1; i++)
            {
                while ((long)1 << h < arr[i])
                {
                    h += 1;
                }
            }
            h += 1;
            a = new long[n];
            fa = new long[n];
            Array.Copy(arr, a, n);
            Array.Copy(arr, fa, n);
            T[] values = new T[n];
            for(int i = 0;i <= n - 1; i++)
            {
                values[i] = e;
            }
            Init(operations, values);
        }
        public WaveletMatrixSegTree(ISegmentTree<T> operations, long[] arr, T[] values)
        {
            h = 0;
            n = arr.Length;
            e = operations.E();
            this.operations = operations;
            for (int i = 0; i <= n - 1; i++)
            {
                while ((long)1 << h < arr[i])
                {
                    h += 1;
                }
            }
            h += 1;
            a = new long[n];
            fa = new long[n];
            Array.Copy(arr, a, n);
            Array.Copy(arr, fa, n);
            Init(operations, values);
        }
        void Init(ISegmentTree<T> operations, T[] values)
        {
            dat = new int[h][];
            sts = new SegmentTree<T>[h];
            Queue<(long, T)> left = new Queue<(long, T)>();
            Queue<(long, T)> right = new Queue<(long, T)>();
            for (int i = h - 1; i >= 0; i--)
            {
                T[] vi = new T[n];
                dat[i] = new int[n + 1];
                dat[i][0] = 0;
                for (int j = 0; j <= n - 1; j++)
                {
                    int dir = (int)(a[j] >> i & 1);
                    dat[i][j + 1] = dat[i][j] + dir;
                    vi[j] = values[j];
                    if (dir == 0)
                    {
                        left.Enqueue((a[j], values[j]));
                    }
                    else
                    {
                        right.Enqueue((a[j], values[j]));
                    }
                }
                sts[i] = new SegmentTree<T>(operations, vi);
                int index = 0;
                while (left.TryDequeue(out (long, T) v))
                {
                    a[index] = v.Item1;
                    values[index] = v.Item2;
                    index += 1;
                }
                while (right.TryDequeue(out (long,T) v))
                {
                    a[index] = v.Item1;
                    values[index] = v.Item2;
                    index += 1;
                }
            }
        }
        //l1,r1,l2,r2に(h + 1,l,r)からの左右の部分木の値の範囲を代入
        void GetSubtreeRange(int h, int l, int r)
        {
            int c = n - dat[h][n];
            l1 = l - dat[h][l];
            r1 = r - dat[h][r];
            l2 = c + dat[h][l];
            r2 = c + dat[h][r];
        }
        //[l,r)でk(0-indexed)番目に小さい値を取得
        public long KthSmallest(int l, int r, int k)
        {
            long ret = 0;
            for (int i = h; i >= 1; i--)
            {
                int c = n - dat[i - 1][n];
                l1 = l - dat[i - 1][l];
                r1 = r - dat[i - 1][r];
                l2 = c + dat[i - 1][l];
                r2 = c + dat[i - 1][r];
                int leftsize = r1 - l1;
                if (k < leftsize)
                {
                    l = l1;
                    r = r1;
                }
                else
                {
                    ret += (long)1 << i - 1;
                    l = l2;
                    r = r2;
                    k = k - leftsize;
                }
            }
            return ret;
        }
        //[l,r)でxが出現する回数を返す
        public int Count(int l, int r, long x)
        {
            for (int i = h; i >= 1; i--)
            {
                int c = n - dat[i - 1][n];
                l1 = l - dat[i - 1][l];
                r1 = r - dat[i - 1][r];
                l2 = c + dat[i - 1][l];
                r2 = c + dat[i - 1][r];
                if ((x >> i - 1 & 1) == 0)
                {
                    l = l1;
                    r = r1;
                }
                else
                {
                    l = l2;
                    r = r2;
                }
            }
            return r - l;
        }
        //[l,r)でx以下の値の総積を返す(順序は保証されない)
        public T GetLessEqualRange(int l,int r,long x)
        {
            T ret = e;
            for (int i = h; i >= 1; i--)
            {
                int c = n - dat[i - 1][n];
                l1 = l - dat[i - 1][l];
                r1 = r - dat[i - 1][r];
                l2 = c + dat[i - 1][l];
                r2 = c + dat[i - 1][r];
                if ((x >> i - 1 & 1) == 0)
                {
                    l = l1;
                    r = r1;
                }
                else
                {
                    ret = operations.Op(ret, sts[i - 1].Get(l1, r1));
                    l = l2;
                    r = r2;
                }
            }
            ret = operations.Op(ret, sts[0].Get(l, r));
            return ret;
        }
        //x番目のvalueを変更する
        public void Set(int x, T value)
        {
            int l = x;
            int r = x + 1;
            long y = fa[x];
            for (int i = h; i >= 1; i--)
            {
                int c = n - dat[i - 1][n];
                l1 = l - dat[i - 1][l];
                r1 = r - dat[i - 1][r];
                l2 = c + dat[i - 1][l];
                r2 = c + dat[i - 1][r];
                if ((y >> i - 1 & 1) == 0)
                {
                    sts[i - 1].Set(l1, value);
                    l = l1;
                    r = r1;
                }
                else
                {
                    sts[i - 1].Set(l2, value);
                    l = l2;
                    r = r2;
                }
            }
        }
    }
}
