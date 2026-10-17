using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Bank
    {
        private List<Account> accounts;
        private int nextAccountNumber = 100;
        public Bank()
        {
            accounts = new List <Account>();
           
        }
        public void OpenAccount(string name)
        {
            Account acc1 = new Account(name,nextAccountNumber);
            accounts.Add(acc1);
            nextAccountNumber++;
        }

        public List<Account> GetAccounts()
        {
            return accounts;
        }
            



    }
}
