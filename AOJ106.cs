using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOJ
{
    class AOJ106
    {
        
        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);
            
            string outputStr = "";
            while(true)
            {
                int totalGram = int.Parse(Console.ReadLine());
                if (totalGram == 0) break;

                SobaCombiSelector selector = new SobaCombiSelector(totalGram);
                outputStr += selector.Select().TotalPrice.ToString() + "\n";
            }

            // exStdIn.Close();
            Console.Write(outputStr);
            Console.ReadLine();
            // System.Threading.Thread.Sleep(10000);
        }

        public class SobaCombiSelector
        {
            private int totalGram;

            private Soba sobaA;
            private Soba sobaB;
            private Soba sobaC;

            private List<Soba> sobas;
            private List<SobaCombi> candidateSobaCombies;

            public SobaCombiSelector(int totalGram)
            {
                this.totalGram = totalGram;
                sobaA = new Soba("A", 200, 380, 5, 20);
                sobaB = new Soba("B", 300, 550, 4, 15);
                sobaC = new Soba("C", 500, 850, 3, 12);
                sobas = new List<Soba>() { sobaA, sobaB, sobaC };
                candidateSobaCombies = new List<SobaCombi>();
            }

            public SobaCombi Select()
            {
                AllSearch(new List<Soba>(), new SobaCombi(sobas));

                if (candidateSobaCombies.Count == 0) return new SobaCombi(sobas);
                candidateSobaCombies.Sort();
                return candidateSobaCombies.First();
            }

            private void AllSearch(List<Soba> alreadySearchedSoba, SobaCombi combi)
            {
                if (alreadySearchedSoba.Count == sobas.Count) return;
                foreach(Soba soba in sobas)
                {
                    if (alreadySearchedSoba.Contains(soba)) continue;
                    alreadySearchedSoba.Add(soba);
                    int maxNum = totalGram / soba.gram;
                    for(int n = maxNum; n >= 0; n--)
                    {
                        combi.sobaNums[soba] = n;

                        if (combi.TotalGram == totalGram)
                        {
                            if (!candidateSobaCombies.Contains(combi)) candidateSobaCombies.Add(combi.Clone());
                        }
                        if (combi.TotalGram >= totalGram) continue;

                        AllSearch(new List<Soba>(alreadySearchedSoba), combi.Clone());
                    }
                }
            }
        }

        public class SobaCombi : IEquatable<SobaCombi>, IComparable<SobaCombi>
        {
            public Dictionary<Soba, int> sobaNums;
            private List<Soba> sobas;

            public SobaCombi() { }

            public SobaCombi(List<Soba> sobas)
            {
                this.sobas = sobas;
                sobaNums = new Dictionary<Soba, int>();
                foreach(Soba soba in this.sobas)
                {
                    sobaNums.Add(soba, 0);
                }
            }

            public int TotalPrice
            {
                get
                {
                    int totalPrice = 0;
                    foreach (var sobaNum in sobaNums)
                    {
                        totalPrice += sobaNum.Key.GetTotalPrice(sobaNum.Value);
                    }
                    return totalPrice;
                }
            }

            public int TotalGram
            {
                get
                {
                    int totalGram = 0;
                    foreach (var sobaNum in sobaNums)
                    {
                        totalGram += sobaNum.Key.GetTotalGram(sobaNum.Value);
                    }
                    return totalGram;
                }
            }

            public SobaCombi Clone()
            {
                SobaCombi clone = new SobaCombi
                {
                    sobas = new List<Soba>(sobas),
                    sobaNums = new Dictionary<Soba, int>(sobaNums)
                };
                return clone;
            }

            public bool Equals(SobaCombi combi)
            {
                if (sobaNums.Count != combi.sobaNums.Count) return false;
                foreach (var sobaNum in sobaNums)
                {
                    if (combi.sobaNums.ContainsKey(sobaNum.Key)) return false;
                    if (combi.sobaNums[sobaNum.Key] != sobaNum.Value) return false;
                }
                return true;
            }

            public int CompareTo(SobaCombi combi)
            {
                if (TotalPrice < combi.TotalPrice)
                {
                    return -1;
                }
                else if (TotalPrice > combi.TotalPrice)
                {
                    return 1;
                }
                return 0;
            }

            public override string ToString()
            {
                return string.Empty;
            }
        }

        public class Soba : IEquatable<Soba>
        {
            public string shopName;
            public int gram;
            public int price;
            public int discountPackNum;
            public int discountRatio;

            public Soba(string shopName, int gram, int price, int discountPackNum, int discountRatio)
            {
                this.shopName = shopName;
                this.gram = gram;
                this.price = price;
                this.discountPackNum = discountPackNum;
                this.discountRatio = discountRatio;
            }

            public float PricePerGram
            {
                get { return (float)price / gram; }
            }

            public float DiscountPricePerGram
            {
                get { return PricePerGram * (1.0f - discountRatio); }
            }

            public bool Equals(Soba soba)
            {
                return shopName == soba.shopName;
            }

            public int GetTotalPrice(int packNum)
            {
                int totalPrice = price * (packNum % discountPackNum);
                totalPrice += (int)(price * (packNum - packNum % discountPackNum) * (100 - discountRatio) / 100);
                return totalPrice;
            }

            public int GetTotalGram(int packNum)
            {
                return packNum * gram;
            }
        }
    }
}
