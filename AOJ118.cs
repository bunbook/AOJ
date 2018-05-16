using System;

namespace AOJ
{
    class AOJ118
    {
        const char NONE = '_';

        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);

            string output = string.Empty;

            while (true)
            {
                int[] mapSize = Array.ConvertAll<string, int>(Console.ReadLine().Split(), int.Parse);
                if (mapSize[0] == 0 && mapSize[1] == 0)
                    break;

                char[,] map = new char[mapSize[0] + 2, mapSize[1] + 2];

                for (int i = 0; i < mapSize[0] + 2; i++)
                {
                    for (int j = 0; j < mapSize[1] + 2; j++)
                    {
                        map[i, j] = NONE;
                    }
                }

                for (int i = 1; i < mapSize[0] + 1; i++)
                {
                    string mapData = Console.ReadLine();
                    for (int j = 1; j < mapSize[1] + 1; j++)
                    {
                        map[i, j] = mapData[j - 1];
                    }
                }
                int dividableNum = 0;
                for (int i = 1; i < mapSize[0] + 1; i++)
                {
                    for (int j = 1; j < mapSize[1] + 1; j++)
                    {
                        if (map[i, j] == NONE)
                            continue;
                        RewriteMap(map, j, i, map[i, j]);
                        dividableNum++;
                    }
                }
                output += dividableNum.ToString() + "\n";
            }
            
            Console.Write(output);
            Console.ReadLine();
        }

        static void RewriteMap(char[,] map, int posX, int posY, char targetData)
        {
            map[posY, posX] = NONE;

            int[] dx = { 0, 0, 1, -1};
            int[] dy = { 1, -1, 0, 0};

            for (int d = 0; d < 4; d++)
            {
                int checkPosX = posX + dx[d];
                int checkPosY = posY + dy[d];
                if (map[checkPosY, checkPosX] == targetData)
                    RewriteMap(map, checkPosX, checkPosY, targetData);
            }
        }
    }
}
