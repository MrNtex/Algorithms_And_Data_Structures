using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDS8
{
    public class Heatmap
    {
        public static void GenerateHeatmap(List<Point> points, int precision)
        {
            double[][] heatmap = new double[precision][];

            for (int i = 0; i < precision; i++)
            {
                heatmap[i] = new double[precision];

                for (int j = 0; j < precision; j++)
                {
                    double distance = 0;
                    foreach (Point point in points)
                    {
                        if (i == point.X * precision && j == point.Y * precision)
                        {
                            distance = 1;
                            break;
                        }
                        distance += 1/(Math.Pow(i - point.X * precision, 2) + Math.Pow(j - point.Y * precision, 2));
                    }
                    heatmap[i][j] = distance;
                }
            }

            string fileName = $"heatmap_{precision}";
            string filePath = $"C:\\Users\\ntexy\\Documents\\algorytithms\\Lab8_Most_Isolated_Point\\{fileName}.txt";

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
            {
                for (int i = 0; i < precision; i++)
                {
                    writer.WriteLine(string.Join(",", heatmap[i]));
                }
            }
        }
    }
}
