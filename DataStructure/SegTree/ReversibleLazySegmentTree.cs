using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        struct Node
        {
            public T value;
            public T dat;
            public T revdat;
            public V lazy;
            public int LChild;
            public int RChild;
            public int count;
            public int priority;
            public bool rev;
            public bool hasLazy;
        }
        Node[] nodearr;
        int next;
        int root;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int NewNode(T val)
        {
            int nodeId = next;
            next++;
            if (nodearr.Length <= nodeId)
            {
                Array.Resize(ref nodearr, nodearr.Length * 2);
            }
            ref Node node = ref nodearr[nodeId];
            node.value = val;
            node.dat = val;
            node.revdat = val;
            node.lazy = id;
            node.hasLazy = false;
            node.LChild = 0;
            node.RChild = 0;
            node.count = 1;
            node.rev = false;
            node.priority = rnd.Next();
            return nodeId;
        }
        public ReversibleLazySegmentTree(ILazySegmentTree<T, V> operations, T[] arr, int querycount = 200000)
        {
            this.operations = operations;
            e = operations.E();
            id = operations.Id();
            next = 1;
            root = 0;
            int cap = arr.Length + querycount + 1;
            nodearr = new Node[cap];
            nodearr[0].count = 0;
            nodearr[0].dat = e;
            nodearr[0].revdat = e;
            nodearr[0].lazy = id;
            nodearr[0].hasLazy = false;
            Build(arr);
        }
        public ReversibleLazySegmentTree(ILazySegmentTree<T, V> operations, int n, int querycount = 200000)
        {
            this.operations = operations;
            e = operations.E();
            id = operations.Id();
            next = 1;
            root = 0;
            int cap = n + querycount + 1;
            nodearr = new Node[cap];
            nodearr[0].count = 0;
            nodearr[0].dat = e;
            nodearr[0].revdat = e;
            nodearr[0].lazy = id;
            nodearr[0].hasLazy = false;
            T[] arr = new T[n];
            for (int i = 0; i <= n - 1; i++)
            {
                arr[i] = e;
            }
            Build(arr);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Build(T[] arr)
        {
            int n = arr.Length;
            if (n == 0)
            {
                root = 0;
                return;
            }
            int[] st = new int[n];
            int top = 0;
            for (int i = 0; i < n; i++)
            {
                int cur = NewNode(arr[i]);
                int last = 0;
                while (top > 0 && nodearr[st[top - 1]].priority < nodearr[cur].priority)
                {
                    top--;
                    int x = st[top];
                    Update(x);
                    last = x;
                }
                nodearr[cur].LChild = last;
                if (top > 0)
                {
                    nodearr[st[top - 1]].RChild = cur;
                }
                st[top] = cur;
                top++;
            }
            for (int i = top - 1; i >= 0; i--)
            {
                Update(st[i]);
            }
            root = st[0];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int Update(int id)
        {
            ref Node node = ref nodearr[id];
            ref Node l = ref nodearr[node.LChild];
            ref Node r = ref nodearr[node.RChild];
            node.count = l.count + r.count + 1;
            node.dat = operations.Op(operations.Op(l.dat, node.value), r.dat);
            node.revdat = operations.Op(operations.Op(r.revdat, node.value), l.revdat);
            return id;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ApplyRev(int id)
        {
            if (id == 0)
            {
                return;
            }
            ref Node node = ref nodearr[id];
            (node.LChild, node.RChild) = (node.RChild, node.LChild);
            (node.dat, node.revdat) = (node.revdat, node.dat);
            node.rev = !node.rev;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ApplyLazy(int nodeId, V f)
        {
            if (nodeId == 0)
            {
                return;
            }
            ref Node node = ref nodearr[nodeId];
            node.value = operations.Mapping(f, node.value);
            node.dat = operations.Mapping(f, node.dat);
            node.revdat = operations.Mapping(f, node.revdat);
            if (node.hasLazy)
            {
                node.lazy = operations.Composition(f, node.lazy);
            }
            else
            {
                node.lazy = f;
                node.hasLazy = true;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Push(int nodeId)
        {
            ref Node node = ref nodearr[nodeId];
            if (node.rev)
            {
                ApplyRev(node.RChild);
                ApplyRev(node.LChild);
                node.rev = false;
            }
            if (node.hasLazy)
            {
                ApplyLazy(node.RChild, node.lazy);
                ApplyLazy(node.LChild, node.lazy);
                node.lazy = id;
                node.hasLazy = false;
            }
        }
        int Merge(int l, int r)
        {
            if (l == 0)
            {
                return r;
            }
            if (r == 0)
            {
                return l;
            }
            if (nodearr[l].priority > nodearr[r].priority)
            {
                Push(l);
                ref Node node = ref nodearr[l];
                node.RChild = Merge(node.RChild, r);
                return Update(l);
            }
            else
            {
                Push(r);
                ref Node node = ref nodearr[r];
                node.LChild = Merge(l, node.LChild);
                return Update(r);
            }
        }
        void Split(int id, int k, out int left, out int right)
        {
            if (id == 0)
            {
                left = 0;
                right = 0;
                return;
            }
            ref Node node = ref nodearr[id];
            Push(id);
            ref Node l = ref nodearr[node.LChild];
            if (k <= l.count)
            {
                Split(node.LChild, k, out left, out int t);
                node.LChild = t;
                Update(id);
                right = id;
                return;
            }
            else
            {
                Split(node.RChild, k - l.count - 1, out int t, out right);
                node.RChild = t;
                Update(id);
                left = id;
                return;
            }
        }
        public void Reverse(int l, int r)
        {
            Split(root, l, out int a, out int b);
            Split(b, r - l, out int c, out int d);
            ApplyRev(c);
            root = Merge(a, Merge(c, d));
        }
        public T Get(int l, int r)
        {
            return Get(root, l, r);
        }
        T Get(int id, int l, int r)
        {
            if (l >= r)
            {
                return e;
            }
            ref Node node = ref nodearr[id];
            if (l == 0 && r == node.count)
            {
                return node.dat;
            }
            Push(id);
            int lc = nodearr[node.LChild].count;
            T ret = e;
            bool updated = false;
            if (l < lc)
            {
                ret = Get(node.LChild, l, Math.Min(r, lc));
                updated = true;
            }
            if (l <= lc && lc < r)
            {
                ret = updated ? operations.Op(ret, node.value) : node.value;
                updated = true;
            }
            if (lc + 1 < r)
            {
                ret = updated ? operations.Op(ret, Get(node.RChild, Math.Max(0, l - lc - 1), r - lc - 1)) : Get(node.RChild, Math.Max(0, l - lc - 1), r - lc - 1);
            }
            return ret;
        }
        public void SetIndex(int index, T value)
        {
            root = SetIndex(root, index, value);
        }
        int SetIndex(int id, int index, T val)
        {
            Push(id);
            ref Node node = ref nodearr[id];
            ref Node l = ref nodearr[node.LChild];
            int lc = l.count;
            if (lc > index)
            {
                node.LChild = SetIndex(node.LChild, index, val);
            }
            else if (lc == index)
            {
                node.value = val;
            }
            else
            {
                node.RChild = SetIndex(node.RChild, index - lc - 1, val);
            }
            return Update(id);
        }
        public T GetIndex(int index)
        {
            int id = root;
            while (true)
            {
                Push(id);
                ref Node node = ref nodearr[id];
                ref Node l = ref nodearr[node.LChild];
                int lc = l.count;
                if (lc > index)
                {
                    id = node.LChild;
                }
                else if (lc == index)
                {
                    return node.value;
                }
                else
                {
                    id = node.RChild;
                    index -= lc + 1;
                }
            }
        }
        public void Apply(int l, int r, V f)
        {
            Split(root, l, out int a, out int b);
            Split(b, r - l, out int c, out int d);
            ApplyLazy(c, f);
            root = Merge(a, Merge(c, d));
        }
        //挿入した要素がindex番目(0-indexed)に来るように要素を挿入
        public void InsertAt(int index, T value)
        {
            Split(root, index, out int a, out int b);
            root = Merge(a, Merge(NewNode(value), b));
        }
        //index番目(0-indexed)の要素を削除
        public void RemoveAt(int index)
        {
            root = RemoveAt(root, index);
        }
        int RemoveAt(int id, int index)
        {
            Push(id);
            ref Node node = ref nodearr[id];
            ref Node l = ref nodearr[node.LChild];
            int lc = l.count;
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
            return Update(id);
        }
    }
}
