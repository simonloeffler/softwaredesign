using System;

namespace L08_TicTacToe
{
    static class TicTacToe
    {
        static char[] Field = new char[9];
        static int Counter = 0;
        static char Turn;

        static void Main(string[] args)
        {
            for(int i = 0; i < Field.Length; i++)
                Field[i] = ' ';
            Console.WriteLine("Welcome to the incredible TicTacToe!");
            for(;;)
            {
                if(Counter % 2 == 0)
                    Turn = 'X';
                else
                    Turn = 'O';
                WriteField();
                Console.WriteLine("Player " + Turn + " please make your turn by entering the number of the pad you like to put your mark in (1-9)." );
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Thanks for playing!");
                    break;  
                }
                int inputField;
                if(Int32.TryParse(input.Substring(0, 1), out inputField) == true)
                {
                    inputField = inputField -1;

                    if(Field[inputField] == ' ')
                    {
                        Field[inputField] = Turn;
                        Counter ++;
                        if (CheckForWin() == true)
                        {
                            WriteField();
                            WriteConfirmation("Player " + Turn + " wins!");
                            break;
                        }
                        else
                            if(Counter == 9)
                            {
                                WriteField();
                                WriteConfirmation("Draw! - All pads on the field are marked!");
                                break;
                            }
                    }
                    else
                        WriteAlert("Please check your input. There is already a mark set in the pad you chose.");
                }
                else
                    WriteAlert("Please check your input (only numbers from 1-9 are allowed, no letters or special characters).");    
            };
        }

        public static void WriteField ()
        {
            Console.WriteLine("  - - -  ");
            Console.WriteLine("| " + Field[0] + " " + Field[1] + " " + Field[2] + " |");
            Console.WriteLine("| " + Field[3] + " " + Field[4] + " " + Field[5] + " |");
            Console.WriteLine("| " + Field[6] + " " + Field[7] + " " + Field[8] + " |");
            Console.WriteLine("  - - -  ");
        }

        public static bool CheckForWin ()
        {
            if(((Field[0] == Field[1]) && (Field[1] == Field[2]) && (Field[0] != ' '))
            || (Field[3] == Field[4]) && (Field[4] == Field[5]) && (Field[3] != ' ')
            || (Field[6] == Field[7]) && (Field[7] == Field[8]) && (Field[6] != ' ')
            || (Field[0] == Field[3]) && (Field[3] == Field[6]) && (Field[0] != ' ')
            || (Field[1] == Field[4]) && (Field[4] == Field[7]) && (Field[1] != ' ')
            || (Field[2] == Field[5]) && (Field[5] == Field[8]) && (Field[2] != ' ')
            || (Field[0] == Field[4]) && (Field[4] == Field[8]) && (Field[0] != ' ')
            || (Field[2] == Field[4]) && (Field[4] == Field[6]) && (Field[2] != ' '))
                return true;
            else
                return false;
        }

        public static void WriteAlert (string alert)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(alert);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteConfirmation (string alert)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(alert);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
