using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.DataStructure.SegTree.Interface;

namespace TomoLibrary.DataStructure.SegTree.Advanced
{
    public class PersistentDynamicLazySegmentTree<T, V>
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
        long n;
        NodePool nodes;
        ILazySegmentTree<T, V> operations;
        T e;
        V id;
        List<T> compe;
        T init;
        bool initEqualE;
        Dictionary<long, T> compDict;
        int next;
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
        public PersistentDynamicLazySegmentTree(ILazySegmentTree<T, V> op, long size) : this(op, size, op.E())
        {
        }
        public PersistentDynamicLazySegmentTree(ILazySegmentTree<T, V> op, long size, T initial)
        {
            n = size;
            operations = op;
            e = operations.E();
            if (e.Equals(initial))
            {
                initEqualE = true;
            }
            else
            {
                initEqualE = false;
            }
            compDict = new Dictionary<long, T>();
            id = operations.Id();
            init = initial;
            compe = new List<T>();
            compe.Add(initial);
            MakeCompE();
            nodes = new NodePool();
            next = 0;
            time = 0;
            roots = new List<int>();
            roots.Add(NewNode(GetCompE(n), id, -1, -1));
        }
        public PersistentDynamicLazySegmentTree(ILazySegmentTree<T, V> op, T[] arr)
        {
            long size = 1;
            while (size < arr.Length)
            {
                size <<= 1;
            }
            n = size;
            operations = op;
            e = operations.E();
            initEqualE = true;
            compDict = new Dictionary<long, T>();
            id = operations.Id();
            nodes = new NodePool();
            next = 0;
            time = 0;
            roots = new List<int>();
            Init(arr);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Init(T[] arr)
        {
            int[] indices = new int[(int)n * 2];
            for (int i = 0; i < (int)n; i++)
            {
                T val = i < arr.Length ? arr[i] : e;
                indices[(int)n + i] = NewNode(val, id, -1, -1);
            }
            for (int i = (int)n - 1; i >= 1; i--)
            {
                int lc = indices[i << 1];
                int rc = indices[i << 1 | 1];
                T val = operations.Op(nodes[lc].value, nodes[rc].value);
                indices[i] = NewNode(val, id, lc, rc);
            }
            roots.Add(indices[1]);
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
        void Push(int idx, long l, long r)
        {
            if (nodes[idx].lazy.Equals(id) || r - l <= 1) return;
            V f = nodes[idx].lazy;
            long center = l + (r - l) / 2;
            int lc = nodes[idx].LChild;
            int rc = nodes[idx].RChild;
            if (nodes[idx].LChild == -1)
            {
                lc = NewNode(GetCompE(center - l), id, -1, -1);
            }
            else
            {
                lc = Clone(lc);
            }
            if (nodes[idx].RChild == -1)
            {
                rc = NewNode(GetCompE(r - center), id, -1, -1);
            }
            else
            {
                rc = Clone(rc);
            }
            ApplyNode(lc, f);
            ApplyNode(rc, f);
            nodes[idx].LChild = lc;
            nodes[idx].RChild = rc;
            nodes[idx].lazy = id;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void MakeCompE()
        {
            long len = 1;
            while (len <= n / 2)
            {
                T prev = compe[compe.Count - 1];
                compe.Add(operations.Op(prev, prev));
                len *= 2;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T GetCompE(long len)
        {
            if (initEqualE)
            {
                return e;
            }
            if (compDict.ContainsKey(len))
            {
                return compDict[len];
            }
            long mem = len;
            T ret = e;
            int index = 0;
            while (len > 0)
            {
                if ((len & 1) != 0)
                {
                    ret = operations.Op(ret, compe[index]);
                }
                len >>= 1;
                index++;
            }
            compDict[mem] = ret;
            return ret;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Update(int idx, long a, long b)
        {
            long center = a + (b - a) / 2;
            int li = nodes[idx].LChild;
            int ri = nodes[idx].RChild;
            T lv = li != -1 ? nodes[li].value : GetCompE(center - a);
            T rv = ri != -1 ? nodes[ri].value : GetCompE(b - center);
            nodes[idx].value = operations.Op(lv, rv);
        }

        public void SetIndex(int t, long index, T value)
        {
            if (index < 0 || index >= n) throw new ArgumentOutOfRangeException(nameof(index));
            roots.Add(SetIndex(roots[t], 0, n, index, value));
            time += 1;
        }
        int SetIndex(int idx, long a, long b, long p, T value)
        {
            if (idx == -1) idx = NewNode(GetCompE(b - a), id, -1, -1);
            idx = Clone(idx);
            if (b - a == 1)
            {
                ref Node node = ref nodes[idx];
                node.value = value;
                node.lazy = id;
                return idx;
            }
            Push(idx, a, b);
            long center = a + (b - a) / 2;
            if (p < center)
            {
                int child = SetIndex(nodes[idx].LChild, a, center, p, value);
                nodes[idx].LChild = child;
            }
            else
            {
                int child = SetIndex(nodes[idx].RChild, center, b, p, value);
                nodes[idx].RChild = child;
            }
            Update(idx, a, b);
            return idx;
        }
        public void Apply(int t, long l, long r, V lazy)
        {
            if (l < 0 || l > r || r > n) throw new ArgumentOutOfRangeException();
            if (l == r)
            {
                roots.Add(Clone(roots[t]));
                time += 1;
                return;
            }
            roots.Add(Apply(roots[t], 0, n, l, r, lazy));
            time += 1;
        }
        int Apply(int idx, long a, long b, long l, long r, V f)
        {
            if (b <= l || r <= a) return idx;
            if (idx == -1) idx = NewNode(GetCompE(b - a), id, -1, -1);
            idx = Clone(idx);
            if (l <= a && b <= r)
            {
                ApplyNode(idx, f);
                return idx;
            }
            Push(idx, a, b);
            long center = a + (b - a) / 2;
            if (l < center)
            {
                int child = Apply(nodes[idx].LChild, a, center, l, r, f);
                nodes[idx].LChild = child;
            }
            if (center < r)
            {
                int child = Apply(nodes[idx].RChild, center, b, l, r, f);
                nodes[idx].RChild = child;
            }
            Update(idx, a, b);
            return idx;
        }
        public T Get(int t, long l, long r)
        {
            if (l < 0 || l > r || r > n) throw new ArgumentOutOfRangeException();
            if (l == r) return e;
            return Get(roots[t], 0, n, l, r, id);
        }
        T Get(int idx, long a, long b, long l, long r, V lazy)
        {
            if (b <= l || r <= a) return e;
            if (idx == -1)
            {
                T v = GetCompE(Math.Min(b, r) - Math.Max(a, l));
                return operations.Mapping(lazy, v);
            }
            if (l <= a && b <= r) return operations.Mapping(lazy, nodes[idx].value);
            V nextlazy = operations.Composition(lazy, nodes[idx].lazy);
            long center = a + (b - a) / 2;
            T lv = Get(nodes[idx].LChild, a, center, l, r, nextlazy);
            T rv = Get(nodes[idx].RChild, center, b, l, r, nextlazy);
            return operations.Op(lv, rv);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ApplyNode(int idx, V f)
        {
            ref Node node = ref nodes[idx];
            node.value = operations.Mapping(f, node.value);
            if (!node.lazy.Equals(id))
            {
                node.lazy = operations.Composition(f, node.lazy);
            }
            else
            {
                node.lazy = f;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int AttachLazy(int idx, V lazy)
        {
            if (lazy.Equals(id))
            {
                return idx;
            }
            idx = Clone(idx);
            ApplyNode(idx, lazy);
            return idx;
        }
        //時刻tをコピーし、時刻sの区間[l,r)をコピーしてきて最新状態にする
        public void Copy(int t, int s, long l, long r)
        {
            time++;
            int now = Copy(roots[t], roots[s], 0, n, l, r, id, id);
            roots.Add(now);
        }

        int Copy(int t, int s, long a, long b, long l, long r, V tlazy, V slazy)
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
            long center = a + b >> 1;
            int lc = Copy(nodes[t].LChild, nodes[s].LChild, a, center, l, r, tf, sf);
            int rc = Copy(nodes[t].RChild, nodes[s].RChild, center, b, l, r, tf, sf);
            return NewNode(operations.Op(nodes[lc].value, nodes[rc].value), id, lc, rc);
        }
    }
}
