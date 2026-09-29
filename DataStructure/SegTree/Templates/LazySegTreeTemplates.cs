using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.DataStructure.SegTree;

namespace TomoLibrary.DataStructure.SegTree.Templates
{
    //Range Set Range Sum
    public class RSRS : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return a + b;
        }
        public long E()
        {
            return 0;
        }
        public long Mapping(long f, long x)
        {
            return f;
        }
        public long Composition(long f, long g)
        {
            if (f == long.MinValue / 2)
            {
                return g;
            }
            return f;
        }
        public long Id()
        {
            return long.MinValue / 2;
        }
    }
    public class RSRSmod998244353 : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return (a + b) % 998244353;
        }
        public long E()
        {
            return 0;
        }
        public long Mapping(long f, long x)
        {
            return f;
        }
        public long Composition(long f, long g)
        {
            if (f == long.MinValue / 2)
            {
                return g;
            }
            return f;
        }
        public long Id()
        {
            return long.MinValue / 2;
        }
    }
    public class RSRSmod1000000007 : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return (a + b) % 1000000007;
        }
        public long E()
        {
            return 0;
        }
        public long Mapping(long f, long x)
        {
            return f;
        }
        public long Composition(long f, long g)
        {
            if (f == long.MinValue / 2)
            {
                return g;
            }
            return f;
        }
        public long Id()
        {
            return long.MinValue / 2;
        }
    }
    public class RSRMin : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return Math.Min(a, b);
        }
        public long E()
        {
            return long.MaxValue / 2;
        }
        public long Mapping(long f, long x)
        {
            return f;
        }
        public long Composition(long f, long g)
        {
            if (f == long.MinValue / 2)
            {
                return g;
            }
            return f;
        }
        public long Id()
        {
            return long.MinValue / 2;
        }
    }
    public class RSRMax : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return Math.Max(a, b);
        }
        public long E()
        {
            return long.MinValue / 2;
        }
        public long Mapping(long f, long x)
        {
            return f;
        }
        public long Composition(long f, long g)
        {
            if (f == long.MinValue / 2)
            {
                return g;
            }
            return f;
        }
        public long Id()
        {
            return long.MinValue / 2;
        }
    }
    //Range Add Range Sum
    public class RARS : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return a + b;
        }
        public long E()
        {
            return 0;
        }
        public long Mapping(long f, long x)
        {
            return f + x;
        }
        public long Composition(long f, long g)
        {
            return f + g;
        }
        public long Id()
        {
            return 0;
        }
    }
    public class RARSmod998244353 : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return (a + b) % 998244353;
        }
        public long E()
        {
            return 0;
        }
        public long Mapping(long f, long x)
        {
            return (f + x) % 998244353;
        }
        public long Composition(long f, long g)
        {
            return (f + g) % 998244353;
        }
        public long Id()
        {
            return 0;
        }
    }
    public class RARSmod1000000007 : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return (a + b) % 1000000007;
        }
        public long E()
        {
            return 0;
        }
        public long Mapping(long f, long x)
        {
            return (f + x) % 1000000007;
        }
        public long Composition(long f, long g)
        {
            return (f + g) % 1000000007;
        }
        public long Id()
        {
            return 0;
        }
    }
    public class RARMin : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return Math.Min(a, b);
        }
        public long E()
        {
            return long.MaxValue / 2;
        }
        public long Mapping(long f, long x)
        {
            return f + x;
        }
        public long Composition(long f, long g)
        {
            return f + g;
        }
        public long Id()
        {
            return 0;
        }
    }
    public class RARMax : ILazySegmentTree<long, long>
    {
        public long Op(long a, long b)
        {
            return Math.Max(a, b);
        }
        public long E()
        {
            return long.MinValue / 2;
        }
        public long Mapping(long f, long x)
        {
            return f + x;
        }
        public long Composition(long f, long g)
        {
            return f + g;
        }
        public long Id()
        {
            return 0;
        }
    }
}
