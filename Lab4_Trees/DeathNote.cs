using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DeathNote
{
    public static void DN()
    {
        BST<string> deathNote = new BST<string>();

        deathNote.Add("Light Yagami");
        deathNote.Add("L");
        deathNote.Add("Misa Amane");
        deathNote.Add("Ryuk");

        Console.WriteLine("Death Note:");
        Console.WriteLine("Light Yagami: " + BFS(deathNote.Root, "Light Yagami"));
        Console.WriteLine("Nie pamietam juz postaci: " + BFS(deathNote.Root, "jgfdsbhgjds"));
        Console.WriteLine("L: " + DFS(deathNote.Root, "L"));
        Console.WriteLine("Nie pamietam juz postaci: " + DFS(deathNote.Root, "jgfdsbhgjds"));
    }

    private static bool BFS(TreeNode<string> node, string value)
    {
        if (node == null)
        {
            return false;
        }

        Queue<TreeNode<string>> queue = new Queue<TreeNode<string>>();
        queue.Enqueue(node);

        while (queue.Count != 0)
        {
            TreeNode<string> current = queue.Dequeue();
            if (current.Value.Equals(value))
            {
                return true;
            }

            if (current.Left != null)
            {
                queue.Enqueue(current.Left);
            }

            if (current.Right != null)
            {
                queue.Enqueue(current.Right);
            }
        }

        return false;
    }
    private static bool DFS(TreeNode<string> node, string name)
    {
        if (node == null)
        {
            return false;
        }

        if (node.Value.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (string.Compare(name, node.Value) < 0)
        {
            return DFS(node.Left, name);
        }
        else
        {
            return DFS(node.Right, name);
        }
    }
}