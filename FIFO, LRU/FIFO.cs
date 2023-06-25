using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS3_LAB
{
    class FIFO
    {
        List<int> memory=new List<int>();
        List<int> oldStage=new List<int>();
        List<string> listInterrupt = new List<string>(); 
        int countInterrupt = 0;
        int numberPB;
        bool flag = false;
       public FIFO(int numberPB, List<int> startQueue) 
        { 
            this.numberPB = numberPB;
            if (startQueue.Count > 1) 
            {
                for (int k = 0; k < startQueue.Count; k++)
                {
                    memory.Add(startQueue[k]);
                    oldStage.Add(startQueue[0]);
                    listInterrupt.Add(" ");
                }
            } 
        }
      public   List<int> Algorithm(List<int> queue, int stage)
        {
            flag = false;
            if(queue.Count < numberPB) 
            {
                if (queue.Contains(stage) == false)
                { queue.Add(stage);
                    countInterrupt++;
                    flag = true;
                } 
                
            }
            else { 
            if (!queue.Contains(stage))
                {
                    countInterrupt++;
                    flag = true;
                    for (int i=0; i<queue.Count-1; i++)
                    {
                        queue[i] = queue[i + 1];
                    }
                    queue[queue.Count - 1] = stage;
                }
            }

            oldStage.Add(queue[0]);
            memory.Add(stage);
            if (flag) listInterrupt.Add("p");
            else listInterrupt.Add(" ");
            return queue;
        }
        
        public void PrintInfo(List<int> queue)
        {
            Console.WriteLine(" ________________________________");
            Console.WriteLine("|Результаты работы алгоритма FIFO|");
            Console.WriteLine("|________________________________|");
            Console.WriteLine();
            Console.WriteLine("Содержание страничных блоков:");
            for(int i=0; i<queue.Count; i++)
            {
                Console.Write(queue[i]+" ");
            }
            Console.WriteLine();
            Console.WriteLine("Количество прерываний: {0}", countInterrupt);
            Console.WriteLine("Последовательность обращений");
            for (int j=0; j<memory.Count; j++)
            {
                Console.Write(memory[j]+ " "); 
            }
            Console.WriteLine();
            Console.WriteLine("Самая старая страница");
            for (int k = 0; k < oldStage.Count; k++)
            {
                Console.Write(oldStage[k] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Журнал прерываний");
            for (int f = 0; f < listInterrupt.Count; f++)
            {
                Console.Write(listInterrupt[f] + " ");
            }
            Console.WriteLine();
            Console.WriteLine(" ________________________________");
            Console.WriteLine();
        }
    }
}
