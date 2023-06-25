using System;
using System.Collections.Generic;
using System.IO;

namespace OS2_LAB
{
    class Program
    {

        static void Main(string[] args)
        {
            Input data=new Input();
            data.Scanner();
            data.SaveFileSystem();
            Console.WriteLine();
            Console.WriteLine("Исправленная ФС:");
            data.PrintfSystem();

           
        }
    }
}
