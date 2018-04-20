using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace AOJ
{
    class AOJ104
    {
        static void Main(string[] args)
        {
            string outputStr = "";

            int dataSetNum = int.Parse(Console.ReadLine());

            BaseballSimulator bbSim = new BaseballSimulator(dataSetNum);

            bbSim.InputDataSet();
            bbSim.Simulate();
            outputStr += bbSim.OutputResult();

            Console.Write(outputStr);
            Console.ReadLine();
        }
    }

    class TurnStatus
    {
        public enum Event
        {
            Hit = 0,
            Homerun,
            Out
        }

        /*
        public class Event
        {
            public static string ObtainString(this Event value)
            {
                string[] values = { "HIT", "HOMERUN", "OUT" };
                return values[(int)value];
            }

            public static Event Get(string str)
            {
                Dictionary<string, Event> eventDic = new Dictionary<string, Event>
                { 
                    {"HIT", Hit}, 
                    {"HOMERUN", Homerun}, 
                    {"OUT", Out}
                };

                return eventDic[str];
            }
        }
        */

        public Event GetEvent (string str)
        {
            Dictionary<string, Event> eventDic = new Dictionary<string, Event>
                { 
                    {"HIT", Event.Hit}, 
                    {"HOMERUN", Event.Homerun}, 
                    {"OUT", Event.Out}
                };

            return eventDic[str];
        }
        
        public enum BaseStatus
        {
            NNN = 0, 
            NNF,
            NSF, 
            TSF, 
            HTSF
        }

        public Dictionary<int, BaseStatus> baseStatus = new Dictionary<int, BaseStatus> 
        {
            { 0, BaseStatus.NNN },
            { 1, BaseStatus.NNF },
            { 3, BaseStatus.NSF }, //((NNF << 1) + NNF),
            { 7, BaseStatus.TSF }, //((NSF << 1) + NNF)
            { 15, BaseStatus.HTSF }
        };

        public List<Event> events;
        public int score;
        public int bases;

        public TurnStatus()
        {
            this.events = new List<Event>();
            this.score = 0;
            this.bases = 0;
        }
    }

    class BaseballSimulator
    {
        int dataSetNum;
        List<TurnStatus> turns;

        public BaseballSimulator(int dataSetNum)
        {
            this.dataSetNum = dataSetNum;
            this.turns = new List<TurnStatus>();
        }

        public void InputDataSet()
        {
            for (int n = 0; n < dataSetNum; n++)
            {
                TurnStatus turn = new TurnStatus();
                int outCount = 0;

                while(true)
                {
                    string str = Console.ReadLine();

                    turn.events.Add(turn.GetEvent(str));
                    
                    if (turn.GetEvent(str) == TurnStatus.Event.Out) outCount++;
                  
                    if (outCount >= 3) break;
                }
                turns.Add(turn);
            }
        }

        public void Simulate()
        {
            foreach (TurnStatus turn in turns)
            {
                ProcessTurn(turn);
            }
        }

        private void ProcessTurn(TurnStatus turn)
        {
            int outCount = 0;

            foreach(TurnStatus.Event ev in turn.events)
            {
                #if DEBUG
                    Console.WriteLine(ev);
                    Console.WriteLine(turn.bases);
                    Console.WriteLine("this: " + turn.bases);
                #endif

                switch (ev)
                {
                    case TurnStatus.Event.Hit:
                        turn.bases = (turn.bases << 1) + 1;
                        if (turn.baseStatus[turn.bases] == TurnStatus.BaseStatus.HTSF)
                        {
                            turn.score++;
                            turn.bases = turn.bases >> 1;
                        }
                        break;
                    case TurnStatus.Event.Homerun:
                        turn.score += (int)turn.baseStatus[turn.bases] + 1;
                        turn.bases = 0;
                        break;
                    case TurnStatus.Event.Out:
                        outCount++;
                        break;
                }
                #if DEBUG
                    Console.WriteLine(turn.baseStatus[turn.bases] + " " + turn.bases);
                #endif
                if (outCount >= 3) break;
            }
        }

        public string OutputResult()
        {
            string outputStr = "";

            foreach (TurnStatus turn in turns)
            {
                outputStr += turn.score.ToString() + "\n";
            }
            return outputStr;
        }
    }
}
