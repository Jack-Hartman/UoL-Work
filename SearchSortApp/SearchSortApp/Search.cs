using System;
using System.Collections.Generic;

namespace SearchSortApp
{
    class Search
    {
        public static int binaryCount; // Defines binaryCount
        public static int intpolCount; // Defines intpolCount

        // ResetValues void
        public static void ResetValues()
        {
            binaryCount = 0; // Sets binaryCount to 0
            intpolCount = 0; // Sets intpolCount to 0
        }

        // FindNearest List integer requires integer array, midpoint integer, key integer
        private static List<int> FindNearest(int[] array, int midPoint, int key)
        {
            List<int> found = new List<int>(); // Defines found as new List integer
            int newMid = midPoint; // Defines newMid to midPoint
            found.Add(midPoint); // Adds integer midPoint to found
            try // Initiates try
            {
                while (key == array[newMid]) // While key equals array value of newMid
                {
                    newMid++; // Increments newMid by 1
                    if (key == array[newMid]) // If key == array value of newMid
                    {
                        found.Add(newMid); // Adds integer midPoint to found
                    }
                }
            }
            catch { }; // Catches errors
            newMid = midPoint; // Sets newMid to midPoint
            try // Initiates try
            {
                while (key == array[newMid]) // While key equals array value of newMid
                {
                    newMid --; // Decrements newMid by 1
                    if (key == array[newMid]) // If key == array value of newMid
                    {
                        found.Add(newMid); // Adds integer midPoint to found
                    }
                }
            }
            catch { }; // Catches errors
            return found; // Returns found list
        }

        // FindClosest List integer requires integer array, left integer, right integer
        private static List<int> FindClosest(int[] array, int left, int right)
        {
            List<int> found = new List<int>(); // Defines found as new List integer
            if (left != -1 && left <= array.Length - 1) found.AddRange(FindNearest(array, left, array[left])); // If left != -1 and left less or equal to array length minus 1, add returned value of FindNearest() to found
            if (right != -1 && right <= array.Length - 1) found.AddRange(FindNearest(array, right, array[right])); // If right != -1 and right less or equal to array length minus 1, add returned value of FindNearest() to found
            return found; // Return found list array
        }

        // BinarySearch List interger requires key integer, integer array, integer left, integer right and boolean asc
        public static List<int> BinarySearch (int key, int[] array, int left, int right, bool asc)
        {
            int midpoint; // Defines integer midpoint
            List<int> found = new List<int>(); // Defines found new list integer
            while (left <= right) // While left less or equal to right
            {
                binaryCount++; // Increments binaryCount by 1
                if (asc) // If asc equals true
                {
                    midpoint = (left + right) / 2; // Sets midpoint to left + right divided by 2
                    if (key == array[midpoint]) // If key equals array midpoint
                    {
                        return FindNearest(array, midpoint, key); // Returns result from FindNearest void
                    }
                    else if (key > array[midpoint]) left = midpoint + 1; // Else if key is more than array midpoint, sets left to midpoint + 1
                    else right = midpoint - 1; // Else set right to midpoint - 1
                }
                else // Else asc does not equals false
                {
                    midpoint = (left + right) / 2; // Sets midpoint to left + right divided by 2
                    if (key == array[midpoint]) // If key equals array midpoint
                    {
                        return FindNearest(array, midpoint, key); // Returns result from FindNearest void
                    }
                    else if (key < array[midpoint]) left = midpoint + 1; // Else if key is less than array midpoint, sets left to midpoint + 1
                    else right = midpoint + 1; // Else set right to midpoint + 1
                }
            }
            if (found.ToArray().Length == 0) // If found to array length equals 0
            {
                return FindClosest(array, left, right); // Returns value from FindClosest 
            }
            return found; // Returns found List
        }

        // InterpolationSearch List integer requires array interger, interger key, boolean asc
        public static List<int> InterpolationSearch (int[] array, int key, bool asc)
        {
            int low, high, mid; // Defines low, high and mid as integer
            int denom = 0; // Defines denom integer
            low = 0; high = array.Length-1; // Sets low to 0 and high to array length minus 1
            List<int> found = new List<int>(); // Defines found as List integer
            if (asc) // If asc equals true
            {
                if (array[low] <= key && key <= array[high]) // If array[low] is less than or equal to key and key is less then or qual to array[high]
                {
                    while (low <= high) // While low is less than or equal to high
                    {
                        intpolCount++; // Increments intpolCount by 1
                        denom = array[high] - array[low]; // Sets denom to array[high] - array[low]
                        if (denom == 0) mid = low; // If denom equals 0 set mid to low
                        else mid = low + ((key - array[low]) * (high - low) / denom); // Else set mid to low + key - array[low] times high - low divided by denom
                        if (key == array[mid]) return FindNearest(array, mid, key); // If key equals array[mid] return returned value from FindNeares
                        else if (key < array[mid]) high = mid - 1; // Else iif key is less than array[mid] set high to mid - 1
                        else low = mid + 1; // Else sett low to mid = 1
                    }
                }
            }
            else // Else asc does not equals false
            {
                if (array[low] >= key && key >= array[high]) // If array[low] is more than or equal to key and key is more then or qual to array[high]
                {
                    while (low <= high) // While low is less than or equal to high
                    {
                        intpolCount++; // Increments intpolCount by 1
                        denom = array[high] - array[low]; // Sets denom to array[high] - array[low]
                        if (denom == 0) mid = low; // If denom equals 0 set mid to low
                        else mid = low + ((key - array[low]) * (high - low) / denom); // Else set mid to low + key - array[low] times high - low divided by denom
                        if (key == array[mid]) return FindNearest(array, mid, key); // If key equals array[mid] return returned value from FindNearest
                        else if (key < array[mid]) high = mid + 1; // Else iif key is less than array[mid] set high to mid + 1
                        else low = mid - 1; // Else sett low to mid - 1
                    }
                }
            }
            if (found.ToArray().Length == 0) return FindClosest(array, low-1, high); // If found length equals 0 return value from FindClosest
            return found; // return found
        }
    }
}
