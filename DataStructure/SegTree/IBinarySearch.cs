using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure.SegTree
{
    public interface IBinarySearch<T>
    {
        public bool F(T value);
    }
}
