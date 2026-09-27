using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class PersistentLazySegmentTree<T,V>
    {
        class Node
        {
            public Node LChild;
            public Node RChild;
            public T value;
            public V lazy;
            public int time;
        }
        List<Node> roots;
        int time;
        int n;
        int log;
        int size;
        ILazySegmentTree<T, V> operations;
        T e;
        V id;
        public PersistentLazySegmentTree(ILazySegmentTree<T,V> operations,int n)
        {
            this.n = n;
            this.operations = operations;
            e = operations.E();
            id = operations.Id();
            time = 0;
            size = 1;
            log = 0;
            while(size < n)
            {
                size *= 2;
                log += 1;
            }
            roots = new List<Node>();
            Node[] nodearr = new Node[size * 2];
            for(int i = 0;i <= size - 1; i++)
            {
                nodearr[i + size] = new Node();
                nodearr[i + size].value = e;
                nodearr[i + size].lazy = id;
                nodearr[i + size].time = 0;
            }
            for(int i = size - 1;i >= 1; i--)
            {
                nodearr[i] = new Node();
                nodearr[i].value = operations.Op(nodearr[i * 2].value, nodearr[i * 2 + 1].value);
                nodearr[i].lazy = id;
                nodearr[i].LChild = nodearr[i * 2];
                nodearr[i].RChild = nodearr[i * 2 + 1];
                nodearr[i].time = 0;
            }
            roots.Add(nodearr[1]);
        }
        public PersistentLazySegmentTree(ILazySegmentTree<T,V> operations, T[] arr)
        {
            n = arr.Length;
            this.operations = operations;
            e = operations.E();
            id = operations.Id();
            time = 0;
            size = 1;
            log = 0;
            while (size < n)
            {
                size *= 2;
                log += 1;
            }
            roots = new List<Node>();
            Node[] nodearr = new Node[size * 2];
            for (int i = 0; i <= size - 1; i++)
            {
                nodearr[i + size] = new Node();
                if(i <= n - 1)
                {
                    nodearr[i + size].value = arr[i];
                }
                else
                {
                    nodearr[i + size].value = e;
                }
                nodearr[i + size].lazy = id;
                nodearr[i + size].time = 0;
            }
            for (int i = size - 1; i >= 1; i--)
            {
                nodearr[i] = new Node();
                nodearr[i].value = operations.Op(nodearr[i * 2].value, nodearr[i * 2 + 1].value);
                nodearr[i].lazy = id;
                nodearr[i].LChild = nodearr[i * 2];
                nodearr[i].RChild = nodearr[i * 2 + 1];
                nodearr[i].time = 0;
            }
            roots.Add(nodearr[1]);
        }
        public int GetTime()
        {
            return time;
        }
        Node Clone(Node node)
        {
            Node ret = new Node();
            ret.time = node.time;
            ret.value = node.value;
            ret.lazy = node.lazy;
            ret.LChild = node.LChild;
            ret.RChild = node.RChild;
            return ret;
        }
        Node Push(Node node)
        {
            if (node.lazy.Equals(id))
            {
                return node;
            }
            node = Clone(node);
            if(node.LChild != null)
            {
                node.LChild = Clone(node.LChild);
                node.LChild.lazy = operations.Composition(node.lazy, node.LChild.lazy);
            }
            if(node.RChild != null)
            {
                node.RChild = Clone(node.RChild);
                node.RChild.lazy = operations.Composition(node.lazy, node.RChild.lazy);
            }
            node.value = operations.Mapping(node.lazy, node.value);
            node.lazy = id;
            return node;
        }
        void Pull(Node node)
        {
            T ret = e;
            if(node.LChild != null)
            {
                ret = operations.Op(ret, node.LChild.value);
            }
            if(node.RChild != null)
            {
                ret = operations.Op(ret, node.RChild.value);
            }
            node.value = ret;
        }
        public void Apply(int t,int l,int r,V lazy)
        {
            time += 1;
            Node now = Apply(roots[t], 0, size, l, r, lazy);
            roots.Add(now);
        }
        Node Apply(Node node,int a,int b,int l,int r,V lazy)
        {
            node = Push(node);
            if(b <= l || r <= a)
            {
                return node;
            }
            node = Clone(node);
            if(l <= a && b <= r)
            {
                node.lazy = lazy;
                return Push(node);
            }
            if(b - a > 1)
            {
                int center = (a + b) / 2;
                node.LChild = Apply(node.LChild, a, center, l, r, lazy);
                node.RChild = Apply(node.RChild, center, b, l, r, lazy);
                Pull(node);
            }
            return node;
        }
        public T Get(int t,int l,int r)
        {
            return Get(roots[t], 0, size, l, r);
        }
        T Get(Node node,int a,int b,int l,int r)
        {
            node = Push(node);
            if(b <= l || r <= a)
            {
                return e;
            }
            if(l <= a && b <= r)
            {
                return node.value;
            }
            int center = (a + b) / 2;
            T vl = Get(node.LChild, a, center, l, r);
            T vr = Get(node.RChild, center, b, l, r);
            return operations.Op(vl, vr);
        }
        //時刻tをコピーし、時刻sの区間[l,r)をコピーしてきて最新状態にする
        public void Copy(int t,int s,int l,int r)
        {
            time += 1;
            Node now = Copy(roots[t], roots[s], 0, size, l, r);
            roots.Add(now);
        }
        Node Copy(Node t,Node s,int a,int b,int l,int r)
        {
            t = Push(t);
            s = Push(s);
            if(b <= l || r <= a)
            {
                return t;
            }
            if(l <= a && b <= r)
            {
                return s;
            }
            Node node = new Node();
            node.lazy = id;
            int center = (a + b) / 2;
            node.LChild = Copy(t.LChild, s.LChild, a, center, l, r);
            node.RChild = Copy(t.RChild, s.RChild, center, b, l, r);
            node.LChild = Push(node.LChild);
            node.RChild = Push(node.RChild);
            Pull(node);
            return node;
        }
    }
}
