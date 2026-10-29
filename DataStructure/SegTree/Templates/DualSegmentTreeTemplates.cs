using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.DataStructure.SegTree.Interface;

namespace TomoLibrary.DataStructure.SegTree.Templates
{
    public class RA : IDualSegmentTree<long, long>
    {
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
    public class RAmod998244353 : IDualSegmentTree<long, long>
    {
        long mod = 998244353;
        public long E()
        {
            return 0;
        }
        public long Mapping(long f, long x)
        {
            return (f + x) % mod;
        }
        public long Composition(long f, long g)
        {
            return (f + g) % mod;
        }
        public long Id()
        {
            return 0;
        }
    }
    public class RAmod1000000007 : IDualSegmentTree<long, long>
    {
        long mod = 1000000007;
        public long E()
        {
            return 0;
        }
        public long Mapping(long f, long x)
        {
            return (f + x) % mod;
        }
        public long Composition(long f, long g)
        {
            return (f + g) % mod;
        }
        public long Id()
        {
            return 0;
        }
    }
    public class RSet : IDualSegmentTree<long, long>
    {
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
            return f;
        }
        public long Id()
        {
            return 0;
        }
    }
}
