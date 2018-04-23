using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOJ
{
    class AOJ105
    {
        static void Main(string[] args)
        {
            // var exStdIn = new System.IO.StreamReader("stdin.txt");
            // Console.SetIn(exStdIn);
            
            string outputStr = "";

            BookIndex bookIndex = new BookIndex();

            while(true)
            {
                try
                {
                    string[] inputStrs = (Console.ReadLine()).Split();
                    bookIndex.Add(inputStrs[0], int.Parse(inputStrs[1]));
                }
                catch (NullReferenceException nre)
                {
                    string hoge = nre.ToString();
                    hoge += "hoge";
                    break;
                    //throw;
                }
            }

            outputStr = bookIndex.ToString();

            // exStdIn.Close();
            Console.Write(outputStr);
            Console.ReadLine();
            // System.Threading.Thread.Sleep(10000);
        }

        public class BookIndex
        {
            public Dictionary<string, List<int>> wordIndex;
            public List<string> words;

            public BookIndex()
            {
                wordIndex = new Dictionary<string, List<int>>();
                words = new List<string>();
            }

            public void Add(string word, int page)
            {
                if (words.Contains(word))
                {
                    wordIndex[word].Add(page);
                    return;
                }

                words.Add(word);
                wordIndex.Add(word, new List<int>() { page });
            }

            public override string ToString()
            {
                string outputStr = string.Empty;

                words.Sort();

                foreach(string word in words)
                {
                    wordIndex[word].Sort();
                    outputStr += String.Format("{0}\n", word);
                    foreach(int page in wordIndex[word])
                    {
                        if (wordIndex[word].IndexOf(page) != 0) outputStr += " ";
                        outputStr += page.ToString();
                    }
                    outputStr += "\n";
                }

                return outputStr;
            }
        }
    }
}
