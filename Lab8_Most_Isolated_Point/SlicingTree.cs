using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDS8
{
    public class Slicing()
    {
        float x;
        float y;
        public void SlicingMethod(List<Point> points)
        {
            List<Point> new_points = new List<Point>();
            bool isX = true;

            int step = 4;

            double x = 0;
            double y = 0;

            while (points.Count > 0)
            {
                double threshold = 1.0 / (step / 2);
                foreach (Point point in points)
                {
                    if (isX && point.X > threshold)
                    {
                        new_points.Add(point);
                    }
                    else if (!isX && point.Y > threshold)
                    {
                        new_points.Add(point);
                    }
                }

                
                if (new_points.Count > points.Count / 2)
                {
                    points = points.Except(new_points).ToList();
                    if (isX)
                    {
                        x += threshold;
                    }
                    else
                    {
                        y += threshold;
                    }
                }
                else
                {
                    points = new_points;
                }
                isX = !isX;
                new_points = new List<Point>();
                foreach (Point point in points)
                {
                    Console.WriteLine($"x: {point.X}, y: {point.Y}");
                }
                Console.WriteLine($"x: {x}-{1.0 / (step / 2)}, y: {y}-{1.0 / (step / 2)}");
                Console.ReadLine(); // Wait
            }
            Console.WriteLine($"x: {x}, y: {y}");
        }
    }
}
