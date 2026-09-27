using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;
using TomoLibrary.ARR;
using TomoLibrary.DataStructure.SegTree;

namespace TomoLibrary.VP
{
    public interface IRectangleSum<T> : ISegmentTree<T>
    {
        public T Inv(T x);
    }
    public class RectangleSum<T>
    {
        IRectangleSum<T> operations;
        T e;
        Dictionary<(long, long), int> dict;
        T[] startvalue;
        long[] x;
        long[] y;
        T[] v;
        int n;
        int h;
        int[][] dat;
        SegmentTree<T>[] sts;
        int l1;
        int r1;
        int l2;
        int r2;
        public RectangleSum(IRectangleSum<T> operations,(long, long)[] points, T[] arr)
        {
            this.operations = operations;
            Init(points.ToList(), arr);
        }
        public RectangleSum(IRectangleSum<T> operations, List<(long,long)> points, T[] arr)
        {
            this.operations = operations;
            Init(points, arr);
        }
        public RectangleSum(IRectangleSum<T> operations, HashSet<(long,long)> points, T[] arr)
        {
            this.operations = operations;
            Init(points.ToList(), arr);
        }
        void Init(List<(long,long)> points, T[] arr)
        {
            Dictionary<(long, long), T> d = new Dictionary<(long, long), T>();
            e = operations.E();
            for(int i = 0;i <= points.Count - 1; i++)
            {
                (long, long) p = points[i];
                if (!d.ContainsKey(p))
                {
                    d.Add(p, e);
                }
                d[p] = operations.Op(d[p], arr[i]);
            }
            List<(long, long, T)> ps = new List<(long, long, T)>();
            foreach((long,long) p in d.Keys)
            {
                ps.Add((p.Item1, p.Item2, d[p]));
            }
            ps.Sort((a, b) => a.Item1 == b.Item1 ? Math.Sign(a.Item2 - b.Item2) : Math.Sign(a.Item1 - b.Item1));
            n = ps.Count;
            x = new long[n];
            y = new long[n];
            v = new T[n];
            dict = new Dictionary<(long, long), int>();
            startvalue = new T[n];
            for(int i = 0;i <= n - 1; i++)
            {
                x[i] = ps[i].Item1;
                y[i] = ps[i].Item2;
                v[i] = ps[i].Item3;
                dict.Add((ps[i].Item1, ps[i].Item2), i);
                startvalue[i] = v[i];
            }
            h = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                while(((long)1 << h) < y[i])
                {
                    h += 1;
                }
            }
            h += 1;
            sts = new SegmentTree<T>[h];
            dat = new int[h][];
            Queue<(long, T)> left = new Queue<(long, T)>();
            Queue<(long, T)> right = new Queue<(long, T)>();
            for(int i = h - 1;i >= 0; i--)
            {
                dat[i] = new int[n + 1];
                dat[i][0] = 0;
                for (int j = 0; j <= n - 1; j++)
                {
                    int dir = (int)((y[j] >> i) & 1);
                    dat[i][j + 1] = dat[i][j] + dir;
                    if (dir == 0)
                    {
                        left.Enqueue((y[j], v[j]));
                    }
                    else
                    {
                        right.Enqueue((y[j], v[j]));
                    }
                }
                int index = 0;
                while (left.TryDequeue(out (long, T) item))
                {
                    y[index] = item.Item1;
                    v[index] = item.Item2;
                    index += 1;
                }
                while (right.TryDequeue(out (long, T) item))
                {
                    y[index] = item.Item1;
                    v[index] = item.Item2;
                    index += 1;
                }
                sts[i] = new SegmentTree<T>(operations, v);
            }
        }
        //l <= x < r かつ 0 <= y < border に含まれる点の総積を返す
        public T GetLowArea(long lx,long rx,long border)
        {
            T ret = e;
            int l = BinarySearch.EqualMoreThan(x, lx);
            int r = BinarySearch.LessThan(x, rx) + 1;
            for (int i = h; i >= 1; i--)
            {
                int c = n - dat[i - 1][n];
                l1 = l - dat[i - 1][l];
                r1 = r - dat[i - 1][r];
                l2 = c + dat[i - 1][l];
                r2 = c + dat[i - 1][r];
                if(((border >> (i - 1)) & 1) == 0)
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
            return ret;
        }
        //h1 <= x < h2 かつ w1 <= y < w2 に含まれる点の総積を返す
        public T GetArea(long h1,long w1,long h2,long w2)
        {
            T up = GetLowArea(h1, h2, w2);
            T down = GetLowArea(h1, h2, w1);
            return operations.Op(up, operations.Inv(down));
        }
        //(a,b)の値を取得する
        public T GetIndex(long a,long b)
        {
            if (!dict.ContainsKey((a, b)))
            {
                return e;
            }
            return startvalue[dict[(a, b)]];
        }
        //(a,b)の値をvalueにする
        public void SetValue(long a,long b,T value)
        {
            if (!dict.ContainsKey((a, b)))
            {
                throw new Exception("点がありません！");
            }
            startvalue[dict[(a, b)]] = value;
            int l = dict[(a, b)];
            int r = dict[(a, b)] + 1;
            for (int i = h; i >= 1; i--)
            {
                int c = n - dat[i - 1][n];
                l1 = l - dat[i - 1][l];
                r1 = r - dat[i - 1][r];
                l2 = c + dat[i - 1][l];
                r2 = c + dat[i - 1][r];
                if(l1 != r1)
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
