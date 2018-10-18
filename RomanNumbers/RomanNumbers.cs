using System;
using System.Collections.Generic;
using System.Text;

namespace NumberConverter
{
    static class RomanNumbers
    {
        static void Main(string[] args)
        {
            int input = Int32.Parse(args[0]);
            Console.WriteLine(GetRomanNumber(input));
        }

        public static string GetRomanNumber(int input)
        {
            if ((input <= 0) || (input > 999) )
                return "Eingabe ausserhalb des Wertebereichs!!!";
            int[] inputDigits = GetIntArray(input);
            string hundered = "";
            string ten = "";
            string one = "";
            if(inputDigits.Length == 3)
            {
                hundered = GetHundered(inputDigits[0]);
                ten = GetTens(inputDigits[1]);
                one = GetOnes(inputDigits[2]);
            } 
            else 
                if (inputDigits.Length == 2)
                {
                    ten = GetTens(inputDigits[0]);
                    one = GetOnes(inputDigits[1]);
                }
                else
                    if(inputDigits.Length == 1)
                        one = GetOnes(inputDigits[0]);

            return hundered + ten + one;
        }

        public static string GetHundered(int digit)
        {
             var HundredsDictionary = new Dictionary <int, string>
            {
                { 9, "CM" },
                { 5, "D" },
                { 4, "CD" },
                { 1, "C" },
            };

            var roman = new StringBuilder();

            foreach (var number in HundredsDictionary)
            {
                while (digit >= number.Key)
                {
                    roman.Append(number.Value);
                    digit -= number.Key;
                }
            }

            return roman.ToString();
        }

        public static string GetTens(int digit)
        {
             var TensDictionary = new Dictionary <int, string>
            {
                { 9, "XC" },
                { 5, "L" },
                { 4, "XL" },
                { 1, "X" },
            };

            var roman = new StringBuilder();

            foreach (var number in TensDictionary)
            {
                while (digit >= number.Key)
                {
                    roman.Append(number.Value);
                    digit -= number.Key;
                }
            }

            return roman.ToString();
        }

        public static string GetOnes(int digit)
        {
            var OnesDictionary = new Dictionary <int, string>
            {
                { 9, "IX" },
                { 5, "V" },
                { 4, "IV" },
                { 1, "I" },
            };

            var roman = new StringBuilder();

            foreach (var number in OnesDictionary)
            {
                while (digit >= number.Key)
                {
                    roman.Append(number.Value);
                    digit -= number.Key;
                }
            }

            return roman.ToString();
        }

        public static int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while(num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }
    }
}