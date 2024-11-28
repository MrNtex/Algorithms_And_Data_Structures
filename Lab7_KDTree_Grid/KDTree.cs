using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDS7
{
    public class KDNode
    {
        public const int k = 2;

        public KDNode left;
        public KDNode right;

        public KDPoint point;

        public int artifactId = -1;

        public KDNode(double[] point)
        {
            this.point = new KDPoint(point);
        }
        public KDNode(Artifact artifact)
        {
            this.point = new KDPoint(new double[] { artifact.X, artifact.Y });
            this.artifactId = artifact.Id;
        }
        public class KDPoint
        {
            public double[] point = new double[k];

            public KDPoint(double[] point)
            {
                if (point.Length != k)
                {
                    throw new ArgumentException("Point length must be " + k);
                }
                this.point = point;
            }

            public double Get(int depth)
            {
                return point[depth % k];
            }
        }
    }
    public class KDTree
    {
        const double h = .5;

        public const int k = 2;

        public KDNode root = null;

        public KDTree(KDNode root)
        {
            this.root = root;
        }

        public KDTree(double[][] points)
        {
            root = new KDNode(points[0]);

            for (int i = 1; i < points.Length; i++)
            {
                Insert(points[i]);
            }
        }

        public KDTree(List<Artifact> artifacts)
        {
            root = new KDNode(artifacts[0]);

            foreach (var artifact in artifacts.Skip(1))
            {
                Insert(artifact);
            }
        }
        

        public void Insert(double[] point)
        {
            KDNode current = root;
            int depth = 0;

            while (true)
            {
                if (point[depth % k] < current.point.Get(depth))
                {
                    if (current.left == null)
                    {
                        current.left = new KDNode(point);
                        break;
                    }
                    else
                    {
                        current = current.left;
                    }
                }
                else
                {
                    if (current.right == null)
                    {
                        current.right = new KDNode(point);
                        break;
                    }
                    else
                    {
                        current = current.right;
                    }
                }
                depth++;
            }
        }

        public void Insert(Artifact artifact)
        {
            KDNode current = root;
            int depth = 0;

            while (true)
            {
                if (artifact.X < current.point.Get(depth))
                {
                    if (current.left == null)
                    {
                        current.left = new KDNode(artifact);
                        break;
                    }
                    else
                    {
                        current = current.left;
                    }
                }
                else
                {
                    if (current.right == null)
                    {
                        current.right = new KDNode(artifact);
                        break;
                    }
                    else
                    {
                        current = current.right;
                    }
                }
                depth++;
            }
        }

        public KDNode NearestNeighbor(KDNode root, KDNode.KDPoint target, int depth)
        {
            if (root == null)
            {
                return null;
            }

            KDNode nextBranch = null;
            KDNode otherBranch = null;

            if (target.Get(depth) < root.point.Get(depth))
            {
                nextBranch = root.left;
                otherBranch = root.right;
            }
            else
            {
                nextBranch = root.right;
                otherBranch = root.left;
            }


            // Recurse down the branch that contains the point
            KDNode temp = NearestNeighbor(nextBranch, target, depth + 1);
            KDNode best = Closest(root, temp, target);

            // Check if we need to recurse down the other branch
            double pithagorean = Distance(target, best.point);

            double dist = target.Get(depth) - root.point.Get(depth);

            if (pithagorean >= dist * dist)
            {
                temp = NearestNeighbor(otherBranch, target, depth + 1);
                best = Closest(root, temp, target);
            }

            if (Distance(target, best.point) < h)
                return best;

            //Console.WriteLine($"Dla punktu {target.point} nie ma Jedi");
            return null;
        }
        public KDNode Closest(KDNode a, KDNode b, KDNode.KDPoint target)
        {
            if (a == null) return b;

            if (b == null) return a;

            double distA = Distance(a.point, target);
            double distB = Distance(b.point, target);

            if (distA < distB)
                return a;
            else
                return b;
        }
        public static double Distance(KDNode.KDPoint a, KDNode.KDPoint b)
        {
            double distance = 0;
            for (int i = 0; i < k; i++)
            {
                distance += Math.Pow(a.point[i] - b.point[i], 2);
            }
            return distance; // We don't need to take the square root, since we're only comparing distances
        }
    }
}