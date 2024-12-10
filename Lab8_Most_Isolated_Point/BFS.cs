using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIDS8.Discreatify;

namespace AIDS8
{
    public class BFS_Class
    {
        public static void BFS(List<DiscretePoint> discretePoints, int precision, int area, int[][] shops)
        {
            const int freeSpots = 50;
            int taken = 0;
            Queue<DiscretePoint> queue = new Queue<DiscretePoint>(discretePoints);

            while (queue.Count > 0 && taken < area - freeSpots)
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
