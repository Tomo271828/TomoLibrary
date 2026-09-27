using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class SortedMultiSet<T> : SortedSet<T> where T : IComparable
    {
        public override void Add(T value)
        {
            if (root == null)
            {
                root = new SBBST<T, bool>.Node(value, false);
            }
            else
            {
                root = SBBST<T, bool>.Insert(root, value, false);
            }
        }
    }
}
