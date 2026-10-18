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
            accounts = new List<Account>();

        }
        public void OpenAccount(string name)
        {
            Account acc1 = new Account(name, nextAccountNumber);
            accounts.Add(acc1);
            nextAccountNumber++;
        }

        public List<Account> GetAccounts()
        {
            return accounts;
        }

        public int FindAccountIndex(int accountNumber)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].AccountNumber == accountNumber)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool DepositMoney(int accountNumber, double amount)
        {
            int index = FindAccountIndex(accountNumber);

            if (index == -1)
            {
                return false;
            }

            return accounts[index].deposit(amount);
        }

        public bool WithdrawMoney(int accountNumber, double amount)
        {
            int index = FindAccountIndex(accountNumber);

            if (index == -1)
            {
                return false;
            }

            return accounts[index].withdraw(amount);
        }

        public bool DeleteAccount(int accountNumber)
        {
            int index = FindAccountIndex(accountNumber);

            if (index == -1)
            {
                return false;
            }

            accounts.RemoveAt(index);
            return true;
        }

        public bool TransferMoney(int senderNumber, int receiverNumber, double amount)
        {
            int senderIndex = FindAccountIndex(senderNumber);
            int receiverIndex = FindAccountIndex(receiverNumber);

            if (senderIndex == -1 ||  receiverIndex == -1 || senderIndex == receiverIndex)
            {
                return false;
            }

            if (accounts[senderIndex].withdraw(amount) == false)
            {
                return false;
            }

            accounts[receiverIndex].deposit(amount);
            
            return true;
        }
    }
}
