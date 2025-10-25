using System.Transactions;

namespace BankingApp.Models;

public class Person
{
    int Id {get; set;}
    string Name {get; set;}
    private int balance {get; set;}
    private string email {get; set;}
    
    private List<Account> accounts {get; set;}
    


}