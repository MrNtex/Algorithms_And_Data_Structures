using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

public enum ProblemCategory
{
    MilitaryThreat,
    Finance,
    InternalThreat,
    Diplomacy
}

public class Problem
{
    public int Priority { get; }
    public ProblemCategory Category { get; }
    public string Description { get; }

    public Problem(int priority, ProblemCategory category, string description)
    {
        Priority = priority;
        Category = category;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Category} (Priorytet: {Priority}): {Description}";
    }
}

public partial class ProblemHeap
{
    public List<Problem> heap { get; private set; } = new List<Problem>();
    private int size = 0;

    private int Parent(int index) => (index - 1) / 2;
    private int LeftChild(int index) => 2 * index + 1;
    private int RightChild(int index) => 2 * index + 2;

    public void Insert(Problem problem)
    {
        if (size < heap.Count)
            heap[size] = problem;
        else
            heap.Add(problem);
        size++;

        int index = size - 1;

        while (index > 0 && heap[Parent(index)].Priority < heap[index].Priority)
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    public Problem Pop()
    {
        if (size == 0)
            throw new InvalidOperationException("Heap is empty");

        var root = heap[0];
        heap[0] = heap[size - 1];
        size--;

        int index = 0;
        while (index < size)
        {
            int biggest = index;
            int left = LeftChild(index);
            int right = RightChild(index);

            if (left < size && heap[left].Priority > heap[biggest].Priority)
                biggest = left;
            if (right < size && heap[right].Priority > heap[biggest].Priority)
                biggest = right;

            if (biggest == index)
                break;
            else
            {
                Swap(index, biggest);
                index = biggest;
            }
        }
        

        
        return root;
    }

    private void Swap(int i, int j)
    {
        Problem temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    public void DisplayHeap(int index, int level)
    {
        if (index >= size) return;

        DisplayHeap(RightChild(index), level + 1);

        Console.WriteLine(new string(' ', 4 * level) + heap[index].Priority);

        DisplayHeap(LeftChild(index), level + 1);
    }
}

public class Program
{
    const int N = 50;
    public static void Main()
    {
        ProblemHeap heap = new ProblemHeap();

        Random rnd = new Random();
        for (int i = 0; i < N; i++)
        {
            heap.Insert(new Problem(rnd.Next(12), (ProblemCategory)rnd.Next(4), $"Problem {i}"));
        }

        ProblemHeap heap2 = new ProblemHeap();
        for (int i = 0; i < N; i++)
        {
            heap2.Insert(new Problem(rnd.Next(12), (ProblemCategory)rnd.Next(4), $"Problem {i}"));
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        heap.Merge1(heap2);
        sw.Stop();
        Console.WriteLine($"Merge1: {sw.ElapsedMilliseconds}ms");

        heap = new ProblemHeap();
        rnd = new Random();
        for (int i = 0; i < N; i++)
        {
            heap.Insert(new Problem(rnd.Next(12), (ProblemCategory)rnd.Next(4), $"Problem {i}"));
        }

        heap2 = new ProblemHeap();
        for (int i = 0; i < N; i++)
        {
            heap2.Insert(new Problem(rnd.Next(12), (ProblemCategory)rnd.Next(4), $"Problem {i}"));
        }
        sw.Restart();
        heap.Merge2(heap2);
        sw.Stop();
        Console.WriteLine($"Merge2: {sw.ElapsedMilliseconds}ms");

        Dictionary<ProblemCategory, ProblemHeap> divided = heap.Divide();
        foreach (var category in divided)
        {
            Console.WriteLine(category.Key);
            category.Value.DisplayHeap(0,0);
        }
    }
}
