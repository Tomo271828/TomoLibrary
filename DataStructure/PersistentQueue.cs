using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class PersistentQueue<T>
    {
        PersistentArray<T> arr;
        int capacity;
        int time;
        List<int> start;
        List<int> size;
        public PersistentQueue(int capacity)
        {
            time = 0;
            this.capacity = capacity;
            arr = new PersistentArray<T>(capacity);
            start = new List<int>();
            size = new List<int>();
            start.Add(0);
            size.Add(0);
        }
        public void Enqueue(int t,T value)
        {
            arr[t, (start[t] + size[t]) % capacity] = value;
            start.Add(start[t]);
            size.Add(size[t] + 1);
            time += 1;
        }
        public T Dequeue(int t)
        {
            T ret = arr[t, start[t]];
            arr[t, start[t]] = ret;
            start.Add((start[t] + 1) % capacity);
            size.Add(size[t] - 1);
            return ret;
        }
        public T Peek(int t)
        {
            return arr[t, start[t]];
        }
        public T GetAt(int t,int index)
        {
            return arr[t, (start[t] + index) % capacity];
        }
        public int GetTime()
        {
            return time;
        }
    }
}
