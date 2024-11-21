using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

void Swap()
{
    ArrayList listaBohaterow = new ArrayList();
    ArrayList listaPrzeciwnikow = new ArrayList();

    listaBohaterow.Add("Aragorn");
    listaBohaterow.Add("Gandalf");
    listaBohaterow.Add("Legolas");
    listaBohaterow.Add("Gimli");


    listaPrzeciwnikow.Add("Sauron");
    listaPrzeciwnikow.Add("Nazgul");
    listaPrzeciwnikow.Add("Uruk-hai");
    listaPrzeciwnikow.Add("Ork");
    listaPrzeciwnikow.Add("Troll");


    ArrayList temp = listaBohaterow;

    while (listaBohaterow.count > 0 || listaPrzeciwnikow.count > 0)
    {
        if(listaBohaterow.count > 0)
        {
            temp.Add(listaBohaterow.RemoveAt(0));
        }
        if(listaPrzeciwnikow.count > 0)
        {
            temp.Add(listaPrzeciwnikow.RemoveAt(0));
        }
    }

}