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
            if (left != -1 && left <= array.Length - 1) found.AddRange(FindNearest(array, left, array[left]));
            if(right != -1 && right <= array.Length - 1) found.AddRange(FindNearest(array, right, array[right]));
            return found;
        }

        public static List<int> BinarySearch (int key, int[] array, int left, int right, bool asc)
        {
            int midpoint;
            List<int> found = new List<int>();
            while (left <= right)
            {
                binaryCount++;
                if (asc)
                {
                    midpoint = (left + right) / 2;
                    if (key == array[midpoint])
                    {
                        return FindNearest(array, midpoint, key);
                    }
                    else if (key > array[midpoint]) left = midpoint + 1;
                    else right = midpoint - 1;
                }
                else
                {
                    midpoint = (left + right) / 2;
                    if (key == array[midpoint])
                    {
                        return FindNearest(array, midpoint, key);
                    }
                    else if (key < array[midpoint]) left = midpoint + 1;
                    else right = midpoint + 1;                    
                }
            }
            if (found.ToArray().Length == 0)
            {
                return FindClosest(array, left, right);
            }
            return found;
        }

        public static List<int> InterpolationSearch (int[] array, int key, bool asc)
        {
            int low, high, mid;
            int denom = 0;
            low = 0; high = array.Length-1;
            List<int> found = new List<int>();
            if (asc)
            {
                if (array[low] <= key && key <= array[high])
                {
                    while (low <= high)
                    {
                        intpolCount++;
                        denom = array[high] - array[low];
                        if (denom == 0) mid = low;
                        else mid = low + ((key - array[low]) * (high - low) / denom);
                        if (key == array[mid]) {
                            return FindNearest(array, mid, key);
                        }
                        else if (key < array[mid]) high = mid - 1;
                        else low = mid + 1;
                    }
                }
            }
            else
            {
                if (array[low] >= key && key >= array[high])
                {
                    while (low <= high)
                    {
                        intpolCount++;
                        denom = array[high] - array[low];
                        if (denom == 0) mid = low;
                        else mid = low + ((key - array[low]) * (high - low) / denom);
                        if (key == array[mid])
                        {
                            return FindNearest(array, mid, key);
                        }
                        else if (key < array[mid]) high = mid + 1;
                        else low = mid - 1;
                    }
                }
            }
            if (found.ToArray().Length == 0)
            {
                return FindClosest(array, low-1, high);
            }
            return found;
        }
    }
}
