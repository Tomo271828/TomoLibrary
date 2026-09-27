using System;
using System.Collections.Generic;
using TomoLibrary;

namespace TomoLibrary.Graph
{
    public interface ZeroOneOnTreeScore
    {
        /*
        比較する側の0の個数をa0、1の個数をa1、
        比較される側の0の個数をb0、1の個数をb1としたとき、
        比較する側の方が手前におかれているときの答えへの寄与を返す
        */

        /*
        Scoreは推移的である(A<BかつB<CならばA<C)必要があり、
        Scoreは0の個数と1の個数のみから計算できる必要がある
        */

        /*
        転倒数の場合は
        return a1 * b0;
        */
        public long Score(long a0, long a1, long b0, long b1);
    }
    class ZeroOneOnTreeComparer : IComparer<ZeroOneOnTreeTarget>
    {
        private readonly ZeroOneOnTreeScore func;
        public ZeroOneOnTreeComparer(ZeroOneOnTreeScore func)
        {
            this.func = func;
        }
        public int Compare(ZeroOneOnTreeTarget x, ZeroOneOnTreeTarget y)
        {
            bool xzero = x.c0 == 0 && x.c1 == 0;
            bool yzero = y.c0 == 0 && y.c1 == 0;
            if (xzero && yzero)
            {
                return 0;
            }
            if (xzero)
            {
                return 1;
            }
            if (yzero)
            {
                return -1;
            }
            long xy = func.Score(x.c0, x.c1, y.c0, y.c1);
            long yx = func.Score(y.c0, y.c1, x.c0, x.c1);
            return xy.CompareTo(yx);
        }
    }
    readonly struct ZeroOneOnTreeTarget
    {
        public readonly long c0, c1;
        public ZeroOneOnTreeTarget(long c0, long c1)
        {
            this.c0 = c0;
            this.c1 = c1;
        }
    }
    public class ZeroOneOnTree
    {
        int n;
        int[] p;
        long[] zero, one;
        ZeroOneOnTreeScore func;
        //parentには各頂点の親の頂点番号(0-indexed)をいれる  根に関しては考慮せず、長さN-1の配列を与える
        //zeroとoneはそれぞれ各頂点が持つ0と1の個数
        public ZeroOneOnTree(int[] parent, long[] zero, long[] one, ZeroOneOnTreeScore func)
        {
            if (parent.Length + 1 != zero.Length || parent.Length + 1 != one.Length)
            {
                throw new ArgumentException("木を表す配列と他の配列の長さがあっていません！");
            }
            n = parent.Length + 1;
            p = new int[n];
            for (int i = 1; i <= n - 1; i++)
            {
                p[i] = parent[i - 1];
            }
            this.zero = zero;
            this.one = one;
            this.func = func;
        }
        //木上の頂点を親子関係にしたがってトポロジカルソートしたときの、Scoreの総和の最小値を求める
        //復元はしない
        public long Solve()
        {
            long answer = 0;
            long[] count0 = new long[n];
            long[] count1 = new long[n];
            PriorityQueue<int, ZeroOneOnTreeTarget> queue = new PriorityQueue<int, ZeroOneOnTreeTarget>(new ZeroOneOnTreeComparer(func));
            int[] uf = new int[n];
            for (int i = 0; i <= n - 1; i++)
            {
                count0[i] = zero[i];
                count1[i] = one[i];
                uf[i] = i;
                if (i != 0)
                {
                    ZeroOneOnTreeTarget target = new ZeroOneOnTreeTarget(count0[i], count1[i]);
                    queue.Enqueue(i, target);
                }
            }
            while (queue.TryDequeue(out int index, out ZeroOneOnTreeTarget target))
            {
                long c0 = target.c0;
                long c1 = target.c1;
                if (count0[index] != c0 || count1[index] != c1)
                {
                    continue;
                }
                if (uf[index] != index)
                {
                    continue;
                }
                int par = p[index];
                while (uf[par] != par)
                {
                    uf[par] = uf[uf[par]];
                    par = uf[par];
                }
                uf[index] = par;
                answer += func.Score(count0[par], count1[par], c0, c1);
                count0[par] += c0;
                count1[par] += c1;
                if (par != 0)
                {
                    ZeroOneOnTreeTarget newtar = new ZeroOneOnTreeTarget(count0[par], count1[par]);
                    queue.Enqueue(par, newtar);
                }
            }
            return answer;
        }
        //復元結果付き
        public (long, int[]) Construct()
        {
            long answer = 0;
            int[] ret = new int[n];
            long[] count0 = new long[n];
            long[] count1 = new long[n];
            PriorityQueue<int, ZeroOneOnTreeTarget> queue = new PriorityQueue<int, ZeroOneOnTreeTarget>(new ZeroOneOnTreeComparer(func));
            int[] uf = new int[n];
            int[] first = new int[n];
            int[] last = new int[n];
            int[] next = new int[n];
            for (int i = 0; i <= n - 1; i++)
            {
                count0[i] = zero[i];
                count1[i] = one[i];
                uf[i] = i;
                first[i] = i;
                last[i] = i;
                next[i] = i;
                if (i != 0)
                {
                    ZeroOneOnTreeTarget target = new ZeroOneOnTreeTarget(count0[i], count1[i]);
                    queue.Enqueue(i, target);
                }
            }
            while (queue.TryDequeue(out int index, out ZeroOneOnTreeTarget target))
            {
                long c0 = target.c0;
                long c1 = target.c1;
                if (count0[index] != c0 || count1[index] != c1)
                {
                    continue;
                }
                if (uf[index] != index)
                {
                    continue;
                }
                int par = p[index];
                while (uf[par] != par)
                {
                    uf[par] = uf[uf[par]];
                    par = uf[par];
                }
                uf[index] = par;
                next[last[par]] = index;
                last[par] = last[index];
                answer += func.Score(count0[par], count1[par], c0, c1);
                count0[par] += c0;
                count1[par] += c1;
                if (par != 0)
                {
                    ZeroOneOnTreeTarget newtar = new ZeroOneOnTreeTarget(count0[par], count1[par]);
                    queue.Enqueue(par, newtar);
                }
            }
            int node = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                node = next[node];
                ret[i] = node;
            }
            return (answer, ret);
        }
    }
}
