using System;
using System.Collections.Generic;

namespace AOJ
{
    class AOJ2000
    {
        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);
            string outputStr = "";

            while (true)
            {
                int allGemNum = int.Parse(Console.ReadLine());
                if (allGemNum == 0)
                    break;

                Robot robot = new Robot();
                for (int n = 0; n < allGemNum; n++)
                {
                    string[] gemPos = (Console.ReadLine()).Split();
                    robot.SetGemPosInMap(int.Parse(gemPos[0]), int.Parse(gemPos[1]));
                }

                int comandNum = int.Parse(Console.ReadLine());
                for (int n = 0; n < comandNum; n++)
                {
                    string[] comand = (Console.ReadLine()).Split();
                    robot.SetMoveComand(comand[0], int.Parse(comand[1]));
                }

                robot.SearchGem();
                outputStr += robot.Answer + "\n";
            }

            // exStdIn.Close();
            Console.Write(outputStr);
            Console.ReadLine();
        }
        
    }

    public class Robot
    {
        const int mapSizeX = 21;
        const int mapSizeY = 21;
        const int initialPosX = 10;
        const int initialPosY = 10;

        int[] pos;
        int[,] map;
        int allGemNum;
        List<int[]> moveComands;

        Dictionary<string, int> dirs = new Dictionary<string, int>{ { "N", 0}, { "E", 1 }, { "S", 2 }, { "W", 3 }, { "C", 4 }};
        int[] dx = new int[5] { 0, 1, 0, -1, 0};
        int[] dy = new int[5] { 1, 0, -1, 0, 0};
        
        public Robot()
        {
            allGemNum = 0;
            map = new int[mapSizeY, mapSizeX];
            pos = new int[2] { initialPosY, initialPosX };
            moveComands = new List<int[]>();
            SetMoveComand("C", 1);
            IsSearchSuccess = false;
        }

        public bool IsSearchSuccess { get; private set; }
        public string Answer
        {
            get{ return IsSearchSuccess ? "Yes" : "No"; }
        }

        public void SearchGem()
        {
            int collectedGemNum = 0;
            while(moveComands.Count > 0)
            {
                int[] moveComand = moveComands[0];
                moveComands.RemoveAt(0);

                for (int c = 0; c < moveComand[1]; c++)
                {
                    pos[0] += dy[moveComand[0]];
                    pos[1] += dx[moveComand[0]];

                    // PrintMap();

                    if (map[pos[0], pos[1]] != 1)
                        continue;
                    map[pos[0], pos[1]] = 2;
                    collectedGemNum++;

                    /* 移動経路保持
                    if (map[pos[0], pos[1]] == 1)
                        collectedGemNum++;
                    map[pos[0], pos[1]] = 2;
                    */
                }
            }
            if (collectedGemNum == allGemNum)
                IsSearchSuccess = true;
            else
                IsSearchSuccess = false;
        }
        
        public void SetGemPosInMap(int posX, int posY)
        {
            map[posY, posX] = 1;
            allGemNum++;

        }

        public void SetMoveComand(string dir, int moveVal)
        {
            int dirNum = dirs[dir];
            if (moveComands.Count < 1)
                moveComands.Add(new int[2] { dirNum, moveVal });
            else
            {
                if (dirNum == moveComands[moveComands.Count - 1][0])
                    moveComands[moveComands.Count - 1][1] += moveVal;
                else
                    moveComands.Add(new int[2] { dirNum, moveVal });
            }
        }

        public void PrintMap()
        {
            string output = "->\n";
            for (int y = 0; y < mapSizeY; y++)
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    output += map[y, x] + " ";
                }
                output += "\n";
            }
            Console.WriteLine(output);
        }
    }
}
