using System;

namespace SearchSortApp
{
    class Sort
    {
        public static int quickSortIntInner;
        public static int quickSortIntOuter;
        public static int heapSortIntInner;
        public static int heapSortIntOuter;
        public static int insertionSortIntInner;
        public static int insertionSortIntOuter;
        public static int mergeSortIntInner;
        public static int mergeSortIntOuter;
        public static int[] sortedArray;

        public static void ResetValues()
        {
            quickSortIntInner = 0;
            quickSortIntOuter = 0;
            heapSortIntInner = 0;
            heapSortIntOuter = 0;
            insertionSortIntInner = 0;
            insertionSortIntOuter = 0;
            mergeSortIntInner = 0;
            mergeSortIntOuter = 0;
        }

        public static void QuickSortInit (int[] data, bool asc)
        {
            QuickSort(data, 0, data.Length - 1, asc);
        }

        private static void QuickSort (int[] data, int left, int right, bool asc)
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
                if (asc)
                {
                    while ((data[i] < pivot) && (i < right)) i++;
                    while ((pivot < data[j]) && (j > left)) j--;
                }
                else
                {
                    while ((data[i] > pivot) && (i < right)) i++;
                    while ((pivot > data[j]) && (j > left)) j--;
                }
                quickSortIntInner++;
                if (i <= j)
                {
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                }
                else sortedArray = data;
            } while (i <= j);
            if (left < j) QuickSort(data, left, j, asc);
            if (i < right) QuickSort(data, i, right, asc);
        }

        public static void HeapSortInit (int[] heap, bool asc)
        {
            int heapSize = heap.Length;
            int i;
            for (i = (heapSize - 1) / 2; i >= 0; i--)
            {
                heapSortIntOuter++;
                HeapSort(heap, heapSize, i, asc);
            }
            for (i = heap.Length - 1; i > 0; i--)
            {
                int temp = heap[i];
                heap[i] = heap[0];
                heap[0] = temp;
                heapSize--;
                heapSortIntOuter++;
                HeapSort(heap, heapSize, 0, asc);
            }
        }

        private static void HeapSort (int[] heap, int heapSize, int index, bool asc)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;
            heapSortIntInner++;
            if (asc)
            {
                if (left < heapSize && heap[left] > heap[index]) largest = left;
                else largest = index;
                if (right < heapSize && heap[right] > heap[largest]) largest = right;
            }
            else
            {
                if (left < heapSize && heap[left] < heap[index]) largest = left;
                else largest = index;
                if (right < heapSize && heap[right] < heap[largest]) largest = right;
            }
            if(largest != index)
            {
                int temp = heap[index];
                heap[index] = heap[largest];
                heap[largest] = temp;
                HeapSort(heap, heapSize, largest, asc);
            }
            sortedArray = heap;
            Program.PrintArray(heap);
        }

        public static void InsertionSort (int[] data, bool asc)
        {
            int numSorted = 1;
            int index;
            while (numSorted < data.Length)
            {
                insertionSortIntOuter++;
                int temp = data[numSorted];
                for (index = numSorted; index > 0; index--)
                {
                    insertionSortIntInner++;
                    if (asc) 
                    {
                        if (temp < data[index - 1]) data[index] = data[index - 1];
                        else
                        {
                            sortedArray = data;
                            break;
                        }
                    }
                    else
                    {
                        if (temp > data[index - 1]) data[index] = data[index - 1];
                        else
                        {
                            sortedArray = data;
                            break;
                        }
                    }
                }
                data[index] = temp;
                numSorted++;
                Program.PrintArray(data);
            }
        }
        public static void MergeSortInit(int[] data, bool asc)
        {
            int[] temp = new int[data.Length];
            MergeSortRecursive(data, temp, 0, data.Length - 1, asc);
        }

        private static void MergeSortRecursive(int[] data, int[] temp, int low, int high, bool asc)
        {
            int n = high - low + 1;
            int middle = low + n / 2;
            int i;
            mergeSortIntOuter++;
            if (n < 2) return;
            for (i = low; i < middle; i++)
            {
                temp[i] = data[i];
            }
            MergeSortRecursive(temp, data, low, middle - 1, asc);
            MergeSortRecursive(data, temp, middle, high, asc);
            Merge(data, temp, low, middle, high, asc);
        }

        private static void Merge (int[] data, int[] temp, int low, int middle, int high, bool asc)
        {
            int ri = low;
            int ti = low;
            int di = middle;
            mergeSortIntInner++;
            while(ti < middle && di <= high)
            {
                if (asc)
                {
                    if (data[di] < temp[ti]) data[ri++] = data[di++];
                    else data[ri++] = temp[ti++];
                }
                else
                {
                    if (data[di] >= temp[ti]) data[ri++] = data[di++];
                    else data[ri++] = temp[ti++];
                }
                
            }
            while (ti < middle)
            {
                data[ri++] = temp[ti++];
            }
            sortedArray = data;
            Program.PrintArray(data);
        }
    }
}
