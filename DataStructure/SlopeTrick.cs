using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class SlopeTrick
    {
        long min;
        long inf = 1000000000000000001;
        PriorityQueue<long, long> lqueue;
        PriorityQueue<long, long> rqueue;
        public SlopeTrick(long value = 0)
        {
            min = value;
            lqueue = new PriorityQueue<long, long>();
            rqueue = new PriorityQueue<long, long>();
            lqueue.Enqueue(-inf, inf);
            rqueue.Enqueue(inf, inf);
        }
        //定数関数 f(x) = a を加算
        public void AddConst(long a)
        {
            min += a;
        }
        //関数 f(x) = max(0, x - a) を加算
        //右上がりな関数
        public void AddRight(long a)
        {
            lqueue.Enqueue(a, -a);
            long pop = lqueue.Dequeue();
            rqueue.Enqueue(pop, pop);
            min += Math.Max(0, pop - a);
        }
        //関数 f(x) = max(0, a - x) を加算
        //左上がりな関数
        public void AddLeft(long a)
        {
            rqueue.Enqueue(a, a);
            long pop = rqueue.Dequeue();
            lqueue.Enqueue(pop, -pop);
            min += Math.Max(0, a - pop);
        }
        //関数 f(x) = |x - a| を加算
        public void AddAbsolute(long a)
        {
            AddRight(a);
            AddLeft(a);
        }
        //f(x) <- min_{y <= x} f(y) に置き換える
        //左側の累積min
        //右側を平坦にする
        public void ClearRight()
        {
            rqueue.Clear();
            rqueue.Enqueue(inf, inf);
        }
        //f(x) <- min_{x <= y} f(y) に置き換える
        //右側の累積min
        //左側を平坦にする
        public void ClearLeft()
        {
            lqueue.Clear();
            lqueue.Enqueue(-inf, inf);
        }
        //最小値を返す
        public long Min()
        {
            return min;
        }
        //最小値をとる中で最小の値
        public long MinLeft()
        {
            return lqueue.Peek();
        }
        //最小値をとる中で最大の値
        public long MinRight()
        {
            return rqueue.Peek();
        }
    }
}
