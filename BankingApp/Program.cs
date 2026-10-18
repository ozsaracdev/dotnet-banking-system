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
                        Console.Write("Name:");
                        string name = Console.ReadLine();

                        bank.OpenAccount(name);
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

                        Console.Write("enter account number:");
                        int accnum = int.Parse(Console.ReadLine());

                        Console.Write("enter amount to deposit:");
                        double amount = double.Parse(Console.ReadLine());

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
                        Console.Write("enter account number:");
                        int accnum2 = int.Parse(Console.ReadLine());

                        Console.Write("enter amount to withdraw:");
                        double amount2 = double.Parse(Console.ReadLine());

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
                        if (bank.GetAccounts().Count == 0)
                        {
                            Console.WriteLine("No account found please open an account first.");
                            break;
                        }

                        PrintAccounts(bank.GetAccounts());

                        Console.Write("enter sender account number:");
                        int senderNum = int.Parse(Console.ReadLine());

                        Console.Write("enter receiver account number:");
                        int receiverNum = int.Parse(Console.ReadLine());

                        Console.Write("enter amount to transfer:");
                        double amount3 = double.Parse(Console.ReadLine());

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
                        Console.Write("enter account number:");
                        int accnum3 = int.Parse(Console.ReadLine());

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
    }
}
