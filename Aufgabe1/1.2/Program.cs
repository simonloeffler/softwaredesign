using System;

namespace _1._2
{
    class Program
    {

        static string[] subjects = { "Harry", "Hermine", "Ron", "Hagrid", "Snape", "Dumbledore" };
        static string[] verbs = { "braut", "liebt", "studiert", "hasst", "zaubert", "zerstört" };
        static string[] objects = { "Zaubertränke", "den Grimm", "Lupin", "Hogwards", "die Karte des Rumtreibers", "Dementoren" };

        static void Main(string[] args)
        {
            string[] poem = new string[5];
            for (int i = 0; i < 5; i++)
            {
                poem[i] = GetVerse();
            }
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(poem[i]);
            }
        }

        public static string GetVerse()
        {
            Random rnd = new Random();
            string verse = subjects[rnd.Next(subjects.Length - 1)] + " " + verbs[rnd.Next(verbs.Length - 1)] + " " + objects[rnd.Next(objects.Length - 1)];
            return verse;
        }
    }
}
