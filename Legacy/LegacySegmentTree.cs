using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.Legacy
{
    //配列は1-indexed, 区間取得は半開区間[l,r)
    public class LegacySegmentTree
    {
        public long[] dat;
        public int size;
        public LegacySegmentTree(int length,long defo)
        {
            size = 1;
            while(length > size)
            {
                size *= 2;
            }
            dat = new long[size * 2];
            for(int i = 1;i <= size * 2 - 1; i++)
            {
                dat[i] = defo;
            }
        }
        public void SetMax(int pos, long x)
        {
            pos = pos + size - 1;
            dat[pos] = x;
            while (pos >= 2)
            {
                pos /= 2;
                dat[pos] = Math.Max(dat[pos * 2], dat[pos * 2 + 1]);
            }
        }
        public void SetMin(int pos, long x)
        {
            pos = pos + size - 1;
            dat[pos] = x;
            while (pos >= 2)
            {
                pos /= 2;
                dat[pos] = Math.Min(dat[pos * 2], dat[pos * 2 + 1]);
            }
        }
        public void SetSum(int pos, long x)
        {
            pos = pos + size - 1;
            dat[pos] = x;
            while (pos >= 2)
            {
                pos /= 2;
                dat[pos] = dat[pos * 2] + dat[pos * 2 + 1];
            }
        }
        public long GetMax(int l, int r)
        {
            l += size - 1;
            r += size - 1;
            long vl = 0;
            long vr = 0;
            while(l < r)
            {
                if((l & 1) == 1)
                {
                    vl = Math.Max(vl, dat[l]);
                    l += 1;
                }
                if((r & 1) == 1)
                {
                    r -= 1;
                    vr = Math.Max(vr, dat[r]);
                }
                l /= 2;
                r /= 2;
            }
            return Math.Max(vl, vr);
        }
        public long GetMin(int l, int r)
        {
            l += size - 1;
            r += size - 1;
            long vl = long.MaxValue;
            long vr = long.MaxValue;
            while (l < r)
            {
                if ((l & 1) == 1)
                {
                    vl = Math.Min(vl, dat[l]);
                    l += 1;
                }
                if ((r & 1) == 1)
                {
                    r -= 1;
                    vr = Math.Min(vr, dat[r]);
                }
                l /= 2;
                r /= 2;
            }
            return Math.Min(vl, vr);
        }
        public long GetSum(int l, int r)
        {
            l += size - 1;
            r += size - 1;
            long vl = 0;
            long vr = 0;
            while (l < r)
            {
                if ((l & 1) == 1)
                {
                    vl += dat[l];
                    l += 1;
                }
                if ((r & 1) == 1)
                {
                    r -= 1;
                    vr += dat[r];
                }
                l /= 2;
                r /= 2;
            }
            return vl + vr;
        }
        public long GetIndex(int index)
        {
            return dat[size + index - 1];
        }
        public void PrintDat()
        {
            Console.WriteLine(string.Join(" ", dat));
        }
    }
}
