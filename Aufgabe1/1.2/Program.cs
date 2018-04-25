using System;

namespace _1._2
{
    class Program
    {

        static string[] Subjects = { "Harry", "Hermine", "Ron", "Hagrid", "Snape", "Dumbledore"};
        static string[] Verbs = { "braut", "liebt", "studiert", "hasst", "zaubert", "zerstört"};
        static string[] Objects = { "Zaubertränke", "den Grimm", "Lupin", "Hogwards", "die Karte des Rumtreibers", "Dementoren"};

        static void Main(string[] args)
        {
            string[] poem = new string[Subjects.Length]; //only if: subjects.Length == verbs.Length == objects.Length => asumed in this task
            for (int i = 0; i < poem.Length; i++)
            {
                poem[i] = GetVerse(poem, i);
            }
            
            for (int i = 0; i < poem.Length; i++)
            {
                Console.WriteLine(poem[i]);
            }
        }

        public static string GetVerse(string[] poem, int line)
        {
            Random rnd = new Random();
            string sub = Subjects[rnd.Next(Subjects.Length)];
            string ver = Verbs[rnd.Next(Verbs.Length)];
            string obj = Objects[rnd.Next(Objects.Length)];

            for (int i = 0; i < line; i++)
            {
                if (poem[i].Contains(sub))
                {
                    sub = Subjects[rnd.Next(Subjects.Length)];
                    i = -1;
                }
            }

            for (int i = 0; i < line; i++)
            {
                if (poem[i].Contains(ver))
                {
                    ver = Verbs[rnd.Next(Verbs.Length)];
                    i = -1;
                }
            }

            for (int i = 0; i < line; i++)
            {
                if (poem[i].Contains(obj))
                {
                    obj = Objects[rnd.Next(Objects.Length)];
                    i = -1;
                }
            }

            return $"{sub} {ver} {obj}";

        }
    }
}
