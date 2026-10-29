using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        //頂点数64以下のみ
        public List<int> MinimumVertexCover()
        {
            if (n > 64)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "頂点数は64以下である必要があります。");
            }
            ulong[] adj = new ulong[n];
            ulong forced = 0;
            foreach (Edge e in allEdge)
            {
                int s = e.start;
                int g = e.goal;
                if (s == g)
                {
                    forced |= 1UL << s;
                }
                else
                {
                    adj[s] |= 1UL << g;
                    adj[g] |= 1UL << s;
                }
            }
            ulong all = ulong.MaxValue;
            if (n != 64)
            {
                all = (1UL << n) - 1;
            }
            int PopCount(ulong mask)
            {
                return BitOperations.PopCount(mask);
            }
            int First(ulong mask)
            {
                return BitOperations.TrailingZeroCount(mask);
            }
            ulong SolveLowDegree(ulong mask)
            {
                ulong ret = 0;
                ulong unseen = mask;
                while (unseen != 0)
                {
                    int root = First(unseen);
                    ulong component = 0;
                    ulong front = 1UL << root;
                    while (front != 0)
                    {
                        int v = First(front);
                        ulong bit = 1UL << v;
                        front &= ~bit;
                        if ((component & bit) != 0)
                        {
                            continue;
                        }
                        component |= bit;
                        front |= adj[v] & mask & ~component;
                    }
                    unseen &= ~component;
                    int start = root;
                    bool cycle = true;
                    ulong scan = component;
                    while (scan != 0)
                    {
                        int v = First(scan);
                        scan &= scan - 1;
                        int deg = PopCount(adj[v] & component);
                        if (deg <= 1)
                        {
                            start = v;
                            cycle = false;
                            break;
                        }
                    }
                    List<int> order = new List<int>();
                    int now = start;
                    int pre = -1;
                    while (true)
                    {
                        order.Add(now);
                        ulong nextmask = adj[now] & component;
                        if (pre != -1)
                        {
                            nextmask &= ~(1UL << pre);
                        }
                        if (nextmask == 0)
                        {
                            break;
                        }
                        int next = First(nextmask);
                        if (next == start)
                        {
                            break;
                        }
                        pre = now;
                        now = next;
                    }
                    int offset = 1;
                    if (cycle && order.Count % 2 == 1)
                    {
                        offset = 0;
                    }
                    for (int i = offset; i < order.Count; i += 2)
                    {
                        ret |= 1UL << order[i];
                    }
                }
                return ret;
            }
            ulong Solve(ulong mask)
            {
                if (mask == 0)
                {
                    return 0;
                }
                int select = -1;
                int maxdeg = 2;
                ulong scan = mask;
                while (scan != 0)
                {
                    int v = First(scan);
                    scan &= scan - 1;
                    int deg = PopCount(adj[v] & mask);
                    if (deg > maxdeg)
                    {
                        maxdeg = deg;
                        select = v;
                    }
                }
                if (select == -1)
                {
                    return SolveLowDegree(mask);
                }
                ulong bit = 1UL << select;
                ulong nei = adj[select] & mask;
                ulong a = Solve(mask & ~bit) | bit;
                ulong b = Solve(mask & ~(bit | nei)) | nei;
                return PopCount(a) < PopCount(b) ? a : b;
            }
            ulong result = forced | Solve(all & ~forced);
            List<int> answer = new List<int>();
            for (int i = 0; i <= n - 1; i++)
            {
                if ((result & (1UL << i)) != 0)
                {
                    answer.Add(i);
                }
            }
            return answer;
        }
        //頂点数64以下のみ
        public List<int> MaximumIndependentSet()
        {
            List<int> mvc = MinimumVertexCover();
            bool[] check = new bool[n];
            for (int i = 0; i <= mvc.Count - 1; i++)
            {
                check[mvc[i]] = true;
            }
            List<int> answer = new List<int>();
            for (int i = 0; i <= n - 1; i++)
            {
                if (check[i] == false)
                {
                    answer.Add(i);
                }
            }
            return answer;
        }
    }
}
