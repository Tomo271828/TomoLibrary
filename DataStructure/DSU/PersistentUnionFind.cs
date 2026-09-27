using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.DataStructure;

namespace TomoLibrary.DataStructure.DSU
{
    public class PersistentUnionFind
    {
        int time;
        List<int> group;
        int n;
        PersistentArray<int> arr;
        PersistentArray<int> ranks;
        PersistentArray<int> connect;
        public PersistentUnionFind(int n)
        {
            group = new List<int>();
            group.Add(n);
            time = 0;
            int[] a = new int[n];
            int[] r = new int[n];
            int[] c = new int[n];
            for(int i = 0;i <= n - 1; i++)
            {
                a[i] = i;
                r[i] = 1;
                c[i] = 1;
            }
            arr = new PersistentArray<int>(a);
            ranks = new PersistentArray<int>(r);
            connect = new PersistentArray<int>(c);
            this.n = n;
        }
        public int Find(int t,int node)
        {
            if (arr[t,node] == node)
            {
                return node;
            }
            else
            {
                return Find(t, arr[t, node]);
            }
        }
        //根が異なっていたかどうかにかかわらず時刻が1経過
        public void Union(int t,int nodeA,int nodeB)
        {
            int rootA = Find(t, nodeA);
            int rootB = Find(t, nodeB);
            int g = group[t];
            if(rootA != rootB)
            {
                group.Add(g - 1);
                if (ranks[t,rootA] == ranks[t, rootB])
                {
                    arr[t, rootB] = rootA;
                    connect[t, rootA] = connect[t, rootA] + connect[t, rootB];
                    ranks[t, rootA] = ranks[t, rootA] + 1;
                }
                else if(ranks[t, rootA] > ranks[t, rootB])
                {
                    arr[t, rootB] = rootA;
                    connect[t, rootA] = connect[t, rootA] + connect[t, rootB];
                    ranks[t, rootA] = ranks[t, rootA];
                }
                else
                {
                    arr[t, rootA] = rootB;
                    connect[t, rootB] = connect[t, rootA] + connect[t, rootB];
                    ranks[t, rootA] = ranks[t, rootA];
                }
            }
            else
            {
                group.Add(g);
                arr[t, rootA] = arr[t, rootA];
                connect[t, rootA] = connect[t, rootA];
                ranks[t, rootA] = ranks[t, rootA];
            }
            time += 1;
        }
        public bool IsSame(int t,int nodeA,int nodeB)
        {
            return Find(t, nodeA) == Find(t, nodeB);
        }
        public int CountGroup(int t)
        {
            return group[t];
        }
        public int[] GetParent(int t)
        {
            int[] ret = new int[n];
            for(int i = 0;i <= n - 1; i++)
            {
                ret[i] = Find(t, i);
            }
            return ret;
        }
        public int GetConnectNodeCount(int t, int node)
        {
            return connect[t, Find(t, node)];
        }
        public int GetTime()
        {
            return time;
        }
    }
}
