using System.Transactions;

namespace BankingApp.Models;

public class User
{
    int Id {get; set;}
    string Name {get; set;}
    private int balance { get; set; } = 0;
    private string email {get; set;} = string.Empty;
    
    private List<Account> Accounts {get; set;} = new List<Account>();
    private List<SavingsAccount> SavingAccounts {get; set;} = new List<SavingsAccount>();
                        
    
    User(int id, string name)
    {
        Id = id;
        Name = name;
    }


    private void CreateAccount()
    {
        var account = new Account();
        Accounts.Add(account);
        
    }
    
    
    private void addAccount(Account account)
    {
        Accounts.Add(account);
    }
    
    
    

}