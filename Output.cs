using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary
{
    public static class Output
    {
        public static void YesNo(bool answer)
        {
            if (answer)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }
        public static void YESNO(bool answer)
        {
            if (answer)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
        public static void TakahashiAoki(bool answer)
        {
            if (answer)
            {
                Console.WriteLine("Takahashi");
            }
            else
            {
                Console.WriteLine("Aoki");
            }
        }
        public static void IntMap(int[,] answer)
        {
            int h = answer.GetLength(0);
            int w = answer.GetLength(1);
            for (int i = 0; i <= h - 1; i++)
            {
                int[] memo = new int[w];
                for (int j = 0; j <= w - 1; j++)
                {
                    memo[j] = answer[i, j];
                }
                Console.WriteLine(String.Join(" ",memo));
            }
        }
        public static void LongMap(long[,] answer)
        {
            int h = answer.GetLength(0);
            int w = answer.GetLength(1);
            for (int i = 0; i <= h - 1; i++)
            {
                long[] memo = new long[w];
                for (int j = 0; j <= w - 1; j++)
                {
                    memo[j] = answer[i, j];
                }
                Console.WriteLine(String.Join(" ", memo));
            }
        }
        public static void CharMap(char[,] answer)
        {
            int h = answer.GetLength(0);
            int w = answer.GetLength(1);
            for(int i = 0;i <= h - 1; i++)
            {
                char[] memo = new char[w];
                for(int j = 0;j <= w - 1; j++)
                {
                    memo[j] = answer[i, j];
                }
                Console.WriteLine(new string(memo));
            }
        }
        public static void BoolMapOX(bool[,] answer)
        {
            int h = answer.GetLength(0);
            int w = answer.GetLength(1);
            for (int i = 0; i <= h - 1; i++)
            {
                char[] memo = new char[w];
                for (int j = 0; j <= w - 1; j++)
                {
                    if (answer[i, j])
                    {
                        memo[j] = 'o';
                    }
                    else
                    {
                        memo[j] = 'x';
                    }
                }
                Console.WriteLine(new string(memo));
            }
        }
    }
}
