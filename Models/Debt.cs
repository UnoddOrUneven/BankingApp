using Newtonsoft.Json;

namespace BankingApp.Models;

public class Debt
{

    public decimal Amount {get; set;}
    public DateTime Date {get; set;}
    public decimal MonthlyInterestRate {get; set;}
    
    [JsonConstructor]
    public Debt(decimal amount, DateTime date, decimal monthlyInterestRate)
    {
        
        Amount = amount;
        Date = date;
        MonthlyInterestRate = monthlyInterestRate;
        
    }
    

    public void ApplyInterestRate()
    {
        Amount += Amount * MonthlyInterestRate;
    }

    public void Pay(decimal payment)
    {
        Amount -= payment;
        if (Amount < 0)
        {
            Amount = 0;
        }
        
    }

    public bool IsClosingAvailable(Account account)
    {
        return account.Balance >= Amount;
    }

    public void Close(Account account)
    {
        account.Withdraw(Amount);
        Amount = 0;
    }
    
    
}