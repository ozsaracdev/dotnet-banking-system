# 🏦 Console Banking System (C# & SQLite Integration)

This project is a complete refactoring and modernization of a banking console application originally written in **C**. It transitions the system from an imperative, file-based structure to a robust **C#**, **Object-Oriented Programming (OOP)**, and **SQLite** relational database architecture.

The main purpose of this project is to hands-on explore database integration, ACID transaction management, software security measures, and layered application architecture.

---

## 🛠️ Key Features & Architectural Decisions

- **OOP & Encapsulation:** The `Account` class serves as a clean Entity/Model, enforcing encapsulation using `{ get; private set; }` auto-properties to prevent unauthorized state modifications.
- **Separation of Concerns:** Clean boundary between the Presentation/UI layer (`Program.cs`) and the Data Access/Domain logic layer (`Bank.cs`).
- **SQL Injection Prevention:** All database operations utilize **Parameterized Queries (`SqliteParameter`)** instead of string concatenation.
- **ACID & Transaction Management (`SqliteTransaction`):** The `TransferMoney` method is wrapped in a database transaction to guarantee **Atomicity** (All or Nothing). If any step fails (e.g., receiver account not found), a `Rollback()` is executed to preserve data integrity.
- **Concurrency Protection:** Fund withdrawals check for sufficient balance both at the C# level and SQL level (`WHERE Balance >= @amount`) to prevent race condition vulnerabilities and negative balances.
- **Resource Management:** Utilizes C# `using` statements for `SqliteConnection`, `SqliteCommand`, and `SqliteDataReader` to guarantee proper disposal of unmanaged resources and prevent memory leaks.

---

## 🔄 C (Legacy) vs. C# & SQLite (Current) Comparison

| Feature | Legacy C Version | Current C# & SQLite Version |
| :--- | :--- | :--- |
| **Architecture** | Imperative / Procedural (`struct`) | Object-Oriented Programming (OOP) |
| **Data Persistence** | Binary Files (`fwrite` / `fread`) | Relational Database (`bank.db` via SQLite) |
| **Account Deletion** | Shifting Array Elements O(N) | Direct SQL `DELETE` Query O(1) |
| **Transaction Safety** | Manual checks | Atomic Transactions (`Commit` / `Rollback`) |
| **Memory Safety** | Pointers & Manual Buffer Cleanup | Strong Typing & Managed Memory |

---

## 🚀 Getting Started

### Prerequisites
- [.NET SDK 6.0+](https://dotnet.microsoft.com/download) or higher

### Installation & Execution

1. Clone the repository:
   ```bash
   git clone https://github.com/ozsaracdev/dotnet-banking-system.git

2. Navigate to the project directory:
    ```bash
    cd BankingApp

3. Build the project:
   ```bash
   dotnet build

4. Run the application:
   ```bash
   dotnet run

## Tech Stack
Language: C# (.NET)

Database: SQLite

Library: Microsoft.Data.Sqlite

   
