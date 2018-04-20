using System;
using System.Collections.Generic;
using System.Text;

namespace AOJ
{
    class AOJ100
    {
        static void Main(string[] args)
        {
            int readCount = 0;
            string outputStr = "";

            while(true)
            {
                readCount = int.Parse(Console.ReadLine());

                if (readCount < 1) break;

                CompanyData cd = new CompanyData();

                for (int i = 0; i < readCount; i++)
                {
                    string[] splitedStr = (Console.ReadLine()).Split();

                    int employeeId = int.Parse(splitedStr[0]);
                    int unitSalesPrice = int.Parse(splitedStr[1]);
                    int unitSales = int.Parse(splitedStr[2]);
                    int totalSales = unitSalesPrice * unitSales;

                    cd.registerData(employeeId, totalSales);
                }

                outputStr += cd.outputEmployeeIDsOverNsales(1000000);
            };

            Console.Write(outputStr);
            Console.ReadLine();
        }
    }

    class CompanyData
    {
        private List<int> employeeIDs;
        private Dictionary<int, int> saleResults;

        public CompanyData()
        {
            this.employeeIDs = new List<int>();
            this.saleResults = new Dictionary<int,int>();
        }

        public void registerData (int employeeId, int totalSales)
        {
            if (!employeeIDs.Contains(employeeId))
            {
                employeeIDs.Add(employeeId);
            }

            if (!saleResults.ContainsKey(employeeId))
            {
                saleResults.Add(employeeId, totalSales);
            }
            else
            {
                saleResults[employeeId] += totalSales;
            }
        }

        public string outputEmployeeIDsOverNsales(int limitNum)
        {
            string outputStr = "";
            int outputCount = 0;

            foreach (int employeeId in employeeIDs)
            {
                if (!saleResults.ContainsKey(employeeId))
                {
                    continue;
                }

                if(saleResults[employeeId] < limitNum)
                {
                    continue;
                }
                
                outputStr += employeeId.ToString();
                outputStr += "\n";

                outputCount++;
            }

            if (outputCount < 1)
            {
                outputStr += "NA";
                outputStr += "\n";
            }

            return outputStr;
        }
    }
}