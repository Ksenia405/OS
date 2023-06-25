using System;
using System.Collections.Generic;

namespace OS4_LAB
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> arr = new List<int> { 4, 4, 4, 4};
            Proccess A = new Proccess("A", new Resources(2, 0, 0, 0), new Resources(2, 0, 2, 2));
            Proccess B = new Proccess("B", new Resources(2, 2, 0, 0), new Resources(2, 2, 2, 2));
            Proccess C = new Proccess("C", new Resources(0, 2, 2, 0), new Resources(2, 4, 2, 4));
            Proccess D = new Proccess("D", new Resources(0, 0, 2, 2), new Resources(0, 0, 2, 4));
            List<Proccess> proccesses = new List<Proccess>();
            proccesses.Add(A);
            proccesses.Add(B);
            proccesses.Add(C);
            proccesses.Add(D);
            Algorithm data = new Algorithm(proccesses, arr);
            data.CheckSystem();
            data.ShowResult();

        }
    }
}
