using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class Deque<T>
    {
        T[] arr;
        int head;
        int count;
        int mask;
        int capacity;
        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)count)
                {
                    throw new IndexOutOfRangeException();
                }
                return arr[(head + index) & mask];
            }
        }
        public int Count { get { return count; } }
        public Deque(int capacity = 65536)
        {
            capacity = (int)BitOperations.RoundUpToPowerOf2((uint)capacity);
            this.capacity = capacity;
            head = 0;
            count = 0;
            mask = capacity - 1;
            arr = new T[capacity];
        }
        void Expansion()
        {
            T[] newarr = new T[capacity * 2];
            for (int i = 0; i <= count - 1; i++)
            {
                newarr[i] = arr[(head + i) & mask];
            }
            head = 0;
            capacity <<= 1;
            mask = capacity - 1;
            arr = newarr;
        }
        public void PushLast(T item)
        {
            if (count == capacity)
            {
                Expansion();
            }
            arr[(head + count) & mask] = item;
            count++;
        }
        public void PushFirst(T item)
        {
            if (count == capacity)
            {
                Expansion();
            }
            head = (head - 1) & mask;
            arr[head] = item;
            count++;
        }
        public T PopLast()
        {
            if(count == 0)
            {
                throw new ArgumentException("要素が含まれていません!");
            }
            count--;
            return arr[(head + count) & mask];
        }
        public T PopFirst()
        {
            if (count == 0)
            {
                throw new ArgumentException("要素が含まれていません!");
            }
            count--;
            head++;
            return arr[(head - 1) & mask];
        }
        public T PeekLast()
        {
            if (count == 0)
            {
                throw new ArgumentException("要素が含まれていません!");
            }
            return arr[(head + count - 1) & mask];
        }
        public T PeekFirst()
        {
            if (count == 0)
            {
                throw new ArgumentException("要素が含まれていません!");
            }
            return arr[head & mask];
        }
        public T ElementAt(int index)
        {
            if((uint)index >= (uint)count)
            {
                throw new IndexOutOfRangeException();
            }
            return arr[(head + index) & mask];
        }
    }
}
