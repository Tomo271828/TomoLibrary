using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ML.Counting
{
    public class Factorial
    {
        long mod;
        HashSet<long> set;
        Dictionary<long, long> dict;
        long max;
        //arrの要素の階乗を高速に計算できるようにする
        public Factorial(long[] arr,long mod = 998244353)
        {
            set = new HashSet<long>();
            dict = new Dictionary<long, long>();
            max = 0;
            for(int i = 0;i <= arr.Length - 1; i++)
            {
                max = Math.Max(max, arr[i]);
                if (arr[i] != 0)
                {
                    set.Add(arr[i]);
                }
            }
            long fact = 1;
            List<long> target = set.ToList();
            target.Sort();
            int j = 0;
            dict.Add(0, 1);
            for(long i = 1;i <= max; i++)
            {
                fact *= i;
                fact %= mod;
                if (target[j] == i)
                {
                    dict.Add(i, fact);
                    j += 1;
                    if(j == target.Count)
                    {
                        break;
                    }
                }
            }
        }
        public long this[long x] { get { return dict[x]; } }
        public long GetFact(long x)
        {
            return dict[x];
        }
    }
}
