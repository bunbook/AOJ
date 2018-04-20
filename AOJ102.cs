using System;
using System.Collections.Generic;
using System.Text;

namespace AOJ
{
    class AOJ102
    {
        static void Main(string[] args)
        {
            string outputStr = "";

            while(true)
            {
                int dataSideLen = int.Parse(Console.ReadLine());
                if (dataSideLen <= 0) break;

                int num = dataSideLen + 1;

                int[,] matrixData = new int[num, num];

                for (int h = 0; h < num - 1; h++)
                {
                    string[] splitedStr = (Console.ReadLine()).Split();

                    int lineSum = 0;
                    for(int w = 0; w < num - 1; w++)
                    {
                        matrixData[h, w] = int.Parse(splitedStr[w]);
                        lineSum += matrixData[h, w];
                    }
                    matrixData[h, num - 1] = lineSum;
                }

                for (int w = 0; w < num; w++)
                {
                    int lineSum = 0;
                    for (int h = 0; h < num - 1; h++)
                    {
                        lineSum += matrixData[h, w];
                    }
                    matrixData[num - 1, w] = lineSum;
                }

                outputStr += OutputMatrixData(matrixData, num);
            }

            Console.Write(outputStr);
            Console.ReadLine();
        }

        static string OutputMatrixData(int[,] matrixData, int num)
        {
            string outputStr = "";

            for (int h = 0; h < num; h++)
            {
                for (int w = 0; w < num; w++)
                {
                    outputStr += String.Format("{0, 5}", matrixData[h, w]);
                }
                outputStr += "\n";
            }

            return outputStr;
        }

    }
}
