using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class SortedDictionary<T,V> where T : IComparable
    {
        protected SBBST<T, V>.Node root;
        public V this[T key]
        {
            set
            {
                SetValue(key, value);
            }
            get
            {
                return GetValue(key);
            }
        }
        public int Count()
        {
            return SBBST<T, V>.Count(root);
        }
        public virtual void Add(T key,V value)
        {
            if (root == null)
            {
                root = new SBBST<T, V>.Node(key, value);
            }
            else
            {
                if (SBBST<T, V>.Find(root, key) != null)
                {
                    return;
                }
                root = SBBST<T, V>.Insert(root, key, value);
            }
        }
        public void Clear()
        {
            root = null;
        }
        public void Remove(T key)
        {
            root = SBBST<T, V>.Remove(root, key);
        }
        public bool Contains(T key)
        {
            return SBBST<T, V>.Contains(root, key);
        }
        public T KeyAt(int k)
        {
            SBBST<T, V>.Node node = SBBST<T, V>.FindByIndex(root, k);
            if (node == null)
            {
                throw new IndexOutOfRangeException();
            }
            return node.key;
        }
        public V GetValue(T key)
        {
            if(Contains(key) == false)
            {
                throw new IndexOutOfRangeException();
            }
            SBBST<T, V>.Node node = SBBST<T, V>.Find(root, key);
            return node.value;
        }
        public void SetValue(T key,V value)
        {
            if (Contains(key) == false)
            {
                throw new IndexOutOfRangeException();
            }
            SBBST<T, V>.Node node = SBBST<T, V>.Find(root, key);
            node.value = value;
        }
        public int Count(T key)
        {
            return SBBST<T, V>.UpperBound(root, key) - SBBST<T, V>.LowerBound(root, key);
        }
        //探索対象より大きくなる最小Indexを返す
        public int UpperBound(T key)
        {
            return SBBST<T, V>.UpperBound(root, key);
        }
        //探索対象以上になる最小Indexを返す
        public int LowerBound(T key)
        {
            return SBBST<T, V>.LowerBound(root, key);
        }
        //値が探索対象と等しいIndexの範囲(閉区間)を返す
        public (int, int) EqualRange(T key)
        {
            if (Contains(key) == false)
            {
                return (-1, -1);
            }
            return (LowerBound(key), UpperBound(key) - 1);
        }
        public List<T> ToKeyList()
        {
            return new List<T>(SBBST<T, V>.Enumerate(root));
        }
        public List<V> ToValueList()
        {
            List<T> keylist = ToKeyList();
            List<V> ret = new List<V>();
            for(int i = 0;i <= keylist.Count - 1; i++)
            {
                ret.Add(GetValue(keylist[i]));
            }
            return ret;
        }
    }
}
