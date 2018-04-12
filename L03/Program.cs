using System;

namespace L03
{

    public class Person{
        public string FirstName;
        public string LastName;
        public int Age;

        public override string ToString() 
        {
            return ($"{FirstName} {LastName} ist {Age} Jahre alt.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            int[] i = new int[] {5,6,2};
            Person [] personen =
            {
                new Person {FirstName = "Der", LastName = "Dude", Age = 42 },
                new Person {FirstName = "Stuntman", LastName = "Mike", Age = 53 }
            };

            GetPersonen(personen);
            //Console.WriteLine();
        }

        public static void GetPersonen (Person[] personen){
            for(int i = 0; i<personen.Length; i++)
                Console.WriteLine(personen[i]);
        }
    }
}
