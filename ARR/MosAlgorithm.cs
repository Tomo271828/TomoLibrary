using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ARR
{
    public static class MosAlgorithm
    {
        public struct MoQuery : IComparable<MoQuery>
        {
            public int l;
            public int r;
            public int index;
            public long ord;
            public int CompareTo(MoQuery other)
            {
                return ord.CompareTo(other.ord);
            }
        }
        //0-indexed, 閉区間を想定
        //クエリで与えられる範囲は[0,n)とする

        //引数には上のMoQueryの配列を利用する
        /*
        渡す配列の作り方例
        MosAlgorithm.MoQuery[] query = new MosAlgorithm.MoQuery[q];
        for(int i = 0;i <= q - 1; i++)
        {
            int[] input = Input.IntArray();
            query[i] = new MosAlgorithm.MoQuery
            {
                l = input[0],
                r = input[1] - 1,
                index = i
            };
        }
        */
        //query[i].l みたいな形で参照可能
        //破壊的
        public static void SortQuery(MoQuery[] query,int n)
        {
            int q = query.Length;
            int maxn = 1;
            while(n >= maxn)
            {
                maxn <<= 1;
            }
            for(int i = 0;i <= q - 1; i++)
            {
                query[i].ord = HilbertOrder(query[i].l, query[i].r, maxn);
            }
            Array.Sort(query);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static long HilbertOrder(int x,int y,int p)
        {
            long rx = 0;
            long ry = 0;
            long d = 0;
            for(int i = p >> 1;i > 0;i >>= 1)
            {
                rx = (x & i) > 0 ? 1 : 0;
                ry = (y & i) > 0 ? 1 : 0;
                d += 1L * i * i * ((rx * 3) ^ ry);
                if(ry == 0)
                {
                    if(rx == 1)
                    {
                        x = p - 1 - x;
                        y = p - 1 - y;
                    }
                    int memo = x;
                    x = y;
                    y = memo;
                }
            }
            return d;
        }
    }
}
