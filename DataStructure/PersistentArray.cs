using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    //時刻は0がスタート
    public class PersistentArray<T>
    {
        class Node
        {
            public Node RChild;
            public Node LChild;
            public T Value;
        }
        List<Node> roots;
        int n;
        int l;
        int time;
        int log;
        public int Length;
        public T this[int t,int index]
        {
            set
            {
                Set(t, index, value);
            }
            get
            {
                return Get(t, index);
            }
        }
        public PersistentArray(int n)
        {
            this.n = n;
            time = 0;
            log = 0;
            while(1 << log < n)
            {
                log += 1;
            }
            l = 1 << log;
            Node[] nodes = new Node[l * 2];
            for(int i = 1; i <= nodes.Length - 1; i++)
            {
                nodes[i] = new Node();
            }
            for(int i = l - 1;i >= 1; i--)
            {
                nodes[i] = new Node();
                nodes[i].RChild = nodes[i * 2];
                nodes[i].LChild = nodes[i * 2 + 1];
            }
            roots = new List<Node>();
            roots.Add(nodes[1]);
            Length = n;
        }
        public PersistentArray(T[] arr)
        {
            n = arr.Length;
            time = 0;
            log = 0;
            while (1 << log < n)
            {
                log += 1;
            }
            l = 1 << log;
            Node[] nodes = new Node[l * 2];
            for (int i = l; i <= l * 2 - 1; i++)
            {
                nodes[i] = new Node();
                if(i < l + n)
                {
                    nodes[i].Value = arr[i - l];
                }
            }
            for (int i = l - 1; i >= 1; i--)
            {
                nodes[i] = new Node();
                nodes[i].RChild = nodes[i * 2];
                nodes[i].LChild = nodes[i * 2 + 1];
            }
            roots = new List<Node>();
            roots.Add(nodes[1]);
        }
        public T Get(int t,int index)
        {
            if(t > time)
            {
                throw new IndexOutOfRangeException();
            }
            if(index >= n || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            Node now = roots[t];
            for(int i = log - 1;i >= 0; i--)
            {
                if((index & 1 << i) == 0)
                {
                    now = now.RChild;
                }
                else
                {
                    now = now.LChild;
                }
            }
            return now.Value;
        }
        public void Set(int t,int index,T value)
        {
            if (t > time)
            {
                throw new IndexOutOfRangeException();
            }
            if (index >= n || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            Node now = new Node();
            Node prev = roots[t];
            roots.Add(now);
            for (int i = log - 1; i >= 0; i--)
            {
                if ((index & 1 << i) == 0)
                {
                    now.LChild = prev.LChild;
                    prev = prev.RChild;
                    Node node = new Node();
                    now.RChild = node;
                    now = node;
                }
                else
                {
                    now.RChild = prev.RChild;
                    prev = prev.LChild;
                    Node node = new Node();
                    now.LChild = node;
                    now = node;
                }
            }
            now.Value = value;
            time += 1;
        }
        public int GetTime()
        {
            return time;
        }
    }
}
