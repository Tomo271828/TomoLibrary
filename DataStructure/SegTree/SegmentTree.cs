using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class SegmentTree<T>
    {
        //0 - indexed
        int size;
        int n;
        ISegmentTree<T> operations;
        T e;
        T[] dat;
        public SegmentTree(ISegmentTree<T> operations,int n)
        {
            this.operations = operations;
            size = 1;
            while(size < n)
            {
                size *= 2;
            }
            dat = new T[size * 2];
            e = operations.E();
            for(int i = 1;i <= size * 2 - 1; i++)
            {
                dat[i] = e;
            }
            this.n = n;
        }
        public SegmentTree(ISegmentTree<T> operations, T[] a)
        {
            this.operations = operations;
            n = a.Length;
            size = 1;
            while(size <= n)
            {
                size *= 2;
            }
            dat = new T[size * 2];
            e = operations.E();
            for(int i = size;i <= size * 2 - 1; i++)
            {
                if(i - size <= n - 1)
                {
                    dat[i] = a[i - size];
                }
                else
                {
                    dat[i] = e;
                }
            }
            for(int i = size - 1;i >= 1; i--)
            {
                dat[i] = operations.Op(dat[i * 2], dat[i * 2 + 1]);
            }
        }
        public T this[int index]
        {
            get
            {
                return GetIndex(index);
            }
            set
            {
                Set(index, value);
            }
        }
        public void Set(int index,T value)
        {
            index += size;
            dat[index] = value;
            index /= 2;
            while(index >= 1)
            {
                dat[index] = operations.Op(dat[index * 2], dat[index * 2 + 1]);
                index /= 2;
            }
        }
        //半開区間 [l,r) の総積(演算結果)を取得
        public T Get(int l,int r)
        {
            T sml = e;
            T smr = e;
            l += size;
            r += size;
            while(l < r)
            {
                if(l % 2 == 1)
                {
                    sml = operations.Op(sml, dat[l]);
                    l += 1;
                }
                if(r % 2 == 1)
                {
                    r -= 1;
                    smr = operations.Op(dat[r], smr);
                }
                l /= 2;
                r /= 2;
            }
            return operations.Op(sml, smr);
        }
        public T GetAll()
        {
            return dat[1];
        }
        public T GetIndex(int index)
        {
            return dat[index + size];
        }
        public T[] GetArray()
        {
            return dat;
        }
        //F(Get(l,x)) == true となる最大の x を求める
        public int MaxRight(int l, IBinarySearch<T> func)
        {
            if(l == n)
            {
                return n;
            }
            l += size;
            T sum = e;
            while (true)
            {
                while(l % 2 == 0)
                {
                    l /= 2;
                }
                if (func.F(operations.Op(sum, dat[l])) == false)
                {
                    while(l < size)
                    {
                        l *= 2;
                        if (func.F(operations.Op(sum, dat[l])))
                        {
                            sum = operations.Op(sum, dat[l]);
                            l += 1;
                        }
                    }
                    return l - size;
                }
                sum = operations.Op(sum, dat[l]);
                l += 1;
                if((l & -l) == l)
                {
                    break;
                }
            }
            return n;
        }
        //F(Get(x,r)) == true となる最小の x を求める
        public int MinLeft(int r,IBinarySearch<T> func)
        {
            if(r == 0)
            {
                return 0;
            }
            r += size;
            T sum = e;
            while (true)
            {
                r -= 1;
                while(r > 0 && r % 2 == 1)
                {
                    r /= 2;
                }
                if (func.F(operations.Op(dat[r],sum)) == false)
                {
                    while(r < size)
                    {
                        r = 2 * r + 1;
                        if (func.F(operations.Op(dat[r], sum)))
                        {
                            sum = operations.Op(dat[r], sum);
                            r -= 1;
                        }
                    }
                    return r + 1 - size;
                }
                sum = operations.Op(dat[r], sum);
                if((r & -r) == r)
                {
                    break;
                }
            }
            return 0;
        }
    }
}
