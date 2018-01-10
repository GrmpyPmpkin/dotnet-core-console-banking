using System;
using System.Collections.Generic;

namespace dotnetcore_banking_console.Banking
{
    public class BankingProgram
    {

        public static List<Account> userAccount { get; set; } = new List<Account>();

        public static void BankNavigation()
        {
            while (true)
            {
                Console.WriteLine("\n\nWelcome to console banking!");
                Console.WriteLine("\nWhat would you like to do:\n\n\tTo create an account (1)\n\tView account balance (2)\n\tTo withdraw funds (3)\n\tTo make a deposit (4)\n\tView all transactions (5)\n\tExit (6)\n");
                Console.Write("Your choice: ");
                var usersChoice = Console.ReadLine();
                Console.WriteLine();
                var possibleChoices = new List<int> { 1, 2, 3, 4, 5 };

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
                            CreateAccount();
                            BankNavigation();
                            break;
                        case "2":
                            ViewAccountBalance();
                            BankNavigation();
                            break;
                        case "3":
                            MakeWithdrawal();
                            BankNavigation();
                            break;
                        case "4":
                            MakeDeposit();
                            BankNavigation();
                            break;
                        case "5":
                            GetTransactions();
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

        public static void CreateAccount()
        {
            Utils.LogMessage("Entering account creation.");
            string inputedName;
            int inputedInitialBalanace;

            AccountSetup(out inputedName, out inputedInitialBalanace);

            Account generatedAccount = new Account(inputedName, inputedInitialBalanace);
            userAccount.Add(generatedAccount);

            Console.WriteLine($"\nAccount {generatedAccount.Number} was created for {generatedAccount.Owner} with {generatedAccount.Balance} initial balance.");
        }

        private static void AccountSetup(out string inputedName, out int inputedInitialBalanace)
        {
            // check to make sure the details provided are correct

            inputedName = Utils.ExpectUserInput<string>("Please enter your name: ", "name", true);
            inputedInitialBalanace = Utils.ExpectUserInput<int>("Please enter your initial account balance: ", "inital account balance", true);

            while (true)
            {
                Console.Write($"Thank you, the details you provided are:\n\n\tName: {inputedName},\n\tInitial account balance {inputedInitialBalanace}.\n\nAre these details correct (y/n)? ");
                var detailsCorrect = Console.ReadLine();
                var expectedAnswers = new List<string> { "y", "n" };

                if (detailsCorrect == "" || !expectedAnswers.Contains(detailsCorrect))
                {
                    Utils.LogMessage("Please select an option (y/n)", "error");
                    continue;
                }

                if (detailsCorrect == expectedAnswers[1])
                {
                    // need to rerun the name and balance
                    Utils.LogMessage("As you answered no, Please provide your name and initial account balance again.");
                    inputedName = Utils.ExpectUserInput<string>("Please enter your name: ", "name", true);
                    inputedInitialBalanace = Utils.ExpectUserInput<int>("Please enter your initial account balance: ", "inital account balance", true);
                    continue;
                }

                break;
            }
        }

        public static void MakeWithdrawal()
        {
            Utils.LogMessage("Withdraw funds from your account.");
            var account = PleaseProvideAccountNumber();

            while (true)
            {
                Console.Write("How much would you like to withdraw? ");
                var userWithdrawalAmount = Console.ReadLine();
                var userWithdrawalNote = "";

                if (!int.TryParse(userWithdrawalAmount, out var n) && userWithdrawalAmount == "")
                {
                    Utils.LogMessage("Please enter a number.", "error");
                    continue;
                }
                else
                {
                    Console.Write("What is the withdrawal for? ");
                    userWithdrawalNote = Console.ReadLine();
                }

                account.MakeWithdrawal(int.Parse(userWithdrawalAmount), DateTime.Now, userWithdrawalNote);
                Utils.LogMessage($"Withdrawl of {userWithdrawalAmount} from account {account.Number}. Remaining balance - {account.Balance}.");

                break;
            }
        }

        public static void MakeDeposit()
        {
            Utils.LogMessage("Deposit funds into your account.");
            var account = PleaseProvideAccountNumber();

            while (true)
            {
                Console.Write("How much would you like to deposit? ");
                var userDepositAmount = Console.ReadLine();
                var userDepositNote = "";

                if (!int.TryParse(userDepositAmount, out var n) && userDepositAmount == "")
                {
                    Utils.LogMessage("Please enter a number.", "error");
                    continue;
                }
                else
                {
                    Console.Write("What is the desposit for? ");
                    userDepositNote = Console.ReadLine();
                }

                account.MakeDeposit(int.Parse(userDepositAmount), DateTime.Now, userDepositNote);
                Utils.LogMessage($"Deposit of {userDepositAmount} into account {account.Number}. New balance - {account.Balance}.");

                break;
            }
        }

        public static void GetTransactions()
        {
            Utils.LogMessage("View all your account transactions");
            var account = PleaseProvideAccountNumber();

            Console.WriteLine(account.GetAccountHistory());
        }

        public static void ViewAccountBalance()
        {
            Utils.LogMessage("View your account balance");
            var account = PleaseProvideAccountNumber();

            Console.WriteLine($"Account balance: {account.Balance}");
        }

        private static Account PleaseProvideAccountNumber()
        {
            while (true)
            {
                Console.Write("Please provide your account number: ");
                var userAccountNumber = Console.ReadLine();

                var account = userAccount.Find(a => a.Number == userAccountNumber);
                if (account == null)
                {
                    Console.WriteLine("Account not valid.");
                    continue;
                }
                else
                {
                    return account;
                }
            }
        }

    }
}