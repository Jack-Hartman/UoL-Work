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
                    int[] selected = arrays[FindArray(userSelection.ToLower())];
                    Sort.BubbleSort(selected);
                    Sort.HeapSortInit(selected);
                    Sort.InsertionSort(selected);
                    Sort.MergeSortInit(selected);
                    Sort.QuickSortInit(selected);
                    Console.WriteLine("Quick: "+Sort.quickSortIntOuter + ", " + Sort.quickSortIntInner);
                    Console.WriteLine("Heap: " + Sort.heapSortIntOuter + ", " + Sort.heapSortIntInner);
                    Console.WriteLine("Bubble: " + Sort.bubbleSortIntOuter + ", " + Sort.bubbleSortIntInner);

                }
                else Console.WriteLine("Not a valid array\n");
            }
        }

        private static int FindArray(string find)
        {
            int inc = 0;
            foreach(string array in avaliableArrays)
            {
                if(find == array) break;
                inc++;
            }
            return inc;
        }

        public static void PrintArray(int[] array)
        {
            int iterator = 0;
            int interval = 0;
            if (array.Length > 2000) interval = 50;
            else interval = 10;
            foreach (int item in array)
            {
                if(iterator == interval)
                {
                    Console.Write(item + " ");
                    iterator = 0;
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
