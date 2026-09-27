using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.DSU
{
    public interface IValueUnionFind<T>
    {
        //x に y を併合(x の Rank が y の Rank 以下)
        public T Op(T x, T y);
        public T E();
    }
}
