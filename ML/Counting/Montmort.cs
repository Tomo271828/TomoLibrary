using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ML.Counting
{
    public class Montmort
    {
        long[] arr;
        //長さ len の攪乱順列の個数を mod で割った余りを返す
        //長さ 0 の場合は 0 とする
        public long this[int len] { get { return arr[len]; } }
        //長さ 1~n の攪乱順列の個数を mod で割った余りを求める
        public Montmort(int n, long mod = 998244353)
        {
            arr = new long[Math.Max(4, n + 1)];
            arr[2] = 1;
            arr[3] = 2;
            arr[2] %= mod;
            arr[3] %= mod;
            for(int i = 4;i <= arr.Length - 1; i++)
            {
                arr[i] = (i - 1) * (arr[i - 1] + arr[i - 2]);
                arr[i] %= mod;
            }
        }
        public long Get(int len)
        {
            return arr[len];
        }
    }
}
