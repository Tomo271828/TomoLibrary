using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.DataStructure.SegTree;

namespace TomoLibrary.VP
{
    public class GeoLib
    {
        //line1とline2には線分の端点を代入
        //端点は確認しない
        public static bool IsCrossLineSegment(long[][] line1, long[][] line2)
        {
            long[] pos11 = line1[0];
            long[] pos12 = line1[1];
            long[] pos21 = line2[0];
            long[] pos22 = line2[1];
            long s = (pos11[0] - pos12[0]) * (pos21[1] - pos11[1]) - (pos11[1] - pos12[1]) * (pos21[0] - pos11[0]);
            long t = (pos11[0] - pos12[0]) * (pos22[1] - pos11[1]) - (pos11[1] - pos12[1]) * (pos22[0] - pos11[0]);
            if (s * t >= 0)
            {
                return false;
            }
            s = (pos21[0] - pos22[0]) * (pos11[1] - pos21[1]) - (pos21[1] - pos22[1]) * (pos11[0] - pos21[0]);
            t = (pos21[0] - pos22[0]) * (pos12[1] - pos21[1]) - (pos21[1] - pos22[1]) * (pos12[0] - pos21[0]);
            if (s * t >= 0)
            {
                return false;
            }
            return true;
        }
        public static double TwoPointDistanceDouble(long[] a, long[] b)
        {
            return Math.Sqrt(Math.Pow(a[0] - b[0], 2) + Math.Pow(a[1] - b[1], 2));
        }
        public static double TwoPointDistanceDouble(double[] a, double[] b)
        {
            return Math.Sqrt(Math.Pow(a[0] - b[0], 2) + Math.Pow(a[1] - b[1], 2));
        }
        public static decimal TwoPointDistanceDecimal(long[] a, long[] b)
        {
            return (decimal)Math.Sqrt((long)Math.Pow(a[0] - b[0], 2) + (long)Math.Pow(a[1] - b[1], 2));
        }
        public static decimal TwoPointDistanceDecimal(decimal[] a, decimal[] b)
        {
            return (decimal)Math.Sqrt(Math.Pow((double)(a[0] - b[0]), 2) + Math.Pow((double)(a[1] - b[1]), 2));
        }
        //与えられたList<long[]>の各要素の0番目,1番目を座標とみなし、atan2でソートしたものを返す
        //破壊的ではない
        //第3象限->第4象限->第1象限->第2象限の順  x軸上、y座標が負の点は最後に来る
        //(0,0)は第4象限と第1象限の間として扱う
        //atan2が等しい点は原点から近い順
        public static List<long[]> ArgSort(List<long[]> points)
        {
            List<long[]>[] area = new List<long[]>[4];
            List<long[]>[] between = new List<long[]>[4];
            for(int i = 0;i <= 3; i++)
            {
                area[i] = new List<long[]>();
                between[i] = new List<long[]>();
            }
            for(int i = 0;i <= points.Count - 1; i++)
            {
                long[] copy = new long[points[i].Length];
                for(int j = 0;j <= points[i].Length - 1; j++)
                {
                    copy[j] = points[i][j];
                }
                if (copy[0] == 0 || copy[1] == 0)
                {
                    if (copy[1] == 0)
                    {
                        if (copy[0] >= 0)
                        {
                            between[1].Add(copy);
                        }
                        else
                        {
                            between[3].Add(copy);
                        }
                    }
                    else
                    {
                        if (copy[1] > 0)
                        {
                            between[2].Add(copy);
                        }
                        else
                        {
                            between[0].Add(copy);
                        }
                    }
                }
                else
                {
                    if (copy[0] < 0 && copy[1] < 0)
                    {
                        area[0].Add(copy);
                    }
                    if (copy[0] > 0 && copy[1] < 0)
                    {
                        area[1].Add(copy);
                    }
                    if (copy[0] > 0 && copy[1] > 0)
                    {
                        area[2].Add(copy);
                    }
                    if (copy[0] < 0 && copy[1] > 0)
                    {
                        area[3].Add(copy);
                    }
                }
            }
            for(int i = 0;i <= 3; i++)
            {
                if(i <= 1)
                {
                    area[i].Sort((a, b) => a[1] * b[0] == a[0] * b[1] ? Math.Sign(b[1] - a[1]) : Math.Sign(a[1] * b[0] - a[0] * b[1]));
                }
                else
                {
                    area[i].Sort((a, b) => a[1] * b[0] == a[0] * b[1] ? Math.Sign(a[1] - b[1]) : Math.Sign(a[1] * b[0] - a[0] * b[1]));
                }
            }
            between[0].Sort((a, b) => Math.Sign(b[1] - a[1]));
            between[1].Sort((a, b) => Math.Sign(a[0] - b[0]));
            between[2].Sort((a, b) => Math.Sign(a[1] - b[1]));
            between[3].Sort((a, b) => Math.Sign(b[0] - a[0]));
            List<long[]> ret = new List<long[]>();
            for(int i = 0;i <= 3; i++)
            {
                for(int j = 0;j <= area[i].Count - 1; j++)
                {
                    ret.Add(area[i][j]);
                }
                for(int j = 0;j <= between[i].Count - 1; j++)
                {
                    ret.Add(between[i][j]);
                }
            }
            return ret;
        }
        public static long[][] ArgSort(long[][] points)
        {
            List<long[]> target = new List<long[]>();
            for(int i = 0;i <= points.Length - 1; i++)
            {
                target.Add(points[i]);
            }
            List<long[]> r1 = ArgSort(target);
            long[][] ret = new long[points.Length][];
            for(int i = 0;i <= points.Length - 1; i++)
            {
                ret[i] = r1[i];
            }
            return ret;
        }
        //(mx,my,Mx,My)の列を与え、座標平面上にmx<=x<=Mx、my<=y<=Myを満たす領域を配置し、各領域の和集合の面積を求める
        class AUROP : ILazySegmentTree<AURITEM, long>
        {
            public AURITEM Op(AURITEM x,AURITEM y)
            {
                if(x.min == y.min)
                {
                    return new AURITEM(x.min, x.count + y.count);
                }
                if(x.min < y.min)
                {
                    return x;
                }
                return y;
            }
            public AURITEM E()
            {
                return new AURITEM(long.MaxValue / 4, 0);
            }
            public long Composition(long x,long y)
            {
                return x + y;
            }
            public AURITEM Mapping(long f,AURITEM x)
            {
                return new AURITEM(x.min + f, x.count);
            }
            public long Id()
            {
                return 0;
            }
        }
        struct AURITEM
        {
            public long min;
            public long count;
            public AURITEM(long min,long count)
            {
                this.min = min;
                this.count = count;
            }
        }
        struct AURQUERY
        {
            public int l;
            public int r;
            public bool plus;
            public AURQUERY(int l,int r,bool plus)
            {
                this.l = l;
                this.r = r;
                this.plus = plus;
            }
        }
        public static long AreaUnionRectangles(long[][] area)
        {
            int rc = area.Length;
            if(rc == 0)
            {
                return 0;
            }
            HashSet<long> setx = new HashSet<long>();
            HashSet<long> sety = new HashSet<long>();
            for(int i = 0;i <= rc - 1; i++)
            {
                setx.Add(area[i][0]);
                setx.Add(area[i][2]);
                sety.Add(area[i][1]);
                sety.Add(area[i][3]);
            }
            List<long> x = setx.ToList();
            List<long> y = sety.ToList();
            Dictionary<long, int> dictx = new Dictionary<long, int>();
            Dictionary<long, int> dicty = new Dictionary<long, int>();
            List<AURQUERY>[] query = new List<AURQUERY>[y.Count];
            long[] leny = new long[y.Count - 1];
            int n = x.Count;
            int m = y.Count;
            x.Sort();
            y.Sort();
            AURITEM[] arr = new AURITEM[n - 1];
            for(int i = 0;i <= n - 1; i++)
            {
                dictx.Add(x[i], i);
                if(i != 0)
                {
                    arr[i - 1] = new AURITEM(0, x[i] - x[i - 1]);
                }
            }
            for(int i = 0;i <= m - 1; i++)
            {
                dicty.Add(y[i], i);
                if(i != 0)
                {
                    leny[i - 1] = y[i] - y[i - 1];
                }
                query[i] = new List<AURQUERY>();
            }
            for(int i = 0;i <= rc - 1; i++)
            {
                area[i][0] = dictx[area[i][0]];
                area[i][2] = dictx[area[i][2]];
                area[i][1] = dicty[area[i][1]];
                area[i][3] = dicty[area[i][3]];
                query[area[i][1]].Add(new AURQUERY((int)area[i][0], (int)area[i][2], true));
                query[area[i][3]].Add(new AURQUERY((int)area[i][0], (int)area[i][2], false));
            }
            LazySegmentTree<AURITEM, long> lst = new LazySegmentTree<AURITEM, long>(new AUROP(), arr);
            long ret = (x[n - 1] - x[0]) * (y[m - 1] - y[0]);
            for(int i = 0;i <= m - 1; i++)
            {
                if(i != 0)
                {
                    AURITEM all = lst.GetAll();
                    if(all.min == 0)
                    {
                        ret -= all.count * leny[i - 1];
                    }
                }
                foreach(AURQUERY item in query[i])
                {
                    int l = item.l;
                    int r = item.r;
                    if (item.plus)
                    {
                        lst.Apply(l, r, 1);
                    }
                    else
                    {
                        lst.Apply(l, r, -1);
                    }
                }
            }
            return ret;
        }
    }
}
