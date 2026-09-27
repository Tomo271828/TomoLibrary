using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class MultiSet<T>
    {
        public int Count;
        Dictionary<T, int> set;
        LinkedList<T> keylist;
        public MultiSet()
        {
            Count = 0;
            set = new Dictionary<T, int>();
            keylist = new LinkedList<T>();
        }
        public void Add(T item)
        {
            if (!set.ContainsKey(item))
            {
                set.Add(item, 0);
                keylist.AddLast(item);
            }
            set[item] += 1;
            Count += 1;
        }
        public void Remove(T item)
        {
            if (set.ContainsKey(item))
            {
                set[item] -= 1;
                Count -= 1;
                if (set[item] == 0)
                {
                    set.Remove(item);
                }
            }
        }
        public int GetCount()
        {
            return Count;
        }
        public int ItemCount(T item)
        {
            if (!set.ContainsKey(item))
            {
                return 0;
            }
            else
            {
                return set[item];
            }
        }
        public void AddLot(T item,int count)
        {
            if (!set.ContainsKey(item))
            {
                set.Add(item, 0);
                keylist.AddLast(item);
            }
            set[item] += count;
            Count += count;
        }
        public void RemoveLot(T item,int count)
        {
            if (set.ContainsKey(item))
            {
                Count -= Math.Min(count, set[item]);
                set[item] -= count;
                if (set[item] <= 0)
                {
                    set.Remove(item);
                }
            }
        }
        public List<T> ToList()
        {
            List<T> ret = new List<T>();
            foreach(T key in set.Keys)
            {
                int count = set[key];
                for(int i = 0;i <= count - 1; i++)
                {
                    ret.Add(key);
                }
            }
            return ret;
        }
        public T GetOne()
        {
            if(Count == 0)
            {
                throw new IndexOutOfRangeException();
            }
            LinkedListNode<T> node = keylist.First;
            while(true)
            {
                T value = node.Value;
                if (!set.ContainsKey(value))
                {
                    keylist.RemoveFirst();
                    node = keylist.First;
                }
                else
                {
                    return value;
                }
            }
        }
        public bool Contains(T item)
        {
            return set.ContainsKey(item);
        }
    }
}
