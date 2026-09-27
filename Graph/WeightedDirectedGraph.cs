using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.Graph
{
    public class WeightedDirectedGraph
    {
        public class Edge
        {
            public int start;
            public int goal;
            public long weight;
            public Edge(int from, int to, long w)
            {
                start = from;
                goal = to;
                weight = w;
            }
        }
        public int n;
        public List<Edge>[] edgelist;
        public List<Edge>[] reverseedgelist;
        public List<Edge> allEdge;
        public WeightedDirectedGraph(int node)
        {
            n = node;
            edgelist = new List<Edge>[node];
            reverseedgelist = new List<Edge>[node];
            for(int i = 0;i <= node - 1; i++)
            {
                edgelist[i] = new List<Edge>();
                reverseedgelist[i] = new List<Edge>();
            }
            allEdge = new List<Edge>();
        }
        public void AddEdge(int from,int to,long w)
        {
            Edge edge = new Edge(from, to, w);
            allEdge.Add(edge);
            edgelist[from].Add(edge);
            reverseedgelist[to].Add(edge);
        }
        public bool[] ReachabilitySimple(int start)
        {
            bool[] ret = new bool[n];
            var queue = new Queue<int>();
            queue.Enqueue(start);
            ret[start] = true;
            while(queue.TryDequeue(out int node))
            {
                foreach(Edge edge in edgelist[node])
                {
                    if (ret[edge.goal] == false)
                    {
                        ret[edge.goal] = true;
                        queue.Enqueue(edge.goal);
                    }
                }
            }
            return ret;
        }
        public bool[] ReachabilitySimpleReverse(int goal)
        {
            bool[] ret = new bool[n];
            var queue = new Queue<int>();
            queue.Enqueue(goal);
            ret[goal] = true;
            while (queue.TryDequeue(out int node))
            {
                foreach (Edge edge in reverseedgelist[node])
                {
                    if (ret[edge.start] == false)
                    {
                        ret[edge.start] = true;
                        queue.Enqueue(edge.start);
                    }
                }
            }
            return ret;
        }
        //閉路をひとつ発見し、閉路上の頂点列を返す
        //存在しない場合はnullを返す
        public List<int> CycleDetection()
        {
            bool[] reached = new bool[n];
            bool[] finished = new bool[n];
            Stack<int> history = new Stack<int>();
            List<int> ret = new List<int>();
            bool find = false;
            void DFS_CycleDetection(int node)
            {
                if (find)
                {
                    return;
                }
                reached[node] = true;
                history.Push(node);
                foreach(var edge in edgelist[node])
                {
                    int next = edge.goal;
                    if (reached[next] == true && finished[next] == false)
                    {
                        find = true;
                        int now = history.Pop();
                        while(now != next)
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
                        DFS_CycleDetection(next);
                        if (find)
                        {
                            return;
                        }
                    }
                }
                finished[node] = true;
                history.Pop();
            }
            for(int i = 0;i <= n - 1; i++)
            {
                if (reached[i] == false)
                {
                    DFS_CycleDetection(i);
                    if (find)
                    {
                        break;
                    }
                }
            }
            if(ret.Count <= 0)
            {
                return null;
            }
            return ret;
        }
        public bool CycleFind()
        {
            bool[] reached = new bool[n];
            bool[] finished = new bool[n];
            bool find = false;
            void DFS_CycleDetection(int node)
            {
                if (find)
                {
                    return;
                }
                reached[node] = true;
                foreach (var edge in edgelist[node])
                {
                    int next = edge.goal;
                    if (reached[next] == true && finished[next] == false)
                    {
                        find = true;
                        return;
                    }
                    if (reached[next] == false)
                    {
                        DFS_CycleDetection(next);
                        if (find)
                        {
                            return;
                        }
                    }
                }
                finished[node] = true;
            }
            for (int i = 0; i <= n - 1; i++)
            {
                if (reached[i] == false)
                {
                    DFS_CycleDetection(i);
                    if (find)
                    {
                        break;
                    }
                }
            }
            return find;
        }
        public long[] Dijkstra(int start)
        {
            long[] dist = new long[n];
            bool[] visited = new bool[n];
            int visitcount = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                dist[i] = long.MaxValue / 2;
                visited[i] = false;
            }
            PriorityQueue<int, long> queue = new PriorityQueue<int, long>();
            queue.Enqueue(start, 0);
            while(queue.TryDequeue(out int node,out long dis))
            {
                if (visited[node])
                {
                    continue;
                }
                visited[node] = true;
                visitcount += 1;
                dist[node] = dis;
                if (visitcount == n)
                {
                    break;
                }
                foreach (var edge in edgelist[node])
                {
                    if (visited[edge.goal] == false)
                    {
                        queue.Enqueue(edge.goal, dis + edge.weight);
                    }
                }
            }
            for(int i = 0;i <= n - 1; i++)
            {
                if (dist[i] >= long.MaxValue / 2)
                {
                    dist[i] = -1;
                }
            }
            return dist;
        }
        //最短経路の距離、実際の最短経路、各頂点までの最短距離のひとつを返す
        //経路が存在しない場合は(-1,null,最短距離)を返す
        public (long, List<int>, long[]) ShortestPath(int start,int goal)
        {
            long[] dist = new long[n];
            bool[] visited = new bool[n];
            int[] prev = new int[n];
            int visitcount = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                dist[i] = long.MaxValue / 2;
                visited[i] = false;
            }
            PriorityQueue<(int,int), long> queue = new PriorityQueue<(int,int), long>();
            queue.Enqueue((start,-1), 0);
            while (queue.TryDequeue(out (int,int) data, out long dis))
            {
                int node = data.Item1;
                int privious = data.Item2;
                if (visited[node])
                {
                    continue;
                }
                visited[node] = true;
                visitcount += 1;
                dist[node] = dis;
                prev[node] = privious;
                if (visitcount == n)
                {
                    break;
                }
                foreach (var edge in edgelist[node])
                {
                    if (visited[edge.goal] == false)
                    {
                        queue.Enqueue((edge.goal,node), dis + edge.weight);
                    }
                }
            }
            for (int i = 0; i <= n - 1; i++)
            {
                if (dist[i] >= long.MaxValue / 2)
                {
                    dist[i] = -1;
                }
            }
            if (dist[goal] == -1)
            {
                return (-1, null, dist);
            }
            if(start == goal)
            {
                return (0, new List<int> { start }, dist);
            }
            List<int> route = new List<int>();
            int now = goal;
            while(now != -1)
            {
                route.Add(now);
                now = prev[now];
            }
            route.Reverse();
            return (dist[goal], route, dist);
        }
        public long[,] WarshallFloyd()
        {
            long[,] dist = new long[n, n];
            for(int i = 0;i <= n - 1; i++)
            {
                for(int j = 0;j <= n - 1; j++)
                {
                    if(i != j)
                    {
                        dist[i, j] = long.MaxValue / 3;
                    }
                }
            }
            foreach(var edge in allEdge)
            {
                dist[edge.start, edge.goal] = Math.Min(dist[edge.start, edge.goal], edge.weight);
            }
            for(int k = 0;k <= n - 1; k++)
            {
                for(int i = 0;i <= n - 1; i++)
                {
                    for(int j = 0;j <= n - 1; j++)
                    {
                        dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);
                    }
                }
            }
            for(int i = 0;i <= n - 1; i++)
            {
                for(int j = 0;j <= n - 1; j++)
                {
                    if (dist[i, j] >= long.MaxValue / 3)
                    {
                        dist[i, j] = -1;
                    }
                }
            }
            return dist;
        }
        //最短距離, 閉路の存在判定, 閉路上の頂点の一部 を順に出力(閉路上の頂点はさらにReachability関数を適切に用いることで判定可能)
        public (long[],bool,HashSet<int>) BellmanFord(int start)
        {
            long[] dist = new long[n];
            bool updated = false;
            for(int i = 0;i <= n - 1; i++)
            {
                dist[i] = long.MaxValue / 10;
            }
            dist[start] = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                updated = false;
                for(int j = 0;j <= allEdge.Count - 1; j++)
                {
                    Edge edge = allEdge[j];
                    if (dist[edge.goal] > dist[edge.start] + edge.weight)
                    {
                        dist[edge.goal] = dist[edge.start] + edge.weight;
                        updated = true;
                    }
                }
                if(updated == false)
                {
                    break;
                }
            }
            var nodelist = new HashSet<int>();
            if (updated)
            {
                for (int i = 0; i <= n - 1; i++)
                {
                    updated = false;
                    for (int j = 0; j <= allEdge.Count - 1; j++)
                    {
                        Edge edge = allEdge[j];
                        if (dist[edge.goal] > dist[edge.start] + edge.weight)
                        {
                            dist[edge.goal] = dist[edge.start] + edge.weight;
                            nodelist.Add(edge.goal);
                            updated = true;
                        }
                    }
                    if (updated == false)
                    {
                        break;
                    }
                }
            }
            return (dist, updated, nodelist);
        }
        public (long[],bool,HashSet<int>) BellmanFordPositive(int start)
        {
            long[] dist = new long[n];
            bool updated = false;
            for (int i = 0; i <= n - 1; i++)
            {
                dist[i] = -1 * long.MaxValue / 10;
            }
            dist[start] = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                updated = false;
                for (int j = 0; j <= allEdge.Count - 1; j++)
                {
                    Edge edge = allEdge[j];
                    if (dist[edge.goal] < dist[edge.start] + edge.weight)
                    {
                        dist[edge.goal] = dist[edge.start] + edge.weight;
                        updated = true;
                    }
                }
                if (updated == false)
                {
                    break;
                }
            }
            var nodelist = new HashSet<int>();
            if (updated)
            {
                for (int i = 0; i <= n - 1; i++)
                {
                    updated = false;
                    for (int j = 0; j <= allEdge.Count - 1; j++)
                    {
                        Edge edge = allEdge[j];
                        if (dist[edge.goal] < dist[edge.start] + edge.weight)
                        {
                            dist[edge.goal] = dist[edge.start] + edge.weight;
                            nodelist.Add(edge.goal);
                            updated = true;
                        }
                    }
                    if (updated == false)
                    {
                        break;
                    }
                }
            }
            return (dist, updated, nodelist);
        }
        public long FordFulkerson(int start,int goal)
        {
            long[,] dist = new long[n, n];
            var set = new HashSet<int>[n];
            for(int i = 0;i <= n - 1; i++)
            {
                set[i] = new HashSet<int>();
            }
            foreach(Edge e in allEdge)
            {
                dist[e.start, e.goal] = Math.Max(dist[e.start, e.goal], e.weight);
                if(e.weight > 0)
                {
                    set[e.start].Add(e.goal);
                }
            }
            long ret = 0;
            while (true)
            {
                bool[] visited = new bool[n];
                int[] from = new int[n];
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(start);
                while(queue.TryDequeue(out int node))
                {
                    foreach(var i in set[node])
                    {
                        if (visited[i])
                        {
                            continue;
                        }
                        if (dist[node, i] <= 0)
                        {
                            continue;
                        }
                        visited[i] = true;
                        queue.Enqueue(i);
                        from[i] = node;
                    }
                }
                if (visited[goal])
                {
                    int last = goal;
                    List<int> l = new List<int>();
                    l.Add(last);
                    while(last != start)
                    {
                        last = from[last];
                        l.Add(last);
                    }
                    long min = long.MaxValue;
                    for(int i = l.Count - 1;i >= 1; i--)
                    {
                        min = Math.Min(min, dist[l[i], l[i - 1]]);
                    }
                    for(int i = l.Count - 1;i >= 1; i--)
                    {
                        dist[l[i], l[i - 1]] -= min;
                        if (dist[l[i], l[i - 1]] <= 0)
                        {
                            set[l[i]].Remove(l[i - 1]);
                        }
                        dist[l[i - 1], l[i]] += min;
                        if (dist[l[i - 1],l[i]] >= 0)
                        {
                            set[l[i - 1]].Add(l[i]);
                        }
                    }
                    ret += min;
                }
                else
                {
                    break;
                }
            }
            return ret;
        }
        public long Dinic(int start, int goal)
        {
            long ret = 0;
            var graph = new List<(Edge, int)>[n];
            for (int i = 0; i <= n - 1; i++)
            {
                graph[i] = new List<(Edge, int)>();
            }
            foreach (Edge e in allEdge)
            {
                graph[e.start].Add((new Edge(e.start, e.goal, e.weight), graph[e.goal].Count));
                graph[e.goal].Add((new Edge(e.goal, e.start, 0), graph[e.start].Count - 1));
            }
            while (true)
            {
                int[] level = new int[n];
                for (int i = 0; i <= n - 1; i++)
                {
                    level[i] = -1;
                }
                level[start] = 0;
                var queue = new Queue<int>();
                queue.Enqueue(start);
                while (queue.TryDequeue(out int node))
                {
                    foreach ((Edge, int) e in graph[node])
                    {
                        int next = e.Item1.goal;
                        if (level[next] == -1 && e.Item1.weight > 0)
                        {
                            level[next] = level[node] + 1;
                            queue.Enqueue(next);
                        }
                    }
                }
                if (level[goal] == -1)
                {
                    break;
                }
                int[] iter = new int[n];
                long DFS(int node, long flow)
                {
                    if (node == goal)
                    {
                        return flow;
                    }
                    for (int i = iter[node]; i <= graph[node].Count - 1; i++)
                    {
                        iter[node] = i;
                        Edge e = graph[node][i].Item1;
                        int next = e.goal;
                        if (level[node] < level[next] && e.weight > 0)
                        {
                            long d = DFS(next, Math.Min(flow, e.weight));
                            if (d > 0)
                            {
                                e.weight -= d;
                                graph[e.goal][graph[node][i].Item2].Item1.weight += d;
                                return d;
                            }
                        }
                    }
                    return 0;
                }
                long f = DFS(start, int.MaxValue);
                while (f > 0)
                {
                    ret += f;
                    f = DFS(start, int.MaxValue);
                }
            }
            return ret;
        }
        //最大フローを流したとき、最大フローの大きさと各辺を流れるフローの大きさの配列を返す(Dinic法)
        public (long,long[]) FlowSize(int start,int goal)
        {
            long ret = 0;
            var graph = new List<(Edge, int)>[n];
            for (int i = 0; i <= n - 1; i++)
            {
                graph[i] = new List<(Edge, int)>();
            }
            foreach (Edge e in allEdge)
            {
                graph[e.start].Add((new Edge(e.start, e.goal, e.weight), graph[e.goal].Count));
                graph[e.goal].Add((new Edge(e.goal, e.start, 0), graph[e.start].Count - 1));
            }
            while (true)
            {
                int[] level = new int[n];
                for (int i = 0; i <= n - 1; i++)
                {
                    level[i] = -1;
                }
                level[start] = 0;
                var queue = new Queue<int>();
                queue.Enqueue(start);
                while (queue.TryDequeue(out int node))
                {
                    foreach ((Edge, int) e in graph[node])
                    {
                        int next = e.Item1.goal;
                        if (level[next] == -1 && e.Item1.weight > 0)
                        {
                            level[next] = level[node] + 1;
                            queue.Enqueue(next);
                        }
                    }
                }
                if (level[goal] == -1)
                {
                    break;
                }
                int[] iter = new int[n];
                long DFS(int node, long flow)
                {
                    if (node == goal)
                    {
                        return flow;
                    }
                    for (int i = iter[node]; i <= graph[node].Count - 1; i++)
                    {
                        iter[node] = i;
                        Edge e = graph[node][i].Item1;
                        int next = e.goal;
                        if (level[node] < level[next] && e.weight > 0)
                        {
                            long d = DFS(next, Math.Min(flow, e.weight));
                            if (d > 0)
                            {
                                e.weight -= d;
                                graph[e.goal][graph[node][i].Item2].Item1.weight += d;
                                return d;
                            }
                        }
                    }
                    return 0;
                }
                long f = DFS(start, int.MaxValue);
                while (f > 0)
                {
                    ret += f;
                    f = DFS(start, int.MaxValue);
                }
            }
            long[] flowsize = new long[allEdge.Count];
            var dict = new Dictionary<(int, int), int>();
            for(int i = 0;i <= allEdge.Count - 1; i++)
            {
                dict.Add((allEdge[i].start, allEdge[i].goal), i);
            }
            for(int i = 0;i <= n - 1; i++)
            {
                foreach((Edge,int) e in graph[i])
                {
                    int s = e.Item1.start;
                    int g = e.Item1.goal;
                    if (dict.ContainsKey((g, s)))
                    {
                        flowsize[dict[(g, s)]] = e.Item1.weight;
                    }
                }
            }
            return (ret, flowsize);
        }
        //始点側のカットに含まれる頂点集合を返す
        public HashSet<int> MinimumCut(int start,int goal)
        {
            long[,] dist = new long[n, n];
            var set = new HashSet<int>[n];
            for (int i = 0; i <= n - 1; i++)
            {
                set[i] = new HashSet<int>();
            }
            foreach (Edge e in allEdge)
            {
                dist[e.start, e.goal] = Math.Max(dist[e.start, e.goal], e.weight);
                if (e.weight > 0)
                {
                    set[e.start].Add(e.goal);
                }
            }
            while (true)
            {
                bool[] visited = new bool[n];
                int[] from = new int[n];
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(start);
                while (queue.TryDequeue(out int node))
                {
                    foreach (var i in set[node])
                    {
                        if (visited[i])
                        {
                            continue;
                        }
                        if (dist[node, i] <= 0)
                        {
                            continue;
                        }
                        visited[i] = true;
                        queue.Enqueue(i);
                        from[i] = node;
                    }
                }
                if (visited[goal])
                {
                    int last = goal;
                    List<int> l = new List<int>();
                    l.Add(last);
                    while (last != start)
                    {
                        last = from[last];
                        l.Add(last);
                    }
                    long min = long.MaxValue;
                    for (int i = l.Count - 1; i >= 1; i--)
                    {
                        min = Math.Min(min, dist[l[i], l[i - 1]]);
                    }
                    for (int i = l.Count - 1; i >= 1; i--)
                    {
                        dist[l[i], l[i - 1]] -= min;
                        if (dist[l[i], l[i - 1]] <= 0)
                        {
                            set[l[i]].Remove(l[i - 1]);
                        }
                        dist[l[i - 1], l[i]] += min;
                        if (dist[l[i - 1], l[i]] >= 0)
                        {
                            set[l[i - 1]].Add(l[i]);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            HashSet<int> ret = new HashSet<int>();
            var bfsqueue = new Queue<int>();
            bfsqueue.Enqueue(start);
            bool[] bfsvisited = new bool[n];
            while(bfsqueue.TryDequeue(out int node))
            {
                ret.Add(node);
                bfsvisited[node] = true;
                for(int i = 0;i <= n - 1; i++)
                {
                    if (bfsvisited[i])
                    {
                        continue;
                    }
                    if (dist[node,i] <= 0)
                    {
                        continue;
                    }
                    bfsqueue.Enqueue(i);
                    bfsvisited[i] = true;
                }
            }
            return ret;
        }
        //グラフを強連結成分分解して各連結成分をトポロジカルソートして返す
        public List<int[]> SCC()
        {
            int[] t = new int[n];
            bool[] reached1 = new bool[n];
            for(int i = 0;i <= n - 1; i++)
            {
                t[i] = -1;
                reached1[i] = false;
            }
            int order = 0;
            void DFS1(int node)
            {
                foreach(Edge e in edgelist[node])
                {
                    int next = e.goal;
                    if (reached1[next])
                    {
                        continue;
                    }
                    reached1[next] = true;
                    DFS1(next);
                }
                t[node] = order;
                order += 1;
            }
            for(int i = 0;i <= n - 1; i++)
            {
                if (t[i] == -1)
                {
                    reached1[i] = true;
                    DFS1(i);
                }
            }
            int[] revorder = new int[n];
            bool[] reached = new bool[n];
            for(int i = 0;i <= n - 1; i++)
            {
                revorder[t[i]] = i;
                reached[i] = false;
            }
            List<int> component = new List<int>();
            void DFS2(int node)
            {
                component.Add(node);
                foreach(Edge e in reverseedgelist[node])
                {
                    int next = e.start;
                    if (reached[next])
                    {
                        continue;
                    }
                    reached[next] = true;
                    DFS2(next);
                }
            }
            List<int[]> ret = new List<int[]>();
            for(int i = 0;i <= n - 1; i++)
            {
                int node = revorder[n - 1 - i];
                if (reached[node])
                {
                    continue;
                }
                reached[node] = true;
                DFS2(node);
                ret.Add(component.ToArray());
                component.Clear();
            }
            return ret;
        }
    }
}
