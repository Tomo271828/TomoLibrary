using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary.Text
{
    public static class StringLib
    {
        public static string LCS(string a,string b)
        {
            char[] ac = a.ToCharArray();
            char[] bc = b.ToCharArray();
            int h = ac.Length;
            int w = bc.Length;
            int[,] dp = new int[h + 1, w + 1];
            for (int i = 1; i <= h; i++)
            {
                for (int j = 1; j <= w; j++)
                {
                    if (ac[i - 1] == bc[j - 1])
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j - 1] + 1, Math.Max(dp[i, j - 1], dp[i - 1, j]));
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }
            int l = dp[h, w];
            int y = h;
            int x = w;
            char[] ret = new char[l];
            while (l > 0)
            {
                if (ac[y - 1] == bc[x - 1])
                {
                    ret[l - 1] = ac[y - 1];
                    y -= 1;
                    x -= 1;
                    l -= 1;
                }
                else
                {
                    if (dp[y, x] == dp[y - 1, x])
                    {
                        y -= 1;
                    }
                    else
                    {
                        x -= 1;
                    }
                }
            }
            return new string(ret);
        }
        // デフォルトでは英小文字・英大文字対応
        public static int[] SAIS(string str, char min = 'A', char max = 'z')
        {
            int n = str.Length;
            int[] s = new int[n];
            if (min != (char)0)
            {
                min = (char)(min - 1);
            }
            for (int i = 0; i <= n - 1; i++)
            {
                s[i] = str[i] - min;
            }
            int spe = max - min + 1;
            return SAIS(s, spe);
        }
        public static int[] SAIS(int[] sin, int spe)
        {
            if(sin.Length == 1)
            {
                return new int[1] { 0 };
            }
            int n = sin.Length + 1;
            int[] s = new int[n];
            for(int i = 0;i <= n - 2; i++)
            {
                s[i] = sin[i];
            }
            s[n - 1] = 0;
            int[] LS = new int[n];
            bool[] LMS = new bool[n];
            int[] LMSsublen = new int[n];
            int LMScount = 0;
            bool[] used = new bool[n];
            int[] charcount = new int[spe];
            for(int i = 0;i <= n - 1; i++)
            {
                charcount[s[i]] += 1;
            }
            int[][] SA = new int[spe][];
            int[] SAfront = new int[spe];
            int[] SAback = new int[spe];
            for(int i = 0;i <= spe - 1; i++)
            {
                SA[i] = new int[charcount[i]];
                for(int j = 0;j <= charcount[i] - 1; j++)
                {
                    SA[i][j] = -1;
                }
                SAfront[i] = 0;
                SAback[i] = charcount[i] - 1;
            }
            for(int i = n - 2;i >= 0; i--)
            {
                if (s[i] == s[i + 1])
                {
                    LS[i] = LS[i + 1];
                }
                else if (s[i] > s[i + 1])
                {
                    LS[i] = 1;
                }
                else
                {
                    LS[i] = 0;
                }
            }
            for(int i = 1;i <= n - 1; i++)
            {
                if (LS[i] == 0 && LS[i - 1] == 1)
                {
                    LMS[i] = true;
                    LMScount += 1;
                }
            }
            int[] LMSlist = new int[LMScount];
            int memoindex = 0;
            for(int i = 0;i <= n - 1; i++)
            {
                if (LMS[i])
                {
                    LMSlist[memoindex] = i;
                    memoindex += 1;
                }
            }
            for(int i = 0;i <= LMScount - 1; i++)
            {
                if(i != LMScount - 1)
                {
                    LMSsublen[LMSlist[i]] = LMSlist[i + 1] - LMSlist[i];
                }
                else
                {
                    LMSsublen[LMSlist[i]] = 1;
                }
            }
            for(int i = 0;i <= LMScount - 1; i++)
            {
                int index = LMSlist[i];
                SA[s[index]][SAback[s[index]]] = index;
                SAback[s[index]] -= 1;
                used[index] = true;
            }
            for(int i = 0;i <= spe - 1; i++)
            {
                for(int j = 0;j <= charcount[i] - 1; j++)
                {
                    int index = SA[i][j] - 1;
                    if(index >= 0)
                    {
                        if (used[index] == false && LS[index] == 1)
                        {
                            used[index] = true;
                            SA[s[index]][SAfront[s[index]]] = index;
                            SAfront[s[index]] += 1;
                        }
                    }
                }
            }
            for(int i = 1;i <= spe - 1; i++)
            {
                for(int j = 0;j <= charcount[i] - 1; j++)
                {
                    int index = SA[i][j];
                    if (index >= 0)
                    {
                        if (LMS[index])
                        {
                            SA[i][j] = -1;
                            used[index] = false;
                        }
                    }
                }
            }
            for (int i = 0; i <= spe - 1; i++)
            {
                SAback[i] = charcount[i] - 1;
                SAfront[i] = 0;
            }
            for(int i = spe - 1;i >= 0; i--)
            {
                for(int j = charcount[i] - 1;j >= 0; j--)
                {
                    if (SA[i][j] != -1)
                    {
                        int index = SA[i][j] - 1;
                        if(index >= 0)
                        {
                            if (used[index] == false && LS[index] == 0)
                            {
                                SA[s[index]][SAback[s[index]]] = index;
                                SAback[s[index]] -= 1;
                                used[index] = true;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i <= spe - 1; i++)
            {
                SAback[i] = charcount[i] - 1;
            }
            int counter = 0;
            int[] nowsa = new int[1];
            int[] oldsa = new int[1] { -1 };
            int[] LMScounter = new int[n];
            for(int i = 0;i <= spe - 1; i++)
            {
                for(int j = 0;j <= charcount[i] - 1; j++)
                {
                    if (LMS[SA[i][j]])
                    {
                        counter += 1;
                        LMScounter[SA[i][j]] = counter;
                        nowsa = new int[LMSsublen[SA[i][j]] + 1];
                        for(int k = 0;k <= nowsa.Length - 1; k++)
                        {
                            nowsa[k] = -1;
                        }
                        memoindex = 0;
                        for(int k = SA[i][j];k <= n - 1; k++)
                        {
                            nowsa[memoindex] = s[k];
                            memoindex += 1;
                            if(LMS[k] && k != SA[i][j])
                            {
                                break;
                            }
                        }
                        bool same = true;
                        if(nowsa.Length != oldsa.Length)
                        {
                            same = false;
                        }
                        else
                        {
                            for(int k = 0;k <= nowsa.Length - 1; k++)
                            {
                                if (nowsa[k] != oldsa[k])
                                {
                                    same = false;
                                    break;
                                }
                            }
                        }
                        if (same)
                        {
                            counter -= 1;
                            LMScounter[SA[i][j]] = counter;
                        }
                        oldsa = new int[nowsa.Length];
                        for(int k = 0;k <= nowsa.Length - 1; k++)
                        {
                            oldsa[k] = nowsa[k];
                        }
                    }
                }
            }
            int[] news = new int[LMScount];
            int[] LMSindex = new int[LMScount + 1];
            for(int i = 0;i <= LMScount - 1; i++)
            {
                LMSindex[i] = LMSlist[i];
                news[i] = LMScounter[LMSlist[i]];
            }
            int[] newLMSindex = SAIS(news, counter + 1);
            for(int i = 0;i <= n - 1; i++)
            {
                used[i] = false;
            }
            for(int i = 0;i <= spe - 1; i++)
            {
                for(int j = 0;j <= charcount[i] - 1; j++)
                {
                    SA[i][j] = -1;
                }
            }
            for(int i = newLMSindex.Length - 1;i >= 0; i--)
            {
                int value = LMSindex[newLMSindex[i]];
                SA[s[value]][SAback[s[value]]] = value;
                SAback[s[value]] -= 1;
                used[value] = true;
            }
            for(int i = 0;i <= spe - 1; i++)
            {
                for(int j = 0;j <= charcount[i] - 1; j++)
                {
                    int index = SA[i][j] - 1;
                    if(index >= 0)
                    {
                        if (used[index] == false && LS[index] == 1)
                        {
                            used[index] = true;
                            SA[s[index]][SAfront[s[index]]] = index;
                            SAfront[s[index]] += 1;
                        }
                    }
                }
            }
            for(int i = 1;i <= spe - 1; i++)
            {
                for(int j = 0;j <= charcount[i] - 1; j++)
                {
                    int index = SA[i][j];
                    if(index >= 0)
                    {
                        if (LMS[index])
                        {
                            SA[i][j] = -1;
                            used[index] = false;
                        }
                    }
                }
            }
            for (int i = 0; i <= spe - 1; i++)
            {
                SAback[i] = charcount[i] - 1;
                SAfront[i] = 0;
            }
            for (int i = spe - 1; i >= 0; i--)
            {
                for (int j = charcount[i] - 1; j >= 0; j--)
                {
                    if (SA[i][j] != -1)
                    {
                        int index = SA[i][j] - 1;
                        if (index >= 0)
                        {
                            if (used[index] == false && LS[index] == 0)
                            {
                                SA[s[index]][SAback[s[index]]] = index;
                                SAback[s[index]] -= 1;
                                used[index] = true;
                            }
                        }
                    }
                }
            }
            int[] ret = new int[n - 1];
            int retCounter = 0;
            for(int i = 1;i <= spe - 1; i++)
            {
                for(int j = 0;j <= charcount[i] - 1; j++)
                {
                    ret[retCounter] = SA[i][j];
                    retCounter += 1;
                }
            }
            return ret;
        }
        public static int[] ZAlgorithm(string str)
        {
            int n = str.Length;
            int[] ret = new int[n];
            ret[0] = n;
            int i = 1;
            int j = 0;
            while(i < n)
            {
                while(i + j < n && str[j] == str[i + j])
                {
                    j += 1;
                }
                ret[i] = j;
                if(j == 0)
                {
                    i += 1;
                    continue;
                }
                int k = 1;
                while (k < i && k + ret[k] < j)
                {
                    ret[i + k] = ret[k];
                    k += 1;
                }
                i += k;
                j -= k;
            }
            return ret;
        }
        //aがbに含まれていればtrue,そうでなければfalse
        public static bool IsSubstring(string a,string b)
        {
            string check = a + b;
            int firstindex = a.Length;
            int n = check.Length;
            int[] z = ZAlgorithm(check);
            bool ret = false;
            for(int i = firstindex;i <= n - 1; i++)
            {
                if (z[i] >= firstindex)
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }
        public static int[] Manacher(string str)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int n = str.Length;
            int[] ret = new int[n];
            while (i < n)
            {
                while (i - j >= 0 && i + j < n && str[i - j] == str[i + j])
                {
                    j++;
                }
                ret[i] = j;
                k = 1;
                while (i - k >= 0 && k + ret[i - k] < j)
                {
                    ret[i + k] = ret[i - k];
                    k++;
                }
                i += k;
                j -= k;
            }
            return ret;
        }
        // 各文字・各文字の間を中心とした回文の最大長さを求める
        public static int[] PalindromeLength(string s)
        {
            int n = s.Length;
            char[] ns = new char[n * 2 - 1];
            for (int i = 0; i <= n - 1; i++)
            {
                ns[i * 2] = s[i];
                if (i != 0)
                {
                    ns[i * 2 - 1] = '$';
                }
            }
            int[] ret = Manacher(new string(ns));
            n = ret.Length;
            for (int i = 0; i <= n - 1; i++)
            {
                if (i % 2 == 0)
                {
                    ret[i] = ret[i] - (1 - ret[i] % 2);
                }
                else
                {
                    ret[i] = ret[i] - ret[i] % 2;
                }
            }
            return ret;
        }
        // 文字列の LCP Array を返す
        // 最初は 0 で固定、長さは N
        // ASCII が min と max の間に収まる文字列のみ デフォルトでは英小文字・英大文字に対応
        public static int[] LCP(string str, char min = '$', char max = 'z')
        {
            int[] sa = SAIS(str + "$", min, max);
            return LCP(str, sa);
        }
        // 末尾に $ をつけた SA が必要
        public static int[] LCP(string str, int[] sa)
        {
            //Console.WriteLine(String.Join(" ", sa));
            int n = str.Length + 1;
            int[] lcp = new int[n];
            int[] rank = new int[n];
            for (int i = 0; i <= n - 1; i++)
            {
                rank[sa[i]] = i;
            }
            //Console.WriteLine(String.Join(" ", rank));
            int l = 0;
            int j;
            n -= 1;
            for (int i = 0; i <= n - 1; i++)
            {
                j = sa[rank[i] - 1];
                while (i + l < n && j + l < n && str[i + l] == str[j + l])
                {
                    l++;
                }
                lcp[rank[i]] = l;
                l = Math.Max(0, l - 1);
            }
            return lcp;
        }
    }
}
