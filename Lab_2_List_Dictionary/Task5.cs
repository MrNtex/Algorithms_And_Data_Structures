using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RemoveMiddle()
{
    public static void RemoveMiddleMethod(ArrayList list)
    {
        list.RemoveAt(list.count / 2);
    }
}