using System;
using System.IO;
using System.Diagnostics;
public class Program
{
    private const int numberOfFiles = 6;
    public static int[][] data = new int[6][];
	public static void Main()
    {
        Sort.MergeSort([5, 3, 1, 4]);
        for (int i = 0; i < numberOfFiles; i++)
        {
            string path = "data_" + (i+1) + ".txt";
            string[] lines = File.ReadAllLines(path);
            data[i] = new int[lines.Length];
            for (int j = 0; j < lines.Length; j++)
            {
                data[i][j] = int.Parse(lines[j]);
            }
        }

        // Test data
        for (int i = 0; i < numberOfFiles; i++)
        {
            if (data[i] == null || data[i].Length < 1)
            {
                throw new Exception("Data is empty");
            }
        }

        string csvFilePath = "results.csv";
        if (File.Exists(csvFilePath))
        {
            File.Delete(csvFilePath);
        }

        using (StreamWriter sw = File.AppendText(csvFilePath))
        {
            sw.WriteLine("Algorithm;DataSize;Time;Memory");
            foreach (var result in SortData(Sort.MergeSort))
            {
                sw.WriteLine(result.ToCSV());
            }
            foreach (var result in SortData(Sort.QuickSort))
            {
                sw.WriteLine(result.ToCSV());
            }
            foreach (var result in SortData(Sort.ShellSort))
            {
                sw.WriteLine(result.ToCSV());
            }
            foreach (var result in SortData(Sort.BubbleSort))
            {
                sw.WriteLine(result.ToCSV());
            }
        }

        Console.WriteLine("Results saved to " + csvFilePath);
        
        
    }

    public static TestResult[] SortData(Action<int[]> sortMethod)
    {
        TestResult[] results = new TestResult[numberOfFiles];

        int[][] clonedData = new int[numberOfFiles][];
        for (int i = 0; i < numberOfFiles; i++)
        {
            clonedData[i] = new int[data[i].Length];
            Array.Copy(data[i], clonedData[i], data[i].Length);
        }

        

        for (int i = 0; i < numberOfFiles; i++)
        {
            if (sortMethod == Sort.BubbleSort && (i >= 2) || sortMethod == Sort.QuickSort && (i == 3 || i == 4 || i == 5))
            {
                continue;
            }
            bool extendStack = (sortMethod == Sort.QuickSort && (i == 3 || i == 4 || i == 5)) ;
            Stopwatch sw = new Stopwatch();
            long memory = GC.GetTotalMemory(false);
            sw.Start();
            if (extendStack)
            {
                Console.WriteLine("Extending stack");
                Thread thread = new Thread(() => sortMethod(clonedData[i]), 1024 * 1024 * 1024);  // 1 GB stack size
                thread.Start();
                thread.Join(); // Wait for the thread to finish
            }
            else
            {
                sortMethod(clonedData[i]);
            }
            sw.Stop();
            memory = GC.GetTotalMemory(false) - memory;

            if (!Sort.IsSorted(clonedData[i])) throw new Exception("Data is not sorted");

            TestResult result = new TestResult
            {
                Algorithm = sortMethod.Method.Name,
                DataSize = clonedData[i].Length,
                Time = sw.ElapsedMilliseconds,
                Memory = memory
            };

            Console.WriteLine("Algorithm: " + result.ToString());
            results[i] = result;
        }
        return results;
    }
}

public struct TestResult
{
    public string Algorithm;
    public int DataSize;
    public long Time;
    public long Memory;

    public override string ToString()
    {
        return $"Algorithm: {Algorithm}, DataSize: {DataSize}, Time: {Time}, Memory: {Memory}";
    }

    public string ToCSV()
    {
        return $"{Algorithm};{DataSize};{Time};{Memory}";
    }
}

public static class Sort
{
    public static bool IsSorted(int[] data)
    {
        for (int i = 0; i < data.Length - 1; i++)
        {
            if (data[i] > data[i + 1])
            {
                return false;
            }
        }
        return true;
    }
    public static void BubbleSort(int[] data)
    {
        int n = data.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (data[j] > data[j + 1])
                {
                    int temp = data[j];
                    data[j] = data[j + 1];
                    data[j + 1] = temp;
                }
            }
        }
    }

    public static void ShellSort(int[] data)
    {
        int n = data.Length;

        for (int gap = n/2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = data[i];

                int j;
                for (j = i; j >= gap && data[j - gap] > temp; j -= gap)
                {
                    data[j] = data[j - gap];
                }

                data[j] = temp;
            }
        }
    }

    public static void QuickSort(int[] data)
    {
        Sort(0, data.Length-1);

        void Sort(int low, int high)
        {
            if (low >= high) return;

            int pivot = Partition(low, high);
            Sort(low, pivot - 1);
            Sort(pivot + 1, high);
        }

        int Partition(int low, int high)
        {
            int pivot = data[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (data[j] < pivot)
                {
                    i++;
                    int temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                }
            }

            i++;
            int temp2 = data[i];
            data[i] = data[high];
            data[high] = temp2;

            return i;
        }
    }

    public static void MergeSort(int[] data)
    {
        Sort(data);
        void Sort(int[] arr)
        {
            int n = arr.Length;

            if (n < 2) return;

            int mid = n / 2;
            int[] leftArray = new int[mid];
            int[] rightArray = new int[n - mid];

            Array.Copy(arr, 0, leftArray, 0, mid);
            Array.Copy(arr, mid + 1, rightArray, 0, n - mid - 1);

            Sort(leftArray);
            Sort(rightArray);
            Merge(leftArray, rightArray, arr);
        }

        void Merge(int[] leftArray, int[] rightArray, int[] arr)
        {
            int nLeft = leftArray.Length;
            int nRight = rightArray.Length;

            int i = 0, j = 0, k = 0;

            while(i < nLeft && j < nRight)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    arr[k] = leftArray[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            while (i < nLeft)
            {
                arr[k] = leftArray[i];
                i++;
                k++;
            }
            while (j < nRight)
            {
                arr[k] = rightArray[j];
                j++;
                k++;
            }


        }
    }
}