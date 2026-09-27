using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    class Node
    {
        public int Count;
        public Node Child0;
        public Node Child1;
    }
    public class BinaryTrie
    {
        Node root;
        int h;
        int allcount;
        public BinaryTrie()
        {
            h = 63;
            root = new Node();
            allcount = 0;
        }
        public BinaryTrie(int h)
        {
            this.h = h;
            root = new Node();
            allcount = 0;
        }
        public void Add(long x)
        {
            Node now = root;
            for(int i = h - 1;i >= 0; i--)
            {
                now.Count += 1;
                if ((x & (long)1 << i) == 0)
                {
                    if(now.Child0 == null)
                    {
                        Node add = new Node();
                        now.Child0 = add;
                    }
                    now = now.Child0;
                }
                else
                {
                    if(now.Child1 == null)
                    {
                        Node add = new Node();
                        now.Child1 = add;
                    }
                    now = now.Child1;
                }
            }
            now.Count += 1;
            allcount += 1;
        }
        public void Remove(long x)
        {
            Node now = root;
            for (int i = h - 1; i >= 0; i--)
            {
                if ((x & (long)1 << i) == 0)
                {
                    if (now.Child0 == null || now.Child0.Count == 0)
                    {
                        return;
                    }
                    now = now.Child0;
                }
                else
                {
                    if (now.Child1 == null || now.Child1.Count == 0)
                    {
                        return;
                    }
                    now = now.Child1;
                }
            }
            now = root;
            for (int i = h - 1; i >= 0; i--)
            {
                now.Count -= 1;
                if ((x & (long)1 << i) == 0)
                {
                    now = now.Child0;
                }
                else
                {
                    now = now.Child1;
                }
            }
            now.Count -= 1;
            allcount -= 1;
        }
        public void Clear()
        {
            root = new Node();
            allcount = 0;
        }
        public long XorMin(long x)
        {
            if(allcount <= 0)
            {
                throw new Exception();
            }
            long ret = 0;
            Node now = root;
            for(int i = h - 1;i >= 0; i--)
            {
                if(now.Child0 == null || now.Child0.Count == 0)
                {
                    now = now.Child1;
                    ret += (long)1 << i;
                    continue;
                }
                if(now.Child1 == null || now.Child1.Count == 0)
                {
                    now = now.Child0;
                    continue;
                }
                if((x & (long)1 << i) == 0)
                {
                    now = now.Child0;
                }
                else
                {
                    now = now.Child1;
                    ret += (long)1 << i;
                }
            }
            return ret ^ x;
        }
    }
}
