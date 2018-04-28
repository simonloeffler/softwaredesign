using System;

namespace L05_Coding2
{
    class Person
    {
        public string Name;
        public int Alter;
    }

    class Program
    {
        static void Main(string[] args)
        {

            Person einePerson = new Person
            {
                Name = "Heiner",
                Alter = 42
            };


            Console.WriteLine("Hello World!");
        }
    }
}
