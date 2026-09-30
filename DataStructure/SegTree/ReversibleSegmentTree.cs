using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;

namespace TomoLibrary.DataStructure.SegTree
{
    public class ReversibleSegmentTree<T>
    {
        class Node
        {
            public T value;
            public T dat;
            public T revdat;
            public Node LChild;
            public Node RChild;
            public int count;
            public bool rev;
            public Node(T value)
            {
                this.value = value;
                this.dat = value;
                this.revdat = value;
                rev = false;
                count = 1;
            }
        }
        static Random rnd = new Random();
        ISegmentTree<T> operations;
        T e;
        Node root;
        public ReversibleSegmentTree(ISegmentTree<T> operations, T[] arr)
        {
            this.operations = operations;
            e = operations.E();
            root = null;
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                root = Merge(root, new Node(arr[i]));
            }
        }
        public ReversibleSegmentTree(ISegmentTree<T> operations, int n)
        {
            this.operations = operations;
            e = operations.E();
            root = null;
            for (int i = 0; i <= n - 1; i++)
            {
                root = Merge(root, new Node(e));
            }
        }
        int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            else
            {
                return node.count;
            }
        }
        T Dat(Node node)
        {
            if (node == null)
            {
                return e;
            }
            else
            {
                return node.dat;
            }
        }
        T RevDat(Node node)
        {
            if (node == null)
            {
                return e;
            }
            else
            {
                return node.revdat;
            }
        }
        Node Update(Node node)
        {
            node.count = Count(node.LChild) + Count(node.RChild) + 1;
            node.dat = operations.Op(operations.Op(Dat(node.LChild), node.value), Dat(node.RChild));
            node.revdat = operations.Op(operations.Op(RevDat(node.RChild), node.value), RevDat(node.LChild));
            return node;
        }
        void ApplyRev(Node node)
        {
            if (node == null)
            {
                return;
            }
            (node.LChild, node.RChild) = (node.RChild, node.LChild);
            (node.dat, node.revdat) = (node.revdat, node.dat);
            node.rev = !node.rev;
        }
        void Push(Node node)
        {
            if (node == null || !node.rev)
            {
                return;
            }
            ApplyRev(node.RChild);
            ApplyRev(node.LChild);
            node.rev = false;
        }
        Node Merge(Node l, Node r)
        {
            if (l == null || r == null)
            {
                if (l == null)
                {
                    return r;
                }
                else
                {
                    return l;
                }
            }
            Push(l);
            Push(r);
            if (l.count / (double)(l.count + r.count) > rnd.NextDouble())
            {
                l.RChild = Merge(l.RChild, r);
                return Update(l);
            }
            else
            {
                r.LChild = Merge(l, r.LChild);
                return Update(r);
            }
        }
        (Node, Node) Split(Node node, int k)
        {
            if (node == null)
            {
                return (null, null);
            }
            Push(node);
            if (k <= Count(node.LChild))
            {
                (Node, Node) s = Split(node.LChild, k);
                node.LChild = s.Item2;
                return (s.Item1, Update(node));
            }
            else
            {
                (Node, Node) s = Split(node.RChild, k - Count(node.LChild) - 1);
                node.RChild = s.Item1;
                return (Update(node), s.Item2);
            }
        }
        public void Reverse(int l, int r)
        {
            (Node, Node) s1 = Split(root, l);
            (Node, Node) s2 = Split(s1.Item2, r - l);
            ApplyRev(s2.Item1);
            root = Merge(s1.Item1, Merge(s2.Item1, s2.Item2));
        }
        public T Get(int l, int r)
        {
            (Node, Node) s1 = Split(root, l);
            (Node, Node) s2 = Split(s1.Item2, r - l);
            T ret = Dat(s2.Item1);
            root = Merge(s1.Item1, Merge(s2.Item1, s2.Item2));
            return ret;
        }
        public void Set(int index, T value)
        {
            root = Set(root, index, value);
        }
        Node Set(Node node, int index, T value)
        {
            Push(node);
            int lc = Count(node.LChild);
            if (lc > index)
            {
                node.LChild = Set(node.LChild, index, value);
            }
            else if (lc == index)
            {
                node.value = value;
            }
            else
            {
                node.RChild = Set(node.RChild, index - lc - 1, value);
            }
            return Update(node);
        }
        public T GetIndex(int index)
        {
            Node node = root;
            while (true)
            {
                Push(node);
                int lc = Count(node.LChild);
                if (lc > index)
                {
                    node = node.LChild;
                }
                else if (lc == index)
                {
                    return node.value;
                }
                else
                {
                    node = node.RChild;
                    index -= lc + 1;
                }
            }
        }
        //挿入した要素がindex番目(0-indexed)に来るように要素を挿入
        public void InsertAt(int index, T value)
        {
            (Node, Node) s = Split(root, index);
            root = Merge(s.Item1, Merge(new Node(value), s.Item2));
        }
        //index番目(0-indexed)の要素を削除
        public void RemoveAt(int index)
        {
            root = RemoveAt(root, index);
        }
        Node RemoveAt(Node node, int index)
        {
            Push(node);
            int lc = Count(node.LChild);
            if (lc > index)
            {
                node.LChild = RemoveAt(node.LChild, index);
            }
            else if (lc == index)
            {
                return Merge(node.LChild, node.RChild);
            }
            else
            {
                node.RChild = RemoveAt(node.RChild, index - lc - 1);
            }
            return Update(node);
        }
    }
}
