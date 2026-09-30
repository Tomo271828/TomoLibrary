using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    //区間反転と区間更新が可換である必要あり
    public class ReversibleLazySegmentTree<T, V>
    {
        static Random rnd = new Random();
        ILazySegmentTree<T, V> operations;
        T e;
        V id;
        Node root;
        class Node
        {
            public T value;
            public T dat;
            public T revdat;
            public V lazy;
            public Node LChild;
            public Node RChild;
            public int count;
            public bool rev;
            public Node(T value, V id)
            {
                this.value = value;
                this.dat = value;
                this.revdat = value;
                this.lazy = id;
                rev = false;
                count = 1;
            }
        }
        public ReversibleLazySegmentTree(ILazySegmentTree<T, V> operations, T[] arr)
        {
            this.operations = operations;
            e = operations.E();
            id = operations.Id();
            root = null;
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                root = Merge(root, new Node(arr[i], id));
            }
        }
        public ReversibleLazySegmentTree(ILazySegmentTree<T, V> operations, int n)
        {
            this.operations = operations;
            e = operations.E();
            id = operations.Id();
            root = null;
            for (int i = 0; i <= n - 1; i++)
            {
                root = Merge(root, new Node(e, id));
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
            if (node == null)
            {
                return;
            }
            if (node.rev)
            {
                ApplyRev(node.RChild);
                ApplyRev(node.LChild);
                node.rev = false;
            }
            ApplyLazy(node.RChild, node.lazy);
            ApplyLazy(node.LChild, node.lazy);
            node.lazy = id;
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
        void ApplyLazy(Node node, V f)
        {
            if (node == null)
            {
                return;
            }
            node.value = operations.Mapping(f, node.value);
            node.dat = operations.Mapping(f, node.dat);
            node.revdat = operations.Mapping(f, node.revdat);
            node.lazy = operations.Composition(f, node.lazy);
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
        public void SetIndex(int index, T value)
        {
            root = SetIndex(root, index, value);
        }
        Node SetIndex(Node node, int index, T value)
        {
            Push(node);
            int lc = Count(node.LChild);
            if (lc > index)
            {
                node.LChild = SetIndex(node.LChild, index, value);
            }
            else if (lc == index)
            {
                node.value = value;
            }
            else
            {
                node.RChild = SetIndex(node.RChild, index - lc - 1, value);
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
        public void Apply(int l, int r, V f)
        {
            (Node, Node) s1 = Split(root, l);
            (Node, Node) s2 = Split(s1.Item2, r - l);
            ApplyLazy(s2.Item1, f);
            root = Merge(s1.Item1, Merge(s2.Item1, s2.Item2));
        }
        //挿入した要素がindex番目(0-indexed)に来るように要素を挿入
        public void InsertAt(int index, T value)
        {
            (Node, Node) s = Split(root, index);
            root = Merge(s.Item1, Merge(new Node(value, id), s.Item2));
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
