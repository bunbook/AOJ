using System;
using System.Collections.Generic;

namespace AOJ
{ 

    class Def
    {
        public const double INF = 1e10d;
    }

    class AOJ155Dijkstra
    {
        static void Main(string[] args)
        {
            var exStdIn = new System.IO.StreamReader("stdin.txt");
            Console.SetIn(exStdIn);

            string output = string.Empty;
            while (true)
            {
                int nodeNum = int.Parse(Console.ReadLine());
                if (nodeNum == 0)
                    break;

                Solver solver = new Solver(nodeNum);
                solver.ReadData();
                solver.Solve();
                output += solver.answer;
            }
            Console.WriteLine(output.Trim());
            Console.ReadLine();
        }
    }

    class Solver
    {
        public string answer;
        private Node[] nodes;
        private int nodeNum;

        public Solver(int nodeNum)
        {
            answer = string.Empty;
            nodes = new Node[nodeNum];
            this.nodeNum = nodeNum;
        }

        public void ReadData()
        {
            for (int i = 0; i < nodeNum; i++)
            {
                string[] nodeData = Console.ReadLine().Split();
                nodes[int.Parse(nodeData[0]) - 1] = new Node(int.Parse(nodeData[0]) - 1, int.Parse(nodeData[1]), int.Parse(nodeData[2]));
            }

            for (int i = 0; i < nodeNum - 1; i++)
            {
                for (int j = i + 1; j < nodeNum; j++)
                {
                    nodes[i].SetEdge(nodes[j]);
                }
            }
        }

        public void Solve()
        {
            int solveNum = int.Parse(Console.ReadLine());
            int[] startIds = new int[solveNum];
            int[] goalIds = new int[solveNum];

            for (int i = 0; i < solveNum; i++)
            {
                string[] startGoalIds = Console.ReadLine().Split();
                startIds[i] = int.Parse(startGoalIds[0]) - 1;
                goalIds[i] = int.Parse(startGoalIds[1]) - 1;

                Dijkstra(startIds[i], goalIds[i]);

                SetAnswer(startIds[i], goalIds[i]);
                Reset();
            }
        }

        private void Reset()
        {
            for (int i = 0; i < nodeNum; i++)
            {
                nodes[i].cost = Def.INF;
                nodes[i].done = false;
                nodes[i].parent = null;
            }
        }

        // ダイクストラ
        private void Dijkstra(int startId, int goalId)
        {
            nodes[startId].cost = 0d;

            while (true)
            {
                Node currentNode = null;

                for (int i = 0; i < nodeNum; i++)
                {
                    Node node_i = nodes[i];

                    if (node_i.done || node_i.cost > Def.INF)
                        continue;

                    if (currentNode == null)
                        currentNode = node_i;

                    if (node_i.cost < currentNode.cost)
                        currentNode = node_i;
                }

                if (currentNode == null)
                    return;
                if (currentNode.id == goalId)
                    return;

                currentNode.done = true;
                
                // 指定ノードとエッジで繋がれている（リンク先の）各ノードのコストを更新
                foreach (var edge in currentNode.edges)
                {
                    Node linkedNode = edge.Key;

                    // リンク先のノードが処理済みなら飛ばす
                    if (linkedNode.done)
                        continue;

                    // リンク先のノードのコストが、現在の指定ノード + エッジのコストよりも小さいなら
                    // 交換不要なので飛ばす
                    if (linkedNode.cost < currentNode.cost + edge.Value)
                        continue;

                    // リンク先のノードを以下のように変更
                    // 　コスト -> 現在の指定ノード + エッジのコスト
                    // 　親 -> 現在の指定ノード
                    linkedNode.cost = currentNode.cost + edge.Value;
                    linkedNode.parent = currentNode;
                }
            }
        }

        private void SetAnswer(int startId, int goalId)
        {
            string tmpAnswer = string.Empty;
            Node currentNode = nodes[goalId];

            if (currentNode.parent == null)
            {
                answer += "NA\n";
                return;
            }

            while (currentNode.parent != null)
            {
                tmpAnswer = " " + (currentNode.id + 1) + tmpAnswer;
                currentNode = currentNode.parent;
            }
            tmpAnswer = (currentNode.id + 1) + tmpAnswer;
            
            answer += tmpAnswer.Trim() + "\n";
        }
    }

    class Node : IEquatable<Node>
    {
        public int id;
        public int posX;
        public int posY;
        public double cost;
        public Dictionary<Node, double> edges;
        public bool done;
        public Node parent;
        
        public Node(int id, int posY, int posX)
        {
            this.id = id;
            this.posY = posY;
            this.posX = posX;

            cost = Def.INF;

            edges = new Dictionary<Node, double>();
            done = false;
            parent = null;
        }

        public void SetEdge(Node linkedNode)
        {
            double edgeCost = CulcCost(linkedNode);
            double MAX = 50d;
            if (edgeCost > MAX)
                return;
            edges.Add(linkedNode, edgeCost);
            linkedNode.edges.Add(this, edgeCost);
        }

        public double CulcCost(Node linkedNode)
        {
            int dx = posX - linkedNode.posX;
            int dy = posY - linkedNode.posY;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            return "Node " + id + ": " + cost;
        }

        public bool Equals(Node other)
        {
            return id == other.id;
        }
    }
}
