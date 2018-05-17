using System;

namespace AOJ
{
    class AOJ131
    {
        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);
            
            string output = "";

            int dataSetNum = int.Parse(Console.ReadLine());

            for (int n = 0; n < dataSetNum; n++)
            {
                Map map = new Map();
                map.ReadLights();
                map.Search(0, map.particles);
                // map.TestForSolve();
                output += map.GetParticleStr() + "\n";
            }
            Console.Write(output);
            Console.ReadLine();
        }
    }

    class Map
    {
        static readonly int[] dx = { 0, 0, 0, 1, -1 };
        static readonly int[] dy = { 0, 1, -1, 0, 0 };

        const int X = 10;
        const int Y = 10;
        const int NONE = -1;
        const int ON = 1;
        const int OFF = 0;
        
        public int[,] lights;
        public int[,] particles;

        public Map()
        {
            lights = new int[Y + 2, X + 2];
            particles = new int[Y, X];
            Init();
        }

        private void Init()
        {
            for (int i = 0; i< Y + 2; i++)
            {
                for (int j = 0; j< X + 2; j++)
                {
                    lights[i, j] = NONE;
                }
            }
        }

        public void ReadLights()
        {
            for (int i = 1; i < Y + 1; i++)
            {
                string[] data = Console.ReadLine().Split();
                for (int j = 1; j < X + 1; j++)
                {
                    lights[i, j] = int.Parse(data[j - 1]);
                }
            }
        }

        public bool CheckLightsOut(int[,] lights)
        {
            for (int i = 1; i < Y + 1; i++)
            {
                for (int j = 1; j < X + 1; j++)
                {
                    if (lights[i, j] == ON)
                        return false;
                }
            }
            return true;
        }

        public bool Search(int depth, int[,] particles)
        {
            if (depth == 10)
            {
                if (Solve((int[,])lights.Clone(), particles))
                {
                    this.particles = (int[,])particles.Clone();
                    return true;
                }
                return false;
            }

            for (int i = 0; i < 2; i++)
            {
                int[,] newParticles = (int[,])particles.Clone();
                newParticles[0, depth] = i;
                if (Search(depth + 1, newParticles))
                    return true;
            }
            return false;
        }

        public void TestForSolve()
        {
            int[,] newParticles = (int[,])particles.Clone();
            if (Solve((int[,])lights.Clone(), newParticles))
            {
                particles = (int[,])newParticles.Clone();
            }
        }

        public bool Solve(int[,] lights, int[,] particles)
        {
            // Console.WriteLine(GetLightsStr(lights));
            for (int i = 1; i < Y + 1; i++)
            {
                for (int j = 1; j < X + 1; j++)
                {
                    if (particles[i - 1, j - 1] == OFF)
                        continue;

                    for (int d = 0; d < 5; d++)
                    {
                        if (lights[i + dy[d], j + dx[d]] == NONE)
                            continue;
                        lights[i + dy[d], j + dx[d]] = ON & (~lights[i + dy[d], j + dx[d]]);
                    }
                }
                // Console.WriteLine(GetLightsStr(lights));

                if (i == Y)
                    break;

                for (int j = 1; j < X + 1; j++)
                {
                    if (lights[i, j] == ON)
                        particles[i, j - 1] = ON;
                }
                // Console.WriteLine(GetParticleStr(particles));
            }
            return CheckLightsOut(lights);
        }

        public string GetParticleStr()
        {
            string output = string.Empty;

            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    output += particles[i, j];
                    if (j != X - 1)
                        output += " ";
                }
                if (i != Y - 1)
                    output += "\n";
            }
            return output;
        }

        public string GetParticleStr(int[,] particles)
        {
            string output = string.Empty;

            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    output += particles[i, j] + " ";
                }
                output += "\n";
            }
            return output;
        }

        public string GetLightsStr(int[,] lights)
        {
            string output = string.Empty;

            for (int i = 1; i < Y + 1; i++)
            {
                for (int j = 1; j < X + 1; j++)
                {
                    output += lights[i, j] + " ";
                }
                output += "\n";
            }
            return output;
        }
    }
}
