using System;

namespace AOJ
{
    class AOJ117b
    {
        const int NAN = int.MinValue / 3;
        const int INF = int.MaxValue / 3;
        static int[,] pathCosts;

        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);

            int tawnNum = int.Parse(Console.ReadLine());
            int pathNum = int.Parse(Console.ReadLine());

            pathCosts = new int[tawnNum, tawnNum];
            for (int i = 0; i < tawnNum; i++)
                for (int j = 0; j < tawnNum; j++)
                    pathCosts[i, j] = NAN;
            
            for (int n = 0; n < pathNum; n++)
            {
                int[] inputs = Array.ConvertAll<string, int>(Console.ReadLine().Trim().Split(','), int.Parse);
                int tawnIdA = inputs[0] - 1;
                int tawnIdB = inputs[1] - 1;
                int costAtoB = inputs[2];
                int costBtoA = inputs[3];

                pathCosts[tawnIdA, tawnIdB] = costAtoB;
                pathCosts[tawnIdB, tawnIdA] = costBtoA;
            }

            int[] lastInputs = Array.ConvertAll<string, int>(Console.ReadLine().Trim().Split(','), int.Parse);
            int startTawnId = lastInputs[0] - 1;
            int goalTawnId = lastInputs[1] - 1;
            int totalBudget = lastInputs[2];
            int polePrice = lastInputs[3];

            // 変数名で速度に差が出る（passedTawns -> passed : 00:02s -> 00:01s
            bool[] passedTowns = new bool[tawnNum];

            int trafficCost = 0;
            trafficCost += CulcCost(startTawnId, goalTawnId, 0, passedTowns);
            trafficCost += CulcCost(goalTawnId, startTawnId, 0, passedTowns);

            string output = (totalBudget - polePrice - trafficCost).ToString();
            Console.WriteLine(output);
            Console.ReadLine();
        }
        
        static int CulcCost(int current, int goal, int currentTotalCost, bool[] passedTowns)
        {
            if (current == goal)
            {
                /*
                string output = currentTotalCost.ToString() + ": ";
                for (int i = 0; i < passed.Length; i++)
                {
                    if (passed[i])
                        output += i.ToString() + " ";
                }
                Console.WriteLine(output);
                */
                return currentTotalCost;
            }
            bool[] newPassedTowns = (bool[])passedTowns.Clone();
            newPassedTowns[current] = true;

            int cheapestCost = INF;
            for (int i = 0; i < newPassedTowns.Length; i++)
            {
                if (pathCosts[current, i] == NAN || newPassedTowns[i])
                    continue;
                int totalCost = currentTotalCost + pathCosts[current, i];
                totalCost = CulcCost(i, goal, totalCost, newPassedTowns);

                cheapestCost = Math.Min(cheapestCost, totalCost);
            }
            return cheapestCost;
        }
    }
}
