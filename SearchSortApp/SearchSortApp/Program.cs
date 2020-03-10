using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchSortApp
{
    class Program
    {
        public static string[] avaliableArrays = { "net_1_2048", "net_1_256", "net_2_2048", "net_2_256", "net_3_2048", "net_3_256" };
        public static List<int[]> arrays = new List<int[]>();
        public static bool complete = false;

        static void Main()
        {
            ReadFiles();
            while (!complete)
            {
                Console.WriteLine("Please Select An Array To Sort");
                Console.WriteLine(string.Join(", ", avaliableArrays));
                string userSelection = Console.ReadLine();
                if (avaliableArrays.Contains(userSelection.ToLower()))
                {
                    Console.WriteLine("Selected " + userSelection.ToLower());
                    int[] selected = arrays[Array.IndexOf(avaliableArrays, userSelection)];
                }
                else
                {
                    Console.WriteLine("Not a valid array\n");
                }
            }
        }

        public static void PrintArray(int[] array, int n)
        {
            int iterator = 0;
            foreach (int item in array)
            {
                if(iterator == n)
                {
                    Console.Write(item + " ");
                }
                iterator++;                
            }
            Console.WriteLine();
        }

        public static void ReadFiles()
        {
            try
            {
                foreach (string filename in avaliableArrays)
                {
                    arrays.Add(Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\TextFiles\\" + filename + ".txt"), s => int.Parse(s)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Reading Arrays Press Any Key To Exit\n" + e);
                Console.ReadKey(true);
                complete = true;
            }
        }
    }
}
