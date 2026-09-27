using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.ARR;
using TomoLibrary.DataStructure;

namespace TomoLibrary.DataStructure.SegTree
{
    //0-index
    public class DynamicMergeSortTree
    {
        SortedMultiSet<long>[] dat;
        long[] arr;
        int size;
        int n;
        public DynamicMergeSortTree(int n)
        {
            arr = new long[n];
            Init(arr);
        }
        public DynamicMergeSortTree(long[] arr)
        {
            this.arr = arr;
            Init(arr);
        }
        void Init(long[] arr)
        {
            n = arr.Length;
            size = 1;
            while(size < n)
            {
                size *= 2;
            }
            dat = new SortedMultiSet<long>[size * 2];
            for(int i = 1;i <= size * 2 - 1; i++)
            {
                dat[i] = new SortedMultiSet<long>();
            }
            for(int i = 0;i <= size - 1; i++)
            {
                if(i <= n - 1)
                {
                    int index = i + size;
                    while(index > 0)
                    {
                        dat[index].Add(arr[i]);
                        index >>= 1;
                    }
                }
            }
        }
        public void Set(int index,long value)
        {
            int target = index + size;
            long previous = arr[index];
            arr[index] = value;
            while(target > 0)
            {
                dat[target].Remove(previous);
                dat[target].Add(value);
                target >>= 1;
            }
        }
        //l番目からr-1番目までの中でx以下の要素の個数を返す
        public int EqualLessThanCount(int l,int r,long x)
        {
            int ret = 0;
            l += size;
            r += size;
            while (l < r)
            {
                if (l % 2 == 1)
                {
                    ret += dat[l].UpperBound(x);
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    ret += dat[r].UpperBound(x);
                }
                l /= 2;
                r /= 2;
            }
            return ret;
        }
        //l番目からr-1番目までの中でxより大きい要素の個数を返す
        public int MoreThanCount(int l, int r, long x)
        {
            return r - l + 1 - EqualLessThanCount(l, r, x);
        }
        public int Count(int l,int r,long x)
        {
            int ret = 0;
            l += size;
            r += size;
            while (l < r)
            {
                if (l % 2 == 1)
                {
                    ret += dat[l].Count(x);
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    ret += dat[r].Count(x);
                }
                l /= 2;
                r /= 2;
            }
            return ret;
        }
    }
}
