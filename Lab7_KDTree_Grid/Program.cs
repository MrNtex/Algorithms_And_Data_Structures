
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace AIDS7
{
    public class Artifact
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public bool Collected { get; set; }

        public Artifact(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
            Collected = false;
        }
    }

    class Program
    {
        const int NUMOFARTIFACTS = 1000;
        static List<Artifact> artifacts = new List<Artifact>();

        static int[] jedi_files = [2, 5, 11, 16, 20];

        static void Main(string[] args) // O(K(JlogJ + AlogA))  gdzie J to ilosc Jedi a A to ilosc artefaktow i K to ilosc rund
        {
            double[] results = new double[3];

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < NUMOFARTIFACTS; i++)
            {
                artifacts.Add(new Artifact(0,0,0));
            }

            ParsePositions("C:\\Users\\ntexy\\OneDrive\\Documents\\AGH\\AiSD\\AIDS7\\AIDS7\\dane_testowe_1\\pozycje_poczatkowe.txt");

            for (int i = 1; i <= 20; i++)
            {
                ParsePositions($"C:\\Users\\ntexy\\OneDrive\\Documents\\AGH\\AiSD\\AIDS7\\AIDS7\\dane_testowe_1\\przemieszczenia_{i}.txt"); 

                if (jedi_files.Contains(i))
                {
                    Console.WriteLine($"\n---Jedi positions for step {i}---");

                    var jediPositions = GetJediPositions(i);
                    // Find the nearest jedi for each artifact
                    
                    //Build KDTree for jedi
                    KDTree kdTree = new KDTree(jediPositions);

                    int[] numOfArtifactsPerJedi = new int[10];
                    int notCollected = 0;
                    foreach (Artifact artifact in artifacts)
                    {
                        
                        var nearestJedi = kdTree.NearestNeighbor(kdTree.root, (new KDNode(artifact)).point, 0);
                        if (nearestJedi != null)
                        {
                            //Console.WriteLine($"\t Artifact at ({artifact.X}, {artifact.Y}) is closest to Jedi {nearestJedi.artifactId}");
                            numOfArtifactsPerJedi[nearestJedi.artifactId-1]++;
                        }
                        else
                        {
                            notCollected++;
                        }    

                    }

                    Console.WriteLine($"\n---Number of artifacts collected by each Jedi for step {i}---");
                    for (int j = 0; j < 10; j++)
                    {
                        Console.WriteLine($"Jedi {j+1}: {numOfArtifactsPerJedi[j]}");
                    }
                    Console.WriteLine($"Not collected: {notCollected}");

                }
            }
            stopWatch.Stop();
            Console.WriteLine($"Execution Time: {stopWatch.ElapsedMilliseconds} ms");
            results[0] = stopWatch.ElapsedMilliseconds;

            results[1] = BruteForce();
            results[2] = Grid();

            Console.WriteLine($"\n---Execution Time---");
            Console.WriteLine($"KDTree: {results[0]} ms");
            Console.WriteLine($"Brute Force: {results[1]} ms");
            Console.WriteLine($"Grid: {results[2]} ms");
        }


        static double BruteForce()
        {
            Console.WriteLine("Brute Force\n");
            Stopwatch stopWatch = new Stopwatch();
            artifacts.Clear();
            stopWatch.Start();
            for (int i = 0; i < NUMOFARTIFACTS; i++)
            {
                artifacts.Add(new Artifact(0, 0, 0));
            }

            ParsePositions("C:\\Users\\ntexy\\OneDrive\\Documents\\AGH\\AiSD\\AIDS7\\AIDS7\\dane_testowe_1\\pozycje_poczatkowe.txt");

            for (int i = 1; i <= 20; i++)
            {
                ParsePositions($"C:\\Users\\ntexy\\OneDrive\\Documents\\AGH\\AiSD\\AIDS7\\AIDS7\\dane_testowe_1\\przemieszczenia_{i}.txt");

                if (jedi_files.Contains(i))
                {
                    Console.WriteLine($"\n---Jedi positions for step {i}---");

                    var jediPositions = GetJediPositions(i);

                    int[] numOfArtifactsPerJedi = new int[10];
                    int notCollected = 0;
                    foreach (Artifact artifact in artifacts)
                    {
                        double minDistance = double.MaxValue;
                        int nearestJedi = -1;
                        for (int j = 0; j < 10; j++)
                        {
                            double distance = Math.Sqrt(Math.Pow(artifact.X - jediPositions[j].X, 2) + Math.Pow(artifact.Y - jediPositions[j].Y, 2));
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                nearestJedi = j;
                            }
                        }
                        if (minDistance <= .5 && nearestJedi != -1)
                        {
                            //Console.WriteLine($"\t Artifact at ({artifact.X}, {artifact.Y}) is closest to Jedi {nearestJedi}");
                            numOfArtifactsPerJedi[nearestJedi]++;
                        }
                        else
                        {
                            notCollected++;
                        }
                    }

                    Console.WriteLine($"\n---Number of artifacts collected by each Jedi for step {i}---");
                    for (int j = 0; j < 10; j++)
                    {
                        Console.WriteLine($"Jedi {j}: {numOfArtifactsPerJedi[j]}");
                    }
                    Console.WriteLine($"Not collected: {notCollected}");
                }
            }

            stopWatch.Stop();
            Console.WriteLine($"Execution Time: {stopWatch.ElapsedMilliseconds} ms");
            return stopWatch.ElapsedMilliseconds;
        }
        
        static double Grid()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("Grid\n");
            artifacts.Clear();

            const int divisions = 100;
            List<Artifact>[][] grid = new List<Artifact>[1000/ divisions][];
            for (int i = 0; i < 1000/ divisions; i++)
            {
                grid[i] = new List<Artifact>[1000/ divisions];
                for (int j = 0; j < 1000/ divisions; j++)
                {
                    grid[i][j] = new List<Artifact>();
                }
            }

            for (int i = 0; i < NUMOFARTIFACTS; i++)
            {
                artifacts.Add(new Artifact(0, 0, 0));
            }
            ParsePositions("C:\\Users\\ntexy\\OneDrive\\Documents\\AGH\\AiSD\\AIDS7\\AIDS7\\dane_testowe_1\\pozycje_poczatkowe.txt");

            foreach (Artifact artifact in artifacts)
            {
                int x = (int)artifact.X / divisions;
                int y = (int)artifact.Y / divisions;
                grid[x][y].Add(artifact);
            }

            for (int i = 1; i <= 20; i++)
            {
                ParsePositions($"C:\\Users\\ntexy\\OneDrive\\Documents\\AGH\\AiSD\\AIDS7\\AIDS7\\dane_testowe_1\\przemieszczenia_{i}.txt");

                if (jedi_files.Contains(i))
                {
                    Console.WriteLine($"\n---Jedi positions for step {i}---");

                    var jediPositions = GetJediPositions(i);

                    int[] numOfArtifactsPerJedi = new int[10];
                    int collected = 0;
                    foreach (Artifact jedi in jediPositions)
                    {
                        // Check the 3x3 grid around the jedi
                        int x = (int)jedi.X / divisions;
                        int y = (int)jedi.Y / divisions;

                        for (int j = x - 1; j <= x + 1; j++)
                        {
                            for (int k = y - 1; k <= y + 1; k++)
                            {
                                if (j >= 0 && j < 1000 / divisions && k >= 0 && k < 1000 / divisions)
                                {
                                    foreach (Artifact artifact in grid[j][k])
                                    {
                                        double distance = Math.Sqrt(Math.Pow(artifact.X - jedi.X, 2) + Math.Pow(artifact.Y - jedi.Y, 2));
                                        if (distance <= .5 && !artifact.Collected)
                                        {
                                            //Console.WriteLine($"\t Artifact at ({artifact.X}, {artifact.Y}) is closest to Jedi {jedi.Id}");
                                            numOfArtifactsPerJedi[jedi.Id - 1]++;
                                            collected++;
                                            artifact.Collected = true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    Console.WriteLine($"\n---Number of artifacts collected by each Jedi for step {i}---");
                    for (int j = 0; j < 10; j++)
                    {
                        Console.WriteLine($"Jedi {j + 1}: {numOfArtifactsPerJedi[j]}");
                    }
                }
            }

            stopWatch.Stop();
            Console.WriteLine($"Execution Time: {stopWatch.ElapsedMilliseconds} ms");
            return stopWatch.ElapsedMilliseconds;
        }

        static void ParsePositions(string filePath)
        {
            var file = File.ReadLines(filePath);
            for (int i = 0; i < NUMOFARTIFACTS; i++)
            {
                var parts = file.ElementAt(i).Split(' ');
                artifacts[i].Id = int.Parse(parts[0]);
                artifacts[i].X += double.Parse(parts[1]);
                artifacts[i].Y += double.Parse(parts[2]);
            }
        }
        static List<Artifact> GetJediPositions(int idx)
        {
            const int NUMOFJEDIS = 10;
            List<Artifact> jediPositions = new List<Artifact>();

            string filePath = $"C:\\Users\\ntexy\\OneDrive\\Documents\\AGH\\AiSD\\AIDS7\\AIDS7\\jedi_coords\\jedi_po_kroku_{idx}.txt";

            Console.WriteLine(filePath);
            var file = File.ReadLines(filePath);
            for (int i = 0; i < NUMOFJEDIS; i++)
            {
                var parts = file.ElementAt(i).Split(' ');
                jediPositions.Add(new Artifact(int.Parse(parts[0]), double.Parse(parts[1]), double.Parse(parts[2])));
            }

            return jediPositions;
        }
    }
}