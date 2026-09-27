using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class LazySegmentTree<T,V>
    {
        int size;
        int n;
        int log;
        ILazySegmentTree<T, V> operations;
        T e;
        V id;
        T[] data;
        V[] lazy;
        public LazySegmentTree(ILazySegmentTree<T,V> operations,int n)
        {
            this.operations = operations;
            this.n = n;
            e = operations.E();
            id = operations.Id();
            size = 1;
            log = 0;
            while(size < n)
            {
                size *= 2;
                log += 1;
            }
            data = new T[size * 2];
            lazy = new V[size];
            for(int i = 0;i <= size - 1; i++)
            {
                data[i + size] = e;
                lazy[i] = id;
            }
            for(int i = size - 1;i >= 1; i--)
            {
                Update(i);
            }
        }
        public LazySegmentTree(ILazySegmentTree<T, V> operations, T[] arr)
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
            data = new T[size * 2];
            lazy = new V[size];
            for (int i = 0; i <= size - 1; i++)
            {
                if(i <= n - 1)
                {
                    data[i + size] = arr[i];
                }
                else
                {
                    data[i + size] = e;
                }
                lazy[i] = id;
            }
            for (int i = size - 1; i >= 1; i--)
            {
                Update(i);
            }
        }
        public void SetIndex(int p,T x)
        {
            p += size;
            for(int i = log;i >= 1; i--)
            {
                Push(p >> i);
            }
            data[p] = x;
            for(int i = 1;i <= log; i++)
            {
                Update(p >> i);
            }
        }
        //半開区間 [l,r) の総積(演算結果)を取得
        public T Get(int l,int r)
        {
            if(l == r)
            {
                return e;
            }
            l += size;
            r += size;
            for(int i = log;i >= 1; i--)
            {
                if(l >> i << i != l)
                {
                    Push(l >> i);
                }
                if(r >> i << i != r)
                {
                    Push(r - 1 >> i);
                }
            }
            T sml = e;
            T smr = e;
            while (l < r)
            {
                if (l % 2 == 1)
                {
                    sml = operations.Op(sml, data[l]);
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    smr = operations.Op(data[r], smr);
                }
                l /= 2;
                r /= 2;
            }
            return operations.Op(sml, smr);
        }
        public T GetIndex(int p)
        {
            p += size;
            for (int i = log; i >= 1; i--)
            {
                Push(p >> i);
            }
            return data[p];
        }
        public T GetAll()
        {
            return Get(0, n);
        }
        public void Apply(int l,int r,V f)
        {
            if(l == r)
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
            int l2 = l;
            int r2 = r;
            while(l < r)
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
            l = l2;
            r = r2;
            for (int i = 1; i <= log; i++)
            {
                if (l >> i << i != l)
                {
                    Update(l >> i);
                }
                if (r >> i << i != r)
                {
                    Update(r - 1 >> i);
                }
            }
        }
        public void ApplyIndex(int p,V f)
        {
            p += size;
            for(int i = log;i >= 1; i--)
            {
                Push(p >> i);
            }
            data[p] = operations.Mapping(f, data[p]);
            for(int i = 1;i <= log; i++)
            {
                Update(p >> i);
            }
        }
        //F(Get(l,x)) == true となる最大の x を求める
        public int MaxRight(int l, IBinarySearch<T> func)
        {
            if (l == n)
            {
                return n;
            }
            l += size;
            for(int i = log;i >= 1; i--)
            {
                Push(l >> i);
            }
            T sum = e;
            while (true)
            {
                while (l % 2 == 0)
                {
                    l /= 2;
                }
                if (func.F(operations.Op(sum, data[l])) == false)
                {
                    Push(l);
                    while (l < size)
                    {
                        l *= 2;
                        if (func.F(operations.Op(sum, data[l])))
                        {
                            sum = operations.Op(sum, data[l]);
                            l += 1;
                        }
                    }
                    return l - size;
                }
                sum = operations.Op(sum, data[l]);
                l += 1;
                if ((l & -l) == l)
                {
                    break;
                }
            }
            return n;
        }
        //F(Get(x,r)) == true となる最小の x を求める
        public int MinLeft(int r, IBinarySearch<T> func)
        {
            if (r == 0)
            {
                return 0;
            }
            r += size;
            for(int i = log;i >= 1; i--)
            {
                Push(r - 1 >> i);
            }
            T sum = e;
            while (true)
            {
                r -= 1;
                while (r > 0 && r % 2 == 1)
                {
                    r /= 2;
                }
                if (func.F(operations.Op(data[r], sum)) == false)
                {
                    while (r < size)
                    {
                        Push(r);
                        r = 2 * r + 1;
                        if (func.F(operations.Op(data[r], sum)))
                        {
                            sum = operations.Op(data[r], sum);
                            r -= 1;
                        }
                    }
                    return r + 1 - size;
                }
                sum = operations.Op(data[r], sum);
                if ((r & -r) == r)
                {
                    break;
                }
            }
            return 0;
        }
        void Update(int p)
        {
            data[p] = operations.Op(data[p * 2], data[p * 2 + 1]);
        }
        void AllApply(int p,V f)
        {
            data[p] = operations.Mapping(f, data[p]);
            if(p < size)
            {
                lazy[p] = operations.Composition(f, lazy[p]);
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
