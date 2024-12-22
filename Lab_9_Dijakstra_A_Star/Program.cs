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
            Graph.PrintGraph();
        }
    }
}