using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.DSU
{
    public class ValueUnionFind<T>
    {
        int group;
        int[] arr;
        int[] ranks;
        int[] connect;
        T[] values;
        IValueUnionFind<T> operations;
        public ValueUnionFind(IValueUnionFind<T> operations, int nodecount)
        {
            group = nodecount;
            arr = new int[nodecount];
            ranks = new int[nodecount];
            connect = new int[nodecount];
            values = new T[nodecount];
            this.operations = operations;
            for (int i = 0; i <= nodecount - 1; i++)
            {
                arr[i] = i;
                ranks[i] = 1;
                connect[i] = 1;
                values[i] = operations.E();
            }
        }
        public ValueUnionFind(IValueUnionFind<T> operations, T[] array)
        {
            int nodecount = array.Length;
            group = nodecount;
            arr = new int[nodecount];
            ranks = new int[nodecount];
            connect = new int[nodecount];
            values = new T[nodecount];
            this.operations = operations;
            for (int i = 0; i <= nodecount - 1; i++)
            {
                arr[i] = i;
                ranks[i] = 1;
                connect[i] = 1;
                values[i] = array[i];
            }
        }
        public int Find(int node)
        {
            if (arr[node] == node)
            {
                return node;
            }
            else
            {
                arr[node] = Find(arr[node]);
                return arr[node];
            }
        }
        public void Union(int nodeA, int nodeB)
        {
            int rootA = Find(nodeA);
            int rootB = Find(nodeB);
            if (rootA != rootB)
            {
                group -= 1;
                if (ranks[rootA] == ranks[rootB])
                {
                    arr[rootB] = rootA;
                    connect[rootA] += connect[rootB];
                    ranks[rootA] += 1;
                    values[rootA] = operations.Op(values[rootA], values[rootB]);
                }
                else if (ranks[rootA] > ranks[rootB])
                {
                    arr[rootB] = rootA;
                    connect[rootA] += connect[rootB];
                    values[rootA] = operations.Op(values[rootA], values[rootB]);
                }
                else
                {
                    arr[rootA] = rootB;
                    connect[rootB] += connect[rootA];
                    values[rootB] = operations.Op(values[rootB], values[rootA]);
                }
            }
        }
        public bool IsSame(int nodeA, int nodeB)
        {
            int rootA = Find(nodeA);
            int rootB = Find(nodeB);
            if (rootA == rootB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int CountGroup()
        {
            return group;
        }
        public int[] GetParent()
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                arr[i] = Find(i);
            }
            return arr;
        }
        public int GetConnectNodeCount(int node)
        {
            int index = Find(node);
            return connect[index];
        }
        public T GetValue(int node)
        {
            int root = Find(node);
            return values[root];
        }
        public void SetValue(int node,T value)
        {
            int root = Find(node);
            values[root] = value;
        }
        public T[] GetAllValues()
        {
            return values;
        }
    }
}
