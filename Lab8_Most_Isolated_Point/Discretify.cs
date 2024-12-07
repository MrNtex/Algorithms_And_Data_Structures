using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDS8
{
    public class Discreatify
    {
        const int precision = 1000;
        const int freeSpots = 50;
        const int area = precision * precision;
        int[][] shops = new int[precision][];

        int taken = 0;

        bool semaphor = false;

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

            Queue<DiscretePoint> queue = new Queue<DiscretePoint>(discretePoints);


            while (queue.Count > 0 && taken < area - 1)
            {
                DiscretePoint point = queue.Dequeue();
                int x = point.X;
                int y = point.Y;

                if (x < 0 || x >= precision || y < 0 || y >= precision)
                {
                    continue;
                }

                if (shops[x][y] == -1)
                {
                    shops[x][y] = point.id;
                    taken++;
                    queue.Enqueue(new DiscretePoint(x + 1, y, point.id));
                    queue.Enqueue(new DiscretePoint(x - 1, y, point.id));
                    queue.Enqueue(new DiscretePoint(x, y + 1, point.id));
                    queue.Enqueue(new DiscretePoint(x, y - 1, point.id));
                }

                
            }

            foreach (DiscretePoint dp in discretePoints)
            {
                shops[dp.X][dp.Y] = -2;
            }

            string fileName = $"grid_{precision}_{freeSpots}";
            string filePath = $"C:\\Users\\ntexy\\Documents\\algorytithms\\Lab8_Most_Isolated_Point\\{fileName}.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < precision; i++)
                {
                    writer.WriteLine(string.Join(",", shops[i]));
                }
            }
        }
    }
}
