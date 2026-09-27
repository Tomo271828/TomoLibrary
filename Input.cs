using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoLibrary
{
    public static class Input
    {
        public static int[] IntArray()
        {
            string str = Console.ReadLine();
            if(str == "")
            {
                return new int[0];
            }
            return str.Split(" ").Select(int.Parse).ToArray();
        }
        public static long[] LongArray()
        {
            string str = Console.ReadLine();
            if (str == "")
            {
                return new long[0];
            }
            return str.Split(" ").Select(long.Parse).ToArray();
        }
        public static float[] FloatArray()
        {
            string str = Console.ReadLine();
            if (str == "")
            {
                return new float[0];
            }
            return str.Split(" ").Select(float.Parse).ToArray();
        }
        public static double[] DoubleArray()
        {
            string str = Console.ReadLine();
            if (str == "")
            {
                return new double[0];
            }
            return str.Split(" ").Select(double.Parse).ToArray();
        }
        public static decimal[] DecimalArray()
        {
            string str = Console.ReadLine();
            if (str == "")
            {
                return new decimal[0];
            }
            return str.Split(" ").Select(decimal.Parse).ToArray();
        }
        public static ulong[] UlongArray()
        {
            string str = Console.ReadLine();
            if (str == "")
            {
                return new ulong[0];
            }
            return str.Split(" ").Select(ulong.Parse).ToArray();
        }
        public static string[] StringArray()
        {
            string memo = Console.ReadLine();
            string[] arr = new string[memo.Length];
            for(int i = 0;i <= memo.Length - 1; i++)
            {
                arr[i] = memo.Substring(i, 1);
            }
            return arr;
        }
        public static char[,] CharMap(int h,int w)
        {
            char[,] map = new char[h, w];
            for(int i = 0;i <= h - 1; i++)
            {
                char[] input = Console.ReadLine().ToCharArray();
                for(int j = 0;j <= w - 1; j++)
                {
                    map[i, j] = input[j];
                }
            }
            return map;
        }
        public static string[,] StringMap(int h,int w)
        {
            string[,] map = new string[h, w];
            for (int i = 0; i <= h - 1; i++)
            {
                string[] input = StringArray();
                for (int j = 0; j <= w - 1; j++)
                {
                    map[i, j] = input[j];
                }
            }
            return map;
        }
        public static int[,] IntMap(int h,int w)
        {
            int[,] map = new int[h, w];
            for(int i = 0;i <= h - 1; i++)
            {
                int[] input = IntArray();
                for(int j = 0;j <= w - 1; j++)
                {
                    map[i, j] = input[j];
                }
            }
            return map;
        }
        public static long[,] LongMap(int h, int w)
        {
            long[,] map = new long[h, w];
            for (int i = 0; i <= h - 1; i++)
            {
                long[] input = LongArray();
                for (int j = 0; j <= w - 1; j++)
                {
                    map[i, j] = input[j];
                }
            }
            return map;
        }
        public static int[][] IntJag(int n)
        {
            int[][] ret = new int[n][];
            for(int i = 0;i <= n - 1; i++)
            {
                ret[i] = IntArray();
            }
            return ret;
        }
        public static long[][] LongJag(int n)
        {
            long[][] ret = new long[n][];
            for (int i = 0; i <= n - 1; i++)
            {
                ret[i] = LongArray();
            }
            return ret;
        }
        public static double[][] DoubleJag(int n)
        {
            double[][] ret = new double[n][];
            for (int i = 0; i <= n - 1; i++)
            {
                ret[i] = DoubleArray();
            }
            return ret;
        }
        public static decimal[][] DecimalJag(int n)
        {
            decimal[][] ret = new decimal[n][];
            for (int i = 0; i <= n - 1; i++)
            {
                ret[i] = DecimalArray();
            }
            return ret;
        }
        public static char[][] CharJag(int n)
        {
            char[][] ret = new char[n][];
            for (int i = 0; i <= n - 1; i++)
            {
                ret[i] = Console.ReadLine().ToCharArray();
            }
            return ret;
        }
        public static List<int> IntList()
        {
            int[] input = IntArray();
            List<int> ret = new List<int>();
            for(int i = 0;i <= input.Length - 1; i++)
            {
                ret.Add(input[i]);
            }
            return ret;
        }
        public static List<long> LongList()
        {
            long[] input = LongArray();
            List<long> ret = new List<long>();
            for (int i = 0; i <= input.Length - 1; i++)
            {
                ret.Add(input[i]);
            }
            return ret;
        }
        public static List<float> FloatList()
        {
            float[] input = FloatArray();
            List<float> ret = new List<float>();
            for (int i = 0; i <= input.Length - 1; i++)
            {
                ret.Add(input[i]);
            }
            return ret;
        }
        public static List<double> DoubleList()
        {
            double[] input = DoubleArray();
            List<double> ret = new List<double>();
            for (int i = 0; i <= input.Length - 1; i++)
            {
                ret.Add(input[i]);
            }
            return ret;
        }
        public static List<decimal> DecimalList()
        {
            decimal[] input = DecimalArray();
            List<decimal> ret = new List<decimal>();
            for (int i = 0; i <= input.Length - 1; i++)
            {
                ret.Add(input[i]);
            }
            return ret;
        }
        public static List<string> StringList()
        {
            string[] input = Console.ReadLine().Split(" ");
            List<string> ret = new List<string>();
            for (int i = 0; i <= input.Length - 1; i++)
            {
                ret.Add(input[i]);
            }
            return ret;
        }
        public static List<char> CharList()
        {
            char[] input = Console.ReadLine().ToCharArray();
            List<char> ret = new List<char>();
            for (int i = 0; i <= input.Length - 1; i++)
            {
                ret.Add(input[i]);
            }
            return ret;
        }
    }
}
