using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.Text
{
    //英小文字
    public class Trie<T> where T : IComparable<T>
    {
        T def;
        int strcount;
        int nodecount;
        public List<int[]> graph;
        public List<T> values;
        public List<int> count;
        public Trie(T def)
        {
            strcount = 0;
            nodecount = 1;
            graph = new List<int[]>();
            values = new List<T>();
            count = new List<int>();
            this.def = def;
            graph.Add(new int[26]);
            for(int i = 0;i <= 25; i++)
            {
                graph[0][i] = -1;
            }
            values.Add(def);
            count.Add(0);
        }
        public void SetString(string str,T val)
        {
            char[] c = str.ToCharArray();
            int n = c.Length;
            int node = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                int next = c[i] - 'a';
                if (graph[node][next] != -1)
                {
                    node = graph[node][next];
                }
                else
                {
                    graph[node][next] = nodecount;
                    node = nodecount;
                    graph.Add(new int[26]);
                    for(int j = 0;j <= 25; j++)
                    {
                        graph[node][j] = -1;
                    }
                    values.Add(def);
                    count.Add(0);
                    nodecount += 1;
                }
            }
            count[node] += 1;
            values[node] = val;
            strcount += 1;
        }
        public void SetPathString(string str,T val)
        {
            char[] c = str.ToCharArray();
            int n = c.Length;
            int node = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                int next = c[i] - 'a';
                if (graph[node][next] != -1)
                {
                    node = graph[node][next];
                    values[node] = val;
                }
                else
                {
                    graph[node][next] = nodecount;
                    node = nodecount;
                    graph.Add(new int[26]);
                    for (int j = 0; j <= 25; j++)
                    {
                        graph[node][j] = -1;
                    }
                    values.Add(val);
                    count.Add(0);
                    nodecount += 1;
                }
            }
            count[node] += 1;
            strcount += 1;
        }
        public int GetIndex(string str)
        {
            if(Contains(str) == false)
            {
                return -1;
            }
            char[] c = str.ToCharArray();
            int n = c.Length;
            int node = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                int next = c[i] - 'a';
                node = graph[node][next];
            }
            return node;
        }
        public T GetStringValue(string str)
        {
            if(Contains(str) == false)
            {
                return def;
            }
            char[] c = str.ToCharArray();
            int n = c.Length;
            int node = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                int next = c[i] - 'a';
                node = graph[node][next];
            }
            return values[node];
        }
        public int GetStringCount(string str)
        {
            if (Contains(str) == false)
            {
                return 0;
            }
            char[] c = str.ToCharArray();
            int n = c.Length;
            int node = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                int next = c[i] - 'a';
                node = graph[node][next];
            }
            return count[node];
        }
        public bool Contains(string str)
        {
            char[] c = str.ToCharArray();
            int n = c.Length;
            int node = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                int next = c[i] - 'a';
                if (graph[node][next] != -1)
                {
                    node = graph[node][next];
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public bool FindInPath(string str,T target)
        {
            char[] c = str.ToCharArray();
            int n = c.Length;
            int node = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                int next = c[i] - 'a';
                if (graph[node][next] != -1)
                {
                    node = graph[node][next];
                    if (values[node].CompareTo(target) == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }
        public int GetAllStrCount()
        {
            return strcount;
        }
        public int GetAllNodeCount()
        {
            return nodecount;
        }
    }
}
