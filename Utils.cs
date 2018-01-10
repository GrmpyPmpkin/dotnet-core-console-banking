using System;
using System.Globalization;

namespace dotnetcore_banking_console
{
    public class Utils
    {

        public static T ExpectUserInput<T>(string inputMessage, string inputReturnValue, bool required)
        {
            var userInput = "";

            while (true)
            {
                Console.Write(inputMessage);
                userInput = Console.ReadLine();

                if (userInput == "" && required)
                {
                    LogMessage($"Please provide a {inputReturnValue}!\n", "error");
                    continue;
                }
                break;
            }

            return (T) Convert.ChangeType(userInput, typeof(T), CultureInfo.InvariantCulture);
        }

        public static void LogMessage(string message, string logType = "info")
        {
            ConsoleColor foregroundColor;
            string consoleMessage;

            switch (logType)
            {
                case "warning":
                    foregroundColor = ConsoleColor.DarkYellow;
                    consoleMessage = $"Warning: {message}";
                    break;
                case "error":
                    foregroundColor = ConsoleColor.Red;
                    consoleMessage = $"Error: {message}";
                    break;
                default:
                    foregroundColor = ConsoleColor.Blue;
                    consoleMessage = $"Info: {message}";
                    break;
            }

            Console.ForegroundColor = foregroundColor;

            Console.WriteLine(consoleMessage);
            Console.ResetColor();
        }
    }
}