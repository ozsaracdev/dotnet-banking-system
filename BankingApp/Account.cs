using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Account
    {
        private string _name;
        private decimal _balance;
        private int _accountNumber;

        public Account(string name, int accountNumber, decimal balance = 0)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public decimal Balance
        {
            get { return _balance; }
            private set {  _balance = value; } // private set yapmak degeri ilk defa verdikten sonra el ile degisemez sadece metotlar ile degisebilir withdraw deposit gibi
        }

        public int AccountNumber
        {
            get { return _accountNumber; }
            private set { _accountNumber = value; }
        }

        public bool deposit(decimal amount)
        {
            if(amount <= 0)
            {
                return false;
            }
            else
            {
                _balance += amount;
                return true;
            }
        }

        public bool withdraw(decimal amount)
        {
            if(amount <=0 || amount > _balance)
            {
                return false;
            }
            else
            {
                _balance -= amount;
                return true; 
            }
        }


    }
}
