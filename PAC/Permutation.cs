using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TomoLibrary.PAC
{
    public static class Permutation
    {
        public static int[] NextPermutation(int[] arr)
        {
            int change = -1;
            int memo = 0;
            int[] s = arr;
            int n = s.Length;
            bool isLast = false;
            for (int i = 0; i <= n - 2; i++)
            {
                if (s[i] < s[i + 1])
                {
                    change = i;
                }
            }
            if (change == -1)
            {
                isLast = true;
            }
            if(isLast == false)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    if (s[i] > s[change])
                    {
                        memo = s[i];
                        s[i] = s[change];
                        s[change] = memo;
                        break;
                    }
                }
                for (int i = change + 1; i <= n - 1; i++)
                {
                    if (i >= n - 1 - (i - (change + 1)))
                    {
                        break;
                    }
                    memo = s[i];
                    s[i] = s[n - 1 - (i - (change + 1))];
                    s[n - 1 - (i - (change + 1))] = memo;
                }
            }
            return s;
        }
        public static long[] NextPermutation(long[] arr)
        {
            int change = -1;
            long memo = 0;
            long[] s = arr;
            int n = s.Length;
            bool isLast = false;
            for (int i = 0; i <= n - 2; i++)
            {
                if (s[i] < s[i + 1])
                {
                    change = i;
                }
            }
            if (change == -1)
            {
                isLast = true;
            }
            if (isLast == false)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    if (s[i] > s[change])
                    {
                        memo = s[i];
                        s[i] = s[change];
                        s[change] = memo;
                        break;
                    }
                }
                for (int i = change + 1; i <= n - 1; i++)
                {
                    if (i >= n - 1 - (i - (change + 1)))
                    {
                        break;
                    }
                    memo = s[i];
                    s[i] = s[n - 1 - (i - (change + 1))];
                    s[n - 1 - (i - (change + 1))] = memo;
                }
            }
            return s;
        }
        public static char[] NextPermutation(char[] arr)
        {
            int change = -1;
            char memo = '0';
            char[] s = arr;
            int n = s.Length;
            bool isLast = false;
            for (int i = 0; i <= n - 2; i++)
            {
                if (s[i] < s[i + 1])
                {
                    change = i;
                }
            }
            if (change == -1)
            {
                isLast = true;
            }
            if (isLast == false)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    if (s[i] > s[change])
                    {
                        memo = s[i];
                        s[i] = s[change];
                        s[change] = memo;
                        break;
                    }
                }
                for (int i = change + 1; i <= n - 1; i++)
                {
                    if (i >= n - 1 - (i - (change + 1)))
                    {
                        break;
                    }
                    memo = s[i];
                    s[i] = s[n - 1 - (i - (change + 1))];
                    s[n - 1 - (i - (change + 1))] = memo;
                }
            }
            return s;
        }
        public static string[] NextPermutation(string[] arr)
        {
            int change = -1;
            string memo = "";
            string[] s = arr;
            int n = s.Length;
            bool isLast = false;
            for (int i = 0; i <= n - 2; i++)
            {
                if (string.Compare(s[i], s[i + 1]) < 0)
                {
                    change = i;
                }
            }
            if (change == -1)
            {
                isLast = true;
            }
            if (isLast == false)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    if (string.Compare(s[i], s[change]) > 0)
                    {
                        memo = s[i];
                        s[i] = s[change];
                        s[change] = memo;
                        break;
                    }
                }
                for (int i = change + 1; i <= n - 1; i++)
                {
                    if (i >= n - 1 - (i - (change + 1)))
                    {
                        break;
                    }
                    memo = s[i];
                    s[i] = s[n - 1 - (i - (change + 1))];
                    s[n - 1 - (i - (change + 1))] = memo;
                }
            }
            return s;
        }
        public static bool IsLastPermutation(int[] arr)
        {
            int[] s = arr;
            int n = s.Length;
            bool isLast = true;
            for (int i = 0; i <= n - 2; i++)
            {
                if (s[i] < s[i + 1])
                {
                    isLast = false;
                }
            }
            return isLast;
        }
        public static bool IsLastPermutation(long[] arr)
        {
            long[] s = arr;
            int n = s.Length;
            bool isLast = true;
            for (int i = 0; i <= n - 2; i++)
            {
                if (s[i] < s[i + 1])
                {
                    isLast = false;
                }
            }
            return isLast;
        }
        public static bool IsLastPermutation(char[] arr)
        {
            char[] s = arr;
            int n = s.Length;
            bool isLast = true;
            for (int i = 0; i <= n - 2; i++)
            {
                if (s[i] < s[i + 1])
                {
                    isLast = false;
                }
            }
            return isLast;
        }
        public static bool IsLastPermutation(string[] arr)
        {
            string[] s = arr;
            int n = s.Length;
            bool isLast = true;
            for (int i = 0; i <= n - 2; i++)
            {
                if (string.Compare(s[i], s[i + 1]) < 0)
                {
                    isLast = false;
                }
            }
            return isLast;
        }
        public static int[] PreviousPermutation(int[] arr)
        {
            int rev = 0;
            int nearest = 0;
            int[] a = arr;
            int n = a.Length;
            List<int> back = new List<int>();
            for (int i = n - 1; i >= 1; i--)
            {
                back.Add(a[i]);
                if (a[i - 1] > a[i])
                {
                    rev = i;
                    break;
                }
            }
            if(rev != 0)
            {
                for (int i = 0; i <= back.Count - 1; i++)
                {
                    if (back[i] < a[rev - 1] && nearest < back[i])
                    {
                        nearest = back[i];
                    }
                }
                back.Remove(nearest);
                back.Add(a[rev - 1]);
                a[rev - 1] = nearest;
                back.Sort();
                for (int i = 0; i <= back.Count - 1; i++)
                {
                    a[n - 1 - i] = back[i];
                }
            }
            return a;
        }
        public static long[] PreviousPermutation(long[] arr)
        {
            int rev = 0;
            long nearest = 0;
            long[] a = arr;
            int n = a.Length;
            List<long> back = new List<long>();
            for (int i = n - 1; i >= 1; i--)
            {
                back.Add(a[i]);
                if (a[i - 1] > a[i])
                {
                    rev = i;
                    break;
                }
            }
            if (rev != 0)
            {
                for (int i = 0; i <= back.Count - 1; i++)
                {
                    if (back[i] < a[rev - 1] && nearest < back[i])
                    {
                        nearest = back[i];
                    }
                }
                back.Remove(nearest);
                back.Add(a[rev - 1]);
                a[rev - 1] = nearest;
                back.Sort();
                for (int i = 0; i <= back.Count - 1; i++)
                {
                    a[n - 1 - i] = back[i];
                }
            }
            return a;
        }
        public static char[] PreviousPermutation(char[] arr)
        {
            int rev = 0;
            char nearest = '0';
            char[] a = arr;
            int n = a.Length;
            List<char> back = new List<char>();
            for (int i = n - 1; i >= 1; i--)
            {
                back.Add(a[i]);
                if (a[i - 1] > a[i])
                {
                    rev = i;
                    break;
                }
            }
            if (rev != 0)
            {
                for (int i = 0; i <= back.Count - 1; i++)
                {
                    if (back[i] < a[rev - 1] && nearest < back[i])
                    {
                        nearest = back[i];
                    }
                }
                back.Remove(nearest);
                back.Add(a[rev - 1]);
                a[rev - 1] = nearest;
                back.Sort();
                for (int i = 0; i <= back.Count - 1; i++)
                {
                    a[n - 1 - i] = back[i];
                }
            }
            return a;
        }
        public static string[] PreviousPermutation(string[] arr)
        {
            int rev = 0;
            string nearest = "";
            string[] a = arr;
            int n = a.Length;
            List<string> back = new List<string>();
            for (int i = n - 1; i >= 1; i--)
            {
                back.Add(a[i]);
                if (string.Compare(a[i - 1], a[i]) > 0)
                {
                    rev = i;
                    break;
                }
            }
            if (rev != 0)
            {
                for (int i = 0; i <= back.Count - 1; i++)
                {
                    if (string.Compare(back[i], a[rev - 1]) < 0 && string.Compare(nearest, back[i]) < 0)
                    {
                        nearest = back[i];
                    }
                }
                back.Remove(nearest);
                back.Add(a[rev - 1]);
                a[rev - 1] = nearest;
                back.Sort();
                for (int i = 0; i <= back.Count - 1; i++)
                {
                    a[n - 1 - i] = back[i];
                }
            }
            return a;
        }
        public static bool IsFirstPermutation(int[] arr)
        {
            int[] a = arr;
            int n = a.Length;
            bool ret = true;
            for (int i = n - 1; i >= 1; i--)
            {
                if (a[i - 1] > a[i])
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
        public static bool IsFirstPermutation(long[] arr)
        {
            long[] a = arr;
            int n = a.Length;
            bool ret = true;
            for (int i = n - 1; i >= 1; i--)
            {
                if (a[i - 1] > a[i])
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
        public static bool IsFirstPermutation(char[] arr)
        {
            char[] a = arr;
            int n = a.Length;
            bool ret = true;
            for (int i = n - 1; i >= 1; i--)
            {
                if (a[i - 1] > a[i])
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
        public static bool IsFirstPermutation(string[] arr)
        {
            string[] a = arr;
            int n = a.Length;
            bool ret = true;
            for (int i = n - 1; i >= 1; i--)
            {
                if (string.Compare(a[i - 1], a[i]) > 0)
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
    }
}
