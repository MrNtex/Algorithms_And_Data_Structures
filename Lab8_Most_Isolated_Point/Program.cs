using System;
using System.IO;

namespace AIDS8
{
    public class Point
    {
        public int id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y, int id)
        {
            X = x;
            Y = y;
            this.id = id;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\ntexy\\OneDrive\\Documents\\AGH\\AiSD\\AIDS8\\AIDS8\\points.csv";

            List<Point> points = new List<Point>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("ID"))
                    {
                        continue;
                    }
                    string[] parts = line.Split(',');
                    int id = int.Parse(parts[0]);
                    double x = double.Parse(parts[1]);
                    double y = double.Parse(parts[2]);
                    Console.WriteLine($"x: {x}, y: {y}");
                    points.Add(new Point(x, y, id));
                }
            }

            Discreatify dis = new Discreatify();
            dis.Discretify(points);
        }

    }
}