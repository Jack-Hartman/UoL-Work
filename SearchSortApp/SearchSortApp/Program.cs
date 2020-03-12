using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchSortApp
{
    class Program
    {
        public static string[] avaliableArrays = { "net_1_2048", "net_1_256", "net_2_2048", "net_2_256", "net_3_2048", "net_3_256" }; // Array of avaliable files
        public static List<int[]> arrays = new List<int[]>(); // List integer array for files to loaded into
        private static bool error = false; // Error bool
        private static bool asc = false; // Ascending bool

        // Main void to be executed at startup
        static void Main()
        {
            ReadFiles(); // Calles the ReadFiles void
            while (!error) // While error is false
            {
                bool selected = false; // Defines selected boolean
                while (!selected) // While selected is false
                {
                    Console.WriteLine("How many arrays would you like to sort?\none, two"); // Write to console to ask user question
                    string userSelection = Console.ReadLine().ToLower().Trim(); // Gets user input and putting response to lower and trimming
                    if (userSelection == "one") // If user input is one
                    {
                        int[] selectedArray = SelectArray(""); // Get selected array defined as selectedArray by executing SelectArray
                        SortArray(selectedArray); // Execute SortArray and passing selectArray
                        SearchArray(Sort.sortedArray); // Execute SearchArray with the sortedArray from Sort class
                        Sort.ResetValues(); // Executes ResetValues void in Sort calss
                        Search.ResetValues(); // Executes ResetValues void in Search class
                    }
                    else if (userSelection == "two") // Else if user input is two
                    {
                        List<int> merged = new List<int>(); // New list int declared as merged
                        merged.AddRange(SelectArray("1: ")); // Add returned value from SelectArray to merged
                        merged.AddRange(SelectArray("2: ")); // Add returned value from SelectArray to merged
                        SortArray(merged.ToArray()); // Execute SortArray and passing merged list to array
                        SearchArray(Sort.sortedArray); // Execute SearchArray with the sortedArray from Sort class
                        Sort.ResetValues(); // Executes ResetValues void in Sort calss
                        Search.ResetValues(); // Executes ResetValues void in Search class
                    }
                    else Console.WriteLine("Not a valid action"); // Else writes error message
                }
            }
        }

        // SelectArray integer array requries string of extra
        private static int[] SelectArray(string extra)
        {
            bool selected = false; // Defines selected boolean
            int[] selectedArray = null; // Defines selectedArray array as null
            while (!selected) // While selected is false
            {
                Console.WriteLine("\n"+extra+"Please select an array to sort"); // Write to console to ask user question
                Console.WriteLine(string.Join(", ", avaliableArrays) + ", merge"); // Writes list of avaliable files
                string userSelection = Console.ReadLine().ToLower().Trim(); // Gets user input and putting response to lower and trimming
                if (avaliableArrays.Contains(userSelection)) // If avaliableArrays contains the user input(userSelection)
                {
                    selectedArray = arrays[FindArray(userSelection)]; // Sets selectedArray as array found in arrays using FindArray void to return index of array
                    selected = true; // Sets selected bool to true
                }
                else Console.WriteLine("Not a valid array"); // Else writes error message
            }
            return selectedArray; // Returns the selectedArray
        }

        // SortArray void requires integer array
        private static void SortArray(int[] array)
        {
            bool selected = false; // Defines selected boolean
            while (!selected) // While selected is false
            {
                Console.WriteLine("\nPlease select a sort algorithm:\nquick, heap, insertion or merge"); // Write to console to ask user question
                string userSelection = Console.ReadLine().ToLower().Trim(); // Gets user input and putting response to lower and trimming
                switch (userSelection) // Switch on string userSelection
                {
                    case "quick": // When userSelection equals 'quick'
                        Sort.QuickSortInit(array, SortType()); // Calls QuickSortInit void in Sort class passing the array and passing value returned from SortType() void
                        Console.WriteLine("\nOuter - " + Sort.quickSortIntOuter + "\nInner - " + Sort.quickSortIntInner + "\n"); // Writes Outer and Inner values of quickSort
                        selected = true; // Sets selected bool to true
                        break;
                    case "heap": // When userSelection equals 'heap'
                        Sort.HeapSortInit(array, SortType()); // Calls HeapSortInit void in Sort class passing the array and passing value returned from SortType() void
                        Console.WriteLine("\nOuter - " + Sort.heapSortIntOuter + "\nInner - " + Sort.heapSortIntInner + "\n"); // Writes Outer and Inner values of heapSort
                        selected = true; // Sets selected bool to true
                        break;
                    case "insertion": // When userSelection equals 'insertion'
                        Sort.InsertionSort(array, SortType()); // Calls InsertionSort void in Sort class passing the array and passing value returned from SortType() void
                        Console.WriteLine("\nOuter - " + Sort.insertionSortIntOuter + "\nInner - " + Sort.insertionSortIntInner + "\n"); // Writes Outer and Inner values of insertionSort
                        selected = true; // Sets selected bool to true
                        break;
                    case "merge": // When userSelection equals 'merge'
                        Sort.MergeSortInit(array, SortType()); // Calls MergeSortInit void in Sort class passing the array and passing value returned from SortType() void
                        Console.WriteLine("\nOuter - " + Sort.mergeSortIntOuter + "\nInner - " + Sort.mergeSortIntInner + "\n"); // Writes Outer and Inner values of mergeSort
                        selected = true; // Sets selected bool to true
                        break;
                    default: // When userSelection does not equal anything above
                        Console.WriteLine("Not a valid algorithm"); // Writes error message
                        break;
                }
            }
        }

        // SortType boolean
        private static bool SortType()
        {
            bool selected = false; // Defines selected boolean
            bool type = false; // Defines type boolean
            while (!selected) // While selected is false
            {
                Console.WriteLine("\nPlease select a sort order:\nascending, descending"); // Write to console to ask user question
                string userSelection = Console.ReadLine().ToLower().Trim(); // Gets user input and putting response to lower and trimming
                if (userSelection == "ascending") // if userSelection equals 'ascending'
                {
                    type = true; // Sets type bool to true
                    selected = true; // Sets selected bool to true
                    asc = true; // Sets asc bool to true
                }
                else if (userSelection == "descending") // if userSelection equals 'descending'
                {
                    type = false; // Sets type bool to true
                    selected = true; // Sets selected bool to true
                    asc = false; // Sets asc bool to true
                }
                else Console.WriteLine("Please select only ascending or descending"); // Else writes error message
            }
            return type; // Returns type boolean
        }

        // SearchArray void requires integer array
        private static void SearchArray(int[] array)
        {
            bool selected = false; // Defines selected boolean
            while (!selected) // While selected is false
            {
                Console.WriteLine("\nPlease select a search algorithm:\nbinary or interpolation"); // Write to console to ask user question
                string userSelection = Console.ReadLine().ToLower().Trim(); // Gets user input and putting response to lower and trimming
                switch (userSelection) // Switch on string userSelection
                {
                    case "binary": // When userSelection equals 'binary'
                        int binFind = SearchItem(); // Defines binFind int with value returned from SearchItem void
                        List<int> binFound = Search.BinarySearch(binFind, array, 0, array.Length-1, asc); // Defines binFound List integer with returned value from BinarySearch found in Search class
                        Console.WriteLine("Search Complete, number of steps taken:" + Search.binaryCount + "\n"); // Writes counter for binaryCount
                        PrintFindings(array, binFound, binFind); // Calls PrintFindins array passing array, binFound integer and binFind integer
                        selected = true; // Sets selected bool to true
                        break;
                    case "interpolation": // When userSelection equals 'interpolation'
                        int intpolFind = SearchItem(); // Defines intpolFind int with value returned from SearchItem void
                        List<int> intpolfound = Search.InterpolationSearch(array, intpolFind, asc); // Defines intpolfound List integer with returned value from InterpolationSearch found in Search class
                        Console.WriteLine("Search Complete, number of steps taken:" + Search.intpolCount + "\n"); // Writes counter for intpolCount
                        PrintFindings(array, intpolfound, intpolFind); // Calls PrintFindins array passing array, intpolfound integer and intpolFind integer
                        selected = true; // Sets selected bool to true
                        break;
                    default: // When userSelection does not equal anything above
                        Console.WriteLine("Not a valid algorithm"); // Writes error message
                        break;
                }
            }
        }

        // SearchItem integer
        private static int SearchItem()
        {
            bool selected = false; // Defines selected boolean
            int selectedNumber = 0; // Defines selectedNumber integer
            while (!selected) // While selected is false
            {
                Console.WriteLine("\nPlease input a number to search for"); // Write to ask the user to input a number
                string userSelection = Console.ReadLine().Trim(); // Gets user input and putting response and trimming it
                try // Initiates try
                {
                    selectedNumber = int.Parse(userSelection); // Tries to parse inputted number
                    selected = true; // Sets selected bool to true              
                }
                catch // Catches errors
                {
                    Console.WriteLine("Not a number"); // Writes error message
                }
            }
            return selectedNumber; // Returns selectedNumber
        }

        // SearchArray integer requires string find
        private static int FindArray(string find)
        {
            int inc = 0; // Defines increment integer as 0
            foreach(string array in avaliableArrays) // Foreach string in avaliableArrays
            {
                if(find == array) break; // If find string equals array string
                inc++; // Increment integer by one
            }
            return inc; // Returns inc interger
        }

        // PrintFindings void requires integer array, list integer found and integer find
        public static void PrintFindings(int[] array, List<int> found, int find)
        {
            if (array[found.ToArray().First()] == find) // If array location equals find value
            {
                Console.WriteLine("Found at location(s): " + string.Join(", ", found)); // Writes found locations by joining array into string
            }
            else // Else array location does not equal find value
            {
                Console.Write("Unable to find value, Nearest values and locations:"); // Writes where search is able to find nearest values
                int prev = 0; // Defines prev as integer
                foreach (int item in found) // Foreach integer item in found
                { 
                    if (array[item] != prev) // If item equals prev
                    {
                        prev = array[item]; // Sets prev as item
                        Console.Write("\n" + prev + " Found at: " + item); // Writes Nearest value and first location
                    }
                    else // Else item does not equal prev
                    {
                        Console.Write(", " + item); // Writes item location
                    }
                }
            }
            Console.WriteLine(); // WriteLine for spacing
        }

        // PrintArray void requires integer array
        public static void PrintArray(int[] array)
        {
            int iterator = 1; // Defines interator integer
            int interval = 0; // Defines interval integer
            if (array.Length > 2000) // If array length is more than 2000
            {
                interval = 50; // Sets interval as 50
                iterator = 50; // Sets iterator as 50
            }
            else { // Else array is not longer than 2000
                interval = 10; // Sets interval as 10
                iterator = 10; // Sets iterator as 10
            }
            foreach (int item in array) // Foreach integer item in array
            {
                if(iterator == interval) // If iterator equal interval
                {
                    Console.Write(item + " "); // Writes item
                    iterator = 1; // Resets iterator to 1
                }
                iterator++; // Increments iterator by 1
            }
            Console.WriteLine(); // WriteLine for spacing 
        }

        // ReadFiles void
        public static void ReadFiles()
        {
            try // Initiates try
            {
                foreach (string filename in avaliableArrays) // Foreach string filename ini avaliableArrays
                {
                    arrays.Add(Array.ConvertAll(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\TextFiles\\" + filename + ".txt"), item => int.Parse(item))); // Adds new files to list integer array, Converts each array from string array to interger array 
                }
            }
            catch (Exception e) // Catches errors with Exception variable of e
            {
                Console.WriteLine("Error Reading Arrays Press Any Key To Exit\n" + e); // Writes error message with Exception
                Console.ReadKey(true); // Gets user to input any key
                error = true; // Sets error to true
            }
        }
    }
}
