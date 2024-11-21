using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ProblemHeap
{
    public void Merge1(ProblemHeap other) // O(nlogn) ~755ms
    {
        foreach (var problem in other.heap)
            Insert(problem);
    }
    public void Merge2(ProblemHeap other) // O(n)??? ~1159ms
    {
        List<Problem> newHeap = new List<Problem>(size + other.size);

        newHeap.AddRange(heap);
        newHeap.AddRange(other.heap);

        for (int i = newHeap.Count / 2 - 1; i >= 0; i--)
            MaxHeapify(newHeap, size + other.size, i);

        heap = newHeap;

        int n = size + other.size;

        int startIdx = n / 2 - 1; // Find the last non-leaf node
        for (int i = startIdx; i >= 0; i--)
            MaxHeapify(heap, n, i);
    }

    public void MaxHeapify(List<Problem> arr, int n, int idx)
    {
        if (idx >= n)
            return;

        int left = LeftChild(idx);
        int right = RightChild(idx);
        int largest = idx;

        if (left < n && arr[left].Priority > arr[largest].Priority)
            largest = left;
        if (right < n && arr[right].Priority > arr[largest].Priority)
            largest = right;

        if (largest != idx)
        {
            Problem temp = arr[largest];
            arr[largest] = arr[idx];
            arr[idx] = temp;
            MaxHeapify(arr,n, largest);
        }
    }
}
