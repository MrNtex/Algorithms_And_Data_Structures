
using System.Security.Cryptography;

public class MyQueueArray<T>
{
    const int MAX_SIZE = 10;

    private T[] arr = new T[MAX_SIZE];

    private int _ptr = 0;
    private int ptr
    {
        get { return _ptr; }
        set { _ptr = value % MAX_SIZE; } // Update the _ptr field directly
    }
    public int size = 0;

    public T? dequeue()
    {
        if (size == 0)
        {
            return default;
        }

        T val = arr[ptr];

        ptr++; // Move the pointer after dequeueing
        size--;

        return val;
    }

    public void enqueue(T item)
    {
        if (size >= MAX_SIZE)
            throw new ArgumentOutOfRangeException($"Max capacity ({MAX_SIZE}) has been reached");

        arr[(ptr+size)%MAX_SIZE] = item;

        size++;
    }

    public bool isEmpty()
    {
        return size == 0;
    }
}

public class MyQueueDynamic<T>
{
    // private List<T> list = new List<T>(); <- Lista zadziala tak samo jak to co jest nizej
    private int capacity = 4;
    private T[] arr;

    private int _ptr = 0;
    private int ptr
    {
        get { return _ptr; }
        set { _ptr = value % capacity; } // Update the _ptr field directly
    }
    public int size = 0;

    public MyQueueDynamic()
    {
        arr = new T[capacity];
    }

    public void Resize()
    { 
        T[] newArr = new T[capacity * 2];
    
        for (int i = 0; i < size; i++)
        {
            newArr[i] = arr[(ptr + i) % capacity];
        }

        arr = newArr;
        ptr = 0;
        capacity *= 2;
    }

    public T? dequeue()
    {
        if (size == 0)
        {
            return default;
        }

        T val = arr[ptr];

        ptr++; // Move the pointer after dequeueing
        size--;

        return val;
    }

    public void enqueue(T item)
    {
        if (size >= capacity)
        {
            Resize();
        }

        arr[(ptr + size) % capacity] = item;

        size++;
    }

    public bool isEmpty()
    {
        return size == 0;
    }
}

public class Node<T>
{
    public T val;
    public Node<T> next;

    public Node(T val)
    {
        this.val = val;
        this.next = null;
    }
}

public class MyQueueObject<T>
{
    private Node<T> head;
    private Node<T> tail;

    public int size = 0;

    public void enqueue(T item)
    {
        Node<T> newNode = new Node<T>(item);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            head.next = newNode;
            head = newNode;
        }

        size++;
    }

    public T? dequeue()
    {
        if (size == 0 || tail == null)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        T val = tail.val;

        tail = tail.next;
        size--;

        return val;
    }

    public bool isEmpty()
    {
        return size == 0;
    }
}

class MyQueue
{
    static void Main(string[] args)
    {
        
    }
}
