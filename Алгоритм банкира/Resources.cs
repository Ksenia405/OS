using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS4_LAB
{
    class Resources
    {
        public int R1;
        public int R2;
        public int R3;
        public int R4;
        public Resources(int R1, int R2, int R3, int R4)
        {
            this.R1 = R1;
            this.R2 = R2;
            this.R3 = R3;
            this.R4 = R4;
        }

    }
    class Proccess
    {
        public Resources ResourcesProvided;
        public Resources MaximumDemand;
        public string Name;
        public bool complete=false;
        public Proccess(string Name, Resources ResourcesProvided, Resources MaximumDemand)
        {
            this.Name = Name;
            this.ResourcesProvided = ResourcesProvided;
            this.MaximumDemand = MaximumDemand;
        }
    }
    class Algorithm
    {
        List<Proccess> proccesses;
        List<int> countRes=new List<int>();
        List<int> currentRes = new List<int> { 0, 0, 0, 0};
        List<int> freeRes = new List<int>();
        List<string> history = new List<string>();
        bool state;
        public Algorithm(List<Proccess> p, List<int> countRes) { proccesses = p; this.countRes = countRes; }
        public void CheckSystem()
        {
            CurrentRes();
            FreeRes();
            for (int i=0; i<proccesses.Count; i++) 
            {
                state = false;
                foreach (var p in proccesses)
                {
                    if (!p.complete)
                    {
                        if ((p.ResourcesProvided.R1+freeRes[0]>=p.MaximumDemand.R1)&&
                         (p.ResourcesProvided.R2 + freeRes[1] >= p.MaximumDemand.R2)&&
                         (p.ResourcesProvided.R3 + freeRes[2] >= p.MaximumDemand.R3)&&
                         (p.ResourcesProvided.R4 + freeRes[3] >= p.MaximumDemand.R4)) {
                           p.complete = true;
                           history.Add(p.Name);
                           OverRes(p);
                           state = true;
                           break;
                     }
                        
                    }
                }
            }
            for (int j = 0; j < proccesses.Count; j++) if (proccesses[j].complete == false) state = false;

        }
        public void CurrentRes()
        {
            currentRes = new List<int> { 0, 0, 0, 0 };
            for (int i=0; i<proccesses.Count; i++)
            {
                currentRes[0] += proccesses[i].ResourcesProvided.R1;
                currentRes[1] += proccesses[i].ResourcesProvided.R2;
                currentRes[2] += proccesses[i].ResourcesProvided.R3;
                currentRes[3] += proccesses[i].ResourcesProvided.R4;
               
            }
        }
        public void FreeRes()
        {
            freeRes = new List<int>();
            for (int i=0; i<countRes.Count; i++)
            {
             freeRes.Add(countRes[i]-currentRes[i]);
            }

        }
        public void OverRes(Proccess p)
        {
            freeRes[0] += p.ResourcesProvided.R1;
            freeRes[1] += p.ResourcesProvided.R2;
            freeRes[2] += p.ResourcesProvided.R3;
            freeRes[3] += p.ResourcesProvided.R4;

        }
        public void ShowResult()
        {
            if (state) { Console.WriteLine("Состояние надежное"); 
            for (int z=0; z < history.Count; z++)
                {
                    Console.Write(history[z]+ " ");
                }
            }
            else Console.WriteLine("Состояние ненадежное");
        }
    }
}
