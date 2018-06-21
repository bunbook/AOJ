using System;
using System.Collections.Generic;
using System.Text;

namespace AOJ
{
    class AOJ2708
    {
        static void Main(string[] args)
        {
            var exStdIn = new System.IO.StreamReader("stdin.txt");
            Console.SetIn(exStdIn);

            string output = string.Empty;
            string input = Console.ReadLine().Trim();

            Solver solver = new Solver(input);
            output += solver.Solve();

            Console.WriteLine(output);
            Console.ReadLine();
        }
    }

    class Solver
    {
        private string _input;

        public Solver(string input)
        {
            _input = input;
        }

        public string Solve()
        {
            int maxDepth = CountMaxDepth();
            // Console.WriteLine(maxDepth);

            if (maxDepth < 0)
                return "No";

            if (RewriteFromABC())
                return "Yes";

            return "No";
        }

        private int CountMaxDepth()
        {
            int strLength = _input.Length;
            int maxCount = strLength / 3;
            while (strLength > 3)
            {
                strLength = strLength / 3 + strLength % 3;
            }
            return strLength == 3 ? maxCount : -1;
        }

        private bool RewriteFromABC()
        {
            char[] abc = { 'A', 'B', 'C' };
            Stack<string> stack = new Stack<string>();
            stack.Push(_input);

            while (stack.Count > 0)
            {
                string current = stack.Pop();
                // Console.WriteLine(current);
                if (current == "ABC")
                    return true;
                if (current.Length <= 3)
                    continue;

                ABC abcInfo = new ABC();
                for (int i = 0; i < current.Length; i++)
                {
                    if (current[i] == 'A')
                    {
                        abcInfo._aNum++;
                    }
                    else if(current[i] == 'B')
                    {
                        abcInfo._bNum++;
                        continue;
                    }
                    else
                    {
                        abcInfo._cNum++;
                        continue;
                    }

                    if (i >= current.Length - 2)
                        continue;

                    if (current[i] == 'A' && current[i + 1] == 'B' && current[i + 2] == 'C')
                    {
                        abcInfo._abcNum++;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (abcInfo[i] != abcInfo._abcNum)
                        continue;

                    string next = current.Replace("ABC", abc[i].ToString());
                    if (next != current)
                        stack.Push(next);
                }
            }
            return false;
        }
    }

    public struct ABC
    {
        public int _aNum;
        public int _bNum;
        public int _cNum;
        public int _abcNum;

        public ABC(int aNum, int bNum, int cNum, int abcNum)
        {
            _aNum = aNum;
            _bNum = bNum;
            _cNum = cNum;
            _abcNum = abcNum;
        }

        public int this[int i]
        {
            get
            {
                switch(i)
                {
                    case 0:
                        return _aNum;
                    case 1:
                        return _bNum;
                    case 2:
                        return _cNum;
                }
                return -1;
            }
        }
    }
}
