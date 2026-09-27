using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class Deque<T>
    {
        T[] arr;
        int count;
        int capacity;
        int topindex;
        int bottomindex;
        public T this[int index] { get { return ElementAt(index); } }
        public Deque()
        {
            capacity = 65536;
            topindex = 0;
            bottomindex = capacity - 1;
            count = 0;
            arr = new T[capacity];
        }
        void Expansion()
        {
            T[] newarr = new T[capacity * 2];
            for(int i = 0;i <= topindex - 1; i++)
            {
                newarr[i] = arr[i];
            }
            for(int i = bottomindex + 1;i <= capacity - 1; i++)
            {
                newarr[capacity + i] = arr[i];
            }
            bottomindex += capacity;
            capacity *= 2;
            arr = newarr;
        }
        public void PushLast(T item)
        {
            arr[topindex] = item;
            topindex += 1;
            topindex %= capacity;
            if(topindex == bottomindex)
            {
                Expansion();
            }
            count += 1;
        }
        public void PushFirst(T item)
        {
            arr[bottomindex] = item;
            bottomindex += capacity - 1;
            bottomindex %= capacity;
            if(topindex == bottomindex)
            {
                Expansion();
            }
            count += 1;
        }
        public T PopLast()
        {
            if(count == 0)
            {
                throw new ArgumentException("要素が含まれていません!");
            }
            topindex += capacity - 1;
            topindex %= capacity;
            count -= 1;
            return arr[topindex];
        }
        public T PopFirst()
        {
            if (count == 0)
            {
                throw new ArgumentException("要素が含まれていません!");
            }
            bottomindex += 1;
            bottomindex %= capacity;
            count -= 1;
            return arr[bottomindex];
        }
        public T PeekLast()
        {
            if (count == 0)
            {
                throw new ArgumentException("要素が含まれていません!");
            }
            int index = topindex;
            index += capacity - 1;
            index %= capacity;
            return arr[index];
        }
        public T PeekFirst()
        {
            if (count == 0)
            {
                throw new ArgumentException("要素が含まれていません!");
            }
            int index = bottomindex;
            index += 1;
            index %= capacity;
            return arr[index];
        }
        public int GetCount()
        {
            return count;
        }
        public T ElementAt(int index)
        {
            if(index >= count)
            {
                throw new IndexOutOfRangeException();
            }
            index = bottomindex + index + 1;
            index %= capacity;
            return arr[index];
        }
    }
}
