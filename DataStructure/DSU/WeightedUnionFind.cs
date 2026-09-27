using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.DSU
{
    public class WeightedUnionFind
    {
        int[] arr;
        int[] ranks;
        long[] diff_weight;
        int group;
        public WeightedUnionFind(int nodecount)
        {
            arr = new int[nodecount];
            ranks = new int[nodecount];
            diff_weight = new long[nodecount];
            group = nodecount;
            for(int i = 0;i <= nodecount - 1; i++)
            {
                arr[i] = i;
                ranks[i] = 1;
            }
        }
        public int Find(int node)
        {
            if (arr[node] == node)
            {
                return node;
            }
            else
            {
                int r = Find(arr[node]);
                diff_weight[node] += diff_weight[arr[node]];
                arr[node] = r;
                return r;
            }
        }
        public long Weight(int node)
        {
            Find(node);
            return diff_weight[node];
        }
        public long Distance(int nodeA,int nodeB)
        {
            return Weight(nodeA) - Weight(nodeB);
        }
        public void Union(int nodeA,int nodeB,long w)
        {
            long weight = w;
            weight += Weight(nodeA);
            weight -= Weight(nodeB);
            int rootA = Find(nodeA);
            int rootB = Find(nodeB);
            if(rootA != rootB)
            {
                group -= 1;
                if (ranks[rootA] < ranks[rootB])
                {
                    int memo = rootA;
                    rootA = rootB;
                    rootB = memo;
                    weight *= -1;
                }
                if (ranks[rootA] == ranks[rootB])
                {
                    ranks[rootA] += 1;
                }
                arr[rootB] = rootA;
                diff_weight[rootB] = weight;
            }
        }
        public bool IsSame(int nodeA,int nodeB)
        {
            int rootA = Find(nodeA);
            int rootB = Find(nodeB);
            if (rootA == rootB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int CountGroup()
        {
            return group;
        }
        public int[] GetParent()
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                arr[i] = Find(i);
            }
            return arr;
        }
    }
}
