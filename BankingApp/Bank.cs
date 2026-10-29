using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp2
{
    public class Bank
    {
        private const string connectionString = "Data Source = bank.db";
        public Bank()
        {    
            InitializeDatabase();
        }
        public void OpenAccount(string name)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Accounts (Name,Balance) VALUES (@name,@balance);";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@balance", 0);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Account> GetAccounts()
        {
            List<Account> accountsList = new List<Account>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT AccountNumber, Name, Balance FROM Accounts";

                using (var command = new SqliteCommand(query, connection))
                {
                   using (var reader = command.ExecuteReader())
                   {
                        while (reader.Read())
                        {
                            int accNum = Convert.ToInt32(reader["AccountNumber"]);
                            string accName = Convert.ToString(reader["Name"]);
                            decimal accBalance = Convert.ToDecimal(reader["Balance"]);

                            Account acc = new Account(accName, accNum, accBalance);
                            accountsList.Add(acc);
                        }
                    }
                }
            }
            return accountsList;
        }

       
        public bool DepositMoney(int accountNumber, decimal amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("The amount to be deposited must be greater than zero.");
                return false;
            }

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE Accounts
                                SET Balance = Balance + @amount
                                WHERE AccountNumber = @accountNumber";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                    
                }
            }
        }

        public bool WithdrawMoney(int accountNumber, decimal amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("A negative amount cannot be withdrawn.");
                return false;
            }

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE Accounts
                                 SET Balance = Balance - @amount
                                 WHERE AccountNumber = @accountNumber AND Balance >= @amount;";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteAccount(int accountNumber)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM Accounts 
                                 WHERE AccountNumber = @accountNumber";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool TransferMoney(int senderNumber, int receiverNumber, decimal amount)
        {
            if(amount  <= 0)
            {
                Console.WriteLine("The amount must be greater than zero");
                return false;
            }
            if(senderNumber == receiverNumber)
            {
                Console.WriteLine("You cannot transfer to the same account");
                return false;
            }

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string withdrawQuery = @"UPDATE Accounts
                                                 SET Balance = Balance - @amount
                                                 WHERE AccountNumber = @accountNumber AND Balance >= @amount;";

                        using (var cmdWithdraw = new SqliteCommand(withdrawQuery, connection, transaction))
                        {
                            cmdWithdraw.Parameters.AddWithValue("@amount", amount);
                            cmdWithdraw.Parameters.AddWithValue("@accountNumber", senderNumber);
                            int rowsAffected = cmdWithdraw.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                            string depositQuery = @"UPDATE Accounts
                                                    SET Balance = Balance + @amount
                                                    WHERE AccountNumber = @accountNumber;";

                            using (var cmdDeposit = new SqliteCommand(depositQuery, connection, transaction))
                            {
                                cmdDeposit.Parameters.AddWithValue("@amount", amount);
                                cmdDeposit.Parameters.AddWithValue("@accountNumber", receiverNumber);

                                int rowsAffected2 = cmdDeposit.ExecuteNonQuery();

                                if (rowsAffected2 == 0)
                                {
                                    transaction.Rollback();
                                    return false;
                                }

                            }
                        
                        transaction.Commit();
                        return true;
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                       CREATE TABLE IF NOT EXISTS Accounts (
                           AccountNumber INTEGER PRIMARY KEY AUTOINCREMENT,
                           Name TEXT NOT NULL,
                           Balance REAL NOT NULL
                       );";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
