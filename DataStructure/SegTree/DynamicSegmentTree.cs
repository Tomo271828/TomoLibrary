using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class DynamicSegmentTree<T>
    {
        struct Node
        {
            public int LChild;
            public int RChild;
            public T value;
            public T product;
            public long index;
        }
        long n;
        ISegmentTree<T> operations;
        T e;
        Node[] nodearr;
        int root;
        int next;
        public DynamicSegmentTree(ISegmentTree<T> operations, long size, int nodecount = 65536)
        {
            n = size;
            this.operations = operations;
            e = operations.E();
            root = -1;
            nodearr = new Node[nodecount];
            next = 0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Update(int idx)
        {
            ref Node node = ref nodearr[idx];
            int l = node.LChild;
            int r = node.RChild;
            T ret = node.value;
            if (l != -1)
            {
                ret = operations.Op(nodearr[l].product, ret);
            }
            if (r != -1)
            {
                ret = operations.Op(ret, nodearr[r].product);
            }
            node.product = ret;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int NewNode(long p, T val)
        {
            int id = next;
            next += 1;
            int cap = nodearr.Length;
            if (cap <= id)
            {
                cap *= 2;
                Array.Resize(ref nodearr, cap);
            }
            ref Node node = ref nodearr[id];
            node.value = val;
            node.product = val;
            node.LChild = -1;
            node.RChild = -1;
            node.index = p;
            return id;
        }
        public void Set(long index,T value)
        {
            //root = Set(root, 0, n, index, value);
            if (root == -1)
            {
                root = NewNode(index, value);
                return;
            }
            Span<int> path = stackalloc int[128];
            int depth = 0;
            int idx = root;
            long a = 0;
            long b = n;
            while (true)
            {
                ref Node node = ref nodearr[idx];
                if (node.index == index)
                {
                    node.value = value;
                    Update(idx);
                    break;
                }
                long center = a + ((b - a) >> 1);
                if (index < center)
                {
                    if (node.index < index)
                    {
                        long mi = node.index;
                        node.index = index;
                        index = mi;
                        T mv = node.value;
                        node.value = value;
                        value = mv;
                    }
                    path[depth++] = idx;
                    int child = node.LChild;
                    if (child == -1)
                    {
                        int nid = NewNode(index, value);
                        nodearr[idx].LChild = nid;
                        break;
                    }
                    idx = child;
                    b = center;
                }
                else
                {
                    if (node.index > index)
                    {
                        long mi = node.index;
                        node.index = index;
                        index = mi;
                        T mv = node.value;
                        node.value = value;
                        value = mv;
                    }
                    path[depth++] = idx;
                    int child = node.RChild;
                    if (child == -1)
                    {
                        int nid = NewNode(index, value);
                        nodearr[idx].RChild = nid;
                        break;
                    }
                    idx = child;
                    a = center;
                }
            }
            while (depth > 0)
            {
                Update(path[--depth]);
            }
        }
        /*
        int Set(int idx,long a,long b,long p,T val)
        {
            if(idx == -1)
            {
                return NewNode(p, val);
            }
            long index = nodearr[idx].index;
            if(index == p)
            {
                nodearr[idx].value = val;
                Update(idx);
                return idx;
            }
            long center = a + b >> 1;
            if(p < center)
            {
                if(index < p)
                {
                    long i = nodearr[idx].index;
                    nodearr[idx].index = p;
                    p = i;
                    T v = nodearr[idx].value;
                    nodearr[idx].value = val;
                    val = v;
                }
                nodearr[idx].LChild = Set(nodearr[idx].LChild, a, center, p, val);
            }
            else
            {
                if(p < index)
                {
                    long i = nodearr[idx].index;
                    nodearr[idx].index = p;
                    p = i;
                    T v = nodearr[idx].value;
                    nodearr[idx].value = val;
                    val = v;
                }
                nodearr[idx].RChild = Set(nodearr[idx].RChild, center, b, p, val);
            }
            Update(idx);
            return idx;
        }
        */
        public T GetIndex(long index)
        {
            int idx = root;
            while (idx != -1)
            {
                ref Node node = ref nodearr[idx];
                if (node.index == index)
                {
                    return node.value;
                }
                if (node.index < index)
                {
                    idx = node.LChild;
                }
                else
                {
                    idx = node.RChild;
                }
            }
            return e;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetAll()
        {
            if(root == -1)
            {
                return e;
            }
            else
            {
                return nodearr[root].product;
            }
        }
        //半開区間 [l,r) の総積(演算結果)を取得
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(long l,long r)
        {
            if (root == -1 || r <= l)
            {
                return e;
            }
            if (l == 0 && r == n)
            {
                return GetAll();
            }
            int idx = root;
            while (idx != -1)
            {
                ref Node node = ref nodearr[idx];
                if (node.index < l)
                {
                    idx = node.RChild;
                }
                else if (node.index >= r)
                {
                    idx = node.LChild;
                }
                else
                {
                    break;
                }
            }
            if (idx == -1)
            {
                return e;
            }
            ref Node center = ref nodearr[idx];
            T ret = center.value;
            idx = center.LChild;
            while (idx != -1)
            {
                ref Node node = ref nodearr[idx];
                if (node.index < l)
                {
                    idx = node.RChild;
                }
                else
                {
                    if (node.RChild != -1)
                    {
                        ret = operations.Op(nodearr[node.RChild].product, ret);
                    }
                    ret = operations.Op(node.value, ret);
                    idx = node.LChild;
                }
            }
            idx = center.RChild;
            while (idx != -1)
            {
                ref Node node = ref nodearr[idx];
                if (node.index >= r)
                {
                    idx = node.LChild;
                }
                else
                {
                    if (node.LChild != -1)
                    {
                        ret = operations.Op(ret, nodearr[node.LChild].product);
                    }
                    ret = operations.Op(ret, node.value);
                    idx = node.RChild;
                }
            }
            return ret;
        }
    }
}
