using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Zad3 : LinkedList
{
    public int CountMultiple()
    {
        if (tail == null)
        {
            return 0;
        }

        Node node = tail;

        int first = default;
        if (node.data is int)
        {
            first = (int)node.data;
        }
        else
        {
            throw new ArgumentException("First element is not an integer");
        }

        int countMult = 0;

        while (node != null)
        {
            if (node.data is int)
            {
                int data = (int)node.data;

                if (data % first == 0)
                {
                    countMult++;
                }
            }

            node = node.next;
        }

        return countMult;
    }
}