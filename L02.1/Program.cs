using System;

namespace L02._1
{
    class Program
    {
        static string[] subjects = { "Harry", "Hermine", "Ron", "Hagrid", "Snape", "Dumbledore" };
        static string[] verbs = { "braut", "liebt", "studiert", "hasst", "zaubert", "zerstört" };
        static string[] objects = { "Zaubertränke", "den Grimm", "Lupin", "Hogwards", "die Karte des Rumtreibers", "Dementoren" };
        static string subject;
        static string verb;
        static string object_1;
        static int length = subjects.Length;

        static void Main(string[] args)
        {
            string[] verse = new string[length];

            for (int i = 0; i < length; i++)
            {
                VerseCreator();
                verse[i] = subject + " "+ verb + " " + object_1;
            }

            for (int i = 0; i < subjects.Length; i++){
                Console.WriteLine(verse[i]);
            }

        }
        public static void VerseCreator()
        {
            Random randomNumber = new Random();
            int subjectsNumber = randomNumber.Next(0, length);
            int verbsNumber = randomNumber.Next(0, length);
            int objectsNumber = randomNumber.Next(0, length);


            while (subjects[subjectsNumber] == "assigned")
            {
                subjectsNumber = randomNumber.Next(0, length);
            }
            subject = subjects[subjectsNumber];
            subjects[subjectsNumber] = "assigned";

            while (verbs[verbsNumber] == "assigned")
            {
                verbsNumber = randomNumber.Next(0, length);
            }
            verb = verbs[verbsNumber];
            verbs[verbsNumber] = "assigned";

            while (objects[objectsNumber] == "assigned")
            {
                objectsNumber = randomNumber.Next(0, length);
            }
            object_1 = objects[objectsNumber];
            objects[objectsNumber] = "assigned";

        }
    }
}
