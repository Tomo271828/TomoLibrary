using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class SortedSet<T> : IEnumerable<T> where T : IComparable
    {
        protected SBBST<T, bool>.Node root;
        public T this[int index] { get { return ElementAt(index); } }
        public int Count()
        {
            return SBBST<T, bool>.Count(root);
        }
        public virtual void Add(T value)
        {
            if(root == null)
            {
                root = new SBBST<T, bool>.Node(value, false);
            }
            else
            {
                if(SBBST<T, bool>.Find(root,value) != null)
                {
                    //Console.WriteLine("Find");
                    return;
                }
                root = SBBST<T, bool>.Insert(root, value, false);
            }
        }
        public void Clear()
        {
            root = null;
        }
        public void Remove(T value)
        {
            root = SBBST<T, bool>.Remove(root, value);
        }
        public bool Contains(T value)
        {
            return SBBST<T, bool>.Contains(root, value);
        }
        public T ElementAt(int k)
        {
            SBBST<T, bool>.Node node = SBBST<T, bool>.FindByIndex(root, k);
            if(node == null)
            {
                throw new IndexOutOfRangeException();
            }
            return node.key;
        }
        public int Count(T value)
        {
            return SBBST<T,bool>.UpperBound(root,value) - SBBST<T,bool>.LowerBound(root,value);
        }
        //探索対象より大きくなる最小Indexを返す
        public int UpperBound(T value)
        {
            return SBBST<T, bool>.UpperBound(root, value);
        }
        //探索対象以上になる最小Indexを返す
        public int LowerBound(T value)
        {
            return SBBST<T, bool>.LowerBound(root, value);
        }
        //値が探索対象と等しいIndexの範囲(閉区間)を返す
        public (int,int) EqualRange(T value)
        {
            if(Contains(value) == false)
            {
                return (-1, -1);
            }
            return (LowerBound(value), UpperBound(value) - 1);
        }
        public List<T> ToList()
        {
            return new List<T>(SBBST<T, bool>.Enumerate(root));
        }
        public IEnumerator<T> GetEnumerator()
        {
            return SBBST<T, bool>.Enumerate(root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
