
public class StackNode<T>
{
    public T val;
    public StackNode<T> prev;

    public StackNode(T val)
    {
        this.val = val;
        this.prev = null;
    }
}

public class MyStack<T>
{
    private StackNode<T> top;

    public int size = 0;

    public void push(T item)
    {
        StackNode<T> newNode = new StackNode<T>(item);

        if (top == null)
        {
            top = newNode;
        }
        else
        {
            newNode.prev = top;
            
            top = newNode;
        }

        size++;
    }

    public T? pop()
    {
        if (size == 0 || top == null)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        T result = top.val;

        top = top.prev;

        size--;

        return result;
    }
    
    public bool isEmpty()
    {
        return size == 0;
    }

}
