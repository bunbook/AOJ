using System;

namespace AOJ
{
    class AOJ136
    {
        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);
            
            string output = "";

            HeightData heightData = new HeightData();
            int dataNum = int.Parse(Console.ReadLine());
            for (int n = 0; n < dataNum; n++)
            {
                float height = float.Parse(Console.ReadLine());
                heightData.CheckAndCount(height);
            }
            output += heightData.ToString();

            Console.Write(output);
            Console.ReadLine();
        }
        
    }

    class HeightData
    {
        int[] distribution;

        public HeightData()
        {
            distribution = new int[6];
        }

        public void CheckAndCount(float height)
        {
            int no = (int)(height / 165.0f) * (1 + (int)((height % 165.0f) / 5.0f));
            if (no > 5)
                no = 5;
            distribution[no]++;
        }

        public override string ToString()
        {
            string str = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                str += (i + 1).ToString() + ":";
                for (int n = 0; n < distribution[i]; n++)
                {
                    str += "*";
                }
                str += "\n";
            }
            return str;
        }
    }
}
