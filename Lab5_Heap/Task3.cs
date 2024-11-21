using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ProblemHeap
{
    public Dictionary<ProblemCategory, ProblemHeap> Divide()
    {
        Dictionary<ProblemCategory, ProblemHeap> divided = new Dictionary<ProblemCategory, ProblemHeap>();
        for (int i = 0; i < size; i++)
        {
            if (!divided.ContainsKey(heap[i].Category))
                divided.Add(heap[i].Category, new ProblemHeap());
            divided[heap[i].Category].Insert(heap[i]);
        }

        return divided;
    }
}