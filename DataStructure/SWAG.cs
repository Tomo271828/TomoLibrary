using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public interface ISWAG<T>
    {
        T Op(T a, T b);
        T E();
    }
    public class SWAG<T>
    {
        Stack<T> left;
        Stack<T> leftcum;
        Stack<T> right;
        Stack<T> rightcum;
        ISWAG<T> operation;
        T e;
        public SWAG(ISWAG<T> operation)
        {
            this.operation = operation;
            e = operation.E();
            left = new Stack<T>();
            leftcum = new Stack<T>();
            leftcum.Push(e);
            right = new Stack<T>();
            rightcum = new Stack<T>();
            rightcum.Push(e);
        }
        void Balance(bool leftmode)
        {
            int lc = left.Count;
            int rc = right.Count;
            T[] all = new T[lc + rc];
            for(int i = 0;i <= lc - 1; i++)
            {
                all[i] = left.Pop();
            }
            leftcum = new Stack<T>();
            leftcum.Push(e);
            for(int i = lc + rc - 1;i >= lc; i--)
            {
                all[i] = right.Pop();
            }
            rightcum = new Stack<T>();
            rightcum.Push(e);
            int leftstart = (lc + rc) / 2 - 1;
            if (leftmode)
            {
                leftstart += 1;
            }
            for(int i = leftstart;i >= 0; i--)
            {
                PushFirst(all[i]);
            }
            for(int i = leftstart + 1;i <= lc + rc - 1; i++)
            {
                PushLast(all[i]);
            }
        }
        public int Count()
        {
            return left.Count + right.Count;
        }
        public T Get()
        {
            return operation.Op(leftcum.Peek(), rightcum.Peek());
        }
        public void PushLast(T v)
        {
            right.Push(v);
            rightcum.Push(operation.Op(rightcum.Peek(), v));
        }
        public void PushFirst(T v)
        {
            left.Push(v);
            leftcum.Push(operation.Op(v, leftcum.Peek()));
        }
        public T PopLast()
        {
            if(right.Count == 0)
            {
                Balance(false);
            }
            rightcum.Pop();
            return right.Pop();
        }
        public T PopFirst()
        {
            if(left.Count == 0)
            {
                Balance(true);
            }
            leftcum.Pop();
            return left.Pop();
        }
        public T PeekLast()
        {
            if (right.Count == 0)
            {
                Balance(false);
            }
            return right.Peek();
        }
        public T PeekFirst()
        {
            if (left.Count == 0)
            {
                Balance(true);
            }
            return left.Peek();
        }
    }
}
