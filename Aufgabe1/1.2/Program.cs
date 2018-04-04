using System;

namespace _1._2
{
    class Program
    {

        static string[] subjects = { "Harry", "Hermine", "Ron", "Hagrid", "Snape", "Dumbledore"};
        static string[] verbs = { "braut", "liebt", "studiert", "hasst", "zaubert", "zerstört"};
        static string[] objects = { "Zaubertränke", "den Grimm", "Lupin", "Hogwards", "die Karte des Rumtreibers", "Dementoren"};

        static void Main(string[] args)
        {
            string[] poem = new string[subjects.Length];
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
            string sub = subjects[rnd.Next(subjects.Length)];
            string ver = verbs[rnd.Next(verbs.Length)];
            string obj = objects[rnd.Next(objects.Length)];

            if (line==0){
                return sub + " " + ver + " " + obj;
            }else{
                for (int i = 0; i < line; i++){
                    if (poem[i].Contains(sub)){
                        sub = subjects[rnd.Next(subjects.Length)];
                        i = -1;
                    }
                }
                for (int i = 0; i < line; i++){
                    if (poem[i].Contains(ver)){
                        ver = verbs[rnd.Next(verbs.Length)];
                        i = -1;
                    }
                }
                for (int i = 0; i < line; i++){
                    if (poem[i].Contains(obj)){
                        obj = objects[rnd.Next(objects.Length)];
                        i = -1;
                    }
                }

                return sub + " " + ver + " " + obj;

            }
        }
    }
}
