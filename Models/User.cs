using System.Collections.ObjectModel;
using System.Transactions;
using Newtonsoft.Json;

namespace BankingApp.Models;

public class User
{
    public int Id {get; set;}
    public string Name {get; private set;}
    public int Balance { get; private set; } = 0;
    public string Email {get; private set;} = string.Empty;
    public string Password {get; private set;} = string.Empty;
    
    public ObservableCollection<Account> Accounts {get; set;} = new();
    public List<SavingsAccount> SavingAccounts {get; set;} = new List<SavingsAccount>();

    [JsonConstructor]
    public User(string name, string password)
    {
        Name = name;
        Password = password;
    }


    public void CreateAccount(string name = "Account")
    {
        var account = new Account(name);
        Accounts.Add(account);
    }
    
    
    public void addAccount(Account account)
    {
        Accounts.Add(account);
    }
    
    
    

}