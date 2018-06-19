using System;
using System.Collections.Generic;

namespace AOJ
{
    class Def
    {
        public const double INF = 1e10d;
        public const double EPS = 1e-10d;
    }
    class AOJ155
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

    class Node : IEquatable<Node>
    {
        public enum Status
        {
            None,
            Open,
            Closed
        }

        public int id;
        public int posX;
        public int posY;
        public double cost;
        public double heuristic;
        public Dictionary<Node, double> edges;
        public Status status;
        public Node parent;

        public double Score
        {
            get { return cost + heuristic; }
        }

        public Node(int id, int posY, int posX)
        {
            this.id = id;
            this.posY = posY;
            this.posX = posX;
            this.edges = new Dictionary<Node, double>();
            Init();
        }

        public void Init()
        {
            cost = Def.INF;
            heuristic = Def.INF;
            status = Status.None;
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

        public void Open(double cost, Node goalNode, Node parent)
        {
            status = Status.Open;
            this.cost = cost;
            if (heuristic == Def.INF)
                heuristic = CulcCost(goalNode);
            this.parent = parent;
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

    class Solver
    {
        public string answer;
        private Node[] nodes;
        private List<Node> openNodes;
        private int nodeNum;

        public Solver(int nodeNum)
        {
            answer = string.Empty;
            nodes = new Node[nodeNum];
            openNodes = new List<Node>();
            this.nodeNum = nodeNum;
        }

        public void ReadData()
        {
            for (int i = 0; i < nodeNum; i++)
            {
                string[] nodeData = Console.ReadLine().Split();
                int id = int.Parse(nodeData[0]) - 1;
                nodes[id] = new Node(id, int.Parse(nodeData[1]), int.Parse(nodeData[2]));
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

                nodes[startIds[i]].Open(0d, nodes[goalIds[i]], null);
                AStar(nodes[startIds[i]], goalIds[i]);

                SetAnswer(startIds[i], goalIds[i]);
                Reset();
            }
        }

        private void Reset()
        {
            openNodes = new List<Node>();
            foreach (Node node in nodes)
            {
                node.Init();
            }
        }

        private bool AStar(Node currentNode, int goalId)
        {
            currentNode.status = Node.Status.Closed;

            if (currentNode.id == goalId)
                return true;

            foreach (var edge in currentNode.edges)
            {
                Node linkedNode = edge.Key;
                if (linkedNode.status == Node.Status.Closed)
                    continue;
                
                double cost = currentNode.cost + edge.Value;

                if (linkedNode.cost < cost)
                    continue;

                if (linkedNode.status == Node.Status.None)
                    openNodes.Add(linkedNode);

                linkedNode.Open(cost, nodes[goalId], currentNode);
            }

            while (openNodes.Count > 0)
            {
                Node minScoreNode = GetMinScoreNodeFromOpenNodes();
                openNodes.Remove(minScoreNode);
                if (AStar(minScoreNode, goalId))
                    return true;
            }
            return false;
        }

        private Node GetMinScoreNodeFromOpenNodes()
        {
            Node minScoreNode = null;

            foreach (Node node in openNodes)
            {
                if (minScoreNode == null)
                {
                    minScoreNode = node;
                    continue;
                }

                if (node.Score > minScoreNode.Score)
                    continue;
                
                if (minScoreNode.Score - node.Score < Def.EPS
                    && node.cost >= minScoreNode.cost)
                    continue;
                
                minScoreNode = node;
            }
            return minScoreNode;
        }

        private void SetAnswer(int startId, int goalId)
        {
            if (startId == goalId)
            {
                answer += (startId + 1) + "\n";
                return;
            }

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

            if (currentNode.id != startId)
                tmpAnswer = "NA";
            
            answer += tmpAnswer.Trim() + "\n";
        }
    }
}
