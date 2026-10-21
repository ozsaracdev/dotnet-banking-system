namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            while (true)
            {

                Console.WriteLine("1) Open new account");
                Console.WriteLine("2) list accounts");
                Console.WriteLine("3) deposit money");
                Console.WriteLine("4) withdraw money");
                Console.WriteLine("5) transfer money");
                Console.WriteLine("6) delete account");
                Console.WriteLine("7) clear console");
                Console.WriteLine("0) exit");
                Console.Write("make a selection:");
                char choice = Console.ReadKey().KeyChar;

                Console.WriteLine();

                if (choice == '0')
                {
                    Console.WriteLine("logging out");
                    break;
                }


                switch (choice)
                {
                    case '1':
                        string name;
                        while(true)
                        {
                            Console.Write("name:");
                            name = Console.ReadLine();

                            if (IsValidName(name))
                            {
                                break;
                            }

                            Console.WriteLine("invalid input");
                        }
                        bank.OpenAccount(name.Trim());
                        break;

                    case '2':
                        PrintAccounts(bank.GetAccounts());
                        break;

                    case '3':
                        if (bank.GetAccounts().Count == 0)
                        {
                            Console.WriteLine("No account found please open an account first.");
                            break; 
                        }

                        PrintAccounts(bank.GetAccounts());

                        int accnum = GetValidInt("Enter account number:");
                        decimal amount = GetValidDecimal("enter amount to deposit:");

                        bool isSuccess = bank.DepositMoney(accnum, amount);

                        if (isSuccess)
                        {
                            Console.WriteLine("Deposit successful!");
                        }
                        else
                        {
                            Console.WriteLine("Transaction failed! (Account not found or invalid amount)");
                        }

                        break;

                    case '4':
                        if (bank.GetAccounts().Count == 0)
                        {
                            Console.WriteLine("No account found please open an account first.");
                            break;
                        }

                        PrintAccounts(bank.GetAccounts());
                        
                        int accnum2 = GetValidInt("Enter account number:");
                        decimal amount2 = GetValidDecimal("enter amount to withdraw:");

                        bool isSuccess2 = bank.WithdrawMoney(accnum2, amount2);

                        if(isSuccess2)
                        {
                            Console.WriteLine("withdraw successful");

                        }
                        else
                        {
                            Console.WriteLine("Transaction failed! (Account not found or invalid amount)");
                        }

                        break;


                    case '5':
                        if(bank.GetAccounts().Count < 2)
                        {
                            Console.WriteLine("There must be at least two accounts in the system in order to perform a transfer.");
                            break;
                        }

                        PrintAccounts(bank.GetAccounts());

                        
                        int senderNum = GetValidInt("enter sender account number:");

                       
                        int receiverNum = GetValidInt("enter receiver account number:");

                        decimal amount3 = GetValidDecimal("enter amount to transfer:");

                        bool isSuccess4 = bank.TransferMoney(senderNum, receiverNum, amount3);
                         
                        if(isSuccess4)
                        {
                            Console.WriteLine("Successful");
                        }
                        else
                        {
                            Console.WriteLine("Transaction failed! (Account not found or invalid amount)");
                        }
                        break;

                    case '6':
                        if (bank.GetAccounts().Count == 0)
                        {
                            Console.WriteLine("No account found please open an account first.");
                            break;
                        }

                        PrintAccounts(bank.GetAccounts());
                        
                        int accnum3 = GetValidInt("enter account number:");

                        bool isSuccess3 = bank.DeleteAccount(accnum3);

                        if(isSuccess3)
                        {
                            Console.WriteLine("Successful");
                        }
                        else
                        {
                            Console.WriteLine("Account not found");
                        }

                        break;
                    case '7':
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("make a valid selection");
                        break;
                }

                Console.WriteLine();

            }
        }

        public static void PrintAccounts(List<Account> accounts)
        { 
            if(accounts.Count == 0)
            {
                Console.WriteLine("no accounts found");
                return;
            }
            foreach (Account acc in accounts)
            {
                Console.WriteLine($"Name: {acc.Name}  Account Number: {acc.AccountNumber}  Balance: {acc.Balance}");
            }
        }

        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name)
           && name.Any(char.IsLetter)
           && name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        public static int GetValidInt(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Invalid input! Enter again:");
            }

            return value;
        }

        public static decimal GetValidDecimal(string prompt)
        {
            decimal value;
            Console.Write(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.Write("Invalid input! Enter again:");
            }
            return value;
        }
    }
}
