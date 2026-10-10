using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class PersistentLazySegmentTree<T,V>
    {
        struct Node
        {
            public int LChild;
            public int RChild;
            public T value;
            public V lazy;
        }
        class NodePool
        {
            const int SHIFT = 16;
            const int BLOCK_SIZE = 1 << SHIFT;
            const int MASK = BLOCK_SIZE - 1;
            Node[][] blocks = new Node[4][];
            int blockCount = 0;
            public ref Node this[int index]
            {
                get
                {
                    return ref blocks[index >> SHIFT][index & MASK];
                }
            }
            public void Ensure(int index)
            {
                int required = (index >> SHIFT) + 1;
                while (blockCount < required)
                {
                    if (blockCount == blocks.Length)
                    {
                        Array.Resize(ref blocks, blocks.Length * 2);
                    }
                    blocks[blockCount++] = new Node[BLOCK_SIZE];
                }
            }
            public int Capacity => blockCount * BLOCK_SIZE;
        }
        List<int> roots;
        int time;
        int n;
        int log;
        int size;
        ILazySegmentTree<T, V> operations;
        T e;
        V id;
        NodePool nodes;
        int next = 0;
        public PersistentLazySegmentTree(ILazySegmentTree<T, V> operations, int n) : this(operations, new T[0], n)
        {
        }

        public PersistentLazySegmentTree(ILazySegmentTree<T, V> operations, T[] arr) : this(operations, arr, arr.Length)
        {
        }

        private PersistentLazySegmentTree(ILazySegmentTree<T, V> operations, T[] arr, int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }
            this.n = n;
            this.operations = operations;
            e = operations.E();
            id = operations.Id();
            time = 0;
            size = 1;
            log = 0;
            while (size < n)
            {
                size <<= 1;
                log++;
            }
            roots = new List<int>();
            Init(arr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Init(T[] arr)
        {
            nodes = new NodePool();
            next = 0;
            int[] indices = new int[size * 2];
            for (int i = 0; i < size; i++)
            {
                T val = i < arr.Length ? arr[i] : e;
                indices[size + i] = NewNode(val, id, -1, -1);
            }
            for (int i = size - 1; i >= 1; i--)
            {
                int lc = indices[i << 1];
                int rc = indices[i << 1 | 1];
                T val = operations.Op(nodes[lc].value, nodes[rc].value);
                indices[i] = NewNode(val, id, lc, rc);
            }
            roots.Add(indices[1]);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int NewNode(T val, V lazy, int lc, int rc)
        {
            int idx = next;
            next++;
            nodes.Ensure(idx);
            ref Node node = ref nodes[idx];
            node.value = val;
            node.lazy = lazy;
            node.LChild = lc;
            node.RChild = rc;
            return idx;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetTime()
        {
            return time;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int Clone(int idx)
        {
            ref Node node = ref nodes[idx];
            return NewNode(node.value, node.lazy, node.LChild, node.RChild);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int Push(int idx)
        {
            ref Node node = ref nodes[idx];
            if (node.lazy.Equals(id))
            {
                return idx;
            }
            idx = Clone(idx);
            node = ref nodes[idx];
            if(node.LChild != -1)
            {
                int lc = Clone(node.LChild);
                node = ref nodes[idx];
                node.LChild = lc;
                ref Node l = ref nodes[node.LChild];
                l.lazy = operations.Composition(node.lazy, l.lazy);
            }
            if(node.RChild != -1)
            {
                int rc = Clone(node.RChild);
                node = ref nodes[idx];
                node.RChild = rc;
                ref Node r = ref nodes[node.RChild];
                r.lazy = operations.Composition(node.lazy, r.lazy);
            }
            node.value = operations.Mapping(node.lazy, node.value);
            node.lazy = id;
            return idx;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void PushMutable(int idx)
        {
            V f = nodes[idx].lazy;
            if (f.Equals(id))
            {
                return;
            }
            int lc = nodes[idx].LChild;
            int rc = nodes[idx].RChild;
            if (lc != -1)
            {
                lc = Clone(lc);
                nodes[lc].lazy = operations.Composition(f, nodes[lc].lazy);
            }
            if (rc != -1)
            {
                rc = Clone(rc);
                nodes[rc].lazy = operations.Composition(f, nodes[rc].lazy);
            }
            nodes[idx].LChild = lc;
            nodes[idx].RChild = rc;
            nodes[idx].value = operations.Mapping(f, nodes[idx].value);
            nodes[idx].lazy = id;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Pull(int idx)
        {
            int lc = nodes[idx].LChild;
            int rc = nodes[idx].RChild;
            T lv = operations.Mapping(nodes[lc].lazy, nodes[lc].value);
            T rv = operations.Mapping(nodes[rc].lazy, nodes[rc].value);
            nodes[idx].value = operations.Op(lv, rv);
        }
        public void Apply(int t,int l,int r,V lazy)
        {
            time += 1;
            int now = Apply(roots[t], 0, size, l, r, lazy);
            roots.Add(now);
        }
        int Apply(int idx, int a, int b, int l, int r, V lazy)
        {
            if (b <= l || r <= a)
            {
                return idx;
            }
            idx = Clone(idx);
            if (l <= a && b <= r)
            {
                nodes[idx].lazy = operations.Composition(lazy, nodes[idx].lazy);
                return idx;
            }
            PushMutable(idx);
            int center = (a + b) >> 1;
            int lc = Apply(nodes[idx].LChild, a, center, l, r, lazy);
            int rc = Apply(nodes[idx].RChild, center, b, l, r, lazy);
            nodes[idx].LChild = lc;
            nodes[idx].RChild = rc;
            Pull(idx);
            return idx;
        }
        public T Get(int t, int l, int r)
        {
            return Get(roots[t], 0, size, l, r, id);
        }
        T Get(int idx, int a, int b, int l, int r, V lazy)
        {
            if (b <= l || r <= a)
            {
                return e;
            }
            Node node = nodes[idx];
            V f = operations.Composition(lazy, node.lazy);
            if (l <= a && b <= r)
            {
                return operations.Mapping(f, node.value);
            }
            int center = (a + b) >> 1;
            T vl = Get(node.LChild, a, center, l, r, f);
            T vr = Get(node.RChild, center, b, l, r, f);
            return operations.Op(vl, vr);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int AttachLazy(int idx, V lazy)
        {
            if (lazy.Equals(id))
            {
                return idx;
            }
            idx = Clone(idx);
            nodes[idx].lazy = operations.Composition(lazy, nodes[idx].lazy);
            return idx;
        }
        //時刻tをコピーし、時刻sの区間[l,r)をコピーしてきて最新状態にする
        public void Copy(int t, int s, int l, int r)
        {
            time++;
            int now = Copy(roots[t], roots[s], 0, size, l, r, id, id);
            roots.Add(now);
        }

        int Copy(int t, int s, int a, int b, int l, int r, V tlazy, V slazy)
        {
            if (b <= l || r <= a)
            {
                return AttachLazy(t, tlazy);
            }
            if (l <= a && b <= r)
            {
                return AttachLazy(s, slazy);
            }
            V tf = operations.Composition(tlazy, nodes[t].lazy);
            V sf = operations.Composition(slazy, nodes[s].lazy);
            int center = (a + b) >> 1;
            int lc = Copy(nodes[t].LChild, nodes[s].LChild, a, center, l, r, tf, sf);
            int rc = Copy(nodes[t].RChild, nodes[s].RChild, center, b, l, r, tf, sf);
            T lv = operations.Mapping(nodes[lc].lazy, nodes[lc].value);
            T rv = operations.Mapping(nodes[rc].lazy, nodes[rc].value);
            return NewNode(operations.Op(lv, rv), id, lc, rc);
        }
    }
}
