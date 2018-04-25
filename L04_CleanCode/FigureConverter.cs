using System;

namespace L04_CleanCode
{
    class FigureConverter
    {
        static void Main(string[] args)
        {
            int toBase = int.Parse(args[0]);
            int fromBase = int.Parse(args[1]);
            int value = int.Parse(args[2]);
            int converted = ConvertNumberToBaseFromBase(toBase, fromBase, value);

            Console.WriteLine ($"The Number {value} on basis {fromBase} is converted to {converted} on basis {toBase}.");
        }

        public static int ConvertDeciamlToHexal (int value)
        {
            int basis = 6;
            int hexal = int.Parse(Euclidean(basis, value));
            return hexal;
        }

        public static int ConvertHexalToDecimal(int value)
        {
            int basis = 6;
            return ConvertToDecimalFromBase(basis, value);
        }

        public static int ConvertToBaseFromDecimal(int toBase, int value)
        {
            return int.Parse(Euclidean(toBase, value));
        }

        public static int ConvertToDecimalFromBase(int fromBase, int value)
        {
            //converts value from int to string to step through each figure in the value
            string valueString = value.ToString();

            int converted = 0;
            for(int i = 1; i <= valueString.Length; i++ )
            {
                //make string from char and parse it to int
                int valueAtIndex = int.Parse(valueString[valueString.Length-i].ToString());

                int basisAtIndex = Convert.ToInt32(Math.Pow(fromBase, i-1));
                converted += valueAtIndex*basisAtIndex;
            };
            return converted;
        }

        public static int ConvertNumberToBaseFromBase(int toBase, int fromBase, int value)
        {
            int errorCode = -1;
            if(toBase < 2 || toBase > 10 || fromBase < 2 || fromBase > 10)
            {
                Console.WriteLine("Please enter bases within 2 and 10!");
                return errorCode;
            }
            return  ConvertToBaseFromDecimal(toBase, ConvertToDecimalFromBase(fromBase, value));
        }

        // converts any given decimal to a number with a given basis (within 2 and 10)
        // returns string because its simpler to add numbers to string (recursive)
        public static string Euclidean(int basis, int value)
        {
            int remainder = value % basis;
            int result = (value - remainder) / basis;
            if(result == 0)
                return remainder.ToString();
            else
                return Euclidean(basis, result) + remainder.ToString();
        }
    }
}
