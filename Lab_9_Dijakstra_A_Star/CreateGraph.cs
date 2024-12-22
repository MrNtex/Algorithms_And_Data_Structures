using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDS9
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;

    public enum NodeType
    {
        Start,
        End,
        Empty,
        Dijakstra,
        DeadEnd
    }

    public class Node
    {
        public int x { get; set; }
        public int y { get; set; }

        public int cost { get; set; }
        public List<(Node, int)> Neighbors { get; set; }

        public NodeType nodeType = NodeType.Empty;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
            Neighbors = new List<(Node, int)>();
        }

        public Node(int x, int y, int cost, Node parent)
        {
            this.x = x;
            this.y = y;
            this.cost = cost;
            Neighbors = [(parent, cost)];
        }
    }

    public class Graph
    {
        public static Dictionary<(int, int), Node> nodes = new Dictionary<(int, int), Node>();
        static string[] labyrinth = new string[10];

        public static List<Node> BuildGraph(string[] newLabyrinth)
        {
            labyrinth = newLabyrinth;

            int rows = labyrinth.Length;
            int cols = labyrinth[0].Length;

            Stack<Node> activeNodes = new Stack<Node>();
            // find start
            for (int i = 0; i < cols; i++)
            {
                if (labyrinth[0][i] == 'A')
                {
                    nodes.Add((0, i), new Node(0, i));
                    activeNodes.Push(nodes[(0, i)]);
                    activeNodes.Peek().nodeType = NodeType.Start;
                    break;
                }
            }

            while (activeNodes.Count > 0)
            {
                // x, y, cost
                Queue<(int, int, int)> dirs = new Queue<(int, int, int)>();
                Node parent = activeNodes.Pop();
                HashSet<(int, int)> visited = new HashSet<(int, int)>();
                visited.Add((parent.x, parent.y));

                List<(int, int, int)> origins = GetDirs(parent.x, parent.y, 0);

                foreach ((int, int, int) d in origins)
                {
                    dirs.Enqueue(d);
                }

                // BFS

                while (dirs.Count > 0)
                {
                    (int, int, int) dir = dirs.Dequeue();
                    if (visited.Contains((dir.Item1, dir.Item2)))
                    {
                        continue;
                    }
                    visited.Add((dir.Item1, dir.Item2));

                    List<(int, int, int)> parentDirs = GetDirs(dir.Item1, dir.Item2, dir.Item3);
                    if (parentDirs.Count != 2)
                    {
                        // add node

                        if (nodes.ContainsKey((dir.Item1, dir.Item2)))
                        {
                            // It's already in the graph
                            Node node1 = nodes[(dir.Item1, dir.Item2)];
                            parent.Neighbors.Add((node1, dir.Item3));
                            node1.Neighbors.Add((parent, dir.Item3));
                            node1.cost = Math.Min(node1.cost, dir.Item3);
                            continue;
                        }

                        Node node = new Node(dir.Item1, dir.Item2, dir.Item3, parent);
                        activeNodes.Push(node);
                        node.Neighbors.Add((parent, dir.Item3));
                        parent.Neighbors.Add((node, dir.Item3));
                        nodes.Add((dir.Item1, dir.Item2), node);

                        if (labyrinth[dir.Item1][dir.Item2] == 'B')
                        {
                            node.nodeType = NodeType.End;
                        }
                        else if (parentDirs.Count == 1)
                        {
                            node.nodeType = NodeType.DeadEnd;
                        }

                        continue;
                    }

                    foreach ((int, int, int) d in parentDirs)
                    {
                        if (!visited.Contains((d.Item1, d.Item2))) dirs.Enqueue(d);
                    }
                }

            }

            Console.WriteLine("Graph built");
            PrintGraph();

            return new List<Node>(nodes.Values);
        }

        public static List<(int, int, int)> GetDirs(int row, int col, int cost)
        {
            List<(int, int, int)> dirs = new List<(int, int, int)>();

            if (row > 0 && labyrinth[row - 1][col] != 'x')
            {
                dirs.Add((row - 1, col, cost + 1));
            }
            if (row < labyrinth.Length - 1 && labyrinth[row + 1][col] != 'x')
            {
                dirs.Add((row + 1, col, cost + 1));
            }
            if (col > 0 && labyrinth[row][col - 1] != 'x')
            {
                dirs.Add((row, col - 1, cost + 1));
            }
            if (col < labyrinth[0].Length - 1 && labyrinth[row][col + 1] != 'x')
            {
                dirs.Add((row, col + 1, cost + 1));
            }
            return dirs;
        }
        public static void Color(Node node)
        {
            switch (node.nodeType)
            {
                case NodeType.Start:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case NodeType.End:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case NodeType.DeadEnd:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case NodeType.Dijakstra:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
        public static void PrintGraph()
        {
            int rows = labyrinth.Length;
            int cols = labyrinth[0].Length;

            foreach (KeyValuePair<(int, int), Node> entry in nodes)
            {
                // Save the current color
                var originalColor = Console.ForegroundColor;

                Color(entry.Value);

                Console.WriteLine($"{entry.Key} - {entry.Value.nodeType.ToString()}");

                // Restore the original color
                Console.ForegroundColor = originalColor;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (nodes.ContainsKey((i, j)))
                    {
                        // Save the current color
                        var originalColor = Console.ForegroundColor;

                        Color(nodes[(i, j)]);

                        Console.Write(nodes[(i, j)].cost);

                        // Restore the original color
                        Console.ForegroundColor = originalColor;
                    }
                    else
                    {
                        Console.Write(labyrinth[i][j]);
                    }
                }
                Console.WriteLine();
            }

        }
    }

}