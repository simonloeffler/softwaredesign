using System;
using System.Collections.Generic;

namespace L06_UML
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class Person 
    {
        public string Name;
        public int Alter;
    }

    class Teilnehmer: Person
    {
        public int Matrikelnummer;

        //Beziehungen
        public List<Kurs> Kurse;
    }

    class Dozent: Person
    {
        public string Büro;
        public string Sprechstunde;

        //Beziehungen
        public List<Kurs> Kurse;

        public void GibKurseAus ()
        {
            foreach (Kurs kurs in Kurse)
                Console.WriteLine(kurs.Titel);
        }

        public List<Teilnehmer> GibTeilnehmerZurück()
        {
            List<Teilnehmer> gesamtTeilnehmer = new List<Teilnehmer>();
            
            foreach (Kurs kurs in Kurse)
                foreach(Teilnehmer teilnehmer in kurs.Teilnehmer)
                    if(!gesamtTeilnehmer.Contains(teilnehmer))
                        gesamtTeilnehmer.Add(teilnehmer);

            return gesamtTeilnehmer;
        }
    }

    class Kurs
    {
        public string Titel;
        public string Termin;
        public string Raum;

        // Beziehungen 
        public Dozent Dozent;
        public List<Teilnehmer> Teilnehmer;

        public void GibInfotextAus()
        {
            Console.WriteLine("Der Kurs " + Titel + " findet am " + Termin + " in Raum " + Raum + " statt.");
        }
    }
}
