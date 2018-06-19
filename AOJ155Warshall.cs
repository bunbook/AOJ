using System;

namespace AOJ
{
    class Def
    {
        public const double INF = 1e10d;
    }

    // ワーシャルフロイド
    class AOJ155Warshall
    {
        static void Main(string[] args)
        {
            var exStdIn = new System.IO.StreamReader("stdin.txt");
            Console.SetIn(exStdIn);

            while (true)
            {
                int nodeNum = int.Parse(Console.ReadLine());
                if (nodeNum == 0)
                    break;

                Solver solver = new Solver(nodeNum);
                solver.ReadData();
                solver.Solve();
            }
            Console.ReadLine();
        }
    }

    class Node
    {
        public int id;
        public int posX;
        public int posY;

        public Node(int id, int posY, int posX)
        {
            this.id = id;
            this.posY = posY;
            this.posX = posX;
        }
    }

    class Solver
    {
        public string answer;
        private Node[] nodes;
        private int nodeNum;

        private double[,] dp;
        private int[,] next;

        public Solver(int nodeNum)
        {
            answer = string.Empty;
            nodes = new Node[nodeNum];
            this.nodeNum = nodeNum;

            dp = new double[nodeNum, nodeNum];
            next = new int[nodeNum, nodeNum];
        }

        public void ReadData()
        {
            for (int i = 0; i < nodeNum; i++)
            {
                string[] nodeData = Console.ReadLine().Split();
                int id = int.Parse(nodeData[0]) - 1;
                nodes[id] = new Node(id, int.Parse(nodeData[1]), int.Parse(nodeData[2]));
            }
            
            for (int i = 0; i < nodeNum; i++)
            {
                for (int j = 0; j < nodeNum; j++)
                {
                    next[i, j] = j;
                    if (i == j) dp[i, j] = 0;
                    else
                    {
                        int dx = nodes[j].posX - nodes[i].posX;
                        int dy = nodes[j].posY - nodes[i].posY;


                        double cost = dx * dx + dy * dy;
                        if (cost > 2500d)
                            cost = Def.INF;
                        else
                            cost = Math.Sqrt(cost);
                        dp[i, j] = cost;
                    }
                }
            }
        }

        public void GoWarshall()
        {
            var n = nodeNum;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (dp[j, i] + dp[i, k] < dp[j, k])
                        {
                            dp[j, k] = dp[j, i] + dp[i, k];
                            next[j, k] = next[j, i];
                        }
                    }
                }
            }
        }

        public void PutAnswer(int s, int g)
        {
            if (s == g)
            {
                answer += (s + 1) + "\n";
                Console.Write(answer);
                answer = string.Empty;
                return;
            }
            if (next[s, g] == g && dp[s, g] > 50d)
            {
                answer += "NA\n";
                Console.Write(answer);
                answer = string.Empty;
                return;
            }
            answer += (s + 1) + " ";
            while (true)
            {
                var ns = next[s, g];
                if (ns == g)
                {
                    answer += (g + 1) + "\n";
                    Console.Write(answer);
                    answer = string.Empty;
                    break;
                }
                else answer += (ns + 1) + " ";
                s = ns;
            }
        }

        public void Solve()
        {
            GoWarshall();

            int solveNum = int.Parse(Console.ReadLine());
            int[] startIds = new int[solveNum];
            int[] goalIds = new int[solveNum];

            for (int i = 0; i < solveNum; i++)
            {
                string[] startGoalIds = Console.ReadLine().Split();
                startIds[i] = int.Parse(startGoalIds[0]) - 1;
                goalIds[i] = int.Parse(startGoalIds[1]) - 1;

                PutAnswer(startIds[i], goalIds[i]);
            }
        }
    }
}
