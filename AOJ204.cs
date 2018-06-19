using System;
using System.Collections.Generic;

namespace AOJ
{
    class AOJ204
    {
        static void Main(string[] args)
        {
            var exStdIn = new System.IO.StreamReader("stdin.txt");
            Console.SetIn(exStdIn);
            
            string output = "";

            while(true)
            {
                int[] input0 = Array.ConvertAll<string, int>(Console.ReadLine().Split(), int.Parse);
                if (input0[0] == 0 && input0[1] == 0)
                    break;

                LasorCannon cannon = new LasorCannon(input0[0], input0[1]);
                
                for (int i = 0; i < input0[1]; i++)
                {
                    int[] inputN = Array.ConvertAll<string, int>(Console.ReadLine().Split(), int.Parse);
                    cannon.ufos.Add(new Ufo(inputN[0], inputN[1], inputN[2], inputN[3]));
                }

                cannon.Simulate();
                output += cannon.invadedNum + "\n";
            }

            Console.Write(output);
            Console.ReadLine();
        }
    }

    struct Vector2
    {
        public double x;
        public double y;

        public double Magnitude
        {
            get { return Math.Sqrt(x * x + y * y); }
        }

        public Vector2(double x = 0d, double y = 0d)
        {
            this.x = x;
            this.y = y;
        }

        public static double Cross(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        public static double Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator *(double d, Vector2 v)
        {
            return new Vector2(d * v.x, d * v.y);
        }
    }

    class Ufo
    {
        public Vector2 pos;
        public int r;
        public int v;
        public double sita;
        public bool isDead;

        public Ufo(int x, int y, int r, int v)
        {
            this.pos = new Vector2(x, y);
            this.r = r;
            this.v = v;
            this.sita = Math.Atan2(pos.y, pos.x);
            this.isDead = false;
        }

        public void Move()
        {
            double d = Math.Max(pos.Magnitude - v, 0d);
            pos.x = Math.Cos(sita) * d;
            pos.y = Math.Sin(sita) * d;
        }
    }
    
    class LasorCannon
    {
        private const double INF = 1e10d;
        private const double EPS = 1e-10d;

        public List<Ufo> ufos;
        private int noDefenseR;
        public int invadedNum;

        public LasorCannon(int noDefenseR, int ufoNum)
        {
            this.noDefenseR = noDefenseR;
            this.ufos = new List<Ufo>();
            this.invadedNum = 0;
        }

        public void Simulate()
        {
            while(true)
            {
                foreach(Ufo ufo in ufos)
                {
                    if (ufo.isDead)
                        continue;

                    ufo.Move();

                    if (ufo.pos.Magnitude > noDefenseR + EPS)
                        continue;

                    ufo.isDead = true;
                    invadedNum++;
                }

                ufos.RemoveAll(u => u.isDead);

                if (ufos.Count <= 0)
                    break;

                Vector2 nearestUfoPos = new Vector2(INF, INF);
                foreach (Ufo ufo in ufos)
                {
                    if (ufo.isDead)
                        continue;

                    if (ufo.pos.Magnitude < nearestUfoPos.Magnitude + EPS)
                        nearestUfoPos = ufo.pos;
                }

                foreach (Ufo ufo in ufos)
                {
                    if (ufo.isDead)
                        continue;
                    double shotAngle = Math.Atan2(nearestUfoPos.y, nearestUfoPos.x);
                    Vector2 targetDir = new Vector2(Math.Cos(shotAngle), Math.Sin(shotAngle));
                    double d = DistancePointToLine(ufo.pos, INF * targetDir, noDefenseR * targetDir);

                    if (d <= ufo.r + EPS)
                        ufo.isDead = true;
                }
            }
        }

        private double DistancePointToLine(Vector2 p, Vector2 l1, Vector2 l2)
        {
            if (Vector2.Dot(l2 - l1, p - l1) < EPS)
                return (l1 - p).Magnitude;
            if (Vector2.Dot(l1 - l2, p - l2) < EPS)
                return (l2 - p).Magnitude;
            return Math.Abs(Vector2.Cross(l2 - l1, p - l1)) / (l2 - l1).Magnitude;
        }
    }
}
