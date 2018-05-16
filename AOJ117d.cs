using System;

namespace AOJ
{
    /// <summary>
    /// 賢い人のほぼコピペソース
    /// </summary>
    class AOJ117d
    {
        const int INF = int.MaxValue / 3;
        static int[,] costs;

        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);

            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());

            costs = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i == j) costs[i, j] = 0;
                    else costs[i, j] = INF;
            
            for (int c = 0; c < e; c++)
            {
                int[] inputs3_e = Array.ConvertAll<string, int>(Console.ReadLine().Trim().Split(','), int.Parse);
                int a = inputs3_e[0] - 1;
                int b = inputs3_e[1] - 1;
                int costAtoB = inputs3_e[2];
                int costBtoA = inputs3_e[3];
                costs[a, b] = costAtoB;
                costs[b, a] = costBtoA;
            }

            int[] inputs = Array.ConvertAll<string, int>(Console.ReadLine().Trim().Split(','), int.Parse);
            int s = inputs[0] - 1;
            int g = inputs[1] - 1;
            int total = inputs[2];
            int price = inputs[3];
            CulcAllCost(n);
            int rewards = total - price - costs[s, g] - costs[g, s];
            Console.WriteLine(rewards);
            Console.ReadLine();
        }
        
        /// <summary>
        /// ノード間の到達コスト表を全計算
        /// </summary>
        /// <param name="n"></param>
        static void CulcAllCost(int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        costs[j, k] = Math.Min(costs[j, k], costs[j, i] + costs[i, k]);
        }
        
    }
}
