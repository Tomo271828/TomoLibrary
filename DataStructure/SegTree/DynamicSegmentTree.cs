using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class DynamicSegmentTree<T>
    {
        class Node
        {
            public Node LChild;
            public Node RChild;
            public T value;
            public T product;
            public long index;
        }
        long n;
        ISegmentTree<T> operations;
        T e;
        Node root = null;
        public DynamicSegmentTree(ISegmentTree<T> operations, long size)
        {
            n = size;
            this.operations = operations;
            e = operations.E();
        }
        void Update(Node node)
        {
            T ret = e;
            if (node.LChild != null)
            {
                ret = operations.Op(ret, node.LChild.product);
            }
            ret = operations.Op(ret, node.value);
            if (node.RChild != null)
            {
                ret = operations.Op(ret, node.RChild.product);
            }
            node.product = ret;
        }
        public void Set(long index,T value)
        {
            root = Set(root, 0, n, index, value);
        }
        Node Set(Node node,long a,long b,long p,T val)
        {
            if(node == null)
            {
                node = new Node();
                node.value = val;
                node.index = p;
                node.product = val;
                return node;
            }
            long index = node.index;
            if(index == p)
            {
                node.value = val;
                Update(node);
                return node;
            }
            long center = a + b >> 1;
            if(p < center)
            {
                if(index < p)
                {
                    long i = node.index;
                    node.index = p;
                    p = i;
                    T v = node.value;
                    node.value = val;
                    val = v;
                }
                node.LChild = Set(node.LChild, a, center, p, val);
            }
            else
            {
                if(p < index)
                {
                    long i = node.index;
                    node.index = p;
                    p = i;
                    T v = node.value;
                    node.value = val;
                    val = v;
                }
                node.RChild = Set(node.RChild, center, b, p, val);
            }
            Update(node);
            return node;
        }
        public T GetIndex(long index)
        {
            return GetIndex(root, 0, n, index);
        }
        T GetIndex(Node node,long a,long b,long p)
        {
            if (node == null)
            {
                return e;
            }
            if(node.index == p)
            {
                return node.value;
            }
            long center = a + b >> 1;
            if(p < center)
            {
                return GetIndex(node.LChild, a, center, p);
            }
            else
            {
                return GetIndex(node.RChild, center, b, p);
            }
        }
        public T GetAll()
        {
            if(root == null)
            {
                return e;
            }
            else
            {
                return root.product;
            }
        }
        //半開区間 [l,r) の総積(演算結果)を取得
        public T Get(long l,long r)
        {
            return Get(root, 0, n, l, r);
        }
        T Get(Node node,long a,long b,long l,long r)
        {
            if(node == null || b <= l || r <= a)
            {
                return e;
            }
            if(l <= a && b <= r)
            {
                return node.product;
            }
            long center = a + b >> 1;
            long index = node.index;
            T ret = Get(node.LChild, a, center, l, r);
            if(l <= index && index < r)
            {
                ret = operations.Op(ret, node.value);
            }
            ret = operations.Op(ret, Get(node.RChild, center, b, l, r));
            return ret;
        }
    }
}
