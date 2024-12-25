using AIDS_10;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

public class Graph
{
    public Dictionary<int, List<int>> adjList;

    public Graph()
    {
        adjList = new Dictionary<int, List<int>>();
    }

    public void AddEdge(int node1, int node2)
    {
        if (!adjList.ContainsKey(node1))
        {
            adjList[node1] = new List<int>();
        }
        if (!adjList.ContainsKey(node2))
        {
            adjList[node2] = new List<int>();
        }

        adjList[node1].Add(node2);
        adjList[node2].Add(node1);
    }

    public List<int> GetNeighbors(int node)
    {
        return adjList.ContainsKey(node) ? adjList[node] : new List<int>();
    }
}

public class Program
{
    public static Graph ReadGraphFromFile(string filename)
    {
        Graph graph = new Graph();

        try
        {
            foreach (var line in File.ReadLines(filename))
            {
                string[] parts = line.Split();
                if (parts.Length == 2)
                {
                    int node1 = int.Parse(parts[0]);
                    int node2 = int.Parse(parts[1]);

                    graph.AddEdge(node1, node2);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
        }

        return graph;
    }

    static void Main()
    {
        string filename = "C:\\Users\\ntexy\\source\\repos\\AIDS_10\\AIDS_10\\facebook_combined.txt";
        Graph graph = ReadGraphFromFile(filename);

        Console.WriteLine("Graph created from file:");
        Console.WriteLine($"Number of nodes: {graph.adjList.Count}");
        Console.WriteLine($"Number of edges: {graph.adjList.Count / 2}");

        const int numberOfTests = 10;

        double[][] results = new double[4][];
        
        for (int i = 0; i < 4; i++)
        {
            results[i] = new double[numberOfTests];
        }

        for (int i = 0; i < numberOfTests; i++)
        {
            Random random = new Random();
            int start = random.Next(0, graph.adjList.Count);
            int target = random.Next(0, graph.adjList.Count);

            Console.WriteLine($"Test {i + 1}: Search from {start} to {target}");
            results[0][i] = StopwatchDecorator.Measure(() => Search.BFS(graph, start, target));
            results[1][i] = StopwatchDecorator.Measure(() => Search.BFSStack(graph, start, target));
            results[1][i] = StopwatchDecorator.Measure(() => Search.BFSStack(graph, start, target));
            results[2][i] = StopwatchDecorator.Measure(() => Search.DepthFirstSearchStack(graph, start, target));
            results[3][i] = StopwatchDecorator.Measure(() => Search.DFSQueue(graph, start, target));
        }

        Console.WriteLine("Results:");
        Console.WriteLine("BFS Queue: " + Average(results[0]));
        Console.WriteLine("BFS Stack: " + Average(results[1]));
        Console.WriteLine("DFS Stack: " + Average(results[2]));
        Console.WriteLine("DFS Queue: " + Average(results[3]));

    }

    static string Average(double[] array)
    {
        double sum = 0;
        int valid = 0;
        int errors = 0;
        foreach (var item in array)
        {
            if(item == -1)
            {
                errors++;
                continue;
            }

            sum += item;
            valid++;
        }

        return $"{sum / array.Length} ms, with {errors} erros.";
    }
}
