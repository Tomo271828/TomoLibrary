using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.DataStructure
{
    public class WaveletMatrix
    {
        int h;
        int n;
        long[] a;
        int[][] dat;
        int l1;
        int r1;
        int l2;
        int r2;
        public WaveletMatrix(long[] arr)
        {
            h = 0;
            n = arr.Length;
            for(int i = 0;i <= n - 1; i++)
            {
                while((long)1 << h < arr[i])
                {
                    h += 1;
                }
            }
            h += 1;
            a = new long[n];
            Array.Copy(arr, a, n);
            Init();
        }
        public WaveletMatrix(long[] arr,int h)
        {
            this.h = h;
            n = arr.Length;
            a = new long[n];
            Array.Copy(arr, a, n);
            Init();
        }
        void Init()
        {
            dat = new int[h][];
            Queue<long> left = new Queue<long>();
            Queue<long> right = new Queue<long>();
            for (int i = h - 1;i >= 0; i--)
            {
                dat[i] = new int[n + 1];
                dat[i][0] = 0;
                for(int j = 0;j <= n - 1; j++)
                {
                    int dir = (int)(a[j] >> i & 1);
                    dat[i][j + 1] = dat[i][j] + dir;
                    if(dir == 0)
                    {
                        left.Enqueue(a[j]);
                    }
                    else
                    {
                        right.Enqueue(a[j]);
                    }
                }
                int index = 0;
                while(left.TryDequeue(out long v))
                {
                    a[index] = v;
                    index += 1;
                }
                while(right.TryDequeue(out long v))
                {
                    a[index] = v;
                    index += 1;
                }
            }
        }
        //l1,r1,l2,r2に(h + 1,l,r)からの左右の部分木の値の範囲を代入
        void GetSubtreeRange(int h,int l,int r)
        {
            int c = n - dat[h][n];
            l1 = l - dat[h][l];
            r1 = r - dat[h][r];
            l2 = c + dat[h][l];
            r2 = c + dat[h][r];
        }
        //[l,r)でk(0-indexed)番目に小さい値を取得
        public long KthSmallest(int l,int r,int k)
        {
            long ret = 0;
            for(int i = h;i >= 1; i--)
            {
                int c = n - dat[i - 1][n];
                l1 = l - dat[i - 1][l];
                r1 = r - dat[i - 1][r];
                l2 = c + dat[i - 1][l];
                r2 = c + dat[i - 1][r];
                int leftsize = r1 - l1;
                if(k < leftsize)
                {
                    l = l1;
                    r = r1;
                }
                else
                {
                    ret += (long)1 << i - 1;
                    l = l2;
                    r = r2;
                    k = k - leftsize;
                }
            }
            return ret;
        }
        //[l,r)でxが出現する回数を返す
        public int Count(int l,int r,long x)
        {
            for(int i = h;i >= 1; i--)
            {
                int c = n - dat[i - 1][n];
                l1 = l - dat[i - 1][l];
                r1 = r - dat[i - 1][r];
                l2 = c + dat[i - 1][l];
                r2 = c + dat[i - 1][r];
                if((x >> i - 1 & 1) == 0)
                {
                    l = l1;
                    r = r1;
                }
                else
                {
                    l = l2;
                    r = r2;
                }
            }
            return r - l;
        }
    }
}
