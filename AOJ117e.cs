using System;
using System.Collections.Generic;

namespace AOJ
{
    class Def
    {
        public const int INF = int.MaxValue / 3;
    }

    /// <summary>
    /// ダイクストラの典型例だったらしいので組んだ
    /// </summary>
    class AOJ117e
    {
        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);

            int nodeNum = int.Parse(Console.ReadLine());
            int edgeNum = int.Parse(Console.ReadLine());

            Carpenter carpenter = new Carpenter(nodeNum);
            for (int n = 0; n < edgeNum; n++)
            {
                int[] inputs = Array.ConvertAll<string, int>(Console.ReadLine().Trim().Split(','), int.Parse);
                int a = inputs[0] - 1;
                int b = inputs[1] - 1;
                int costAtoB = inputs[2];
                int costBtoA = inputs[3];

                carpenter.tawnNodes[a].edges.Add(carpenter.tawnNodes[b], costAtoB);
                carpenter.tawnNodes[b].edges.Add(carpenter.tawnNodes[a], costBtoA);
            }

            int[] lastInputs = Array.ConvertAll<string, int>(Console.ReadLine().Trim().Split(','), int.Parse);
            int start = lastInputs[0] - 1;
            int goal = lastInputs[1] - 1;
            int total = lastInputs[2];
            int price = lastInputs[3];

            string output = carpenter.GetRewards(nodeNum, start, goal, total, price).ToString();

            Console.WriteLine(output);
            Console.ReadLine();
        }
        
    }

    public class Carpenter
    {
        public TawnNode[] tawnNodes;

        public Carpenter(int nodeNum)
        {
            tawnNodes = new TawnNode[nodeNum];
            for (int i = 0; i < nodeNum; i++)
            {
                tawnNodes[i] = new TawnNode(i);
            }
        }

        private void InitNode()
        {
            foreach (TawnNode node in tawnNodes)
            {
                node.cost = Def.INF;
                node.done = false;
            }
        }

        public int GetRewards(int nodeNum, int start, int goal, int total, int price)
        {
            int cost = 0;

            Dijkstra(nodeNum, start, goal);
            cost += tawnNodes[goal].cost;

            InitNode();

            Dijkstra(nodeNum, goal, start);
            cost += tawnNodes[start].cost;

            return total - price - cost;
        }

        private void Dijkstra(int nodeNum, int start, int goal)
        {
            tawnNodes[start].cost = 0;

            while (true)
            {
                TawnNode currentNode = null;

                for (int i = 0; i < nodeNum; i++)
                {
                    TawnNode node = tawnNodes[i];
                    if (node.cost == Def.INF || node.done)
                        continue;

                    if (currentNode == null)
                        currentNode = node;

                    if (currentNode.cost > node.cost)
                        currentNode = node;
                }

                if (currentNode == null || currentNode.id == goal)
                    break;

                currentNode.done = true;

                foreach (var edge in currentNode.edges)
                {
                    TawnNode linkedNode = edge.Key;
                    if (linkedNode.done)
                        continue;
                    linkedNode.cost = Math.Min(linkedNode.cost, currentNode.cost + edge.Value);
                }
            }
        }
    }

    public class TawnNode
    {
        public int id;
        public int cost;
        public bool done;

        public Dictionary<TawnNode, int> edges;

        public TawnNode(int id)
        {
            this.id = id;
            this.cost = Def.INF;
            this.done = false;
            this.edges = new Dictionary<TawnNode, int>();
        }
        
        public override string ToString()
        {
            return "Tawn" + (id + 1).ToString();
        }
    }
}
