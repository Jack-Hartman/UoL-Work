using System;

namespace SearchSortApp
{
    class Sort
    {
        public static int quickSortIntInner; // Defines quickSortIntInner integer
        public static int quickSortIntOuter; // Defines quickSortIntOuter integer
        public static int heapSortIntInner; // Defines heapSortIntInner integer
        public static int heapSortIntOuter; // Defines heapSortIntOuter integer
        public static int insertionSortIntInner; // Defines insertionSortIntInner integer
        public static int insertionSortIntOuter; // Defines insertionSortIntOuter integer
        public static int mergeSortIntInner; // Defines mergeSortIntInner integer
        public static int mergeSortIntOuter; // Defines mergeSortIntOuter integer
        public static int[] sortedArray; // Defines sortedArray integer array

        // ResetValues void
        public static void ResetValues()
        {
            quickSortIntInner = 0; // Sets quickSortIntInner integer to 0
            quickSortIntOuter = 0; // Sets quickSortIntOuter integer to 0
            heapSortIntInner = 0; // Sets heapSortIntInner integer to 0
            heapSortIntOuter = 0; // Sets heapSortIntOuter integer to 0
            insertionSortIntInner = 0; // Sets insertionSortIntInner integer to 0
            insertionSortIntOuter = 0; // Sets insertionSortIntOuter integer to 0
            mergeSortIntInner = 0; // Sets mergeSortIntInner integer to 0
            mergeSortIntOuter = 0; // Sets mergeSortIntOuter integer to 0
        }

        // QuickSortInit void, requires integer array and boolean
        public static void QuickSortInit (int[] data, bool asc)
        {
            QuickSort(data, 0, data.Length - 1, asc); // Calls QuickSort void using data integer array, length of the array and the asc boolean
        }

        // QuickSortInit void, requires integer array, left integer, right integer and boolean
        private static void QuickSort (int[] data, int left, int right, bool asc)
        {
            Program.PrintArray(data); // Calls PrintArray from Program passing data integer array
            int i, j; // Defines integer i and j
            int pivot, temp; // Defines integer pivot and temp
            i = left; // Sets i integer to left integer
            j = right; // Sets j integer to right integer
            pivot = data[(left + right) / 2]; // Sets pivot integer to value in data array using left and right divided by 2
            quickSortIntOuter++; // Increments quickSortIntOuter by 1
            do // Do statement for loop
            {
                if (asc) // If asc boolean equal true
                {
                    while ((data[i] < pivot) && (i < right)) i++; // While data array value of i is less than pivot and i is more than right increment i by 1
                    while ((pivot < data[j]) && (j > left)) j--; // while pivot is less than data array value of j and j is more than left decrement j by 1
                }
                else // Else asc boolean does not equal true
                {
                    while ((data[i] > pivot) && (i < right)) i++; // While data array value of i is more than pivot and i is more than right increment i by 1
                    while ((pivot > data[j]) && (j > left)) j--; // While pivot is more than data array value of j and j is more than left decrement j by 1
                }
                quickSortIntInner++; // Increments quickSortIntInner by 1
                if (i <= j) // if i is less than or equal to j
                { 
                    temp = data[i]; // Sets temp to data array value of i
                    data[i] = data[j]; // Sets data array value of i to data array value of j 
                    data[j] = temp; // Sets data array value of j to temp
                    i++; // Increments i by 1
                    j--; // Decrements j by 1
                }
                else sortedArray = data; // Else i is more than j, set sortedArray to data array
            } while (i <= j); // While loop definition for do, while i is less than or equal to j
            if (left < j) QuickSort(data, left, j, asc); // If left is more than j, call QuickSort passing data, left, j, asc
            if (i < right) QuickSort(data, i, right, asc); // If i is more than right, call QuickSort passing data, i , right, asc
        }

        // HeapSortInit void requires heap integer array and asc boolean
        public static void HeapSortInit (int[] heap, bool asc)
        {
            int heapSize = heap.Length; // Defines heapSize integer to size of heap array
            int i; // Defines i integer
            for (i = (heapSize - 1) / 2; i >= 0; i--) // For i equals heapsize minus 1 divided by two, i more than or equal to 0, decrement i by one
            {
                heapSortIntOuter++; // Increments heapSortIntOuter by one
                HeapSort(heap, heapSize, i, asc); // Calls HeapSort void passing heap, heapsize, i and asc
            }
            for (i = heap.Length - 1; i > 0; i--) // For i = heap length -1, i is more than -, decrement i by 1
            {
                int temp = heap[i]; // Defines temp as heap array value of i
                heap[i] = heap[0]; // Sets heap array value of i to heap array value of 0
                heap[0] = temp; // Sets heap array value of 0 to temp
                heapSize--; // Decrements heapSzie by 1
                heapSortIntOuter++; // Increments heapSortIntOuter by 1
                HeapSort(heap, heapSize, 0, asc); // Calls HeapSort void passing heap, heapsize, 0 and asc
            }
        }

