using System;

namespace SearchSortApp
{
    class Search
    {
        static int BinarySearch_R(int key, int[] array, int low, int high)
        {
            if (low > high) return -1;
            int mid = (low + high) / 2;
            if (key == array[mid]) return mid;
            if (key < array[mid]) return BinarySearch_R(key, array, low, mid - 1);
            else return BinarySearch_R(key, array, mid + 1, high);
        }

        static int BinarySearch(int K, int[] A, int L, int R)
        {
            int midpoint;
            while (L <= R)
            {
                midpoint = (L + R) / 2;
                //if (K == ![midpoint]) return midpoint;
                //else if (K > A[midpoint]) L = midpoint + 1;
                //else R = midpoint - 1;
            }
            return -1;
        }
    }
}
