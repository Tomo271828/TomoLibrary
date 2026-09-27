using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class DynamicLazySegmentTree<T,V>
    {
        class Node
        {
            public Node LChild;
            public Node RChild;
            public T value;
            public V lazy;
        }
        Node root = null;
        long n;
        ILazySegmentTree<T, V> operations;
        T e;
        V id;
        List<T> compe;
        public DynamicLazySegmentTree(ILazySegmentTree<T,V> op,long size)
        {
            n = size;
            operations = op;
            e = operations.E();
            id = operations.Id();
            root = new Node();
            root.value = e;
            root.lazy = id;
            compe = new List<T>();
            compe.Add(e);
            MakeCompE();
        }
        void MakeCompE()
        {
            long ll = 2;
            int index = 0;
            while(ll <= n)
            {
                compe.Add(operations.Op(compe[index], compe[index]));
                index += 1;
                ll *= 2;
            }
        }
        T GetCompE(long len)
        {
            T ret = e;
            for(int i = 0;i <= 64; i++)
            {
                if((len & (long)1 << i) != 0)
                {
                    len -= (long)1 << i;
                    ret = compe[i];
                    break;
                }
            }
            int index = 0;
            while(len > 0)
            {
                if ((len & (long)1 << index) != 0)
                {
                    len -= (long)1 << index;
                    ret = operations.Op(ret, compe[index]);
                }
                index += 1;
            }
            return ret;
        }
        public void SetIndex(long index,T value)
        {
            SetIndex(root, 0, n, index, value);
        }
        Node SetIndex(Node node,long a,long b,long p,T value)
        {
            Eval(node, a, b);
            if(node == null)
            {
                node = new Node();
                node.value = e;
                node.lazy = id;
            }
            if(b - a == 1)
            {
                node.value = value;
                return node;
            }
            long center = (a + b) / 2;
            if(p < center)
            {
                node.LChild = SetIndex(node.LChild, a, center, p, value);
            }
            else
            {
                node.RChild = SetIndex(node.RChild, center, b, p, value);
            }
            T l = GetCompE(center - a);
            T r = GetCompE(b - center);
            if(node.LChild != null)
            {
                l = node.LChild.value;
            }
            if(node.RChild != null)
            {
                r = node.RChild.value;
            }
            node.value = operations.Op(l, r);
            return node;
        }
        void Eval(Node node,long l,long r)
        {
            if(node != null)
            {
                node.value = operations.Mapping(node.lazy, node.value);
                if (r - l > 1)
                {
                    long center = (r + l) / 2;
                    if(node.LChild == null)
                    {
                        node.LChild = new Node();
                        node.LChild.value = GetCompE(center - l);
                        node.LChild.lazy = id;
                    }
                    if(node.RChild == null)
                    {
                        node.RChild = new Node();
                        node.RChild.value = GetCompE(r - center);
                        node.RChild.lazy = id;
                    }
                    node.LChild.lazy = operations.Composition(node.lazy, node.LChild.lazy);
                    node.RChild.lazy = operations.Composition(node.lazy, node.RChild.lazy);
                }
                node.lazy = id;
            }
        }
        public void Apply(long l,long r,V lazy)
        {
            Apply(root, 0, n, l, r, lazy);
        }
        void Apply(Node node,long a,long b,long l,long r,V lazy)
        {
            Eval(node, a, b);
            if(b <= l || r <= a)
            {
                return;
            }
            if(l <= a && b <= r)
            {
                node.lazy = operations.Composition(lazy, node.lazy);
                Eval(node, a, b);
            }
            else
            {
                long center = (a + b) / 2;
                if (node.LChild == null)
                {
                    node.LChild = new Node();
                    node.LChild.value = GetCompE(center - a);
                    node.LChild.lazy = id;
                }
                if(node.RChild == null)
                {
                    node.RChild = new Node();
                    node.RChild.value = GetCompE(b - center);
                    node.RChild.lazy = id;
                }
                Apply(node.LChild, a, center, l, r, lazy);
                Apply(node.RChild, center, b, l, r, lazy);
                node.value = operations.Op(node.LChild.value, node.RChild.value);
            }
        }
        public T Get(long l,long r)
        {
            return Get(root, 0, n, l, r);
        }
        T Get(Node node,long a,long b,long l,long r)
        {
            Eval(node, a, b);
            if (l <= a && b <= r)
            {
                return node.value;
            }
            else
            {
                long center = (a + b) / 2;
                if(node.LChild == null)
                {
                    node.LChild = new Node();
                    node.LChild.value = GetCompE(center - a);
                    node.LChild.lazy = id;
                }
                if (node.RChild == null)
                {
                    node.RChild = new Node();
                    node.RChild.value = GetCompE(b - center);
                    node.RChild.lazy = id;
                }
                if(center <= l || r <= a)
                {
                    return Get(node.RChild, center, b, l, r);
                }
                else if(b <= l || r <= center)
                {
                    return Get(node.LChild, a, center, l, r);
                }
                else
                {
                    T v1 = Get(node.LChild, a, center, l, r);
                    T v2 = Get(node.RChild, center, b, l, r);
                    return operations.Op(v1, v2);
                }
            }
        }
    }
}
