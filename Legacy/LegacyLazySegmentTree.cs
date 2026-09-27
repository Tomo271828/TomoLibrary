using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.Legacy
{
    //区間は全て半開区間[a,b)を想定,入力は0-indexed
    public class LegacyLazySegmentTree
    {
        int n;
        long[] max;
        long[] lazymax;
        long[] min;
        long[] lazymin;
        public LegacyLazySegmentTree(int length)
        {
            n = 1;
            while (n < length)
            {
                n *= 2;
            }
            max = new long[n * 2];
            lazymax = new long[n * 2];
            min = new long[n * 2];
            lazymin = new long[n * 2];
        }
        public void FirstSet(long[] arr)
        {
            int n2 = arr.Length;
            for(int i = n * 2 - 2;i >= 0; i--)
            {
                if(i >= n - 1)
                {
                    if(i - n + 1 < n2)
                    {
                        min[i] = arr[i - n + 1];
                        max[i] = arr[i - n + 1];
                    }
                    else
                    {
                        min[i] = long.MaxValue / 2;
                        max[i] = 0;
                    }
                }
                else
                {
                    min[i] = Math.Min(min[i * 2 + 1], min[i * 2 + 2]);
                    max[i] = Math.Max(max[i * 2 + 1], max[i * 2 + 2]);
                }
            }
        }
        void EvalMax(int index, int l, int r)
        {
            if (lazymax[index] != 0)
            {
                max[index] = lazymax[index];
                if (r - l > 1)
                {
                    lazymax[2 * index + 1] = lazymax[index];
                    lazymax[2 * index + 2] = lazymax[index];
                }
                lazymax[index] = 0;
            }
        }
        public void SetMax(int a, int b, long value)
        {
            SetMax(a, b, value, 0, 0, n);
        }
        void SetMax(int a, int b, long value, int index, int left, int right)
        {
            EvalMax(index, left, right);
            if (b <= left || right <= a)
            {
                return;
            }
            if (a <= left && right <= b)
            {
                lazymax[index] = value;
                EvalMax(index, left, right);
            }
            else
            {
                SetMax(a, b, value, 2 * index + 1, left, (left + right) / 2);
                SetMax(a, b, value, 2 * index + 2, (left + right) / 2, right);
                max[index] = Math.Max(max[2 * index + 1], max[2 * index + 2]);
            }
        }
        public long GetMax(int a, int b)
        {
            return GetMax(a, b, 0, 0, n);
        }
        long GetMax(int a, int b, int index, int left, int right)
        {
            if (a >= right || b <= left)
            {
                return 0;
            }
            EvalMax(index, left, right);
            if (a <= left && right <= b)
            {
                return max[index];
            }
            long value1 = GetMax(a, b, 2 * index + 1, left, (left + right) / 2);
            long value2 = GetMax(a, b, 2 * index + 2, (left + right) / 2, right);
            return Math.Max(value1, value2);
        }
        void EvalMaxAdd(int index, int l, int r)
        {
            if (lazymax[index] != 0)
            {
                max[index] += lazymax[index];
                if (r - l > 1)
                {
                    lazymax[2 * index + 1] += lazymax[index];
                    lazymax[2 * index + 2] += lazymax[index];
                }
                lazymax[index] = 0;
            }
        }
        public void SetMaxAdd(int a, int b, long value)
        {
            SetMaxAdd(a, b, value, 0, 0, n);
        }
        void SetMaxAdd(int a, int b, long value, int index, int left, int right)
        {
            EvalMaxAdd(index, left, right);
            if (b <= left || right <= a)
            {
                return;
            }
            if (a <= left && right <= b)
            {
                lazymax[index] += value;
                EvalMaxAdd(index, left, right);
            }
            else
            {
                SetMaxAdd(a, b, value, 2 * index + 1, left, (left + right) / 2);
                SetMaxAdd(a, b, value, 2 * index + 2, (left + right) / 2, right);
                max[index] = Math.Max(max[2 * index + 1], max[2 * index + 2]);
            }
        }
        public long GetMaxAdd(int a, int b)
        {
            return GetMaxAdd(a, b, 0, 0, n);
        }
        long GetMaxAdd(int a, int b, int index, int left, int right)
        {
            if (a >= right || b <= left)
            {
                return 0;
            }
            EvalMaxAdd(index, left, right);
            if (a <= left && right <= b)
            {
                return max[index];
            }
            long value1 = GetMaxAdd(a, b, 2 * index + 1, left, (left + right) / 2);
            long value2 = GetMaxAdd(a, b, 2 * index + 2, (left + right) / 2, right);
            return Math.Max(value1, value2);
        }
        void EvalMin(int index, int l, int r)
        {
            if (lazymin[index] != 0)
            {
                min[index] = lazymin[index];
                if (r - l > 1)
                {
                    lazymin[2 * index + 1] = lazymin[index];
                    lazymin[2 * index + 2] = lazymin[index];
                }
                lazymin[index] = 0;
            }
        }
        public void SetMin(int a, int b, long value)
        {
            SetMin(a, b, value, 0, 0, n);
        }
        void SetMin(int a, int b, long value, int index, int left, int right)
        {
            EvalMin(index, left, right);
            if (b <= left || right <= a)
            {
                return;
            }
            if (a <= left && right <= b)
            {
                lazymin[index] = value;
                EvalMin(index, left, right);
            }
            else
            {
                SetMin(a, b, value, 2 * index + 1, left, (left + right) / 2);
                SetMin(a, b, value, 2 * index + 2, (left + right) / 2, right);
                min[index] = Math.Min(min[2 * index + 1], min[2 * index + 2]);
            }
        }
        public long GetMin(int a, int b)
        {
            return GetMin(a, b, 0, 0, n);
        }
        long GetMin(int a, int b, int index, int left, int right)
        {
            if (a >= right || b <= left)
            {
                return long.MaxValue / 2;
            }
            EvalMin(index, left, right);
            if (a <= left && right <= b)
            {
                return min[index];
            }
            long value1 = GetMin(a, b, 2 * index + 1, left, (left + right) / 2);
            long value2 = GetMin(a, b, 2 * index + 2, (left + right) / 2, right);
            return Math.Min(value1, value2);
        }
        void EvalMinAdd(int index, int l, int r)
        {
            if (lazymin[index] != 0)
            {
                min[index] += lazymin[index];
                if (r - l > 1)
                {
                    lazymin[2 * index + 1] += lazymin[index];
                    lazymin[2 * index + 2] += lazymin[index];
                }
                lazymin[index] = 0;
            }
        }
        public void SetMinAdd(int a, int b, long value)
        {
            SetMinAdd(a, b, value, 0, 0, n);
        }
        void SetMinAdd(int a, int b, long value, int index, int left, int right)
        {
            EvalMinAdd(index, left, right);
            if (b <= left || right <= a)
            {
                return;
            }
            if (a <= left && right <= b)
            {
                lazymin[index] += value;
                EvalMinAdd(index, left, right);
            }
            else
            {
                SetMinAdd(a, b, value, 2 * index + 1, left, (left + right) / 2);
                SetMinAdd(a, b, value, 2 * index + 2, (left + right) / 2, right);
                min[index] = Math.Min(min[2 * index + 1], min[2 * index + 2]);
            }
        }
        public long GetMinAdd(int a, int b)
        {
            return GetMinAdd(a, b, 0, 0, n);
        }
        long GetMinAdd(int a, int b, int index, int left, int right)
        {
            if (a >= right || b <= left)
            {
                return long.MaxValue / 2;
            }
            EvalMinAdd(index, left, right);
            if (a <= left && right <= b)
            {
                return min[index];
            }
            long value1 = GetMinAdd(a, b, 2 * index + 1, left, (left + right) / 2);
            long value2 = GetMinAdd(a, b, 2 * index + 2, (left + right) / 2, right);
            return Math.Min(value1, value2);
        }
        public void PrintMinAll()
        {
            Console.WriteLine(string.Join(" ", min));
        }
        public void PrimtMaxAll()
        {
            Console.WriteLine(string.Join(" ", max));
        }
    }
}
