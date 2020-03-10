using System;

namespace SearchSortApp
{
    class Sort
    {
        public static int quickSortIntInner;
        public static int quickSortIntOuter;
        public static int heapSortIntInner;
        public static int heapSortIntOuter;
        public static int bubbleSortIntInner;
        public static int bubbleSortIntOuter;
        public static void QuickSortInit (int[] data)
        {
            quickSortIntInner = 0;
            quickSortIntOuter = 0;
            QuickSort(data, 0, data.Length - 1);
        }

        private static void QuickSort (int[] data, int left, int right)
        {
            Program.PrintArray(data);
            int i, j;
            int pivot, temp;
            i = left;
            j = right;
            pivot = data[(left + right) / 2];
            quickSortIntOuter++;
            do
            {
                while ((data[i] < pivot) && (i < right)) i++;
                while ((pivot < data[j]) && (j > left)) j--;
                quickSortIntInner++;
                if (i <= j)
                {
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                }
            } while (i <= j);
            if (left < j) QuickSort(data, left, j);
            if (i < right) QuickSort(data, i, right);
        }

        public static void HeapSortInit (int[] heap)
        {
            int HeapSize = heap.Length;
            int i;
            for (i = (HeapSize - 1) / 2; i >= 0; i--)
            {
                HeapSort(heap, HeapSize, i);
            }
            for (i = heap.Length - 1; i > 0; i--)
            {
                int temp = heap[i];
                heap[0] = temp;

                HeapSize--;
                HeapSort(heap, HeapSize, 0);
            }
        }

        private static void HeapSort (int[] heap, int heapSize, int index)
        {
            Program.PrintArray(heap);
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;
            heapSortIntOuter++;
            if (left < heapSize && heap[left] > heap[index]) largest = left;
            else largest = index;
            if(right < heapSize && heap[right] > heap[largest]) largest = right;
            if(largest != index)
            {
                heapSortIntInner++;
                int temp = heap[index];
                heap[index] = heap[largest];
                heap[largest] = temp;
                HeapSort(heap, heapSize, largest);
            }
        }

        public static void BubbleSort (int[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bubbleSortIntOuter++;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (a[j + 1] < a[j])
                    {
                        bubbleSortIntInner++;
                        int temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                    }
                }
                Program.PrintArray(a);
            }
        }

        public static void InsertionSort (int[] data)
        {
            int numSorted = 1;
            int index;
            while (numSorted < data.Length)
            {
                int temp = data[numSorted];
                for (index = numSorted; index > 0; index--)
                {
                    if (temp < data[index - 1]) data[index] = data[index - 1];
                    else break;
                }
                data[index] = temp;
                numSorted++;
                Program.PrintArray(data);
            }
        }

        private static void Merge (int[] data, int[] temp, int low, int middle, int high)
        {
            int ri = low;
            int ti = low;
            int di = middle;
            while(ti < middle && di <= high)
            {
                if (data[di] < temp[ti]) data[ri++] = data[di++];
                else data[ri++] = temp[ti++];
            }
            while (ti < middle)
            {
                data[ri++] = temp[ti++];
            }
        }

        private static void MergeSortRecursive (int[] data, int[] temp, int low, int high)
        {
            int n = high - low + 1;
            int middle = low + n / 2;
            int i;
            if (n < 2) return;
            for(i=low; i< middle; i++)
            {
                temp[i] = data[i];
            }
            MergeSortRecursive(temp, data, low, middle - 1);
            MergeSortRecursive(data, temp, middle, high);
            Merge(data, temp, low, middle, high);
            Program.PrintArray(data);
        }

        public static void MergeSortInit (int[] data)
        {
            int[] temp = new int[data.Length];
            MergeSortRecursive(data, temp, 0, data.Length - 1);
        }
    }
}
