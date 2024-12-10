using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDS8
{
    public class Discreatify
    {
        const int precision = 5000;
        
        const int area = precision * precision;
        int[][] shops = new int[precision][];

        public struct DiscretePoint
        {
            public int X;
            public int Y;
            public int id;

            public DiscretePoint(int x, int y, int id)
            {
                X = x;
                Y = y;
                this.id = id;
            }
        }
        public void Discretify(List<Point> points)
        {

            for (int i = 0; i < precision; i++)
            {
                shops[i] = new int[precision];

                for (int j = 0; j < precision; j++)
                {
                    shops[i][j] = -1;
                }
            }

            List<DiscretePoint> discretePoints = new List<DiscretePoint>();
            foreach (Point point in points)
            {
                int x = (int)(point.X * precision);
                int y = (int)(point.Y * precision);
                discretePoints.Add(new DiscretePoint(x, y, point.id));

                Console.WriteLine($"id: {point.id}, x: {x}, y: {y}");
            }

            
            //BFS_Class.BFS(discretePoints, precision, area, shops);
            Heatmap.GenerateHeatmap(points, precision);
            
        }
    }
}
