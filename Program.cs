using System;
using System.Collections.Generic;
using dotnetcore_banking_console.Banking;

namespace dotnetcore_banking_console
{
    class Program : BankingProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConsoleNavigation();
        }

        private static void ConsoleNavigation()
        {
            while (true)
            {
                Console.WriteLine("\nWhat would you like to do:\n\n\t- To banking (1)\n\t- Exit (5)\n");
                Console.Write("Your choice: ");
                var usersChoice = Console.ReadLine();
                var possibleChoices = new List<int> { 1, 2 };

                if (!int.TryParse(usersChoice, out var n))
                {
                    Utils.LogMessage("Please enter a number.", "error");
                    continue;
                }
                else if (possibleChoices.Contains(int.Parse(usersChoice)))
                {
                    switch (usersChoice)
                    {
                        case "1":
                            BankNavigation();
                            break;
                        default:
                            Utils.LogMessage("Exiting console banking");
                            Environment.Exit(0);
                            break;
                    }
                }
                break;
            }
        }
    }
}