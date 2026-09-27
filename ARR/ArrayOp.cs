using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ARR
{
    public static class ArrayOp
    {
        public static int[,] RotateMap(int[,] map)
        {
            int h = map.GetLength(0);
            int w = map.GetLength(1);
            int[,] ret = new int[w, h];
            for(int i = 0;i <= h - 1; i++)
            {
                for(int j = 0;j <= w - 1; j++)
                {
                    ret[j, h - 1 - i] = map[i, j];
                }
            }
            return ret;
        }
        public static long[,] RotateMap(long[,] map)
        {
            int h = map.GetLength(0);
            int w = map.GetLength(1);
            long[,] ret = new long[w, h];
            for (int i = 0; i <= h - 1; i++)
            {
                for (int j = 0; j <= w - 1; j++)
                {
                    ret[j, h - 1 - i] = map[i, j];
                }
            }
            return ret;
        }
        public static char[,] RotateMap(char[,] map)
        {
            int h = map.GetLength(0);
            int w = map.GetLength(1);
            char[,] ret = new char[w, h];
            for (int i = 0; i <= h - 1; i++)
            {
                for (int j = 0; j <= w - 1; j++)
                {
                    ret[j, h - 1 - i] = map[i, j];
                }
            }
            return ret;
        }
        public static string[,] RotateMap(string[,] map)
        {
            int h = map.GetLength(0);
            int w = map.GetLength(1);
            string[,] ret = new string[w, h];
            for (int i = 0; i <= h - 1; i++)
            {
                for (int j = 0; j <= w - 1; j++)
                {
                    ret[j, h - 1 - i] = map[i, j];
                }
            }
            return ret;
        }
        public static bool IsSameArray(int[] arr1,int[] arr2)
        {
            if(arr1.Length != arr2.Length)
            {
                return false;
            }
            else
            {
                bool ret = true;
                for(int i = 0;i <= arr1.Length - 1; i++)
                {
                    if (arr1[i] != arr2[i])
                    {
                        ret = false;
                        break;
                    }
                }
                return ret;
            }
        }
        public static bool IsSameArray(long[] arr1, long[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            else
            {
                bool ret = true;
                for (int i = 0; i <= arr1.Length - 1; i++)
                {
                    if (arr1[i] != arr2[i])
                    {
                        ret = false;
                        break;
                    }
                }
                return ret;
            }
        }
        public static bool IsSameMap(int[,] map1, int[,] map2)
        {
            int h1 = map1.GetLength(0);
            int h2 = map2.GetLength(0);
            int w1 = map1.GetLength(1);
            int w2 = map2.GetLength(1);
            if(h1 != h2 || w1 != w2)
            {
                return false;
            }
            bool ret = true;
            for(int i = 0;i <= h1 - 1; i++)
            {
                for(int j = 0;j <= w1 - 1; j++)
                {
                    if (map1[i,j] != map2[i, j])
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }
        public static bool IsSameMap(long[,] map1, long[,] map2)
        {
            int h1 = map1.GetLength(0);
            int h2 = map2.GetLength(0);
            int w1 = map1.GetLength(1);
            int w2 = map2.GetLength(1);
            if (h1 != h2 || w1 != w2)
            {
                return false;
            }
            bool ret = true;
            for (int i = 0; i <= h1 - 1; i++)
            {
                for (int j = 0; j <= w1 - 1; j++)
                {
                    if (map1[i, j] != map2[i, j])
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }
        public static bool IsSameArray(char[] arr1, char[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            else
            {
                bool ret = true;
                for (int i = 0; i <= arr1.Length - 1; i++)
                {
                    if (arr1[i] != arr2[i])
                    {
                        ret = false;
                        break;
                    }
                }
                return ret;
            }
        }
        public static bool IsSameArray(string[] arr1, string[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            else
            {
                bool ret = true;
                for (int i = 0; i <= arr1.Length - 1; i++)
                {
                    if (arr1[i] != arr2[i])
                    {
                        ret = false;
                        break;
                    }
                }
                return ret;
            }
        }
        public static bool IsSameMap(char[,] map1, char[,] map2)
        {
            int h1 = map1.GetLength(0);
            int h2 = map2.GetLength(0);
            int w1 = map1.GetLength(1);
            int w2 = map2.GetLength(1);
            if (h1 != h2 || w1 != w2)
            {
                return false;
            }
            bool ret = true;
            for (int i = 0; i <= h1 - 1; i++)
            {
                for (int j = 0; j <= w1 - 1; j++)
                {
                    if (map1[i, j] != map2[i, j])
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }
        public static bool IsSameMap(string[,] map1, string[,] map2)
        {
            int h1 = map1.GetLength(0);
            int h2 = map2.GetLength(0);
            int w1 = map1.GetLength(1);
            int w2 = map2.GetLength(1);
            if (h1 != h2 || w1 != w2)
            {
                return false;
            }
            bool ret = true;
            for (int i = 0; i <= h1 - 1; i++)
            {
                for (int j = 0; j <= w1 - 1; j++)
                {
                    if (map1[i, j] != map2[i, j])
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }
        public static int Max(int[] arr)
        {
            int n = arr.Length;
            int max = arr[0];
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
        public static long Max(long[] arr)
        {
            int n = arr.Length;
            long max = arr[0];
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
        public static float Max(float[] arr)
        {
            int n = arr.Length;
            float max = arr[0];
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
        public static double Max(double[] arr)
        {
            int n = arr.Length;
            double max = arr[0];
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
        public static int MaxIndex(int[] arr)
        {
            int n = arr.Length;
            int max = arr[0];
            int ret = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    ret = i;
                }
            }
            return ret;
        }
        public static int MaxIndex(long[] arr)
        {
            int n = arr.Length;
            long max = arr[0];
            int ret = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    ret = i;
                }
            }
            return ret;
        }
        public static int MaxIndex(float[] arr)
        {
            int n = arr.Length;
            float max = arr[0];
            int ret = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    ret = i;
                }
            }
            return ret;
        }
        public static int MaxIndex(double[] arr)
        {
            int n = arr.Length;
            double max = arr[0];
            int ret = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    ret = i;
                }
            }
            return ret;
        }
        public static int Min(int[] arr)
        {
            int n = arr.Length;
            int min = arr[0];
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            return min;
        }
        public static long Min(long[] arr)
        {
            int n = arr.Length;
            long min = arr[0];
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            return min;
        }
        public static float Min(float[] arr)
        {
            int n = arr.Length;
            float min = arr[0];
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            return min;
        }
        public static double Min(double[] arr)
        {
            int n = arr.Length;
            double min = arr[0];
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            return min;
        }
        public static int MinIndex(int[] arr)
        {
            int n = arr.Length;
            int min = arr[0];
            int ret = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    ret = i;
                }
            }
            return ret;
        }
        public static int MinIndex(long[] arr)
        {
            int n = arr.Length;
            long min = arr[0];
            int ret = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    ret = i;
                }
            }
            return ret;
        }
        public static int MinIndex(float[] arr)
        {
            int n = arr.Length;
            float min = arr[0];
            int ret = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    ret = i;
                }
            }
            return ret;
        }
        public static int MinIndex(double[] arr)
        {
            int n = arr.Length;
            double min = arr[0];
            int ret = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    ret = i;
                }
            }
            return ret;
        }
        public static int Sum(int[] arr)
        {
            int n = arr.Length;
            int sum = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                sum += arr[i];
            }
            return sum;
        }
        public static long Sum(long[] arr)
        {
            int n = arr.Length;
            long sum = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                sum += arr[i];
            }
            return sum;
        }
        public static float Sum(float[] arr)
        {
            int n = arr.Length;
            float sum = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                sum += arr[i];
            }
            return sum;
        }
        public static double Sum(double[] arr)
        {
            int n = arr.Length;
            double sum = 0;
            for (int i = 0; i <= n - 1; i++)
            {
                sum += arr[i];
            }
            return sum;
        }
        public static int[] AllPlus(int[] arr,int x)
        {
            int[] ret = arr;
            int n = ret.Length;
            for(int i = 0;i <= n - 1; i++)
            {
                ret[i] += x;
            }
            return ret;
        }
        public static long[] AllPlus(long[] arr, long x)
        {
            long[] ret = arr;
            int n = ret.Length;
            for (int i = 0; i <= n - 1; i++)
            {
                ret[i] += x;
            }
            return ret;
        }
        public static int[] CumulativeSum(int[] arr)
        {
            int[] ret = new int[arr.Length];
            ret[0] = arr[0];
            for(int i = 1;i <= arr.Length - 1; i++)
            {
                ret[i] = ret[i - 1] + arr[i];
            }
            return ret;
        }
        public static long[] CumulativeSum(long[] arr)
        {
            long[] ret = new long[arr.Length];
            ret[0] = arr[0];
            for (int i = 1; i <= arr.Length - 1; i++)
            {
                ret[i] = ret[i - 1] + arr[i];
            }
            return ret;
        }
        public static int[] CumulativeSumExt0(int[] arr)
        {
            int[] ret = new int[arr.Length + 1];
            ret[0] = 0;
            for (int i = 1; i <= arr.Length; i++)
            {
                ret[i] = ret[i - 1] + arr[i - 1];
            }
            return ret;
        }
        public static long[] CumulativeSumExt0(long[] arr)
        {
            long[] ret = new long[arr.Length + 1];
            ret[0] = 0;
            for (int i = 1; i <= arr.Length; i++)
            {
                ret[i] = ret[i - 1] + arr[i - 1];
            }
            return ret;
        }
        public static int[] CoordinateCompression(int[] a)
        {
            int[] ret = new int[a.Length];
            var set = new SortedSet<int>();
            for(int i = 0;i <= a.Length - 1; i++)
            {
                set.Add(a[i]);
            }
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int j = 0;
            foreach(int item in set)
            {
                dict[item] = j;
                j += 1;
            }
            for(int i = 0;i <= a.Length - 1; i++)
            {
                ret[i] = dict[a[i]];
            }
            return ret;
        }
        public static void CoordinateCompressionInPlace(int[] a)
        {
            var set = new SortedSet<int>();
            for (int i = 0; i <= a.Length - 1; i++)
            {
                set.Add(a[i]);
            }
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int j = 0;
            foreach (int item in set)
            {
                dict[item] = j;
                j += 1;
            }
            for (int i = 0; i <= a.Length - 1; i++)
            {
                a[i] = dict[a[i]];
            }
        }
        public static long[] CoordinateCompression(long[] a)
        {
            long[] ret = new long[a.Length];
            var set = new SortedSet<long>();
            for (int i = 0; i <= a.Length - 1; i++)
            {
                set.Add(a[i]);
            }
            Dictionary<long, long> dict = new Dictionary<long, long>();
            long j = 0;
            foreach (long item in set)
            {
                dict[item] = j;
                j += 1;
            }
            for (int i = 0; i <= a.Length - 1; i++)
            {
                ret[i] = dict[a[i]];
            }
            return ret;
        }
        public static void CoordinateCompressionInPlace(long[] a)
        {
            var set = new SortedSet<long>();
            for (int i = 0; i <= a.Length - 1; i++)
            {
                set.Add(a[i]);
            }
            Dictionary<long, long> dict = new Dictionary<long, long>();
            long j = 0;
            foreach (long item in set)
            {
                dict[item] = j;
                j += 1;
            }
            for (int i = 0; i <= a.Length - 1; i++)
            {
                a[i] = dict[a[i]];
            }
        }
        public static string CharListToString(List<char> CharList)
        {
            int n = CharList.Count;
            char[] c = new char[n];
            for(int i = 0;i <= n - 1; i++)
            {
                c[i] = CharList[i];
            }
            return new string(c);
        }
        public static int[] RotateArray(int[] arr,int count)
        {
            int n = arr.Length;
            int[] ret = new int[n];
            for(int i = 0;i <= n - 1; i++)
            {
                int index = i + count;
                while(index < 0)
                {
                    index += n;
                }
                index %= n;
                ret[index] = arr[i];
            }
            return ret;
        }
        public static long[] RotateArray(long[] arr, int count)
        {
            int n = arr.Length;
            long[] ret = new long[n];
            for (int i = 0; i <= n - 1; i++)
            {
                int index = i + count;
                while (index < 0)
                {
                    index += n;
                }
                index %= n;
                ret[index] = arr[i];
            }
            return ret;
        }
        public static float[] RotateArray(float[] arr, int count)
        {
            int n = arr.Length;
            float[] ret = new float[n];
            for (int i = 0; i <= n - 1; i++)
            {
                int index = i + count;
                while (index < 0)
                {
                    index += n;
                }
                index %= n;
                ret[index] = arr[i];
            }
            return ret;
        }
        public static string[] RotateArray(string[] arr, int count)
        {
            int n = arr.Length;
            string[] ret = new string[n];
            for (int i = 0; i <= n - 1; i++)
            {
                int index = i + count;
                while (index < 0)
                {
                    index += n;
                }
                index %= n;
                ret[index] = arr[i];
            }
            return ret;
        }
        public static char[] RotateArray(char[] arr, int count)
        {
            int n = arr.Length;
            char[] ret = new char[n];
            for (int i = 0; i <= n - 1; i++)
            {
                int index = i + count;
                while (index < 0)
                {
                    index += n;
                }
                index %= n;
                ret[index] = arr[i];
            }
            return ret;
        }
        public static List<int> RotateList(List<int> l, int count)
        {
            int n = l.Count;
            List<int> ret = new List<int>();
            for(int i = 0;i <= n - 1; i++)
            {
                int index = i - count;
                while(index < 0)
                {
                    index += n;
                }
                index %= n;
                ret.Add(l[index]);
            }
            return ret;
        }
        public static List<long> RotateList(List<long> l, int count)
        {
            int n = l.Count;
            List<long> ret = new List<long>();
            for (int i = 0; i <= n - 1; i++)
            {
                int index = i - count;
                while (index < 0)
                {
                    index += n;
                }
                index %= n;
                ret.Add(l[index]);
            }
            return ret;
        }
        public static List<float> RotateList(List<float> l, int count)
        {
            int n = l.Count;
            List<float> ret = new List<float>();
            for (int i = 0; i <= n - 1; i++)
            {
                int index = i - count;
                while (index < 0)
                {
                    index += n;
                }
                index %= n;
                ret.Add(l[index]);
            }
            return ret;
        }
        public static List<string> RotateList(List<string> l, int count)
        {
            int n = l.Count;
            List<string> ret = new List<string>();
            for (int i = 0; i <= n - 1; i++)
            {
                int index = i - count;
                while (index < 0)
                {
                    index += n;
                }
                index %= n;
                ret.Add(l[index]);
            }
            return ret;
        }
        public static List<char> RotateList(List<char> l, int count)
        {
            int n = l.Count;
            List<char> ret = new List<char>();
            for (int i = 0; i <= n - 1; i++)
            {
                int index = i - count;
                while (index < 0)
                {
                    index += n;
                }
                index %= n;
                ret.Add(l[index]);
            }
            return ret;
        }
        public static bool AllSame(int[] arr)
        {
            for(int i = 0;i <= arr.Length - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(long[] arr)
        {
            for (int i = 0; i <= arr.Length - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(string[] arr)
        {
            for (int i = 0; i <= arr.Length - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(char[] arr)
        {
            for (int i = 0; i <= arr.Length - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(bool[] arr)
        {
            for (int i = 0; i <= arr.Length - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(int[] arr,int target)
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(long[] arr, long target)
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(string[] arr, string target)
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(char[] arr, char target)
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(bool[] arr, bool target)
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<int> arr)
        {
            for (int i = 0; i <= arr.Count - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<long> arr)
        {
            for (int i = 0; i <= arr.Count - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<string> arr)
        {
            for (int i = 0; i <= arr.Count - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<char> arr)
        {
            for (int i = 0; i <= arr.Count - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<bool> arr)
        {
            for (int i = 0; i <= arr.Count - 2; i++)
            {
                if (arr[i] != arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<int> arr, int target)
        {
            for (int i = 0; i <= arr.Count - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<long> arr, long target)
        {
            for (int i = 0; i <= arr.Count - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<string> arr, string target)
        {
            for (int i = 0; i <= arr.Count - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<char> arr, char target)
        {
            for (int i = 0; i <= arr.Count - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AllSame(List<bool> arr, bool target)
        {
            for (int i = 0; i <= arr.Count - 1; i++)
            {
                if (arr[i] != target)
                {
                    return false;
                }
            }
            return true;
        }
        // 辞書順最小の LIS のインデックスの配列を返す
        public static int[] LIS(long[] arr)
        {
            int n = arr.Length;
            long[] lis = new long[n + 1];
            for (int i = 0; i <= n; i++)
            {
                lis[i] = 3000000000000000000;
            }
            int len = 0;
            int[] index = new int[n];
            for (int i = 0; i <= n - 1; i++)
            {
                int ind = BinarySearch.EqualMoreThan(lis, arr[i]);
                lis[ind] = arr[i];
                len = Math.Max(len, ind + 1);
                index[i] = ind;
            }
            //Console.WriteLine(String.Join(" ", index));
            int[] ret = new int[len];
            for (int i = n - 1; i >= 0; i--)
            {
                if (index[i] == len - 1)
                {
                    ret[len - 1] = i;
                    len -= 1;
                }
            }
            return ret;
        }
    }
}
