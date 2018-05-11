using System;

namespace AOJ
{
    class AOJ110
    {
        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);
            string outputStr = string.Empty;

            while(true)
            {
                string inputStr = Console.ReadLine();
                if (inputStr == null)
                    break;

                string[] equation = (inputStr).Split('=');
                outputStr += SolveAlphametic(equation) + "\n";
            }
            Console.Write(outputStr);
            Console.ReadLine();
        }

        static string SolveAlphametic(string[] equation)
        {
            string[] values = equation[0].Split('+');
            string answer = equation[1];

            int lengthDifference0 = answer.Length - values[0].Length;
            int lengthDifference1 = answer.Length - values[1].Length;
            if (lengthDifference0 < 0 || lengthDifference1 < 0)
                return "NA";
            
            for (int n = 0; n < lengthDifference0; n++)
            {
                values[0] = "0" + values[0];
            }
            for (int n = 0; n < lengthDifference1; n++)
            {
                values[1] = "0" + values[1];
            }

            for (int x = 0; x <= 9; x++)
            {
                bool isSolved = true;
                int upper = 0;
                for (int i = answer.Length - 1; i >= 0; i--)
                {
                    int v0 = CharToInt(values[0][i], x);
                    int v1 = CharToInt(values[1][i], x);
                    int sum = v0 + v1 + upper;
                    upper = sum / 10;
                    int ans = CharToInt(answer[i], x);
                    
                    if (sum % 10 != ans)
                    {
                        isSolved = false;
                        break;
                    }
                }
                if (isSolved && upper == 0)
                    return x.ToString();
            }
            return "NA";
        }
        
        static int CharToInt(char c, int x)
        {
            return c == 'X' ? x : c - '0';
        }
    }
}
