using System.Transactions;

namespace BankingApp.Models;

public class Account
{
    public Account(string name = "Account")
    {
        Name = name;
        Balance = 0;
    }


    public Account(decimal balance)
    {
        Balance = balance;
    }


    public bool IsOpen { get; set; } = true;
    public string Name { get; set; } = "Account";
    public decimal Balance { get; set; }
    protected decimal Time { get; set; }

    protected List<string> Transactions { get; set; } = new List<string>();


    public void Deposit(int amount)
    {
        Balance += amount;
    }

    public void Withdraw(int amount)
    {
        if (Balance > amount)
        {
            Balance -= amount;
        }
        else
        {
            throw new ArgumentException("Not enough Balance");
        }
    }

    public void LogTransaction(string transaction)
    {
        Transactions.Add(transaction);
    }

    public void Close()
    {
        IsOpen = false;
    }
    
}