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

    public enum NodeType
    {
        Start,
        End,
        Empty
    }

    public class Node
    {
        public int x { get; set; }
        public int y { get; set; }
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
            Neighbors = [(parent, cost)];
        }
    }

    public class Graph
    {
        public static Dictionary<(int,int), Node> nodes = new Dictionary<(int, int), Node>();
        static string[] labyrinth = new string[10];

        public static void BuildGraph(string[] newLabyrinth)
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
                    nodes.Add((0, i), new Node(0,i));
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

                    List<(int, int, int)> parentDirs = GetDirs(dir.Item1, dir.Item2, 0);
                    if (parentDirs.Count != 2)
                    {
                        // add node

                        if (nodes.ContainsKey((dir.Item1, dir.Item2)))
                        {
                            // It's already in the graph
                            parent.Neighbors.Add((nodes[(dir.Item1, dir.Item2)], dir.Item3));
                            nodes[(dir.Item1, dir.Item2)].Neighbors.Add((parent, dir.Item3));
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

                        continue;
                    }

                    foreach ((int, int, int) d in parentDirs)
                    {
                        if(!visited.Contains((d.Item1, d.Item2))) dirs.Enqueue(d);
                    }
                }
                
            }

            Console.WriteLine("Graph built");
            foreach (KeyValuePair<(int, int), Node> entry in nodes)
            {
                if (entry.Value.nodeType == NodeType.Start)
                {
                    Console.WriteLine("Start");
                }
                else if (entry.Value.nodeType == NodeType.End)
                {
                    Console.WriteLine("End");
                }
                Console.WriteLine(entry.Key);
            }
        }

        public static List<(int, int, int)> GetDirs(int row, int col, int cost)
        {
            List<(int, int, int)> dirs = new List<(int, int, int)>();

            if (row > 0 && labyrinth[row - 1][col] != 'x')
            {
                dirs.Add((row - 1, col, cost+1));
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
    }

}
