using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.DataStructure.SegTree;

namespace TomoLibrary.DataStructure.SegTree.Templates
{
    public class RS : ISegmentTree<long>
    {
        public long Op(long a, long b)
        {
            return a + b;
        }
        public long E()
        {
            return 0;
        }
    }
    public class RSmod998244353 : ISegmentTree<long>
    {
        public long Op(long a, long b)
        {
            return (a + b) % 998244353;
        }
        public long E()
        {
            return 0;
        }
    }
    public class RSmod1000000007 : ISegmentTree<long>
    {
        public long Op(long a, long b)
        {
            return (a + b) % 1000000007;
        }
        public long E()
        {
            return 0;
        }
    }
    public class RMin : ISegmentTree<long>
    {
        public long Op(long a, long b)
        {
            return Math.Min(a, b);
        }
        public long E()
        {
            return long.MaxValue / 2;
        }
    }
    public class RMax : ISegmentTree<long>
    {
        public long Op(long a, long b)
        {
            return Math.Max(a, b);
        }
        public long E()
        {
            return long.MinValue / 2;
        }
    }
}
