using System;

namespace SearchSortApp
{
    class Sort
    {
        public static void QuickSortInit(int[] data)
        {
            QuickSort(data, 0, data.Length - 1);
        }

        private static void QuickSort(int[] data, int left, int right)
        {
            int i, j;
            int pivot, temp;
            i = left;
            j = right;
            pivot = data[(left + right) / 2];
            do
            {
                while ((data[i] < pivot) && (i < right)) i++;
                while ((pivot < data[j]) && (j > left)) j--;
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

        public static void HeapSortInit(int[] heap)
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

        private static void HeapSort(int[] heap, int heapSize, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;
            if (left < heapSize && heap[left] > heap[index]) largest = left;
            else largest = index;
            if(right < heapSize && heap[right] > heap[largest]) largest = right;
            if(largest != index)
            {
                int temp = heap[index];
                heap[index] = heap[largest];
                heap[largest] = temp;
                HeapSort(heap, heapSize, largest);
            }
        }

        public static void BubbleSort(int[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (a[j + 1] < a[j])
                    {
                        int temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                    }
                }
            }
        }

        public static void InsertionSort(int[] data)
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
            }
        }
    }
}
