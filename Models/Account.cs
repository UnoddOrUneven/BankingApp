using System.ComponentModel;
using System.Transactions;

namespace BankingApp.Models;

public class Account : INotifyPropertyChanged
{
    public Account(string name = "Account")
    {
        Name = name;
        Balance = 0;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    public Account(decimal balance)
    {
        Balance = balance;
    }

    
    public bool IsOpen { get; set; } = true;
    public string Name { get; set; } = "Account";
    private decimal _balance;
    public decimal Balance { 
        get => _balance;
        set
        {
            _balance = value;
            OnPropertyChanged(nameof(Balance));
        }
        
        
    }
    protected decimal Time { get; set; }
    
    
    
    protected List<string> Transactions { get; set; } = new List<string>();


    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (Balance >= amount)
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

    public void ChangeName(string newname)
    {
        Name = newname;
    }
    
}