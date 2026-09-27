using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.DSU
{
    public class RollbackUnionFind
    {
        int n;
        int[] arr;
        int[] edgecount;
        Stack<(int, int, int)> history;
        int snap = -1;
        public RollbackUnionFind(int nodecount)
        {
            arr = new int[nodecount];
            edgecount = new int[nodecount];
            n = nodecount;
            for(int i = 0;i <= nodecount - 1; i++)
            {
                arr[i] = -1;
                edgecount[i] = 0;
            }
            history = new Stack<(int, int, int)>();
        }
        public int Find(int node)
        {
            int k = node;
            while (arr[k] >= 0)
            {
                k = arr[k];
            }
            return k;
        }
        public void Union(int nodeA, int nodeB)
        {
            int a = Find(nodeA);
            int b = Find(nodeB);
            if(a == b)
            {
                history.Push((a, arr[a], edgecount[a]));
                history.Push((a, arr[a], edgecount[a]));
                edgecount[a] += 1;
                return;
            }
            if(a > b)
            {
                int memo = a;
                a = b;
                b = memo;
            }
            history.Push((a, arr[a], edgecount[a]));
            history.Push((b, arr[b], edgecount[b]));
            arr[a] += arr[b];
            arr[b] = a;
            edgecount[a] += edgecount[b];
            edgecount[a] += 1;
        }
        public bool IsSame(int nodeA, int nodeB)
        {
            return Find(nodeA) == Find(nodeB);
        }
        public void Undo()
        {
            for(int i = 0;i <= 1; i++)
            {
                (int, int, int) h = history.Pop();
                arr[h.Item1] = h.Item2;
                edgecount[h.Item1] = h.Item3;
            }
        }
        public void Snapshot()
        {
            snap = history.Count / 2;
        }
        public void Rollback(int state = -1)
        {
            if(state == -1)
            {
                state = snap;
            }
            state *= 2;
            while(state < history.Count)
            {
                Undo();
            }
        }
        public int GetConnectedNodeCount(int node)
        {
            return -arr[Find(node)];
        }
        public int GetEdgeCount(int node)
        {
            return edgecount[Find(node)];
        }
    }
}
