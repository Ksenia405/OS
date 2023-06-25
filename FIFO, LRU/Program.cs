using System;
using System.Collections.Generic;
namespace OS3_LAB
{
    class Program
    {
        public static int GetInt()
        {
            int a = 0;
            string b = null;
            b = Console.ReadLine();
            while (int.TryParse(b, out a) == false)
            {
                Console.Write("Ошибка, пожалуйста введите число еще раз: ");
                b = Console.ReadLine();
            }
            a = Convert.ToInt32(b);
            return a;
        }

        enum MenuItem{Algorithms=1, Settings, Quit };

        static void Main(string[] args)
        {
            int count, userChoice, setChoice;
            int numberPB=5, stage, value;
            List<int> queueFIFO=new List<int>();
            List<int> queueLRU = new List<int>();
            do {
                Console.WriteLine("Выберите одно из следующих:\n 1-Запуск алгоритмов\n 2-Настройки\n 3-Выход\n");
                userChoice = GetInt();
                switch (userChoice)
                {
                    case ((int)MenuItem.Algorithms): {
                            FIFO dataFIFO = new FIFO(numberPB, queueFIFO);
                            LRU dataLRU = new LRU(numberPB, queueLRU);
                            Console.WriteLine("Введите кол-во обращений");
                            count = GetInt();
                            for(int i=0; i<count; i++)
                            {
                                Console.WriteLine("Введите страницу:");
                                stage = GetInt();
                                queueFIFO = dataFIFO.Algorithm(queueFIFO, stage);
                                queueLRU = dataLRU.Algorithm(queueLRU, stage);
                            }
                            dataFIFO.PrintInfo(queueFIFO);
                            dataLRU.PrintInfo(queueLRU);
                            queueFIFO = new List<int>();
                            queueLRU = new List<int>();
                        } break;
                    case ((int)MenuItem.Settings): 
                        {
                            Console.WriteLine("Выберите одно из следующих:\n 1-Начальное заполнение страничных блоков\n 2-Изменить кол-во страничных блоков\n");
                            setChoice = GetInt();
                            switch (setChoice) 
                            {
                                case (1): {
                                        queueLRU = new List<int>();
                                        queueFIFO = new List<int>();
                                        Console.WriteLine("Введите номера начальных страничных блоков");
                                        for (int i=0; i<numberPB; i++) {
                                            Console.WriteLine("Введите {0} страницу", i+1);
                                            value = GetInt();
                                            queueFIFO.Add(value);
                                            queueLRU.Add(value);
                                        }
                                    }
                                    break;
                                case (2): {
                                        Console.WriteLine("Ведите кол-во страничных блоков");
                                        numberPB = GetInt();
                                    } break;
                            }
                        } break;
                    default: Console.WriteLine("Такого пункта нет"); break;
                }
            } while (userChoice != (int)MenuItem.Quit);
        }
    }
}
