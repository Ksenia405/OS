using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS2_LAB
{
    // 1 - конец файла
    // 0 - пустой кластер
    // -1 - bad


    class Input
    {

        Dictionary<string, int> directory;
        Dictionary<int, int> fat;
        Dictionary<string, List<int>> file;
        List<int> LinkMatrix;
        public Input()
        {
            string filePath1 = @"input1.txt";
            string filePath2 = @"input2.txt";
            directory = new Dictionary<string, int>();
            fat = new Dictionary<int, int>();
            file = new Dictionary<string, List<int>>();
            LinkMatrix = new List<int>();
            string[] text = null;
            StreamReader reader = new StreamReader(filePath1);
            text = File.ReadAllText(filePath1).Split(' ', '\r', '\n').ToArray();
            for (int i = 0; i < text.Length; i += 2)
            {
                if (text[i] != "") { directory.Add(text[i], Int32.Parse(text[i + 1])); }
                else i--;
            }
            reader.Close();

            text = null;
            reader = new StreamReader(filePath2);
            text = File.ReadAllText(filePath2).Split(' ', '\r', '\n').ToArray();
            for (int i = 0; i < text.Length; i += 2)
            {
                if (text[i] != "") { fat.Add(Int32.Parse(text[i]), Int32.Parse(text[i + 1])); }
                else i--;
            }
            reader.Close();


        }

        public void Scanner()
        {
            bool find = false;
            foreach (var current in directory)
            {
                find = fat.ContainsKey(current.Value);
                if (find) {
                    file.Add(current.Key, AllFile(current));
                }
                else { }
            }
            Console.WriteLine("Исходная ФС:");
            PrintfSystem();
            CheckCoincidence();
            CheckSystem();
            BadBackupbt();
            //CorrectFileSystem();
            file = new Dictionary<string, List<int>>();
            foreach (var current in directory)
            {  
                    file.Add(current.Key, AllFile(current));
            }

        }

        public List<int> AllFile(KeyValuePair<string, int> current) {
            List<int> array = new List<int>();
            List<int> arrFile = new List<int>();
            int value,  lastValue;
            bool check, cycle=false;
            value = current.Value;
            lastValue = value;
            while (value != 1)
            {
                cycle = false;
                
          
                if (value == -1)
                {
                    cycle = true;
                }
                if (cycle) break;

                array.Add(value);
                lastValue = value;
                check=fat.TryGetValue(lastValue, out value);
                if (check == false) { value = 1; }

            }
            return array;
        }

        public void BadBackupbt()
        {
            Console.WriteLine();
            Console.WriteLine("Дефектные блоки:");
            string filePath = @"reserveBad.txt";
            StreamWriter writer = new StreamWriter(filePath);
            foreach (var t in fat)
            {
                if (t.Value == -1) { writer.Write(t.Key + " ");
                    Console.Write(t.Key + " ");
                }
            }
            writer.Close();
        }

        public void CheckCoincidence()
        {
            Console.WriteLine();
            Console.WriteLine("Файлы с пересекающимеся блоками: ");
            List<int> array1;
            List<int> array2;
            bool s = false;
            foreach (var t in file)
            {
                file.TryGetValue(t.Key, out array1);
                foreach (var f in file)
                {
                    if (t.Key != f.Key)
                    {
                        file.TryGetValue(f.Key, out array2);
                        for (int i=0; i<array1.Count; i++)
                        {
                        for(int j=0; j<array2.Count; j++)
                            {
                                if (array1[i] == array2[j])
                                {
                                    s = true;
                                    for (int k=2; k<50; k++)
                                    {
                                        if ((fat[k]==0)) { 
                                        if (j != 0) { 
                                                fat[array2[j-1]]= k;
                                                if((j+1)< array2.Count) fat[k]= array2[j+1];
                                                else fat[k]=1;
                                                array2[j] = k;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (s) Console.Write(t.Key + " и " + f.Key + "; ");
                        s = false;
                    }
                    
                }
            }
        }

        public void CheckSystem()
        {
            Console.WriteLine();
            Console.Write("Потерянные кластеры:");
            Console.WriteLine();
            char name;
            bool g=false;
            KeyValuePair<string, int> current;
         for (int i=2; i<50; i++)
            {
                if (fat[i]==0) LinkMatrix.Add(0);
                else LinkMatrix.Add(1);
            }
         for(int j=0; j<LinkMatrix.Count; j++)
            {
                if (LinkMatrix[j]==1) {
                    foreach (var node in fat) {
                    if (node.Value == j+2) { g = true; break; }
                    }
                    if ((g == false)&&(fat[j+2] != -1))
                    {

                     
                            foreach (var dir in directory)
                            {
                                if (dir.Value == j+2) { g = true; break; }
                            }
                            if (g == false)
                            {
                            Console.Write(j + 2 + "; ");
                            name = (char)(directory.Keys.Count + 65);
                                directory.Add("XXX" + name.ToString(), j+2);
                                current = new KeyValuePair<string, int>("XXX" + name.ToString(), j+2);
                                file.Add("XXX" + name.ToString(), AllFile(current));
                            }
                        
                    }
                }
                g = false;
            }
        }

   /*    public void CorrectFileSystem()
        {
            List<int> array=new List<int>();
            int k;
            int i = 2;

                foreach (var t in file)
                {
                    file.TryGetValue(t.Key, out array);
                    for (int j=0; j<array.Count; j++)
                    {
                    if ((fat[i] != -1))
                    {
                        for (k = i + 1; k < fat.Count; k++)
                        {
                            if (fat[k] != -1) break;
                        }
                        fat[i] = k;
                        if (j == 0)
                        {
                            directory[t.Key] = i;
                        }
                        if (j == array.Count - 1) fat[i] = 1;
                        i = k;

                    }
                    else i++;
                        
                    }
                }
                for (int m=i; m<50; m++)
            {
                if (fat[m] != -1) fat[m] = 0;
            }
        }*/

        public void SaveFileSystem()
        {
            string filePath1 = @"conclusion1.txt";
            string filePath2 = @"conclusion2.txt";
            StreamWriter writer = new StreamWriter(filePath2);
            foreach (var t in fat)
            {
                writer.WriteLine(t.Key+ " "+t.Value);
            }
            writer.Close();

            StreamWriter writer2 = new StreamWriter(filePath1);
            foreach (var f in directory)
            {
                writer2.WriteLine(f.Key + " " + f.Value);
            }
            writer2.Close();
        }


        public void PrintfSystem()
        {
            Console.WriteLine(" _________________");
            Console.WriteLine("|        |        |");
            foreach (var current in directory)
            {
                Console.WriteLine("|{0,5}   |{1,5}   |", current.Key, current.Value);
                Console.WriteLine("|--------|--------|");
            }
            Console.WriteLine("|________|________|");
            Console.WriteLine();
            Console.WriteLine(" ____________");
            Console.WriteLine("|   |        |");
            foreach (var temp in fat)
            {
                Console.WriteLine("|{0,3}|{1,5}   |", temp.Key, temp.Value);
                Console.WriteLine("|---|--------|");
            }
            Console.WriteLine("|___|________|");
            Console.WriteLine();
            Console.WriteLine("|     |         ");
            foreach (var rat in file)
            {
                Console.Write("|{0,5}|", rat.Key);
                for (int i = 0; i < rat.Value.Count; i++) Console.Write("{0,3} ", rat.Value[i]);
                Console.WriteLine();
            }

        }



    }
}