        // HeapSort void requires heap integer array, heapSize integer, index integer and asc boolean
        private static void HeapSort (int[] heap, int heapSize, int index, bool asc)
        {
            int left = (index + 1) * 2 - 1; // Defines left integer
            int right = (index + 1) * 2; // Defines right integer
            int largest = 0; // Defines larges  integer
            heapSortIntInner++; // Increments heapSortIntInner by 1
            if (asc) // If asc boolean equals true
            {
                if (left < heapSize && heap[left] > heap[index]) largest = left; // If left integer more than heapSize integer and heap array value left is more than heap array value index, set largest to left
                else largest = index; // Else set largest to index
                if (right < heapSize && heap[right] > heap[largest]) largest = right; // If right is less than heapSize and heap array value right is more than heap array value largest, set largest to right
            }
            else // Else asc does not equal true
            {
                if (left < heapSize && heap[left] < heap[index]) largest = left; // If left integer more than heapSize integer and heap array value left is less than heap array value index, set largest to left
                else largest = index; // Else set largest to index
                if (right < heapSize && heap[right] < heap[largest]) largest = right; // If right is less than heapSize and heap array value right is less than heap array value largest, set largest to right
            }
            if(largest != index) // If largest does not equal index
            {
                int temp = heap[index]; // Defines temp as heap array value of i
                heap[index] = heap[largest]; // Sets heap array value index to heap array value largest
                heap[largest] = temp; // Sets heap array value 0 to temp
                HeapSort(heap, heapSize, largest, asc);
            }
            sortedArray = heap; // Set sortedArray to heap array
            Program.PrintArray(heap); // Calls PrintArray from Program passing heap integer array
        }

        // InsertionSort void requires data array and asc bool
        public static void InsertionSort (int[] data, bool asc)
        {
            int numSorted = 1; // Defines numSorted
            int index; // Defines index
            while (numSorted < data.Length) // While numSorted is more than data length
            { 
                insertionSortIntOuter++; // Increments insertionSortIntOuter by one
                int temp = data[numSorted]; // Defines temp
                for (index = numSorted; index > 0; index--) // For index equals numSorted, index more than 0, decrement index by 1
                {
                    insertionSortIntInner++; // Increments insertionSortIntInner by one
                    if (asc) // If asc equals true
                    {
                        if (temp < data[index - 1]) data[index] = data[index - 1]; // If temp is less than data array value of index minus 1, set data array value of index to data array value of index minus 1
                        else
                        {
                            sortedArray = data; // Set sortedArray to data
                            break;
                        }
                    }
                    else
                    {
                        if (temp > data[index - 1]) data[index] = data[index - 1]; // If temp is more than data array value of index minus 1, set data array value of index to data array value of index minus 1
                        else
                        {
                            sortedArray = data; // Set sortedArray to data
                            break;
                        }
                    }
                }
                data[index] = temp; // Set data array value of index to temp
                numSorted++; // Increment numSorted by one
                Program.PrintArray(data); // Calls PrintArray from Program passing data integer array
            }
        }

        // MergeSortInit void requires data integer array and asc boolean
        public static void MergeSortInit(int[] data, bool asc)
        {
            int[] temp = new int[data.Length]; // Defines temp array with limit of data length
            MergeSortRecursive(data, temp, 0, data.Length - 1, asc); // Calls MergeSortRecursive passing data array, temp, 0, data length minus 1 and asc
        }

        // MergeSortRecursive void requires data integer array, temp array, low integer, high integer and asc boolean
        private static void MergeSortRecursive(int[] data, int[] temp, int low, int high, bool asc)
        {
            int n = high - low + 1; // Defines n integer
            int middle = low + n / 2; // Defines middle integer
            int i; // Defines i integer
            mergeSortIntOuter++; // Increments MergeSortIntOuter by 1
            if (n < 2) return; // If n is more than 2 return
            for (i = low; i < middle; i++) // For i equals low, i is more than middle, increment i by 1
            {
                temp[i] = data[i]; // Sets temp array value of i to data array value of i
            } 
            MergeSortRecursive(temp, data, low, middle - 1, asc); // Calls MergeSortRecursive passing temp array, data, low, middle minus 1 and asc 
            MergeSortRecursive(data, temp, middle, high, asc); // Calls MergeSortRecursive passing data array, temp, middle, high and asc 
            Merge(data, temp, low, middle, high, asc); // Calls Merge passing data array, temp, middle, high and asc
        }

        // Merge void requires data array, temp array, low integer, middle integer, high integer and asc boolean
        private static void Merge (int[] data, int[] temp, int low, int middle, int high, bool asc)
        {
            int ri = low; // Defines ri
            int ti = low; // Defines ti
            int di = middle; // Defines di
            mergeSortIntInner++; // Increments mergeSortIntInner by 1
            while (ti < middle && di <= high) // While ti is less tthan middle and di is less or equal to high
            {
                if (asc) // If asc equals true
                {
                    if (data[di] < temp[ti]) data[ri++] = data[di++]; // If data[di] is more than temp[ti], set data[ri+1] = data[di+1]
                    else data[ri++] = temp[ti++]; // Else data[ri+1] = temp[ti+1]
                }
                else // Else
                {
                    if (data[di] >= temp[ti]) data[ri++] = data[di++];  // If data[di] is more or equal to temp[ti], set data[ri+1] = data[di+1]
                    else data[ri++] = temp[ti++]; // Else data[ri+1] = temp[ti+1] 
                }
            }
            while (ti < middle) // While ti is less then middle
            {
                data[ri++] = temp[ti++]; // Sets data array value of ri +1 to temp array value of ti +1
            }
            sortedArray = data; // Set sortedArray to data array
            Program.PrintArray(data); // Calls PrintArray from Program passing data integer array
        }
    }
}
