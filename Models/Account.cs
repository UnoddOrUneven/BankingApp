using System.Transactions;

namespace BankingApp.Models;

public class Account
{
    public Account()
    {
        Balance = 0;
        
    }
    
    
    public Account(decimal balance)
    {
        Balance = balance;
    }


    protected bool IsOpen { get; set; } = true;
    protected decimal Balance{get;set;}
    protected decimal Time{get;set;}
    
    protected List<string> Transactions{get;set;} = new List<string>();
    
   

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

    
    
    
    

   
    
    
    
     
    
    
    
    
}