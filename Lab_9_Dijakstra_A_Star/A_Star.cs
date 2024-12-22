using System;
using System.Collections.Generic;
using System.Linq;

namespace AIDS9
{
    public class AStar
    {
        public class SPT
        {
            public int shortestDistance = int.MaxValue;  // g(n)
            public Node prevNode;
            public int totalCost = int.MaxValue; // f(n) = g(n) + h(n)
        }

        enum HeuristicType
        {
            Manhattan,
            Euclidean
        }

        private int Heuristic(Node node, Node goal)
        {
            HeuristicType heuristicType = HeuristicType.Manhattan;

            if (heuristicType == HeuristicType.Euclidean)
            {
                return (int)Math.Sqrt(Math.Pow(node.x - goal.x, 2) + Math.Pow(node.y - goal.y, 2));
            }

            // Manhattan Distance
            return Math.Abs(node.x - goal.x) + Math.Abs(node.y - goal.y);
        }

        public List<Node> ShortestPath(Node start, Node end, string[] labirynth)
        {
            // First initialize the sptSet
            Dictionary<Node, SPT> sptSet = new Dictionary<Node, SPT>();

            // Add all nodes to the sptSet and initialize their values
            foreach (var node in Graph.nodes.Values)
            {
                sptSet.Add(node, new SPT());
            }

            // Now that sptSet is initialized, initialize the unvisited set
            SortedSet<Node> unvisited = new SortedSet<Node>(Comparer<Node>.Create((a, b) =>
                sptSet[a].totalCost.CompareTo(sptSet[b].totalCost)));

            // Set start node's g(n) and f(n)
            sptSet[start].shortestDistance = 0;
            sptSet[start].totalCost = Heuristic(start, end);

            while (unvisited.Count > 0)
            {
                Node minDistNode = unvisited.Min;
                unvisited.Remove(minDistNode);

                if (minDistNode == end) break;

                foreach (var neighbor in minDistNode.Neighbors)
                {
                    int tentativeGCost = sptSet[minDistNode].shortestDistance + neighbor.Item2;

                    if (tentativeGCost < sptSet[neighbor.Item1].shortestDistance)
                    {
                        sptSet[neighbor.Item1].shortestDistance = tentativeGCost;
                        sptSet[neighbor.Item1].prevNode = minDistNode;
                        sptSet[neighbor.Item1].totalCost = tentativeGCost + Heuristic(neighbor.Item1, end);

                        unvisited.Add(neighbor.Item1);
                    }
                }
            }

            List<Node> shortestPath = new List<Node>();
            Node curr = end;

            while (curr != null)
            {
                shortestPath.Add(curr);
                curr.nodeType = NodeType.AStar;
                curr = sptSet[curr].prevNode;
            }

            shortestPath.Reverse();
            return shortestPath;
        }
    }
}
