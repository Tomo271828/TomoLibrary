using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.ARR
{
    public static class BinarySearch
    {
        public static int Equal(int[] arr,int num)
        {
            int ok = -1;
            int ng = arr.Length;
            while(ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if(mid == -1)
                {
                    check = true;
                }
                else if(mid != arr.Length)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            if(ok == -1)
            {
                return -1;
            }
            if (arr[ok] == num)
            {
                return ok;
            }
            else
            {
                return -1;
            }
        }
        public static int Equal(long[] arr, long num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            if (ok == -1)
            {
                return -1;
            }
            if (arr[ok] == num)
            {
                return ok;
            }
            else
            {
                return -1;
            }
        }
        public static int Nearest(int[] arr, int num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            if(ok == -1)
            {
                return 0;
            }
            if(ng == arr.Length)
            {
                return arr.Length - 1;
            }
            if (Math.Abs(arr[ok] - num) <= Math.Abs(arr[ng] - num))
            {
                return ok;
            }
            else
            {
                return ng;
            }
        }
        public static int Nearest(long[] arr, long num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            if (ok == -1)
            {
                return 0;
            }
            if (ng == arr.Length)
            {
                return arr.Length - 1;
            }
            if (Math.Abs(arr[ok] - num) <= Math.Abs(arr[ng] - num))
            {
                return ok;
            }
            else
            {
                return ng;
            }
        }
        public static int EqualLessThan(int[] arr, int num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ok;
        }
        public static int EqualLessThan(long[] arr, long num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ok;
        }
        public static int LessThan(int[] arr, int num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] < num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ok;
        }
        public static int LessThan(long[] arr, long num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] < num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ok;
        }
        public static int LessThanCount(int[] arr, int num)
        {
            return LessThan(arr, num) + 1;
        }
        public static int LessThanCount(long[] arr, long num)
        {
            return LessThan(arr, num) + 1;
        }
        public static int EquaLessThanCount(int[] arr, int num)
        {
            return EqualLessThan(arr, num) + 1;
        }
        public static int EquaLessThanCount(long[] arr, long num)
        {
            return EqualLessThan(arr, num) + 1;
        }
        //名前修正版
        public static int EqualLessThanCount(int[] arr, int num)
        {
            return EqualLessThan(arr, num) + 1;
        }
        public static int EqualLessThanCount(long[] arr, long num)
        {
            return EqualLessThan(arr, num) + 1;
        }
        public static int MoreThan(int[] arr, int num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ng;
        }
        public static int MoreThan(long[] arr, long num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ng;
        }
        public static int EqualMoreThan(int[] arr, int num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] < num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ng;
        }
        public static int EqualMoreThan(long[] arr, long num)
        {
            int ok = -1;
            int ng = arr.Length;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Length)
                {
                    if (arr[mid] < num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ng;
        }
        public static int MoreThanCount(int[] arr, int num)
        {
            return arr.Length - MoreThan(arr, num);
        }
        public static int MoreThanCount(long[] arr, long num)
        {
            return arr.Length - MoreThan(arr, num);
        }
        public static int EqualMoreThanCount(int[] arr, int num)
        {
            return arr.Length - EqualMoreThan(arr, num);
        }
        public static int EqualMoreThanCount(long[] arr, long num)
        {
            return arr.Length - EqualMoreThan(arr, num);
        }
        public static int EqualCount(int[] arr, int num)
        {
            int memo = Equal(arr, num);
            if(memo == -1)
            {
                return 0;
            }
            else
            {
                return memo - LessThan(arr, num);
            }
        }
        public static int EqualCount(long[] arr, long num)
        {
            int memo = Equal(arr, num);
            if (memo == -1)
            {
                return 0;
            }
            else
            {
                return memo - LessThan(arr, num);
            }
        }
        public static int Equal(List<int> arr, int num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            if (ok == -1)
            {
                return -1;
            }
            if (arr[ok] == num)
            {
                return ok;
            }
            else
            {
                return -1;
            }
        }
        public static int Equal(List<long> arr, long num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            if (ok == -1)
            {
                return -1;
            }
            if (arr[ok] == num)
            {
                return ok;
            }
            else
            {
                return -1;
            }
        }
        public static int Nearest(List<int> arr, int num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            if (ok == -1)
            {
                return 0;
            }
            if (ng == arr.Count)
            {
                return arr.Count - 1;
            }
            if (Math.Abs(arr[ok] - num) <= Math.Abs(arr[ng] - num))
            {
                return ok;
            }
            else
            {
                return ng;
            }
        }
        public static int Nearest(List<long> arr, long num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            if (ok == -1)
            {
                return 0;
            }
            if (ng == arr.Count)
            {
                return arr.Count - 1;
            }
            if (Math.Abs(arr[ok] - num) <= Math.Abs(arr[ng] - num))
            {
                return ok;
            }
            else
            {
                return ng;
            }
        }
        public static int EqualLessThan(List<int> arr, int num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ok;
        }
        public static int EqualLessThan(List<long> arr, long num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ok;
        }
        public static int LessThan(List<int> arr, int num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] < num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ok;
        }
        public static int LessThan(List<long> arr, long num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] < num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ok;
        }
        public static int LessThanCount(List<int> arr, int num)
        {
            return LessThan(arr, num) + 1;
        }
        public static int LessThanCount(List<long> arr, long num)
        {
            return LessThan(arr, num) + 1;
        }
        public static int EqualLessThanCount(List<int> arr, int num)
        {
            return EqualLessThan(arr, num) + 1;
        }
        public static int EqualLessThanCount(List<long> arr, long num)
        {
            return EqualLessThan(arr, num) + 1;
        }
        public static int MoreThan(List<int> arr, int num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ng;
        }
        public static int MoreThan(List<long> arr, long num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] <= num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ng;
        }
        public static int EqualMoreThan(List<int> arr, int num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] < num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ng;
        }
        public static int EqualMoreThan(List<long> arr, long num)
        {
            int ok = -1;
            int ng = arr.Count;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                bool check = false;
                if (mid == -1)
                {
                    check = true;
                }
                else if (mid != arr.Count)
                {
                    if (arr[mid] < num)
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }
            return ng;
        }
        public static int MoreThanCount(List<int> arr, int num)
        {
            return arr.Count - MoreThan(arr, num);
        }
        public static int MoreThanCount(List<long> arr, long num)
        {
            return arr.Count - MoreThan(arr, num);
        }
        public static int EqualMoreThanCount(List<int> arr, int num)
        {
            return arr.Count - EqualMoreThan(arr, num);
        }
        public static int EqualMoreThanCount(List<long> arr, long num)
        {
            return arr.Count - EqualMoreThan(arr, num);
        }
        public static int EqualCount(List<int> arr, int num)
        {
            int memo = Equal(arr, num);
            if (memo == -1)
            {
                return 0;
            }
            else
            {
                return memo - LessThan(arr, num);
            }
        }
        public static int EqualCount(List<long> arr, long num)
        {
            int memo = Equal(arr, num);
            if (memo == -1)
            {
                return 0;
            }
            else
            {
                return memo - LessThan(arr, num);
            }
        }
    }
}
