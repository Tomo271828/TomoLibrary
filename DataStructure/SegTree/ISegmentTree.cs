using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public interface ISegmentTree<T>
    {
        public T Op(T a, T b);
        public T E();
    }
}
