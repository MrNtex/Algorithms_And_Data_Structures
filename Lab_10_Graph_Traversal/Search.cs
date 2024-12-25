using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDS_10
{
    public class StopwatchDecorator
    {
        public static double Measure(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            try
            {
                action();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1.0; // Return -1 if an error occurs
            }
            

            stopwatch.Stop();

            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
            return stopwatch.ElapsedMilliseconds;
        }
    }

    public class Search
    {
        public static bool DepthFirstSearchStack(Graph graph, int start, int target)
        {
            HashSet<int> visited = new HashSet<int>();

            Stack<int> stack = new Stack<int>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                int current = stack.Pop();

                if (current == target)
                {
                    return true;
                }

                visited.Add(current);

                foreach (int neighbor in graph.GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                    }
                }
            }

            return false;
        }

        public static bool DFSQueue(Graph graph, int start, int target)
        {

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                if (SmallDFS(current))
                {
                    return true;
                }
            }

            bool SmallDFS(int node)
            {
                if (node == target)
                {
                    return true;
                }

                List<int> neighbors = graph.GetNeighbors(node);

                // If the node has no neighbors, return false
                if (neighbors.Count == 0)
                {
                    return false;
                }

                if (neighbors[0] == target)
                {
                    return true;
                }
                

                // Add the rest of the neighbors to the queue
                foreach (int neighbor in graph.GetNeighbors(node).Skip(1))
                {
                    queue.Enqueue(neighbor);
                }

                return false;
            }

            return false;
        }

        public static bool BFS(Graph graph, int start, int target)
        {
            HashSet<int> visited = new HashSet<int>();

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                if (current == target)
                {
                    return true;
                }

                visited.Add(current);

                foreach (int neighbor in graph.GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return false;
        }

        public static bool BFSStack(Graph graph, int start, int target)
        {
            HashSet<int> visited = new HashSet<int>();

            Stack<int> curr = new Stack<int>();
            
            Stack<int> next = new Stack<int>();
            next.Push(start);

            while (next.Count > 0)
            {
                curr = next;
                while (curr.Count > 0)
                {
                    int current = curr.Pop();

                    if (current == target)
                    {
                        return true;
                    }

                    visited.Add(current);

                    foreach (int neighbor in graph.GetNeighbors(current))
                    {
                        if (!visited.Contains(neighbor))
                        {
                            next.Push(neighbor);
                        }
                    }
                }
            }

            return false;
        }

    }
}
