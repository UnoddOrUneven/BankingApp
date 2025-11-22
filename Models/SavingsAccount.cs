namespace BankingApp.Models;

public class SavingsAccount : Account
{

    protected bool IsOpened { get; set; }
    private decimal InterestRate { get; set; } = 6;
    
    private static decimal CalculateInterest(decimal balance, decimal interestRate, decimal time)
    {
        return balance * interestRate * time;    
    }
    
    
    private void ApplyInterest()
    {
        Balance += CalculateInterest(Balance, InterestRate, Time);
    }
    
    
    
}