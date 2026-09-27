using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class SBBST<T,V> where T : IComparable
    {
        public class Node
        {
            public T key;
            public V value;
            public Node LChild;
            public Node RChild;
            public int count;
            public Node(T key,V value)
            {
                this.key = key;
                this.value = value;
                count = 1;
            }
        }
        static Random rnd = new Random();
        public static int Count(Node node)
        {
            if(node == null)
            {
                return 0;
            }
            else
            {
                return node.count;
            }
        }
        static Node Update(Node node)
        {
            node.count = Count(node.LChild) + Count(node.RChild) + 1;
            return node;
        }
        public static Node Merge(Node l,Node r)
        {
            if (l == null || r == null)
            {
                if(l == null)
                {
                    return r;
                }
                else
                {
                    return l;
                }
            }
            if(l.count / (double)(l.count + r.count) > rnd.NextDouble())
            {
                l.RChild = Merge(l.RChild, r);
                return Update(l);
            }
            else
            {
                r.LChild = Merge(l, r.LChild);
                return Update(r);
            }
        }
        public static (Node,Node) Split(Node node,int k)
        {
            if(node == null)
            {
                return (null, null);
            }
            if(k <= Count(node.LChild))
            {
                (Node, Node) s = Split(node.LChild, k);
                node.LChild = s.Item2;
                return (s.Item1, Update(node));
            }
            else
            {
                (Node, Node) s = Split(node.RChild, k - Count(node.LChild) - 1);
                node.RChild = s.Item1;
                return (Update(node), s.Item2);
            }
        }
        public static Node Find(Node node,T key)
        {
            while(node != null)
            {
                int cmp = node.key.CompareTo(key);
                if(cmp > 0)
                {
                    node = node.LChild;
                }
                else if(cmp < 0)
                {
                    node = node.RChild;
                }
                else
                {
                    break;
                }
            }
            return node;
        }
        public static bool Contains(Node node,T key)
        {
            return Find(node, key) != null;
        }
        public static Node FindByIndex(Node node,int index)
        {
            if(node == null)
            {
                return null;
            }
            int currentIndex = Count(node) - Count(node.RChild) - 1;
            while(node != null)
            {
                if(currentIndex == index)
                {
                    return node;
                }
                if(currentIndex > index)
                {
                    node = node.LChild;
                    if(node == null)
                    {
                        currentIndex -= 1;
                    }
                    else
                    {
                        currentIndex -= Count(node.RChild) + 1;
                    }
                }
                else
                {
                    node = node.RChild;
                    if (node == null)
                    {
                        currentIndex += 1;
                    }
                    else
                    {
                        currentIndex += Count(node.LChild) + 1;
                    }
                }
            }
            return null;
        }
        public static int UpperBound(Node node,T key)
        {
            Node nodestatic = node;
            if(node == null)
            {
                return -1;
            }
            int ret = int.MaxValue / 2;
            int index = Count(node) - Count(node.RChild) - 1;
            while (node != null)
            {
                int cmp = node.key.CompareTo(key);
                if(cmp > 0)
                {
                    ret = Math.Min(ret, index);
                    node = node.LChild;
                    if(node == null)
                    {
                        index -= 1;
                    }
                    else
                    {
                        index -= Count(node.RChild) + 1;
                    }
                }
                else
                {
                    node = node.RChild;
                    if(node == null)
                    {
                        index += 1;
                    }
                    else
                    {
                        index += Count(node.LChild) + 1;
                    }
                }
            }
            if(ret == int.MaxValue / 2)
            {
                return Count(nodestatic);
            }
            else
            {
                return ret;
            }
        }
        public static int LowerBound(Node node,T key)
        {
            Node nodestatic = node;
            if(node == null)
            {
                return -1;
            }
            int ret = int.MaxValue / 2;
            int index = Count(node) - Count(node.RChild) - 1;
            while (node != null)
            {
                int cmp = node.key.CompareTo(key);
                if (cmp >= 0)
                {
                    if(cmp == 0)
                    {
                        ret = Math.Min(ret, index);
                    }
                    node = node.LChild;
                    if (node == null)
                    {
                        ret = Math.Min(ret, index);
                    }
                    else
                    {
                        index -= Count(node.RChild) + 1;
                    }
                }
                else
                {
                    node = node.RChild;
                    if (node == null)
                    {
                        index += 1;
                        return index;
                    }
                    else
                    {
                        index += Count(node.LChild) + 1;
                    }
                }
            }
            if (ret == int.MaxValue / 2)
            {
                return Count(nodestatic);
            }
            else
            {
                return ret;
            }
        }
        public static Node RemoveAt(Node node,int k)
        {
            (Node, Node) s1 = Split(node, k);
            (Node, Node) s2 = Split(s1.Item2, 1);
            return Merge(s1.Item1, s2.Item2);
        }
        public static Node Remove(Node node,T key)
        {
            if(Find(node,key) == null)
            {
                return node;
            }
            return RemoveAt(node, LowerBound(node, key));
        }
        public static Node InsertByIndex(Node node,int k,T key,V value)
        {
            (Node, Node) s = Split(node, k);
            return Merge(Merge(s.Item1, new Node(key, value)), s.Item2);
        }
        public static Node Insert(Node node,T key,V value)
        {
            int ub = LowerBound(node, key);
            return InsertByIndex(node, ub, key, value);
        }
        static void Enumerate(Node node,List<T> ret)
        {
            if(node == null)
            {
                return;
            }
            Enumerate(node.LChild, ret);
            ret.Add(node.key);
            Enumerate(node.RChild, ret);
        }
        public static IEnumerable<T> Enumerate(Node node)
        {
            if(node == null)
            {
                yield break;
            }
            foreach(var x in Enumerate(node.LChild))
            {
                yield return x;
            }
            yield return node.key;
            foreach (var x in Enumerate(node.RChild))
            {
                yield return x;
            }
        }
    }
}
