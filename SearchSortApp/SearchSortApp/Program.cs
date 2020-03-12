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
        public static bool error = false;

        static void Main()
        {
            ReadFiles();
            while (!error)
            {
                int[] selectedArray = SelectArray();
                SortArray(selectedArray);
                SearchArray(Sort.sortedArray);
                Sort.ResetValues();
            }
        }

        private static int[] SelectArray()
        {
            bool selected = false;
            int[] selectedArray = null;
            while (!selected)
            {
                Console.WriteLine("Please select an array to sort");
                Console.WriteLine(string.Join(", ", avaliableArrays));
                string userSelection = Console.ReadLine().ToLower().Trim();
                if (avaliableArrays.Contains(userSelection))
                {
                    selectedArray = arrays[FindArray(userSelection)];
                    selected = true;
                }
                else Console.WriteLine("Not a valid array");
            }
            return selectedArray;
        }

        private static void SortArray(int[] array)
        {
            bool selected = false;
            int[] sortedArray = { };
            while (!selected)
            {
                Console.WriteLine("\nPlease select a sort algorithm:\nquick, heap, insertion or merge");
                string userSelection = Console.ReadLine().ToLower().Trim();
                switch (userSelection)
                {
                    case "quick":
                        Sort.QuickSortInit(array, SortType());
                        Console.WriteLine("\nOuter - " + Sort.quickSortIntOuter + "\nInner - " + Sort.quickSortIntInner + "\n");
                        selected = true;
                        break;
                    case "heap":
                        Sort.HeapSortInit(array);
                        Console.WriteLine("\nOuter - " + Sort.heapSortIntOuter + "\nInner - " + Sort.heapSortIntInner + "\n");
                        selected = true;
                        break;
                    case "insertion":
                        Sort.InsertionSort(array, SortType());
                        Console.WriteLine("\nOuter - " + Sort.insertionSortIntOuter + "\nInner - " + Sort.insertionSortIntInner + "\n");
                        selected = true;
                        break;
                    case "merge":
                        Sort.MergeSortInit(array);
                        Console.WriteLine("\nOuter - " + Sort.mergeSortIntOuter + "\nInner - " + Sort.mergeSortIntInner + "\n");
                        selected = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid algorithm");
                        break;
                }
            }
        }

        private static bool SortType()
        {
            bool selected = false;
            bool type = false;
            while (!selected)
            {
                Console.WriteLine("\nPlease select a sort order:\nascending, descending");
                string userSelection = Console.ReadLine().ToLower().Trim();
                if (userSelection == "ascending")
                {
                    type = true;
                    selected = true;
                }
                else if (userSelection == "descending")
                {
                    type = false;
                    selected = true;
                }
                else
                {
                    Console.WriteLine("Please select only ascending or descending");
                }
            }
            return type;
        }

        private static void SearchArray(int[] array)
        {
            bool selected = false;
            while (!selected)
            {
                Console.WriteLine("\nPlease select a search algorithm:\nbinary or interpolation");
                string userSelection = Console.ReadLine().ToLower().Trim();
                switch (userSelection)
                {
                    case "binary":
                        Console.WriteLine(Search.BinarySearch(SearchItem(), array, array.Last(), array.First()));
                        selected = true;
                        break;
                    case "interpolation":
                        Console.WriteLine(Search.BinarySearchRecursive(SearchItem(), array, 0, array.Length));
                        selected = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid algorithm");
                        break;
                }
            }
        }

        private static int SearchItem()
        {
            bool selected = false;
            int selectedNumber = 0;
            while (!selected)
            {
                Console.WriteLine("\nPlease input a number to search for");
                string userSelection = Console.ReadLine().Trim();
                try
                {
                    selectedNumber = int.Parse(userSelection);
                    selected = true;                    
                }
                catch
                {
                    Console.WriteLine("Not a number");
                }
            }
            return selectedNumber;
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
            int iterator = 1;
            int interval = 0;
            if (array.Length > 2000)
            {
                interval = 50;
                iterator = 50;
            }
            else { 
                interval = 10; 
                iterator = 10; 
            }
            foreach (int item in array)
            {
                if(iterator == interval)
                {
                    Console.Write(item + " ");
                    iterator = 1;
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
                    arrays.Add(Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\TextFiles\\" + filename + ".txt"), item => int.Parse(item)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Reading Arrays Press Any Key To Exit\n" + e);
                Console.ReadKey(true);
                error = true;
            }
        }
    }
}
