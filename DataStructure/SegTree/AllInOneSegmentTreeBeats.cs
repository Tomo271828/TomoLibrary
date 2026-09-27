using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class AllInOneSegmentTreeBeats
    {
        long[] max;
        long[] smax;
        long[] cmax;
        long[] min;
        long[] smin;
        long[] cmin;
        long[] sum;
        long[] len;
        long[] ladd;
        int n;
        int size;
        long inf = long.MaxValue / 4;
        public AllInOneSegmentTreeBeats(int n)
        {
            this.n = n;
            size = 1;
            while (size < n)
            {
                size *= 2;
            }
            max = new long[size * 2];
            smax = new long[size * 2];
            cmax = new long[size * 2];
            min = new long[size * 2];
            smin = new long[size * 2];
            cmin = new long[size * 2];
            sum = new long[size * 2];
            len = new long[size * 2];
            ladd = new long[size * 2];
            Init(new long[n]);
        }
        public AllInOneSegmentTreeBeats(long[] arr)
        {
            n = arr.Length;
            size = 1;
            while (size < n)
            {
                size <<= 1;
            }
            max = new long[size * 2];
            smax = new long[size * 2];
            cmax = new long[size * 2];
            min = new long[size * 2];
            smin = new long[size * 2];
            cmin = new long[size * 2];
            sum = new long[size * 2];
            len = new long[size * 2];
            ladd = new long[size * 2];
            Init(arr);
        }
        void Init(long[] arr)
        {
            for(int i = size;i <= size * 2 - 1; i++)
            {
                if(i - size < arr.Length)
                {
                    max[i] = arr[i - size];
                    smax[i] = inf * -1;
                    cmax[i] = 1;
                    min[i] = arr[i - size];
                    smin[i] = inf;
                    cmin[i] = 1;
                    sum[i] = arr[i - size];
                }
                else
                {
                    max[i] = inf * -1;
                    smax[i] = inf * -1;
                    cmax[i] = 0;
                    min[i] = inf;
                    smin[i] = inf;
                    cmin[i] = 0;
                    sum[i] = 0;
                }
            }
            len[1] = size;
            for (int i = 1; i <= size - 1; i++)
            {
                len[i * 2] = len[i] >> 1;
                len[i * 2 + 1] = len[i] >> 1;
            }
            for(int i = size - 1;i >= 1; i--)
            {
                NodeUpdate(i);
            }
        }
        void NodeUpdate(int k)
        {
            sum[k] = sum[2 * k] + sum[2 * k + 1];
            if (max[2 * k] > max[2 * k + 1])
            {
                max[k] = max[2 * k];
                smax[k] = Math.Max(max[2 * k + 1], smax[2 * k]);
                cmax[k] = cmax[2 * k];
            }
            else if(max[2 * k] < max[2 * k + 1])
            {
                max[k] = max[2 * k + 1];
                smax[k] = Math.Max(max[2 * k], smax[2 * k + 1]);
                cmax[k] = cmax[2 * k + 1];
            }
            else
            {
                max[k] = max[2 * k];
                smax[k] = Math.Max(smax[k * 2], smax[k * 2 + 1]);
                cmax[k] = cmax[k * 2] + cmax[k * 2 + 1];
            }
            if (min[2 * k] < min[2 * k + 1])
            {
                min[k] = min[2 * k];
                smin[k] = Math.Min(min[2 * k + 1], smin[2 * k]);
                cmin[k] = cmin[2 * k];
            }
            else if (min[2 * k] > min[2 * k + 1])
            {
                min[k] = min[2 * k + 1];
                smin[k] = Math.Min(min[2 * k], smin[2 * k + 1]);
                cmin[k] = cmin[2 * k + 1];
            }
            else
            {
                min[k] = min[2 * k];
                smin[k] = Math.Min(smin[k * 2], smin[k * 2 + 1]);
                cmin[k] = cmin[k * 2] + cmin[k * 2 + 1];
            }
        }
        void Push(int k)
        {
            if(size <= k)
            {
                return;
            }
            if (ladd[k] != 0)
            {
                AddAll(k * 2, ladd[k]);
                AddAll(k * 2 + 1, ladd[k]);
                ladd[k] = 0;
            }
            if (max[k] < max[2 * k])
            {
                UpdateMax(2 * k, max[k]);
            }
            if (min[2 * k] < min[k])
            {
                UpdateMin(2 * k, min[k]);
            }
            if (max[k] < max[2 * k + 1])
            {
                UpdateMax(2 * k + 1, max[k]);
            }
            if (min[2 * k + 1] < min[k])
            {
                UpdateMin(2 * k + 1, min[k]);
            }
        }
        void UpdateMax(int k,long x)
        {
            sum[k] -= max[k] * cmax[k];
            sum[k] += x * cmax[k];
            if (max[k] == min[k])
            {
                max[k] = x;
                min[k] = x;
            }
            else if (max[k] == smin[k])
            {
                max[k] = x;
                smin[k] = x;
            }
            else
            {
                max[k] = x;
            }
        }
        void UpdateMin(int k,long x)
        {
            sum[k] -= min[k] * cmin[k];
            sum[k] += x * cmin[k];
            if (max[k] == min[k])
            {
                max[k] = x;
                min[k] = x;
            }
            else if (smax[k] == min[k])
            {
                smax[k] = x;
                min[k] = x;
            }
            else
            {
                min[k] = x;
            }
        }
        void AddAll(int k,long x)
        {
            max[k] += x;
            if (smax[k] > inf * -1)
            {
                smax[k] += x;
            }
            min[k] += x;
            if (smin[k] < inf)
            {
                smin[k] += x;
            }
            sum[k] += len[k] * x;
            ladd[k] += x;
        }
        //[a,b)にa_i=min(a_i,x)をする
        public void Chmin(int a,int b,long x)
        {
            Chmin(a, b, 0, size, 1, x);
        }
        void Chmin(int a,int b,int l,int r,int k,long x)
        {
            if(b <= l || r <= a || max[k] <= x)
            {
                return;
            }
            if(a <= l && r <= b && smax[k] < x)
            {
                UpdateMax(k, x);
                return;
            }
            Push(k);
            int center = (l + r) / 2;
            Chmin(a, b, l, center, k * 2, x);
            Chmin(a, b, center, r, k * 2 + 1, x);
            NodeUpdate(k);
        }
        //[a,b)にa_i=max(a_i,x)する
        public void Chmax(int a, int b, long x)
        {
            Chmax(a, b, 0, size, 1, x);
        }
        void Chmax(int a, int b, int l, int r, int k, long x)
        {
            if (b <= l || r <= a || min[k] >= x)
            {
                return;
            }
            if (a <= l && r <= b && smin[k] > x)
            {
                UpdateMin(k, x);
                return;
            }
            Push(k);
            int center = (l + r) / 2;
            Chmax(a, b, l, center, k * 2, x);
            Chmax(a, b, center, r, k * 2 + 1, x);
            NodeUpdate(k);
        }
        //[a,b)にa_i=a_i+xする
        public void Add(int a,int b,long x)
        {
            Add(a, b, 0, size, 1, x);
        }
        void Add(int a,int b,int l,int r,int k,long x)
        {
            if(b <= l || r <= a)
            {
                return;
            }
            if(a <= l && r <= b)
            {
                AddAll(k, x);
                return;
            }
            Push(k);
            int center = (l + r) / 2;
            Add(a, b, l, center, k * 2, x);
            Add(a, b, center, r, k * 2 + 1, x);
            NodeUpdate(k);
        }
        //[a,b)にa_i=xする
        public void Update(int a,int b,long x)
        {
            Chmin(a, b, x);
            Chmax(a, b, x);
        }
        //[a,b)の最大値を取得
        public long GetMax(int a,int b)
        {
            return GetMax(a, b, 0, size, 1);
        }
        long GetMax(int a,int b,int l,int r,int k)
        {
            if(b <= l || r <= a)
            {
                return inf * -1;
            }
            if(a <= l && r <= b)
            {
                return max[k];
            }
            Push(k);
            int center = (l + r) / 2;
            long lv = GetMax(a, b, l, center, k * 2);
            long rv = GetMax(a, b, center, r, k * 2 + 1);
            return Math.Max(lv, rv);
        }
        //[a,b)の最小値を取得
        public long GetMin(int a, int b)
        {
            return GetMin(a, b, 0, size, 1);
        }
        long GetMin(int a, int b, int l, int r, int k)
        {
            if (b <= l || r <= a)
            {
                return inf;
            }
            if (a <= l && r <= b)
            {
                return min[k];
            }
            Push(k);
            int center = (l + r) / 2;
            long lv = GetMin(a, b, l, center, k * 2);
            long rv = GetMin(a, b, center, r, k * 2 + 1);
            return Math.Min(lv, rv);
        }
        //[a,b)の総和を取得
        public long GetSum(int a, int b)
        {
            return GetSum(a, b, 0, size, 1);
        }
        long GetSum(int a, int b, int l, int r, int k)
        {
            if (b <= l || r <= a)
            {
                return 0;
            }
            if (a <= l && r <= b)
            {
                return sum[k];
            }
            Push(k);
            int center = (l + r) / 2;
            long lv = GetSum(a, b, l, center, k * 2);
            long rv = GetSum(a, b, center, r, k * 2 + 1);
            return lv + rv;
        }
    }
}
