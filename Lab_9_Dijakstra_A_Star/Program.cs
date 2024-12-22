namespace AIDS9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = "C:\\Users\\ntexy\\source\\repos\\AIDS9\\AIDS9\\Labirynth.txt";
            string[] labyrinth = System.IO.File.ReadAllLines(path);

            List<Node> nodes = Graph.BuildGraph(labyrinth);
            if (nodes == null)
            {
                Console.WriteLine("Invalid labyrinth");
                return;
            }
            Node start = nodes.Find(node => node.nodeType == NodeType.Start);
            Node end = nodes.Find(node => node.nodeType == NodeType.End);

            Dijakstra dijakstra = new Dijakstra();
            dijakstra.ShortestPath(start, end, labyrinth);
            Graph.PrintGraph("Dijakstra.txt");
            AStar aStar = new AStar();
            aStar.ShortestPath(start, end, labyrinth);
            Graph.PrintGraph("Astar.txt");
            BellmanFord bellmanFord = new BellmanFord();
            bellmanFord.ShortestPath(start, end, labyrinth);
            Graph.PrintGraph("BellmanFord.txt");

            nodes = Graph.PrimitiveGraph(labyrinth);
            start = nodes.Find(node => node.nodeType == NodeType.Start);
            end = nodes.Find(node => node.nodeType == NodeType.End);
            Graph.PrintGraph();
            dijakstra.ShortestPath(start, end, labyrinth);
            
            Graph.PrintGraph("Dijakstra_Primitive.txt");
            aStar.ShortestPath(start, end, labyrinth);
            
            Graph.PrintGraph("Astar_Primitive.txt");

            bellmanFord.ShortestPath(start, end, labyrinth);
            Graph.PrintGraph("BellmanFord_Primitive.txt");
        }
    }
}