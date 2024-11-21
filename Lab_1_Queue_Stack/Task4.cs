using System;
using System.Collections.Generic;
namespace Zad4
{
    public enum ReportType
    {
        Monster,
        Urgent
    }

    public struct Vector2
    {
        public int x;
        public int y;

        public double Distance(Vector2 from)
        {
            return Math.Sqrt(Math.Pow(from.x - x, 2) + Math.Pow(from.y - y, 2));
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Report
    {
        public ReportType Type { get; }
        public string desc { get; }
        public Vector2 location;

        public Report(ReportType type, string desc, Vector2 localtion)
        {
            Type = type;
            this.desc = desc;
            this.location = localtion;
        }
    }

    

    public class GeraltOfRivia
    {
        public Vector2 pos = new Vector2(0, 0);

        public int slainMonstersPtr = 0;
        public Report[] slainMontersReport = new Report[10];

        public void FinishRaport(Report r)
        {
            pos = r.location;

            if (r.Type == ReportType.Monster)
            {
                Console.WriteLine($"Geralt: I've slain a monster at location {r.location.x}, {r.location.y}");
                slainMonstersPtr++;
                slainMontersReport[slainMonstersPtr-1] = r;
                if (slainMonstersPtr % 10 == 0)
                {
                    Console.WriteLine("Geralt: I've slain 10 monsters. I need to report to the village head now.\n");
                    Console.WriteLine("...");
                    Console.WriteLine("Geralt: I report on slaying 10 monsters. They are as follows\n");
                    foreach (var report in slainMontersReport)
                    {
                        Console.WriteLine($"{report.desc} at {report.location.x}, {report.location.y}");
                    }
                    Console.WriteLine("Village head: Geralt, thank you for your service. Here's your reward.\n");

                    slainMonstersPtr = 0;
                    pos = new Vector2(0, 0); // assuming Geralt returns to the village
                }
            }
            else
            {
                Console.WriteLine($"Geralt: I've solved an urgent matter ({r.desc}) at location {r.location.x}, {r.location.y}");
            }
        }
        private void GetOptimalPath(Report[] targets)
        {
            HashSet<Report> unvisited = new HashSet<Report>(targets);

            while (unvisited.Count > 0)
            {
                Report closest = unvisited.First(); // get any element
                double bestDist = closest.location.Distance(pos);
                foreach (Report target in unvisited)
                {
                    double dist = target.location.Distance(pos);
                    if (dist < bestDist)
                    {
                        closest = target;
                        bestDist = dist;
                    }
                }

                unvisited.Remove(closest);
                FinishRaport(closest);
            }
        }

        public void ProcessReports(Report[] reports)
        {
            // Split reports into two lists
            List<Report> monsterReports = new List<Report>();
            List<Report> urgentReports = new List<Report>();

            foreach (var report in reports)
            {
                if (report.Type == ReportType.Monster)
                {
                    monsterReports.Add(report);
                }
                else
                {
                    urgentReports.Add(report);
                }
            }

            GetOptimalPath(urgentReports.ToArray()); // solve urgent matters first

            GetOptimalPath(monsterReports.ToArray());
        }
    }

    public class Program
    {
        static string[] monsters = ["Ghoul", "Griffin", "Werewolf", "Leshen", "Succubus", "Drowner", "Basilisk", "Cockatrice", "Forktail", "Wyvern"];
        static string[] urgentMatters = ["Bandit camp", "Missing person", "Witch hunter patrol", "Monster nest", "Cursed artifact", "Haunted house", "Forest fire", "Flood", "Earthquake", "Drought"];
        public static void Main(string[] args)
        {
            Random rnd = new Random();
            GeraltOfRivia geralt = new GeraltOfRivia();

            for (int day = 1; day <= 7; day++) // 7 days of adventure
            {
                Console.WriteLine($"\nDay {day}:");
                Report[] reports = new Report[5];
                for (int i = 0; i < 5; i++) // Generate 5 reports each day
                {
                    // Randomly choose between monster and urgent report
                    ReportType reportType = (ReportType)rnd.Next(0, 2);
                    Vector2 location = new Vector2(rnd.Next(-100, 100), rnd.Next(-100, 100));
                    string description = reportType == ReportType.Monster ? monsters[rnd.Next(0, monsters.Length)] : urgentMatters[rnd.Next(0, urgentMatters.Length)];

                    reports[i] = new Report(reportType, description, location);
                }
                geralt.ProcessReports(reports);
            }
        }
    }

}
