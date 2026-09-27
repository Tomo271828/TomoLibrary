using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;
using TomoLibrary.DataStructure.DSU;

namespace TomoLibrary.Graph
{
    public class UndirectedGraph : WeightedDirectedGraph
    {
        public struct EdgeWithId
        {
            public Edge edge;
            public int id;
            public EdgeWithId(Edge edge,int id)
            {
                this.edge = edge;
                this.id = id;
            }
        }
        public UnionFind uf;
        List<EdgeWithId>[] edgeswithid;
        int edgecount;
        bool useCycleDetection;
        public UndirectedGraph(int node, bool useCycleDetection = false) : base(node)
        {
            uf = new UnionFind(node);
            this.useCycleDetection = useCycleDetection;
            if (useCycleDetection)
            {
                edgeswithid = new List<EdgeWithId>[node];
                edgecount = 0;
                for (int i = 0; i <= node - 1; i++)
                {
                    edgeswithid[i] = new List<EdgeWithId>();
                }
            }
        }
        public void AddEdge(int from, int to, long w)
        {
            base.AddEdge(from, to, w);
            if(from != to)
            {
                base.AddEdge(to, from, w);
            }
            uf.Union(from, to);
            if (useCycleDetection)
            {
                edgeswithid[from].Add(new EdgeWithId(new Edge(from, to, w), edgecount));
                edgeswithid[to].Add(new EdgeWithId(new Edge(to, from, w), edgecount));
                edgecount += 1;
            }
        }
        public bool Connect(int nodeA,int nodeB)
        {
            return uf.IsSame(nodeA, nodeB);
        }
        //閉路をひとつ発見し、閉路上の頂点列を返す
        //存在しない場合はnullを返す
        //useCycleDetectionがtrueである必要あり
        public List<int> CycleDetection()
        {
            if(useCycleDetection == false)
            {
                throw new Exception("useCycleDetectionがfalseです");
            }
            bool[] reached = new bool[n];
            bool[] finished = new bool[n];
            Stack<int> history = new Stack<int>();
            List<int> ret = new List<int>();
            bool find = false;
            void DFS_CycleDetection(int node, int previous)
            {
                if (find)
                {
                    return;
                }
                reached[node] = true;
                history.Push(node);
                foreach (var data in edgeswithid[node])
                {
                    Edge edge = data.edge;
                    int id = data.id;
                    int next = edge.goal;
                    if(id == previous)
                    {
                        continue;
                    }
                    if (reached[next] == true && finished[next] == false)
                    {
                        find = true;
                        int now = history.Pop();
                        while (now != next)
                        {
                            ret.Add(now);
                            now = history.Pop();
                        }
                        ret.Add(next);
                        ret.Reverse();
                        return;
                    }
                    if (reached[next] == false)
                    {
                        DFS_CycleDetection(next, id);
                        if (find)
                        {
                            return;
                        }
                    }
                }
                finished[node] = true;
                history.Pop();
            }
            for (int i = 0; i <= n - 1; i++)
            {
                if (reached[i] == false)
                {
                    DFS_CycleDetection(i, -1);
                    if (find)
                    {
                        break;
                    }
                }
            }
            if (ret.Count <= 0)
            {
                return null;
            }
            return ret;
        }
        //(MSTの大きさ, MSTに含まれる辺のリスト)を返す
        //存在しない場合は(-1,null)を返す
        public (long,List<(int,int)>) Kruskal()
        {
            if(uf.GetConnectNodeCount(0) != n)
            {
                return (-1, null);
            }
            PriorityQueue<Edge, long> queue = new PriorityQueue<Edge, long>();
            foreach(Edge e in allEdge)
            {
                if(e.start < e.goal)
                {
                    queue.Enqueue(e, e.weight);
                }
            }
            UnionFind mstuf = new UnionFind(n);
            int c = 0;
            long size = 0;
            List<(int, int)> ae = new List<(int, int)>();
            while(queue.TryDequeue(out Edge e,out long weight))
            {
                int a = e.start;
                int b = e.goal;
                if (!mstuf.IsSame(a, b))
                {
                    mstuf.Union(a, b);
                    size += weight;
                    c += 1;
                    ae.Add((a, b));
                    if(c == n - 1)
                    {
                        break;
                    }
                }
            }
            if(c != n - 1)
            {
                return (-1, null);
            }
            return (size, ae);
        }
    }
}
