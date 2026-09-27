using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary;
using TomoLibrary.ARR;

namespace TomoLibrary.Graph
{
    //頂点0を根とする
    public class Tree : UndirectedGraph
    {
        bool lcaCalced;
        bool distCalced;
        public long[] dist;
        public int[] simpledist;
        int[][] parent;
        long[][] edges;
        int log;
        public Tree(int node, long[][] edges) : base(node, false)
        {
            if(edges.Length != node - 1)
            {
                throw new Exception("辺の数が正しくないです");
            }
            for(int i = 0;i <= node - 2; i++)
            {
                if (edges[i].Length == 2)
                {
                    AddEdge((int)edges[i][0], (int)edges[i][1], 1);
                }
                else
                {
                    AddEdge((int)edges[i][0], (int)edges[i][1], edges[i][2]);
                }
            }
            if(uf.GetConnectNodeCount(0) != node)
            {
                throw new Exception("木ではありません");
            }
            this.edges = edges;
            lcaCalced = false;
            distCalced = false;
        }
        public void DistInit()
        {
            distCalced = true;
            dist = Dijkstra(0);
        }
        //(直径,端点1,端点2)を返す
        public (long,int,int) Diameter()
        {
            if(distCalced == false)
            {
                DistInit();
            }
            int a = ArrayOp.MaxIndex(dist);
            long[] d2 = Dijkstra(a);
            int b = ArrayOp.MaxIndex(d2);
            long d = d2[b];
            return (d, a, b);
        }
        public void LCAInit()
        {
            lcaCalced = true;
            log = 1;
            while((1 << log) < n)
            {
                log += 1;
            }
            parent = new int[log][];
            for(int i = 0;i <= log - 1; i++)
            {
                parent[i] = new int[n];
            }
            parent[0][0] = -1;
            simpledist = new int[n];
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((0, -1));
            while(queue.TryDequeue(out (int,int) data))
            {
                int node = data.Item1;
                int previous = data.Item2;
                foreach(Edge e in edgelist[node])
                {
                    int next = e.goal;
                    if(next != previous)
                    {
                        simpledist[next] = simpledist[node] + 1;
                        parent[0][next] = node;
                        queue.Enqueue((next, node));
                    }
                }
            }
            for(int i = 0;i <= log - 2; i++)
            {
                for(int j = 0;j <= n - 1; j++)
                {
                    if (parent[i][j] == -1)
                    {
                        parent[i + 1][j] = -1;
                    }
                    else
                    {
                        parent[i + 1][j] = parent[i][parent[i][j]];
                    }
                }
            }
        }
        public int LCA(int u,int v)
        {
            if(lcaCalced == false)
            {
                LCAInit();
            }
            if (simpledist[u] < simpledist[v])
            {
                (u, v) = (v, u);
            }
            for(int i = 0;i <= log - 1; i++)
            {
                if (((simpledist[u] - simpledist[v]) & (1 << i)) != 0)
                {
                    u = parent[i][u];
                }
            }
            if(u == v)
            {
                return u;
            }
            for(int i = log - 1;i >= 0; i--)
            {
                if (parent[i][u] != parent[i][v])
                {
                    u = parent[i][u];
                    v = parent[i][v];
                }
            }
            return parent[0][u];
        }
        public long TwoPointDist(int u,int v)
        {
            int p = LCA(u, v);
            if (distCalced == false)
            {
                DistInit();
            }
            return dist[u] + dist[v] - dist[p] * 2;
        }
        public int TwoPointSimpleDist(int u,int v)
        {
            int p = LCA(u, v);
            return simpledist[u] + simpledist[v] - simpledist[p] * 2;
        }
        //index, 頂点か否か, 行きがけか否か
        public (int, bool, bool)[] EulerTour()
        {
            (int, bool, bool)[] ret = new (int, bool, bool)[4 * n - 2];
            Dictionary<(int, int), int> edgedict = new Dictionary<(int, int), int>();
            for(int i = 0;i <= n - 2; i++)
            {
                int a = (int)edges[i][0];
                int b = (int)edges[i][1];
                if(a > b)
                {
                    (a, b) = (b, a);
                }
                edgedict.Add((a, b), i);
            }
            int index = 0;
            void DFS(int node,int prev)
            {
                ret[index] = (node, true, true);
                index += 1;
                foreach(Edge e in edgelist[node])
                {
                    int next = e.goal;
                    if(prev != next)
                    {
                        int a = node;
                        int b = next;
                        if(a > b)
                        {
                            (a, b) = (b, a);
                        }
                        ret[index] = (edgedict[(a, b)], false, true);
                        index += 1;
                        DFS(next, node);
                    }
                }
                ret[index] = (node, true, false);
                index += 1;
                if(prev != -1)
                {
                    int a = node;
                    int b = prev;
                    if(a > b)
                    {
                        (a, b) = (b, a);
                    }
                    ret[index] = (edgedict[(a, b)], false, false);
                    index += 1;
                }
            }
            DFS(0, -1);
            return ret;
        }
        //頂点sから頂点tに向かうパスの中で、sからみてd番目の点(0-indexed)を求める
        public int JumpOnTree(int s,int g,int d)
        {
            int x = TwoPointSimpleDist(s, g);
            if(x < d)
            {
                return -1;
            }
            int p = LCA(s, g);
            int pd = simpledist[p];
            int ps = simpledist[s];
            int node = s;
            if(ps - pd < d)
            {
                node = g;
                d = simpledist[g] - simpledist[p] - (d - ps + pd);
            }
            return JumpToRoot(node, d);
        }
        //頂点sから根の方向に向かうパスの中で、sから見てd番目の点(0-indexed)を求める
        public int JumpToRoot(int s,int d)
        {
            if(lcaCalced == false)
            {
                LCAInit();
            }
            if (simpledist[s] < d)
            {
                return -1;
            }
            int i = 0;
            while(d > 0)
            {
                if((d & (1 << i)) != 0)
                {
                    d -= (1 << i);
                    s = parent[i][s];
                }
                i += 1;
            }
            return s;
        }
    }
}
