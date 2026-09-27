using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomoLibrary.Graph;

namespace TomoLibrary.ML
{
    public class TwoSat
    {
        int n;
        WeightedDirectedGraph graph;
        // n個の変数がある 2-Sat を生成
        // x_1, x_2, ... , x_n
        public TwoSat(int n)
        {
            this.n = n;
            graph = new WeightedDirectedGraph(n * 2 + 2);
        }
        // 条件に (x_i OR x_j) を追加
        // i と j を -1 倍した場合、それぞれの否定を表す
        public void AddCondition(int i, int j)
        {
            AddEdge(i * -1, j);
            AddEdge(j * -1, i);
        }
        void AddEdge(int i, int j)
        {
            if (i < 0)
            {
                i = i * -2 + 1;
            }
            else
            {
                i = i * 2;
            }
            if (j < 0)
            {
                j = j * -2 + 1;
            }
            else
            {
                j = j * 2;
            }
            //Console.WriteLine((i, j));
            graph.AddEdge(i, j, 1);
        }
        // 2-Sat を解く
        // 解が存在する場合は解の一つを求め、 i 番目の要素が x_{i+1} となるような配列を返す
        // 解が存在しない場合は null を返す
        public bool[] Solve()
        {
            int[] index = new int[n * 2 + 2];
            List<int[]> scc = graph.SCC();
            for (int i = scc.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j <= scc[i].Length - 1; j++)
                {
                    index[scc[i][j]] = i;
                }
            }
            for (int i = 1; i <= n; i++)
            {
                if (index[i * 2] == index[i * 2 + 1])
                {
                    return null;
                }
            }
            bool[] ret = new bool[n];
            for (int i = 1; i <= n; i++)
            {
                if (index[i * 2] < index[i * 2 + 1])
                {
                    ret[i - 1] = false;
                }
                else
                {
                    ret[i - 1] = true;
                }
            }
            return ret;
        }
    }
}
