using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS3_LAB
{
    class LRU
    {
        List<int> memory = new List<int>();
        List<string> oldStage = new List<string>();
        List<string> listInterrupt = new List<string>();
        List<int> listIncremen = new List<int>();
        int countInterrupt = 0;
        int numberPB;
        bool flag = false;
        public LRU(int numberPB, List<int> startQueue) 
        { 
            this.numberPB = numberPB;
            if (startQueue.Count > 1) 
            {
                for (int k = 0; k < startQueue.Count; k++)
                {
                    memory.Add(startQueue[k]);
                    oldStage.Add(startQueue[k].ToString());
                    listIncremen.Add(0);
                    listInterrupt.Add(" ");
                }
                for (int i = 0; i < numberPB; i++)
                {
                    for (int j = 0; j < startQueue.Count; j++)
                        oldStage.Add(startQueue[j].ToString());
                }

            } 
        }


        public List<int> Algorithm(List<int> queue, int stage)
        {
            flag = false;
            int max;
            if (queue.Count < numberPB)
            {
                if (queue.Contains(stage) == false)
                {
                    for (int j = 0; j < queue.Count; j++)
                        listIncremen[j]++;
                    queue.Add(stage);
                    listIncremen.Add(0);
                    countInterrupt++;
                    flag = true;
                }
                else
                {
                    for (int j=0; j<queue.Count; j++) {
                        if (queue[j] == stage) listIncremen[j] = 0;
                        else listIncremen[j]++;
                    }
                    
                }


                for(int k=0; k<queue.Count; k++)
                {
                    oldStage.Add(queue[k].ToString());
                }
                for (int i=queue.Count; i<numberPB; i++)
                {
                    oldStage.Add(" ");
                }


            }
            else
            {
                if (!queue.Contains(stage))
                {
                    countInterrupt++;
                    flag = true;
                    max = 0;
                    for(int s=0; s<queue.Count; s++)
                    {
                        if (listIncremen[s] > listIncremen[max]) max = s;
                    }
                    queue[max] = stage;
                    listIncremen[max] = 0;
                    for (int i = 0; i < queue.Count; i++)
                    {
                        oldStage.Add(queue[i].ToString());
                        if (i!=max)listIncremen[i]++;
                    }

                }
                else
                {
                    for (int j = 0; j < queue.Count; j++)
                    {
                        if (queue[j] == stage) listIncremen[j] = 0;
                        else listIncremen[j]++;
                        oldStage.Add(queue[j].ToString());
                    }
                }
            }
            memory.Add(stage);
            if (flag) listInterrupt.Add("p");
            else listInterrupt.Add(" ");
          
            return queue;
        }
        public void PrintInfo(List<int> queue)
        {
            Console.WriteLine(" ________________________________");
            Console.WriteLine("|Результаты работы алгоритма LRU |");
            Console.WriteLine("|________________________________|");
            Console.WriteLine();
            Console.WriteLine("Содержание страничных блоков:");
            for (int i = 0; i < queue.Count; i++)
            {
                Console.Write(queue[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Количество прерываний: {0}", countInterrupt);
            Console.WriteLine("Последовательность обращений");
            for (int j = 0; j < memory.Count; j++)
            {
                Console.Write(memory[j] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Таблица работы алгоритма");
            for (int k = 3; k < oldStage.Count; k=k+4)
            {
                    Console.Write(oldStage[k] + " ");
            }
            Console.WriteLine();
            for (int k = 2; k < oldStage.Count; k = k + 4)
            {
                Console.Write(oldStage[k] + " ");
            }
            Console.WriteLine();
            for (int k = 1; k < oldStage.Count; k = k + 4)
            {
                Console.Write(oldStage[k] + " ");
            }
            Console.WriteLine();
            for (int k = 0; k < oldStage.Count; k = k + 4)
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

