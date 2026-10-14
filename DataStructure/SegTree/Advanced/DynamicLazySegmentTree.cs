using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.DataStructure.SegTree.Interface;

namespace TomoLibrary.DataStructure.SegTree.Advanced
{
    public class DynamicLazySegmentTree<T,V>
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
        int root;
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
        int NewNode(long len)
        {
            int idx = next;
            next++;
            nodes.Ensure(idx);
            ref Node node = ref nodes[idx];
            node.LChild = -1;
            node.RChild = -1;
            node.value = GetCompE(len);
            node.lazy = id;
            return idx;
        }
        public DynamicLazySegmentTree(ILazySegmentTree<T,V> op, long size) : this(op, size, op.E())
        {
        }
        public DynamicLazySegmentTree(ILazySegmentTree<T, V> op, long size, T initial)
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
            root = NewNode(n);
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

        public void SetIndex(long index,T value)
        {
            if (index < 0 || index >= n) throw new ArgumentOutOfRangeException(nameof(index));
            root = SetIndex(root, 0, n, index, value);
        }
        int SetIndex(int idx, long a, long b, long p, T value)
        {
            if (idx == -1) idx = NewNode(b - a);
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
        public void Apply(long l,long r,V lazy)
        {
            if (l < 0 || l > r || r > n) throw new ArgumentOutOfRangeException();
            if (l == r) return;
            root = Apply(root, 0, n, l, r, lazy);
        }
        int Apply(int idx, long a, long b, long l, long r, V f)
        {
            if (b <= l || r <= a) return idx;
            if (idx == -1) idx = NewNode(b - a);
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
        public T Get(long l,long r)
        {
            if (l < 0 || l > r || r > n) throw new ArgumentOutOfRangeException();
            if (l == r) return e;
            return Get(root, 0, n, l, r);
        }
        T Get(int idx, long a, long b, long l, long r)
        {
            if (b <= l || r <= a) return e;
            if (idx == -1) return GetCompE(Math.Min(b, r) - Math.Max(a, l));
            if (l <= a && b <= r) return nodes[idx].value;
            Push(idx, a, b);
            long center = a + (b - a) / 2;
            T lv = Get(nodes[idx].LChild, a, center, l, r);
            T rv = Get(nodes[idx].RChild, center, b, l, r);
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
        void Push(int idx, long l, long r)
        {
            if (nodes[idx].lazy.Equals(id) || r - l <= 1) return;
            long center = l + (r - l) / 2;
            if (nodes[idx].LChild == -1) nodes[idx].LChild = NewNode(center - l);
            if (nodes[idx].RChild == -1) nodes[idx].RChild = NewNode(r - center);
            ApplyNode(nodes[idx].LChild, nodes[idx].lazy);
            ApplyNode(nodes[idx].RChild, nodes[idx].lazy);
            nodes[idx].lazy = id;
        }
    }
}
