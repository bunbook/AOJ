using System;
using System.Collections.Generic;
using System.Text;

namespace AOJ
{
    class AOJ104
    {
        const int maxTileX = 100;
        const int maxTileY = 100;

        static void Main(string[] args)
        {
            //var exStdIn = new System.IO.StreamReader("stdin.txt");
            //Console.SetIn(exStdIn);

            string outputStr = "";

            while(true)
            {
                string[] tileNumStrs = (Console.ReadLine()).Split(' ');
                int tileY = int.Parse(tileNumStrs[0]);
                int tileX = int.Parse(tileNumStrs[1]);
                if (tileX == 0 && tileY == 0) break;

                //if (outputStr.Length != 0) outputStr += "\n";

                string tilesStr = "";
                for (int y = 0; y < tileY; y++)
                {
                    tilesStr += Console.ReadLine();
                }

                Tiles tiles = new Tiles(tileX, tilesStr);

                if (tiles.MoveHuman()) outputStr += string.Format("{0} {1}", tiles.humanPos[0], tiles.humanPos[1]);
                else outputStr += "LOOP";
                outputStr += "\n";
            }

            // exStdIn.Close();
            Console.Write(outputStr);
            Console.ReadLine();
        }

        class Tiles
        {
            public int[] humanPos;
            private int[,] alreadyPassedTiles;
            
            private int tileX;
            private string str;

            public Tiles(int tileX, string str)
            {
                this.tileX = tileX;
                this.str = str;
                alreadyPassedTiles = new int[maxTileY, maxTileX];
                humanPos = new int[2];
            }

            public bool MoveHuman()
            {
                
                bool isStop = false;

                while(!isStop)
                {
                    int tileIndex = PositionToIndex(humanPos);

                    if (alreadyPassedTiles[humanPos[1], humanPos[0]] == 0) alreadyPassedTiles[humanPos[1], humanPos[0]] = 1;
                    else return false;

                    switch (str[tileIndex])
                    {
                        case '>':
                            humanPos[0] += 1;
                            break;
                        case '<':
                            humanPos[0] -= 1;
                            break;
                        case '^':
                            humanPos[1] -= 1;
                            break;
                        case 'v':
                            humanPos[1] += 1;
                            break;
                        case '.':
                            isStop = true;
                            break;
                    }
                }
                return true;
            }

            public int PositionToIndex(int[] pos)
            {
                return pos[0] + pos[1] * tileX;
            }
        }
    }
}
