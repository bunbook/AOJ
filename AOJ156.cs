using System;

namespace AOJ
{
    class Utl
    {
        public static readonly int[] dx = { 0, 1, 0, -1 };
        public static readonly int[] dy = { 1, 0, -1, 0 };
    }

    class AOJ156
    {
        
        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);
            
            string output = "";

            while(true)
            {
                int[] mapSize = Array.ConvertAll<string, int>(Console.ReadLine().Split(), int.Parse);
                if (mapSize[0] == 0 & mapSize[1] == 0)
                    break;

                Map map = new Map(mapSize[1], mapSize[0]);
                map.ReadStatus();
                
                Ninja ninja = new Ninja(map);
                ninja.Search();
                output += ninja.OverMoatsCount + "\n";
            }

            Console.Write(output);
            Console.ReadLine();
        }
        
    }

    class Ninja
    {
        public const int NONE = -99;
        public const int NEXT = 0;

        Map map;
        int[,] areaMemo;
        public int currentAreaCount;

        public int OverMoatsCount
        {
            get { return currentAreaCount / 2; }
        }

        public Ninja(Map m)
        {
            map = m;
            currentAreaCount = 0;
            InitMemo();
        }

        private void InitMemo()
        {
            areaMemo = new int[map.sizeHW[0], map.sizeHW[1]];
            for (int i = 0; i < map.sizeHW[0]; i++)
            {
                for (int j = 0; j < map.sizeHW[1]; j++)
                {
                    areaMemo[i, j] = NONE;
                }
            }
            areaMemo[map.startPosYX[0], map.startPosYX[1]] = NEXT;
        }
        
        
        public void Search()
        {
            char targetStatus = Map.PATH;

            while(true)
            {
                currentAreaCount++;
                
                for (int i = 1; i < map.sizeHW[0] - 1; i++)
                {
                    for (int j = 1; j < map.sizeHW[1] - 1; j++)
                    {
                        if (areaMemo[i, j] != NEXT)
                            continue;

                        if (map.status[i, j] != targetStatus && map.status[i, j] != Map.START)
                            continue;

                        if (DFS(new int[2] { i, j }, targetStatus))
                            return;
                    }
                }
                if (currentAreaCount % 2 == 0)
                    targetStatus = Map.PATH;
                else
                    targetStatus = Map.WALL;

                // Console.WriteLine(map.ToString(areaMemo));
            }
        }

        private bool DFS(int[] posYX, char targetStatus)
        {
            if (areaMemo[posYX[0], posYX[1]] != NONE && areaMemo[posYX[0], posYX[1]] != NEXT)
                return false;

            areaMemo[posYX[0], posYX[1]] = currentAreaCount;
            // Console.WriteLine(map.ToString(areaMemo));

            for (int i = 0; i < 4; i++)
            {
                int posY = posYX[0] + Utl.dy[i];
                int posX = posYX[1] + Utl.dx[i];

                if (areaMemo[posY, posX] != NONE && areaMemo[posY, posX] != NEXT)
                    continue;

                if (map.status[posY, posX] == targetStatus)
                {
                    if (DFS(new int[2] { posY, posX }, targetStatus))
                        return true;
                }
                else if (map.status[posY, posX] == Map.GOAL)
                    return true;
                else if (areaMemo[posY, posX] == NONE)
                    areaMemo[posY, posX] = NEXT;
            }
            return false;
        }

    }

    class Map
    {
        public static readonly char WALL = '#';
        public static readonly char START = '&';
        public static readonly char PATH = '.';
        public static readonly char GOAL = 'w';

        public int[] sizeHW;
        public char[,] status;
        public int[] startPosYX;

        public Map(int h, int w)
        {
            sizeHW = new int[2]{ h + 2, w + 2 };
            status = new char[h + 2, w + 2];
            Init();
        }

        private void Init()
        {
            int posX = 0;
            int posY = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < sizeHW[i % 2] - 1; j++)
                {
                    status[posY, posX] = GOAL;
                    posX += Utl.dx[i];
                    posY += Utl.dy[i];
                }
            }
        }

        public void ReadStatus()
        {
            for (int i = 1; i < sizeHW[0] - 1; i++)
            {
                string line = Console.ReadLine();
                for (int j = 1; j < sizeHW[1] - 1; j++)
                {
                    status[i, j] = line[j - 1];
                    if (status[i, j] == START)
                        startPosYX = new int[2] { i, j }; 
                }
            }
        }
        
        public override string ToString()
        {
            string output = string.Empty;
            for (int i = 0; i < sizeHW[0]; i++)
            {
                for (int j = 0; j < sizeHW[1]; j++)
                {
                    output += status[i, j];
                }
                output += "\n";
            }
            return output;
        }

        public string ToString(int[,] areaMemo)
        {
            string output = string.Empty;
            for (int i = 0; i < sizeHW[0]; i++)
            {
                for (int j = 0; j < sizeHW[1]; j++)
                {
                    if (areaMemo[i, j] == Ninja.NONE)
                        output += status[i, j];
                    else
                        output += areaMemo[i, j];
                }
                output += "\n";
            }
            return output;
        }
    }
}
