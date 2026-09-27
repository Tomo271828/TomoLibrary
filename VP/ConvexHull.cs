using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.VP
{
    struct Point<T> : IComparable<Point<T>> where T : INumber<T>
    {
        public T x;
        public T y;
        public int index;
        public Point(T x,T y,int index)
        {
            this.x = x;
            this.y = y;
            this.index = index;
        }
        public int CompareTo(Point<T> other)
        {
            int compare = x.CompareTo(other.x);
            if(compare != 0)
            {
                return compare;
            }
            compare = y.CompareTo(other.y);
            if(compare != 0)
            {
                return compare;
            }
            return index.CompareTo(other.index);
        }
    }
    //座標が整数の凸包を管理
    public class ConvexHull<T> where T : INumber<T>
    {
        List<Point<T>> sortedPoint;
        List<Point<T>> points;
        Point<T>[] hull;
        int n;
        int k;
        int lowSize;
        public ConvexHull(T[][] points)
        {
            n = points.Length;
            this.points = new List<Point<T>>();
            for(int i = 0;i <= n - 1; i++)
            {
                Point<T> point = new Point<T>(points[i][0], points[i][1], i);
                this.points.Add(point);
            }
            MonotoneChain();
        }
        T CrossProduct(Point<T> a,Point<T> b,Point<T> c)
        {
            return (b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y);
        }
        bool IsSame(Point<T> a,Point<T> b)
        {
            return a.x == b.x && a.y == b.y;
        }
        void MonotoneChain()
        {
            points.Sort();
            sortedPoint = new List<Point<T>>();
            for(int i = 0;i <= n - 1; i++)
            {
                if(sortedPoint.Count > 0 && IsSame(sortedPoint[sortedPoint.Count - 1], points[i]))
                {
                    continue;
                }
                sortedPoint.Add(points[i]);
            }
            n = sortedPoint.Count;
            hull = new Point<T>[n + 1];
            if(n == 0)
            {
                k = 0;
                return;
            }
            if(n == 1)
            {
                hull[0] = sortedPoint[0];
                k = 1;
                return;
            }
            sortedPoint.Sort();
            if (n == 2)
            {
                if (IsSame(sortedPoint[0], sortedPoint[1]))
                {
                    hull[0] = sortedPoint[0];
                    k = 1;
                    return;
                }
                hull[0] = sortedPoint[0];
                hull[1] = sortedPoint[1];
                k = 2;
                return;
            }
            k = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                while(k >= 2 && CrossProduct(hull[k - 2], hull[k - 1], sortedPoint[i]) <= T.Zero)
                {
                    k -= 1;
                }
                hull[k] = sortedPoint[i];
                k += 1;
            }
            lowSize = k;
            for(int i = n - 2;i >= 0; i--)
            {
                while(k > lowSize && CrossProduct(hull[k - 2], hull[k - 1], sortedPoint[i]) <= T.Zero)
                {
                    k -= 1;
                }
                hull[k] = sortedPoint[i];
                k += 1;
            }
            k -= 1;
        }
        public int[] HullIndexArray()
        {
            int[] ret = new int[k];
            for(int i = 0;i <= k - 1; i++)
            {
                ret[i] = hull[i].index;
            }
            return ret;
        }
        public T[][] HullPosArray()
        {
            T[][] ret = new T[k][];
            for(int i = 0;i <= k - 1; i++)
            {
                ret[i] = new T[2] { hull[i].x, hull[i].y };
            }
            return ret;
        }
        public int[] LowerHullIndexArray()
        {
            int[] ret = new int[lowSize];
            for(int i = 0;i <= lowSize - 1; i++)
            {
                ret[i] = hull[i].index;
            }
            return ret;
        }
        public T[][] LowerHullPosArray()
        {
            T[][] ret = new T[lowSize][];
            for (int i = 0; i <= lowSize - 1; i++)
            {
                ret[i] = new T[2] { hull[i].x, hull[i].y };
            }
            return ret;
        }
        public int[] UpperHullIndexArray()
        {
            int[] ret = new int[k - lowSize];
            for(int i = lowSize;i <= k - 1; i++)
            {
                ret[i - lowSize] = hull[i].index;
            }
            return ret;
        }
        public T[][] UpperHullPosArray()
        {
            T[][] ret = new T[k - lowSize][];
            for(int i = lowSize;i <= k - 1; i++)
            {
                ret[i - lowSize] = new T[2] { hull[i].x, hull[i].y };
            }
            return ret;
        }
    }
}
