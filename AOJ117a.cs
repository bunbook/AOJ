using System;
using System.Collections.Generic;

namespace AOJ
{
    class AOJ117a
    {
        static void Main(string[] args)
        {
            var exStdIn = new System.IO.StreamReader("stdin.txt");
            Console.SetIn(exStdIn);

            int tawnNum = int.Parse(Console.ReadLine());
            int pathNum = int.Parse(Console.ReadLine());

            Carpenter carpenter = new Carpenter(tawnNum);
            for (int n = 0; n < pathNum; n++)
            {
                int[] inputs = Array.ConvertAll<string, int>(Console.ReadLine().Trim().Split(','), int.Parse);
                int tawnIdA = inputs[0] - 1;
                int tawnIdB = inputs[1] - 1;
                int costAtoB = inputs[2];
                int costBtoA = inputs[3];

                carpenter.tawnMaps[tawnIdA].trafficCosts.Add(carpenter.tawnMaps[tawnIdB], costAtoB);
                carpenter.tawnMaps[tawnIdB].trafficCosts.Add(carpenter.tawnMaps[tawnIdA], costBtoA);
            }

            int[] lastInputs = Array.ConvertAll<string, int>(Console.ReadLine().Trim().Split(','), int.Parse);
            int startTawnId = lastInputs[0] - 1;
            int goalTawnId = lastInputs[1] - 1;
            int totalBudget = lastInputs[2];
            int polePrice = lastInputs[3];

            string output = carpenter.GetRewards(startTawnId, goalTawnId, totalBudget, polePrice).ToString();

            Console.WriteLine(output);
            Console.ReadLine();
        }
        
    }

    public class Carpenter
    {
        private const int INF = int.MaxValue / 3;

        public TawnNode[] tawnMaps;

        public Carpenter(int tawnNum)
        {
            tawnMaps = new TawnNode[tawnNum];
            for (int i = 0; i < tawnNum; i++)
            {
                tawnMaps[i] = new TawnNode(i);
            }
        }

        public int GetRewards(int startTawnId, int goalTawnId, int totalBudget, int polePrice)
        {
            int trafficCost = 0;
            trafficCost += SearchCheapestCost(tawnMaps[startTawnId], 0, new List<TawnNode>(), goalTawnId);
            trafficCost += SearchCheapestCost(tawnMaps[goalTawnId], 0, new List<TawnNode>(), startTawnId);
            return totalBudget - polePrice - trafficCost;
        }

        private int SearchCheapestCost(TawnNode currentTawn, int currentCost, List<TawnNode> passedTawns, int goalTawnId)
        {
            if (currentTawn.id == goalTawnId)
            {
                /*
                string output = currentCost.ToString() + ": ";
                foreach (var town in passedTawns)
                {
                    output += town.ToString() + " ";
                }
                Console.WriteLine(output);
                */
                return currentCost;
            }
            List<TawnNode> newPassedTawns = new List<TawnNode>(passedTawns);
            newPassedTawns.Add(currentTawn);

            int cheapestCost = INF;
            foreach (var trafficCost in currentTawn.trafficCosts)
            {
                TawnNode nextTawn = trafficCost.Key;
                
                if (passedTawns.Contains(nextTawn))
                    continue;

                int totalCost = currentCost + trafficCost.Value;
                totalCost = SearchCheapestCost(nextTawn, totalCost, newPassedTawns, goalTawnId);
                
                cheapestCost = Math.Min(cheapestCost, totalCost);
            }
            return cheapestCost;
        }
    }


    public class TawnNode: IEquatable<TawnNode>
    {
        public int id;

        public Dictionary<TawnNode, int> trafficCosts;

        public TawnNode(int id)
        {
            this.id = id;
            this.trafficCosts = new Dictionary<TawnNode, int>();
        }

        public bool Equals(TawnNode other)
        {
            return this.id == other.id;
        }
        
        public override string ToString()
        {
            return "Tawn" + (id + 1).ToString();
        }
    }
}
