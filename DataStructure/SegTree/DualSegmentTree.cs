using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class DualSegmentTree<T,V>
    {
        int size;
        int n;
        int log;
        IDualSegmentTree<T, V> operations;
        T e;
        V id;
        T[] data;
        V[] lazy;
        public DualSegmentTree(IDualSegmentTree<T,V> operations,int n)
        {
            this.operations = operations;
            this.n = n;
            e = operations.E();
            id = operations.Id();
            size = 1;
            log = 0;
            while (size < n)
            {
                size *= 2;
                log += 1;
            }
            data = new T[size];
            lazy = new V[size];
            for (int i = 0; i <= size - 1; i++)
            {
                data[i] = e;
                lazy[i] = id;
            }
        }
        public DualSegmentTree(IDualSegmentTree<T,V> operations, T[] arr)
        {
            this.operations = operations;
            n = arr.Length;
            e = operations.E();
            id = operations.Id();
            size = 1;
            log = 0;
            while (size <= n)
            {
                size *= 2;
                log += 1;
            }
            data = new T[size];
            lazy = new V[size];
            for (int i = 0; i <= size - 1; i++)
            {
                if (i <= n - 1)
                {
                    data[i] = arr[i];
                }
                else
                {
                    data[i] = e;
                }
                lazy[i] = id;
            }
        }
        public void SetIndex(int p, T x)
        {
            p += size;
            for (int i = log; i >= 1; i--)
            {
                Push(p >> i);
            }
            data[p - size] = x;
        }
        public T GetIndex(int p)
        {
            p += size;
            for (int i = log; i >= 1; i--)
            {
                Push(p >> i);
            }
            return data[p - size];
        }
        public void Apply(int l, int r, V f)
        {
            if (l == r)
            {
                return;
            }
            l += size;
            r += size;
            for (int i = log; i >= 1; i--)
            {
                if (l >> i << i != l)
                {
                    Push(l >> i);
                }
                if (r >> i << i != r)
                {
                    Push(r - 1 >> i);
                }
            }
            while (l < r)
            {
                if (l % 2 == 1)
                {
                    AllApply(l, f);
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    AllApply(r, f);
                }
                l /= 2;
                r /= 2;
            }
        }
        public void ApplyIndex(int p, V f)
        {
            p += size;
            for (int i = log; i >= 1; i--)
            {
                Push(p >> i);
            }
            data[p - size] = operations.Mapping(f, data[p - size]);
        }
        void AllApply(int p, V f)
        {
            if (p < size)
            {
                lazy[p] = operations.Composition(f, lazy[p]);
            }
            else
            {
                data[p - size] = operations.Mapping(f, data[p - size]);
            }
        }
        void Push(int p)
        {
            AllApply(2 * p, lazy[p]);
            AllApply(2 * p + 1, lazy[p]);
            lazy[p] = id;
        }
    }
}
