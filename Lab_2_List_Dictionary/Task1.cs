
using System;

public class ArrayList
{
    private object[] items;
    
    public int count;

    public ArrayList(int capacity = 4)
    {
        items = new object[capacity];
        count = 0;
    }

    public void Resize()
    {
        object[] newItems = new object[items.Length * 2];

        for (int i = 0; i < items.Length; i++)
        {
            newItems[i] = items[i];
        }
        items = newItems;
    }

    public void Add(object item)
    {
        if (count == items.Length)
        {
            Resize();
        }
        items[count] = item;
        count++;
    }

    public object RemoveAt(int idx)
    {
        if (idx < 0 || idx >= count)
        {
            throw new IndexOutOfRangeException();
        }

        object item = items[idx];

        for (int i = idx; i < count; i++)
        {
            items[i] = items[i + 1];
        }

        count--;

        return item;
    }
    public object? Remove(object obj)
    {
        // Find the index of the object
        for (int i = 0; i < count; i++)
        {
            if (items[i].Equals(obj))
            {
                return RemoveAt(i);
            }
        }

        // If the object is not found, return null

        return null;
    }

    public bool isEmpty()
    {
        return count == 0;
    }

    public int size()
    {
        return count;
    }
}

public class LinkedList
{
    protected class Node
    {
        public object data;
        public Node? next;

        public Node(object data)
        {
            this.data = data;
            this.next = null;
        }
    }

    protected Node? head;
    protected Node? tail;
    protected int count;

    public LinkedList()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public void Add(object data)
    {
        count++;

        if (head == null)
        {
            head = new Node(data);
            tail = head;
            return;
        }

        head.next = new Node(data);
        head = head.next;
    }

    public void Remove(object data)
    {
        Node node = tail;
        if (node == null) throw new ArgumentException("List is empty");

        if (node.data.Equals(data))
        {
            tail = tail.next;
            count--;

            if (tail == null) // Odd case, only one element in the list
            {
                head = null;
            }

            return;
        }

        while (node.next != null)
        {
            if (node.next.data.Equals(data))
            {
                if(node.next == head)
                    head = node;
                node.next = node.next.next;
                count--;
                return;
            }
            node = node.next;
        }
    }

    public bool isEmpty()
    {
        return count == 0;
    }

    public int size()
    {
        return count;
    }
}

public class Program
{
    public static void Main()
    {
        MyDictionary<string, Character> myCharacters = new MyDictionary<string, Character>();

        myCharacters.Add("Aragorn", new Character(10,5));
        myCharacters.Add("Gandalf", new Character(5,10));
        myCharacters.Add("Legolas", new Character(7,7));

        Console.WriteLine(myCharacters["Aragorn"]);

        foreach (var character in myCharacters)
        {
            Console.WriteLine(character.ToString());
        }

        // Zadanie 5
        ArrayList list = new ArrayList();
        list.Add("Aragon");
        list.Add("Gandalf");
        list.Add("Legolas");

        RemoveMiddle.RemoveMiddleMethod(list);

        Console.WriteLine(list.count);

    }
}
