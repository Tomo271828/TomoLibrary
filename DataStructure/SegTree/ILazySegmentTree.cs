using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public interface ILazySegmentTree<T,V>
    {
        public T Op(T x, T y);
        public T E();
        public T Mapping(V f, T x);
        //f の方があとの操作
        public V Composition(V f, V g);
        public V Id();
    }
}
