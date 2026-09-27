using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public class PersistentSegmentTree<T>
    {
        //0 - indexed
        class Node
        {
            public Node RChild;
            public Node LChild;
            public T Value;
        }
        List<Node> roots;
        int time;
        int n;
        int log;
        int size;
        ISegmentTree<T> operations;
        T e;
        public PersistentSegmentTree(ISegmentTree<T> operations,int n)
        {
            this.operations = operations;
            e = operations.E();
            time = 0;
            log = 0;
            while (1 << log < n)
            {
                log += 1;
            }
            size = 1 << log;
            Node[] nodes = new Node[size * 2];
            for (int i = size; i <= size * 2 - 1; i++)
            {
                nodes[i] = new Node();
                nodes[i].Value = e;
            }
            for (int i = size - 1; i >= 1; i--)
            {
                nodes[i] = new Node();
                nodes[i].RChild = nodes[i * 2];
                nodes[i].LChild = nodes[i * 2 + 1];
                nodes[i].Value = operations.Op(nodes[i].RChild.Value, nodes[i].LChild.Value);
            }
            roots = new List<Node>();
            roots.Add(nodes[1]);
        }
        public PersistentSegmentTree(ISegmentTree<T> operations, T[] arr)
        {
            this.operations = operations;
            e = operations.E();
            time = 0;
            log = 0;
            n = arr.Length;
            while (1 << log <= n)
            {
                log += 1;
            }
            size = 1 << log;
            Node[] nodes = new Node[size * 2];
            for (int i = size; i <= size * 2 - 1; i++)
            {
                nodes[i] = new Node();
                if (i < size + n)
                {
                    nodes[i].Value = arr[i - size];
                }
                else
                {
                    nodes[i].Value = e;
                }
            }
            for (int i = size - 1; i >= 1; i--)
            {
                nodes[i] = new Node();
                nodes[i].RChild = nodes[i * 2];
                nodes[i].LChild = nodes[i * 2 + 1];
                nodes[i].Value = operations.Op(nodes[i].RChild.Value, nodes[i].LChild.Value);
            }
            roots = new List<Node>();
            roots.Add(nodes[1]);
        }
        T Dat(int t,int index)
        {
            int ll = 0;
            while(1 << ll <= index)
            {
                ll += 1;
            }
            Node now = roots[t];
            for(int i = ll - 2;i >= 0; i--)
            {
                if((index & 1 << i) == 0)
                {
                    now = now.RChild;
                }
                else
                {
                    now = now.LChild;
                }
            }
            return now.Value;
        }
        public int GetTime()
        {
            return time;
        }
        public void Set(int t,int index,T value)
        {
            time += 1;
            index += size;
            Node now = new Node();
            List<Node> path = new List<Node>();
            path.Add(now);
            Node prev = roots[t];
            roots.Add(now);
            for (int i = log - 1; i >= 0; i--)
            {
                if ((index & 1 << i) == 0)
                {
                    now.LChild = prev.LChild;
                    prev = prev.RChild;
                    Node node = new Node();
                    now.RChild = node;
                    now = node;
                }
                else
                {
                    now.RChild = prev.RChild;
                    prev = prev.LChild;
                    Node node = new Node();
                    now.LChild = node;
                    now = node;
                }
                path.Add(now);
            }
            now.Value = value;
            for(int i = path.Count - 2;i >= 0; i--)
            {
                Node node = path[i];
                node.Value = operations.Op(node.RChild.Value,node.LChild.Value);
            }
        }
        //時刻tにおける半開区間 [l,r) の総積(演算結果)を取得
        public T Get(int t, int l, int r)
        {
            T sml = e;
            T smr = e;
            l += size;
            r += size;
            while (l < r)
            {
                if (l % 2 == 1)
                {
                    sml = operations.Op(sml, Dat(t,l));
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    smr = operations.Op(Dat(t, r), smr);
                }
                l /= 2;
                r /= 2;
            }
            return operations.Op(sml, smr);
        }
        public T GetAll(int t)
        {
            return roots[t].Value;
        }
        public T GetIndex(int t,int index)
        {
            return Dat(t, index + size);
        }
        //時刻tにおいてF(Get(l,x)) == true となる最大の x を求める
        public int MaxRight(int t, int l, IBinarySearch<T> func)
        {
            if (l == n)
            {
                return n;
            }
            l += size;
            T sum = e;
            while (true)
            {
                while (l % 2 == 0)
                {
                    l /= 2;
                }
                if (func.F(operations.Op(sum, Dat(t, l))) == false)
                {
                    while (l < size)
                    {
                        l *= 2;
                        if (func.F(operations.Op(sum, Dat(t, l))))
                        {
                            sum = operations.Op(sum, Dat(t, l));
                            l += 1;
                        }
                    }
                    return l - size;
                }
                sum = operations.Op(sum, Dat(t, l));
                l += 1;
                if ((l & -l) == l)
                {
                    break;
                }
            }
            return n;
        }
        //時刻tにおいてF(Get(x,r)) == true となる最小の x を求める
        public int MinLeft(int t, int r, IBinarySearch<T> func)
        {
            if (r == 0)
            {
                return 0;
            }
            r += size;
            T sum = e;
            while (true)
            {
                r -= 1;
                while (r > 0 && r % 2 == 1)
                {
                    r /= 2;
                }
                if (func.F(operations.Op(Dat(t, r), sum)) == false)
                {
                    while (r < size)
                    {
                        r = 2 * r + 1;
                        if (func.F(operations.Op(Dat(t, r), sum)))
                        {
                            sum = operations.Op(Dat(t, r), sum);
                            r -= 1;
                        }
                    }
                    return r + 1 - size;
                }
                sum = operations.Op(Dat(t, r), sum);
                if ((r & -r) == r)
                {
                    break;
                }
            }
            return 0;
        }
    }
}
