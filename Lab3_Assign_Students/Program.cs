
class Program
{
    public class Student
    {
        public int id;
        public float power;
        public float determination;
        public float intelligence;

        public Faculty faculty;

        public Student(int id, float power, float determination, float intelligence)
        {
            this.id = id;
            this.power = power;
            this.determination = determination;
            this.intelligence = intelligence;
        
        }
    }

    public class Faculty
    {
        public string name;

        public float power;
        public float determination;
        public float intelligence;

        public List<Student> native;

        public Faculty(string name, float power, float determination, float intelligence)
        {
            this.name = name;
            this.power = power;
            this.determination = determination;
            this.intelligence = intelligence;
        }
    }
    enum CompareType
    {
        ALL,
        WIMIP,
        WIMIR,
        WIET
    }
    static bool Compare(Student s1, Student s2, CompareType compareType)
    {
        switch (compareType)
        {
            case CompareType.ALL:
                return s1.power + s1.intelligence + s1.determination > s2.power + s2.intelligence + s2.determination;
            case CompareType.WIMIP:
                if (s1.power != s2.power)
                    return s1.power > s2.power;
                if (s1.intelligence != s2.intelligence)
                    return s1.intelligence > s2.intelligence;
                return s1.determination > s2.determination;

            case CompareType.WIMIR:
                if (s1.determination != s2.determination)
                    return s1.determination > s2.determination;
                if (s1.power != s2.power)
                    return s1.power > s2.power;
                return s1.intelligence > s2.intelligence;

            case CompareType.WIET:
                if (s1.intelligence != s2.intelligence)
                    return s1.intelligence > s2.intelligence;
                if (s1.determination != s2.determination)
                    return s1.determination > s2.determination;
                return s1.power > s2.power;
        }

        return false;
    }
    private static Student[] QuickSort(ref Student[] students, CompareType compareType, int left, int right)
    {
        int i = left;
        int j = right;
        Student pivot = students[(left + right) / 2];

        while (i <= j)
        {
            while (Compare(students[i], pivot, compareType))
            {
                i++;
            }

            while (Compare(pivot, students[j], compareType))
            {
                j--;
            }

            if (i <= j)
            {
                Student tmp = students[i];
                students[i] = students[j];
                students[j] = tmp;

                i++;
                j--;
            }
        }

        if (left < j)
        {
            QuickSort(ref students, compareType, left, j);
        }

        if (i < right)
        {
            QuickSort(ref students, compareType, i, right);
        }

        return students;
    }

    static void Main()
    {
        List<Faculty> faculties = new List<Faculty>
        {
            new Faculty("WIMIP", 1.0f, -1f, .1f),
            new Faculty("WIMIR", .1f, 1.0f, -1f),
            new Faculty("WIET", -1f, .1f, 1.0f)
        };

        List<Student> students = new List<Student>();
        students.Capacity = 500;

        for (int i = 0; i < students.Capacity; i++)
        {
            Random rand = new Random();

            students.Add(new Student(i,
                (float)Math.Round(rand.NextDouble(),1),
                (float)Math.Round(rand.NextDouble(), 1),
                (float)Math.Round(rand.NextDouble(), 1)
            ));
        }

        Student[] studentsWIMIP = students.ToArray();
        studentsWIMIP = QuickSort(ref studentsWIMIP, CompareType.WIMIP, 0, students.Count - 1);
        Student[] studentsWIMIR = students.ToArray();
        studentsWIMIR = QuickSort(ref studentsWIMIR, CompareType.WIMIR, 0, students.Count - 1);
        Student[] studentsWIET = students.ToArray();
        studentsWIET = QuickSort(ref studentsWIET, CompareType.WIET, 0, students.Count - 1);

        Student[] fourBestMages = new Student[4];
        
        for (int i = 0; i < 4; i++) // WIMIP has the best mages, before splitting them between faculties
        {
            fourBestMages[i] = studentsWIMIP[i];
        }

        HashSet<int> WIMIPHashSet = new HashSet<int>();
        HashSet<int> WIMIRHashSet = new HashSet<int>();
        HashSet<int> WIETHashSet = new HashSet<int>();
        faculties[0].native = new List<Student>();
        faculties[1].native = new List<Student>();
        faculties[2].native = new List<Student>();
        for (int i = 0; i < students.Count; i++)
        {
            Student studentWIMIP = studentsWIMIP[i];
            Student? studentWIMIR = studentsWIMIR[i];
            Student? studentWIET = studentsWIET[i];
            
            // WIMIP ma pierwszeństwo, potem WIET
            if(studentWIMIP.Equals(studentWIMIR))
            {
                studentWIMIR = null;
            }
            if (studentWIMIP.Equals(studentWIET))
            {
                studentWIET = null;
            }
            if (studentWIMIR != null && studentWIMIR.Equals(studentWIET))
            {
                studentWIMIR = null;
            }

            if ((!WIMIRHashSet.Contains(studentWIMIP.id)) && !WIETHashSet.Contains(studentWIMIP.id))
            {
                faculties[0].native.Add(studentWIMIP);
                studentWIMIP.faculty = faculties[0];
                WIMIPHashSet.Add(studentWIMIP.id);
            }
            if (studentWIMIR != null && !WIMIPHashSet.Contains(studentWIMIR.id) && !WIETHashSet.Contains(studentWIMIR.id))
            {
                faculties[1].native.Add(studentWIMIR);
                studentWIMIR.faculty = faculties[1];
                WIMIRHashSet.Add(studentWIMIR.id);
            }
            if (studentWIET != null && !WIMIPHashSet.Contains(studentWIET.id) && !WIMIRHashSet.Contains(studentWIET.id))
            {
                faculties[2].native.Add(studentWIET);
                studentWIET.faculty = faculties[2];
                WIETHashSet.Add(studentWIET.id);
            }

            //if (students[i].faculty == null)
            //{
            //    throw new Exception("Student " + i + " nie zostal przydzielony do zadnego wydzialu");
            //}
        }

        int controlSum = 0;

        foreach (Faculty faculty in faculties)
        {
            Console.WriteLine("----" + faculty.name + "----");
            for (int i = 0; i < faculty.native.Count; i++)
            {
                controlSum++;
                Console.WriteLine("Student " + i + ": " + faculty.native[i].power + " " + faculty.native[i].determination + " " + faculty.native[i].intelligence);
            }
        }

        Console.WriteLine("Control sum: " + controlSum);

        Console.WriteLine("----Four best mages----");
        for (int i = 0; i < fourBestMages.Length; i++)
        {
            Console.WriteLine("Student " + i + ": " + fourBestMages[i].power + " " + fourBestMages[i].determination + " " + fourBestMages[i].intelligence);
        }
        Console.WriteLine("WIMIP raz jeszcze");
        for (int i = 0; i < faculties[0].native.Count; i++)
        {
            Console.WriteLine("Student " + i + ": " + faculties[0].native[i].power + " " + faculties[0].native[i].determination + " " + faculties[0].native[i].intelligence);
        }

        Console.WriteLine("---Lista studentow---");
        Student[] studentsSumarycznie = students.ToArray();
        studentsSumarycznie = QuickSort(ref studentsWIMIP, CompareType.ALL, 0, students.Count - 1);
        for (int i = 0; i < studentsSumarycznie.Length; i++)
        {
            Console.WriteLine("Student " + studentsSumarycznie[i].id + " Wydzial: " + studentsSumarycznie[i].faculty.name + ": Sumaryczny wynik rekrutacji " + (studentsSumarycznie[i].power + studentsSumarycznie[i].determination + studentsSumarycznie[i].intelligence));
        }
    }
}