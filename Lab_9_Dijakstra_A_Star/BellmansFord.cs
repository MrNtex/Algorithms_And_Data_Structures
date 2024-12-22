using System;
using System.Collections.Generic;
using System.Linq;

namespace AIDS9
{
    public class BellmanFord
    {
        public class Edge
        {
            public Node From { get; set; }
            public Node To { get; set; }
            public int Weight { get; set; }

            public Edge(Node fromNode, Node toNode, int weight)
            {
                From = fromNode;
                To = toNode;
                Weight = weight;
            }
        }

        public class SPT
        {
            public int Distance { get; set; }
            public Node PreviousNode { get; set; }

            public SPT()
            {
                Distance = int.MaxValue;
                PreviousNode = null;
            }
        }

        public List<Node> ShortestPath(Node start, Node end, string[] labirynth)
        {
            List<Edge> edges = new List<Edge>();
            foreach (var node in Graph.nodes.Values)
            {
                foreach (var neighbor in node.Neighbors)
                {
                    edges.Add(new Edge(node, neighbor.Item1, neighbor.Item2));
                }
            }

            Dictionary<Node, SPT> sptSet = new Dictionary<Node, SPT>();
            foreach (var node in Graph.nodes.Values)
            {
                sptSet[node] = new SPT();
            }

            sptSet[start].Distance = 0;

            // Relax edges
            for (int i = 1; i < Graph.nodes.Count; i++)
            {
                foreach (var edge in edges)
                {
                    if (sptSet[edge.From].Distance != int.MaxValue &&
                        sptSet[edge.From].Distance + edge.Weight < sptSet[edge.To].Distance)
                    {
                        sptSet[edge.To].Distance = sptSet[edge.From].Distance + edge.Weight;
                        sptSet[edge.To].PreviousNode = edge.From;
                    }
                }
            }

            foreach (var edge in edges)
            {
                if (sptSet[edge.From].Distance != int.MaxValue &&
                    sptSet[edge.From].Distance + edge.Weight < sptSet[edge.To].Distance)
                {
                    throw new InvalidOperationException("Graph contains a negative weight cycle");
                }
            }

            List<Node> shortestPath = new List<Node>();
            Node current = end;

            while (current != null)
            {
                shortestPath.Add(current);
                current = sptSet[current].PreviousNode;
            }

            shortestPath.Reverse();

            foreach (var node in shortestPath)
            {
                node.nodeType = NodeType.BellmanFord;
                node.cost = sptSet[node].Distance;
            }

            return shortestPath;
        }
    }
}
