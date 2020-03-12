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
        private static bool error = false;
        private static bool asc = false;

        static void Main()
        {
            ReadFiles();
            while (!error)
            {
                bool selected = false;
                while (!selected)
                {
                    Console.WriteLine("How many arrays would you like to sort?\none, two");
                    string userSelection = Console.ReadLine().ToLower().Trim();
                    if (userSelection == "one")
                    {
                        int[] selectedArray = SelectArray("");
                        SortArray(selectedArray);
                        SearchArray(Sort.sortedArray);
                        Sort.ResetValues();
                        Search.ResetValues();
                    }
                    else if (userSelection == "two")
                    {
                        List<int> merged = new List<int>();
                        merged.AddRange(SelectArray("1: "));
                        merged.AddRange(SelectArray("2: "));
                        SortArray(merged.ToArray());
                        SearchArray(Sort.sortedArray);
                        Sort.ResetValues();
                        Search.ResetValues();
                    }
                    else Console.WriteLine("Not a valid action");
                }
                
            }
        }

        private static int[] SelectArray(string extra)
        {
            bool selected = false;
            int[] selectedArray = null;
            while (!selected)
            {
                Console.WriteLine("\n"+extra+"Please select an array to sort");
                Console.WriteLine(string.Join(", ", avaliableArrays) + ", merge");
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
                        Sort.HeapSortInit(array, SortType());
                        Console.WriteLine("\nOuter - " + Sort.heapSortIntOuter + "\nInner - " + Sort.heapSortIntInner + "\n");
                        selected = true;
                        break;
                    case "insertion":
                        Sort.InsertionSort(array, SortType());
                        Console.WriteLine("\nOuter - " + Sort.insertionSortIntOuter + "\nInner - " + Sort.insertionSortIntInner + "\n");
                        selected = true;
                        break;
                    case "merge":
                        Sort.MergeSortInit(array, SortType());
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
                    asc = true;
                }
                else if (userSelection == "descending")
                {
                    type = false;
                    selected = true;
                    asc = false;
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
                        int binfind = SearchItem();
                        List<int> binfound = Search.BinarySearch(binfind, array, 0, array.Length-1, asc);
                        Console.WriteLine("Search Complete, number of steps taken:" + Search.binaryCount + "\n");
                        if(array[binfound.ToArray().First()] == binfind)
                        {
                            Console.WriteLine("Found at location(s): " + string.Join(", ", binfound));
                        }
                        else
                        {
                            PrintFindings(array, binfound, binfind);
                        }
                        selected = true;
                        break;
                    case "interpolation":
                        int intpolfind = SearchItem();
                        List<int> intpolfound = Search.InterpolationSearch(array, intpolfind, asc);
                        Console.WriteLine("Search Complete, number of steps taken:" + Search.intpolCount + "\n");
                        if (array[intpolfound.ToArray().First()] == intpolfind)
                        {
                            Console.WriteLine("Found at location(s): " + string.Join(", ", intpolfound));
                        }
                        else
                        {
                            PrintFindings(array, intpolfound, intpolfind);
                        }
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

        public static void PrintFindings(int[] array, List<int> found, int find)
        {
            if (array[found.ToArray().First()] == find)
            {
                Console.WriteLine("Found at location(s): " + string.Join(", ", found));
            }
            else
            {
                Console.Write("Unable to find value, Nearest values and locations:");
                int prev = 0;
                foreach (int item in found)
                {
                    if (array[item] != prev)
                    {
                        prev = array[item];
                        Console.Write("\n" + prev + " Found at: " + item);
                    }
                    else
                    {
                        Console.Write(", " + item);
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
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
