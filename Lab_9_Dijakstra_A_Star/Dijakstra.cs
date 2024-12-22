using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDS9
{
    public class Dijakstra
    {
        public class SPT
        {
            public int shortestDistance = int.MaxValue;
            public Node prevNode;
        }
        public List<Node> ShortestPath(Node start, Node end, string[] labirynth)
        {
            Dictionary<Node, SPT> sptSet = new Dictionary<Node, SPT>();
            HashSet<Node> unvisited = new HashSet<Node>(Graph.nodes.Values);

            foreach (var node in Graph.nodes.Values)
            {
                sptSet.Add(node, new SPT());
            }

            sptSet[start].shortestDistance = 0;

            while (unvisited.Count > 0)
            {
                Node? minDistNode = unvisited
                    .OrderBy(n => sptSet[n].shortestDistance)
                    .FirstOrDefault();

                if (minDistNode == null) break;

                foreach (var node in minDistNode.Neighbors)
                {
                    if (sptSet[node.Item1].shortestDistance > sptSet[minDistNode].shortestDistance + node.Item2)
                    {
                        sptSet[node.Item1].shortestDistance = sptSet[minDistNode].shortestDistance + node.Item2;
                        sptSet[node.Item1].prevNode = minDistNode;
                    }
                }

                unvisited.Remove(minDistNode);
            }

            List<Node> shortest_path = new List<Node>();

            foreach (Node node in sptSet.Keys)
            {
                if(node.nodeType == NodeType.End)
                {
                    Node curr = node;

                    while(curr != null)
                    {
                        shortest_path.Add(curr);
                        Console.WriteLine($"({curr.x};{curr.y}) -> ");
                        curr.nodeType = NodeType.Dijakstra;
                        curr.cost = sptSet[curr].shortestDistance;
                        curr = sptSet[curr].prevNode;
                    }

                    shortest_path.Reverse();
                    break;
                }
            }

            return shortest_path;
        }
    }
}
