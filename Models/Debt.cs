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

    public void PayDebt(decimal amount)
    {
        Amount -= amount;
        if (Amount < 0)
        {
            
        }
        
    }
    
    
    
    
}