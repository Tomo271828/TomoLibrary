using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.ARR;

namespace TomoLibrary.DataStructure.SegTree
{
    //0-index
    public class MergeSortTree
    {
        long[][] dat;
        long[][] cum;
        int size;
        int n;
        public MergeSortTree(long[] arr)
        {
            size = 1;
            n = arr.Length;
            while(size < n)
            {
                size *= 2;
            }
            dat = new long[size * 2][];
            cum = new long[size * 2][];
            for(int i = size;i <= size * 2 - 1; i++)
            {
                if(i - size <= n - 1)
                {
                    dat[i] = new long[1] { arr[i - size] };
                    cum[i] = new long[2] { 0, arr[i - size] };
                }
                else
                {
                    dat[i] = new long[0];
                    cum[i] = new long[1] { 0 };
                }
            }
            for(int i = size - 1;i >= 1; i--)
            {
                dat[i] = new long[dat[i * 2].Length + dat[i * 2 + 1].Length];
                cum[i] = new long[dat[i].Length + 1];
                cum[i][0] = 0;
                int len = dat[i].Length;
                int i1 = 0;
                int i2 = 0;
                for(int j = 0;j <= len - 1; j++)
                {
                    if(i1 == dat[i * 2].Length)
                    {
                        dat[i][j] = dat[i * 2 + 1][i2];
                        i2 += 1;
                    }
                    else if(i2 == dat[i * 2 + 1].Length)
                    {
                        dat[i][j] = dat[i * 2][i1];
                        i1 += 1;
                    }
                    else
                    {
                        if (dat[i * 2][i1] < dat[i * 2 + 1][i2])
                        {
                            dat[i][j] = dat[i * 2][i1];
                            i1 += 1;
                        }
                        else
                        {
                            dat[i][j] = dat[i * 2 + 1][i2];
                            i2 += 1;
                        }
                    }
                    cum[i][j + 1] = cum[i][j] + dat[i][j];
                }
            }
        }
        //l番目からr-1番目までの中でx以下の要素の個数を返す
        public int EqualLessThanCount(int l,int r,long x)
        {
            int ret = 0;
            l += size;
            r += size;
            while(l < r)
            {
                if(l % 2 == 1)
                {
                    ret += BinarySearch.EqualLessThanCount(dat[l], x);
                    l += 1;
                }
                if(r % 2 == 1)
                {
                    r -= 1;
                    ret += BinarySearch.EqualLessThanCount(dat[r], x);
                }
                l /= 2;
                r /= 2;
            }
            return ret;
        }
        //l番目からr-1番目までの中でxより大きい要素の個数を返す
        public int MoreThanCount(int l, int r, long x)
        {
            int ret = 0;
            l += size;
            r += size;
            while (l < r)
            {
                if (l % 2 == 1)
                {
                    ret += BinarySearch.MoreThanCount(dat[l], x);
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    ret += BinarySearch.MoreThanCount(dat[r], x);
                }
                l /= 2;
                r /= 2;
            }
            return ret;
        }
        //l番目からr-1番目までの中でx以下の要素の総和を返す
        public long EqualLessThanSum(int l,int r,long x)
        {
            long ret = 0;
            l += size;
            r += size;
            while(l < r)
            {
                if (l % 2 == 1)
                {
                    int index = BinarySearch.EqualLessThanCount(dat[l], x);
                    ret += cum[l][index];
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    int index = BinarySearch.EqualLessThanCount(dat[r], x);
                    ret += cum[r][index];
                }
                l /= 2;
                r /= 2;
            }
            return ret;
        }
        //l番目からr-1番目までの中でxより大きい要素の総和を返す
        public long MoreThanSum(int l, int r, long x)
        {
            long ret = 0;
            l += size;
            r += size;
            while (l < r)
            {
                if (l % 2 == 1)
                {
                    int index = BinarySearch.EqualLessThanCount(dat[l], x);
                    ret += cum[l][cum[l].Length - 1] - cum[r][index];
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    int index = BinarySearch.EqualLessThanCount(dat[r], x);
                    ret += cum[r][cum[r].Length - 1] - cum[r][index];
                }
                l /= 2;
                r /= 2;
            }
            return ret;
        }
    }
}
