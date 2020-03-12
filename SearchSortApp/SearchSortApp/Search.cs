using System;
using System.Collections.Generic;

namespace SearchSortApp
{
    class Search
    {
        public static int binaryCount;
        public static int intpolCount;
        public static void ResetValues()
        {
            binaryCount = 0;
            intpolCount = 0;
        }

        private static List<int> FindNearest(int[] array, int midpoint, int key)
        {
            List<int> found = new List<int>();
            int newMid = midpoint;
            found.Add(midpoint);
            try
            {
                while (key == array[newMid])
                {
                    newMid = newMid + 1;
                    if (key == array[newMid])
                    {
                        found.Add(newMid);
                    }
                }
            }
            catch { };
            newMid = midpoint;
            try
            {
                while (key == array[newMid])
                {
                    newMid = newMid - 1;
                    if (key == array[newMid])
                    {
                        found.Add(newMid);
                    }
                }
            }
            catch { };
            return found;
        }

        private static List<int> FindClosest(int[] array, int left, int right)
        {
            List<int> found = new List<int>();
            if(left != -1 && left <= array.Length - 1) found.AddRange(FindNearest(array, left, array[left]));
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
