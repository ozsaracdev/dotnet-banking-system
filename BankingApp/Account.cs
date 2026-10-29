using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Account
    {
        public string Name { get; set; }
        public decimal Balance { get; private set; }
        public int AccountNumber { get; private set; }

        public Account(string name, int accountNumber, decimal balance = 0)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = balance;
        }
  
    }
}
