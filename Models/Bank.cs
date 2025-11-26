using System.Printing;

namespace BankingApp.Models;

public class Bank
{
    private static readonly Bank _instance = new Bank();

    public List<User> Users { get; private set; } = new List<User>();


    private Bank()
    {
    }

    public static Bank instance => _instance;

    static int idCounter = 0;
    

    public void AddUser(User user)
    {
        user.Id = ++idCounter;
        Users.Add(user);
    }

    public bool IsNameAvailable(string name)
    {
        return Users.All(u => u.Name != name);
    }
    
    
}