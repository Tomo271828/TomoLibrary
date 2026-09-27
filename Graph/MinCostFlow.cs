using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.Graph
{
    public class MinCostFlow
    {
        class Edge
        {
            public int start;
            public int goal;
            public long cost;
            public long capacity;
            public Edge reverse;
            public Edge(int start, int goal, long cost, long capacity)
            {
                this.start = start;
                this.goal = goal;
                this.cost = cost;
                this.capacity = capacity;
            }
        }
        int n;
        List<Edge>[] edgelist;
        List<Edge> alledge;
        public MinCostFlow(int nodecount)
        {
            n = nodecount;
            edgelist = new List<Edge>[nodecount];
            for(int i = 0;i <= nodecount - 1; i++)
            {
                edgelist[i] = new List<Edge>();
            }
            alledge = new List<Edge>();
        }
        public void AddEdge(int from,int to,long cost,long capacity)
        {
            Edge edge1 = new Edge(from, to, cost, capacity);
            Edge edge2 = new Edge(to, from, -cost, 0);
            edgelist[from].Add(edge1);
            edgelist[to].Add(edge2);
            alledge.Add(edge1);
            alledge.Add(edge2);
            edge1.reverse = edge2;
            edge2.reverse = edge1;
        }
        public long CalcMaxFlow(int start,int goal,long flow)
        {
            long ret = 0;
            long f = flow;
            while (true)
            {
                FlowCost result = BellmanFordMaxFlow(start, goal, f);
                if(result == null)
                {
                    break;
                }
                f -= result.flow;
                ret += result.cost;
                if(f <= 0)
                {
                    break;
                }
            }
            if(f == 0)
            {
                return ret;
            }
            else
            {
                //流すことに失敗
                return -1;
            }
        }
        FlowCost BellmanFordMaxFlow(int start,int goal,long maxflow)
        {
            Edge[] froms = new Edge[n];
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
                for(int j = 0;j <= n - 1; j++)
                {
                    long olddist = dist[j];
                    foreach(Edge edge in edgelist[j])
                    {
                        if(edge.capacity <= 0)
                        {
                            continue;
                        }
                        if (dist[edge.goal] > edge.cost + olddist)
                        {
                            dist[edge.goal] = edge.cost + olddist;
                            froms[edge.goal] = edge;
                            updated = true;
                        }
                    }
                }
                if(updated == false)
                {
                    break;
                }
            }
            if (updated)
            {
                //負閉路
                return null;
            }
            if (dist[goal] < long.MaxValue / 10)
            {
                List<Edge> edges = new List<Edge>();
                Edge last = froms[goal];
                long flow = maxflow;
                flow = Math.Min(flow, last.capacity);
                edges.Add(last);
                while (true)
                {
                    last = froms[last.start];
                    if(last == null)
                    {
                        break;
                    }
                    edges.Add(last);
                    flow = Math.Min(flow, last.capacity);
                }
                long cost = 0;
                foreach(Edge edge in edges)
                {
                    edge.capacity -= flow;
                    edge.reverse.capacity += flow;
                    cost += edge.cost * flow;
                }
                FlowCost ret = new FlowCost(flow, cost);
                return ret;
            }
            else
            {
                return null;
            }
        }
        class FlowCost
        {
            public long flow;
            public long cost;
            public FlowCost(long flow,long cost)
            {
                this.flow = flow;
                this.cost = cost;
            }
        }
    }
}
