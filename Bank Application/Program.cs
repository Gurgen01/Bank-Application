using System;
using BankLibrary;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("MyBank");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen; 
                Console.WriteLine("1. Open Account \t 2. Withdraw funds  \t 3. Add to Account");
                Console.WriteLine("4. Close Account \t 5. Skip a day \t 6. Exit the program");
                Console.WriteLine("Enter the number of command:");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter the amount of money to open an Account:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Select an Account type 1.DemandAccount 2.DepositAccount");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType,
                sum,
                AddSumHandler,  
                WithdrawSumHandler, 
                (o, e) => Console.WriteLine(e.Message), 
                CloseAccountHandler, 
                OpenAccountHandler); 
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Enter the amount of withdraw");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Account id:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Enter the amount to put on Account:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Account id:");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter Account id which will be closed");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }
        
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
                Console.WriteLine("Let's go spend money");
        }
        
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
