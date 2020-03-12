using System;

namespace SearchSortApp
{
    class Search
    {

        public static int BinarySearch(int K, int[] A, int L, int R)
        {
            int midpoint;
            while (L <= R)
            {
                midpoint = (L + R) / 2;
                if (K == A[midpoint]) return midpoint;
                else if (K > A[midpoint]) L = midpoint + 1;
                else R = midpoint - 1;
            }
            return -1;
        }

        public static int BinarySearchRecursive(int key, int[] array, int low, int high)
        {
            if (low > high) return -1;
            int mid = (low + high) / 2;
            if (key == array[mid]) return mid;
            if (key < array[mid]) return BinarySearchRecursive(key, array, low, mid - 1);
            else return BinarySearchRecursive(key, array, mid + 1, high);
        }
    }
}
