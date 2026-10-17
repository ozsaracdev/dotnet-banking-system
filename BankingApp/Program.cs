namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            while(true)
            {

                Console.WriteLine("1) Open new account");
                Console.WriteLine("2) list accounts");
                Console.WriteLine("3) clear console");
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
                        List <Account> accounts = bank.GetAccounts();
                        foreach (Account acc in accounts)
                        {
                            Console.WriteLine($"Name: {acc.Name}  Account Number: {acc.AccountNumber}  Balance: {acc.Balance}");
                        }
                        break;

                    case '3':
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("make a valid selection");
                        break;
                }

                Console.WriteLine();

            }




        }
    }
}
