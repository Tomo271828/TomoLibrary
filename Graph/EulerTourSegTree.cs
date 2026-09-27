using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;
using TomoLibrary.DataStructure.SegTree;

namespace TomoLibrary.Graph
{
    public interface IEulerTourSegTree<T> : ISegmentTree<T>
    {
        public T Inv(T x);
    }
    class RevSegmentTree<T>
    {
        //0 - indexed
        int size;
        int n;
        ISegmentTree<T> operations;
        T e;
        T[] dat;
        public RevSegmentTree(ISegmentTree<T> operations, int n)
        {
            this.operations = operations;
            size = 1;
            while (size < n)
            {
                size *= 2;
            }
            dat = new T[size * 2];
            e = operations.E();
            for (int i = 1; i <= size * 2 - 1; i++)
            {
                dat[i] = e;
            }
            this.n = n;
        }
        public RevSegmentTree(ISegmentTree<T> operations, T[] a)
        {
            this.operations = operations;
            n = a.Length;
            size = 1;
            while (size <= n)
            {
                size *= 2;
            }
            dat = new T[size * 2];
            e = operations.E();
            for (int i = size; i <= size * 2 - 1; i++)
            {
                if (i - size <= n - 1)
                {
                    dat[i] = a[i - size];
                }
                else
                {
                    dat[i] = e;
                }
            }
            for (int i = size - 1; i >= 1; i--)
            {
                dat[i] = operations.Op(dat[i * 2 + 1], dat[i * 2]);
            }
        }
        public void Set(int index, T value)
        {
            index += size;
            dat[index] = value;
            index /= 2;
            while (index >= 1)
            {
                dat[index] = operations.Op(dat[index * 2 + 1], dat[index * 2]);
                index /= 2;
            }
        }
        //半開区間 [l,r) の総積(演算結果)を取得
        public T Get(int l, int r)
        {
            T sml = e;
            T smr = e;
            l += size;
            r += size;
            while (l < r)
            {
                if (l % 2 == 1)
                {
                    sml = operations.Op(dat[l], sml);
                    l += 1;
                }
                if (r % 2 == 1)
                {
                    r -= 1;
                    smr = operations.Op(smr, dat[r]);
                }
                l /= 2;
                r /= 2;
            }
            return operations.Op(smr, sml);
        }
    }
    public class EulerTourSegTree<T>
    {
        Tree tree;
        IEulerTourSegTree<T> operations;
        SegmentTree<T> st;
        RevSegmentTree<T> revst;
        T e;
        T reve;
        int[] nodearr;
        int[] backnodearr;
        int[] edgearr;
        int[] backedgearr;
        public EulerTourSegTree(IEulerTourSegTree<T> operations, int n, int[][] edges, T[] nodevalue, T[] edgevalue)
        {
            long[][] le = new long[edges.Length][];
            for(int i = 0;i <= edges.Length - 1; i++)
            {
                le[i] = new long[3] { (long)edges[i][0], (long)edges[i][1], 1 };
            }
            tree = new Tree(n, le);
            tree.LCAInit();
            this.operations = operations;
            e = operations.E();
            reve = operations.Inv(e);
            (int, bool, bool)[] eulertour = tree.EulerTour();
            nodearr = new int[n];
            backnodearr = new int[n];
            edgearr = new int[n - 1];
            backedgearr = new int[n - 1];
            for(int i = 0;i <= eulertour.Length - 1; i++)
            {
                int index = eulertour[i].Item1;
                bool isNode = eulertour[i].Item2;
                bool dir = eulertour[i].Item3;
                if(isNode)
                {
                    if (dir)
                    {
                        nodearr[index] = i;
                    }
                    else
                    {
                        backnodearr[index] = i;
                    }
                }
                else
                {
                    if (dir)
                    {
                        edgearr[index] = i;
                    }
                    else
                    {
                        backedgearr[index] = i;
                    }
                }
            }
            T[] arr = new T[n * 4 - 2];
            for(int i = 0;i <= n - 1; i++)
            {
                arr[nodearr[i]] = nodevalue[i];
                arr[backnodearr[i]] = operations.Inv(nodevalue[i]);
            }
            for(int i = 0;i <= n - 2; i++)
            {
                arr[edgearr[i]] = edgevalue[i];
                arr[backedgearr[i]] = operations.Inv(edgevalue[i]);
            }
            st = new SegmentTree<T>(operations, arr);
            revst = new RevSegmentTree<T>(operations, arr);
        }
        public void SetNode(int index,T value)
        {
            int i1 = nodearr[index];
            int i2 = backnodearr[index];
            T inv = operations.Inv(value);
            st.Set(i1, value);
            revst.Set(i1, value);
            st.Set(i2, inv);
            revst.Set(i2, inv);
        }
        public void SetEdge(int index, T value)
        {
            int i1 = edgearr[index];
            int i2 = backedgearr[index];
            T inv = operations.Inv(value);
            st.Set(i1, value);
            revst.Set(i1, value);
            st.Set(i2, inv);
            revst.Set(i2, inv);
        }
        //頂点aから頂点bまでのパスの総積を取得
        public T GetPath(int a,int b)
        {
            int lca = tree.LCA(a, b);
            return operations.Op(revst.Get(nodearr[lca] + 1, nodearr[a] + 1), st.Get(nodearr[lca], nodearr[b] + 1));
        }
        //index番目の頂点の値を取得
        public T GetNode(int index)
        {
            return st.GetIndex(nodearr[index]);
        }
        //index番目の辺の値を取得
        public T GetEdge(int index)
        {
            return st.GetIndex(edgearr[index]);
        }
        //xを根とする部分木内の総積を取得
        //Invを零元にする必要あり
        public T GetSubTree(int x)
        {
            return st.Get(nodearr[x], backnodearr[x]);
        }
    }
}
