using System;

class Program
{
    public static void Main(string[] args)
    {
        // DeathNote.DN();
        Random rand = new Random();
        BST<int> tree = new BST<int>();
        for (int i = 0; i < 15; i++)
        {
            tree.Add(rand.Next(1, 100));
        }
        Console.WriteLine("Height: " + tree.GetHeight());
        Console.WriteLine("Span: " + tree.GetSpan());
        tree.PrintTree();

        tree.Balance();
        Console.WriteLine("Height: " + tree.GetHeight());
        Console.WriteLine("Span: " + tree.GetSpan());
        tree.PrintTree();
    }
}

public class TreeNode<T> where T : IComparable<T>
{
    public T Value;
    public TreeNode<T> Left;
    public TreeNode<T> Right;

    public TreeNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

public class BST<T> where T : IComparable<T>
{
    public TreeNode<T> Root;

    public BST()
    {
        Root = null;
    }

    // Dodawanie nowego węzła
    public void Add(T value)
    {
        Root = AddRecursive(Root, value);
    }

    private TreeNode<T> AddRecursive(TreeNode<T> node, T value)
    {
        if (node == null)
        {
            return new TreeNode<T>(value);
        }

        if (value.CompareTo(node.Value) < 0)
        {
            node.Left = AddRecursive(node.Left, value);
        }
        else if (value.CompareTo(node.Value) > 0)
        {
            node.Right = AddRecursive(node.Right, value);
        }

        return node;
    }

    // Usuwanie węzła
    public void Remove(T value)
    {
        Root = RemoveRecursive(Root, value);
    }

    private TreeNode<T> RemoveRecursive(TreeNode<T> node, T value)
    {
        if (node == null)
        {
            return null;
        }

        if (value.CompareTo(node.Value) < 0)
        {
            node.Left = RemoveRecursive(node.Left, value);
        }
        else if (value.CompareTo(node.Value) > 0)
        {
            node.Right = RemoveRecursive(node.Right, value);
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }

            node.Value = FindMinValue(node.Right);
            node.Right = RemoveRecursive(node.Right, node.Value);
        }

        return node;
    }

    private T FindMinValue(TreeNode<T> node)
    {
        T minValue = node.Value;
        while (node.Left != null)
        {
            minValue = node.Left.Value;
            node = node.Left;
        }
        return minValue;
    }

    public bool Search(T value)
    {
        return SearchRecursive(Root, value);
    }

    private bool SearchRecursive(TreeNode<T> node, T value)
    {
        if (node == null)
        {
            return false;
        }

        if (value.CompareTo(node.Value) == 0)
        {
            return true;
        }

        return value.CompareTo(node.Value) < 0
            ? SearchRecursive(node.Left, value)
            : SearchRecursive(node.Right, value);
    }

    public int GetHeight()
    {
        return GetHeightRecursive(Root);
    }

    private int GetHeightRecursive(TreeNode<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        int leftHeight = GetHeightRecursive(node.Left);
        int rightHeight = GetHeightRecursive(node.Right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }

    public int GetSpan()
    {
        return GetDepthRecursive(Root, true) - GetDepthRecursive(Root, false);
    }

    private int GetDepthRecursive(TreeNode<T> node, bool findMaxDepth)
    {
        if (node == null)
        {
            return 0;
        }

        int leftDepth = GetDepthRecursive(node.Left, findMaxDepth);
        int rightDepth = GetDepthRecursive(node.Right, findMaxDepth);

        return findMaxDepth ? Math.Max(leftDepth, rightDepth) + 1 : Math.Min(leftDepth, rightDepth) + 1;
    }

    private void StoreInOrder(TreeNode<T> root, List<TreeNode<T>> inOrder)
    {
        if (root == null)
        {
            return;
        }

        StoreInOrder(root.Left, inOrder);
        inOrder.Add(root);
        StoreInOrder(root.Right, inOrder);
    }

    public void Balance()
    {
        List<TreeNode<T>> nodes = new List<TreeNode<T>>();
        StoreInOrder(Root, nodes);
        Root = BalanceRecursive(nodes, 0, nodes.Count - 1);
    }

    private TreeNode<T> BalanceRecursive(List<TreeNode<T>> nodes, int start, int end)
    {
        if (start > end)
        {
            return null;
        }

        int mid = (start + end) / 2;
        TreeNode<T> node = nodes[mid];

        node.Left = BalanceRecursive(nodes, start, mid - 1);
        node.Right = BalanceRecursive(nodes, mid + 1, end);

        return node;
    }

    public void PrintTree()
    {
        PrintTreeRecursive(Root, 0);
    }

    private void PrintTreeRecursive(TreeNode<T> node, int level)
    {
        if (node == null)
        {
            return;
        }

        PrintTreeRecursive(node.Right, level + 1);


        Console.WriteLine(new string(' ', level * 4) + node.Value);

        PrintTreeRecursive(node.Left, level + 1);
    }
}
