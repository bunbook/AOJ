using System;
using System.Collections.Generic;
using System.Text;

namespace AOJ
{
    class AOJ101
    {
        static void Main(string[] args)
        {
            int readNum = int.Parse(Console.ReadLine());
            string outputStr = "";

            for (int i = 0; i < readNum; i++ )
            {
                string inputStr = Console.ReadLine();

                outputStr += translateHoshinoToHoshina(inputStr) + "\n";
            }

            Console.Write(outputStr);
            Console.ReadLine();
        }

        static string translateHoshinoToHoshina(string inputStr)
        {
            string outputStr = "";

            int i = 0;
            for (i = 0; i < inputStr.Length - 6; i++)
            {
                if(inputStr[i] != 'H')
                {
                    outputStr += inputStr[i];
                    continue;
                }

                string tempStr = "";

                for (int j = i; j < i + 7; j++)
                {
                    tempStr += inputStr[j];
                }

                if (tempStr == "Hoshino")
                {
                    outputStr += "Hoshina";
                    i += 6;
                }
                else
                {
                    outputStr += inputStr[i];
                }
            }
            for (int j = i; j < inputStr.Length; j++)
            {
                outputStr += inputStr[j];
            }

            return outputStr;
        }
    }
}