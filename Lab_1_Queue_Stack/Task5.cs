namespace Zad5
{
    public struct Vector2
    {
        public int x;
        public int y;

        public double Distance(Vector2 from)
        {
            return Math.Sqrt(Math.Pow(from.x - x, 2) + Math.Pow(from.y - y, 2));
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Location
    {
        public Vector2 pos;
        public string name;

        public Stack<Location> far = new Stack<Location>();

        public Location(Vector2 pos, string name)
        {
            this.pos = pos;
            this.name = name;
        }
    }
    public class Program
    {
        private static Location[] Locations()
        {
            Location[] loc = new Location[8];
            Random rnd = new Random();

            for (int i = 0; i < 8; i++)
            {
                loc[i] = new Location(new Vector2(rnd.Next(0, 100), rnd.Next(0, 100)), $"Location {i}");
            }

            return loc;
        }
        public static void Main(string[] args)
        {
            float r = 50;

            Location[] pos = Locations();

            List<Location> best = new List<Location>();

            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = 0; j < pos.Length; j++)
                {
                    if (i == j)
                        continue;

                    if (pos[i].pos.Distance(pos[j].pos) >= r)
                    {
                        pos[i].far.Push(pos[j]);
                    }
                }
                Console.WriteLine($"Location {pos[i].name} has {pos[i].far.Count} far locations.");
                if (best.Count == 0 || pos[i].far.Count > best[0].far.Count)
                {
                    best.Clear();
                    best.Add(pos[i]);
                }
                else if (pos[i].far.Count == best[0].far.Count)
                {
                    best.Add(pos[i]);
                }
            }

            if (best[0].far.Count == 0)
            {
                Console.WriteLine("No location is far from any other location.");
                return;
            }

            foreach (Location i in best)
            {
                Console.WriteLine($"Location {i.name} is an isolated location.");
            }
        }
    }
}