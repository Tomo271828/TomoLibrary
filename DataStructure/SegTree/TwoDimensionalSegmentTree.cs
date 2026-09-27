using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    //0-index
    //演算の順序は保証しない
    public class TwoDimensionalSegmentTree<T>
    {
        T[,] dat;
        T e;
        int sizeh;
        int sizew;
        ISegmentTree<T> operations;
        public TwoDimensionalSegmentTree(ISegmentTree<T> operations,int h,int w)
        {
            sizeh = 1;
            sizew = 1;
            while(sizeh < h)
            {
                sizeh += 1;
            }
            while(sizew < w)
            {
                sizew += 1;
            }
            dat = new T[sizeh * 2, sizew * 2];
            this.operations = operations;
            e = operations.E();
            for(int i = sizeh;i <= sizeh * 2 - 1; i++)
            {
                for(int j = sizew;j <= sizew * 2 - 1; j++)
                {
                    dat[i, j] = e;
                }
            }
            Build();
        }
        public TwoDimensionalSegmentTree(ISegmentTree<T> operations, T[,] map)
        {
            int h = map.GetLength(0);
            int w = map.GetLength(1);
            sizeh = 1;
            sizew = 1;
            while (sizeh < h)
            {
                sizeh += 1;
            }
            while (sizew < w)
            {
                sizew += 1;
            }
            dat = new T[sizeh * 2, sizew * 2];
            this.operations = operations;
            e = operations.E();
            for (int i = sizeh; i <= sizeh * 2 - 1; i++)
            {
                for (int j = sizew; j <= sizew * 2 - 1; j++)
                {
                    if(i - sizeh <= h - 1 && j - sizew <= w - 1)
                    {
                        dat[i, j] = map[i - sizeh, j - sizew];
                    }
                    else
                    {
                        dat[i, j] = e;
                    }
                }
            }
            Build();
        }
        void Build()
        {
            for(int w = sizew;w <= sizew * 2 - 1; w++)
            {
                for(int h = sizeh - 1;h >= 1; h--)
                {
                    dat[h, w] = operations.Op(dat[h * 2, w], dat[h * 2 + 1, w]);
                }
            }
            for(int h = 0;h <= sizeh * 2 - 1; h++)
            {
                for(int w = sizew - 1;w >= 1; w--)
                {
                    dat[h, w] = operations.Op(dat[h, w * 2], dat[h, w * 2 + 1]);
                }
            }
        }
        public void Set(int h,int w,T value)
        {
            h += sizeh;
            w += sizew;
            dat[h, w] = value;
            for(int i = h / 2;i >= 1;i /= 2)
            {
                dat[i, w] = operations.Op(dat[i * 2, w], dat[i * 2 + 1, w]);
            }
            for(;h >= 1;h /= 2)
            {
                for(int j = w / 2;j >= 1;j /= 2)
                {
                    dat[h, j] = operations.Op(dat[h, j * 2], dat[h, j * 2 + 1]);
                }
            }
        }
        //(h1,w1)を左下角, (h2-1,w2-1)を右上角とする長方形領域の総演算結果を取得
        //h1 < h2 && w1 < w2
        public T Get(int h1,int w1,int h2,int w2)
        {
            HashSet<int> hi = new HashSet<int>();
            int l = h1 + sizeh;
            int r = h2 + sizeh;
            while(l < r)
            {
                if(l % 2 == 1)
                {
                    hi.Add(l);
                    l += 1;
                }
                if(r % 2 == 1)
                {
                    r -= 1;
                    hi.Add(r);
                }
                l /= 2;
                r /= 2;
            }
            l = w1 + sizew;
            r = w2 + sizew;
            T ret = e;
            while(l < r)
            {
                if(l % 2 == 1)
                {
                    foreach(int i in hi)
                    {
                        ret = operations.Op(ret, dat[i, l]);
                    }
                    l += 1;
                }
                if(r % 2 == 1)
                {
                    r -= 1;
                    foreach(int i in hi)
                    {
                        ret = operations.Op(ret, dat[i, r]);
                    }
                }
                l /= 2;
                r /= 2;
            }
            return ret;
        }
        public T GetAll()
        {
            return dat[1, 1];
        }
        public T GetIndex(int h,int w)
        {
            return dat[h + sizeh, w + sizew];
        }
        public T[,] GetMap()
        {
            return dat;
        }
    }
}
