using System;
using System.Collections.Generic;

namespace DesPatternSingleton
{

    public class Person
    {
        private string Name;
        private int Age;
        private int Id;

        public override string ToString()
        {
            return "Name:" + Name + ", Age: " + Age + ", " + "Id: " + Id;
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
            Id = IDGenerator.Instance.GibMirNeId();
            Program.personen.Add(this);
        }
    }

    /*
    class GlobalVariables
    {
        // public static int letzteID = 1;
        public static IDGenerator DerIdMacher = new IDGenerator();
    }
    */

    public class IDGenerator
    {
        private IDGenerator()
        {
            letzteID = 1;
        }

        private static IDGenerator _instance;

        // public static IDGenerator GetInstance()
        // {
        //     if (_instance == null)
        //         _instance = new IDGenerator();
        //     return _instance;
        // }

        public static IDGenerator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IDGenerator();
                return _instance;
            }
        }

        private int letzteID;
        public int GibMirNeId()
        {
            return letzteID++;
        }
    } 

    class Program
    {

        public static List<Person> personen = new List<Person>();

        static void Main(string[] args)
        {

            // Eine Stelle, an der Personen angelegt werden
            new Person("Dieter", 55);
            new Person("Hans", 23);
            new Person("Bernd", 45);
            new Person("Hannes", 15);

            // Eine ANDERE Stelle, an der Personen angelegt werden
            new Person("Tusnelda", 85);
            new Person("Margot", 38);
            new Person("Carola", 51);
            new Person("Luise", 32);

            foreach (var person in personen)
                Console.WriteLine(person);

            Console.WriteLine("Hello World!");
        }
    }
}
