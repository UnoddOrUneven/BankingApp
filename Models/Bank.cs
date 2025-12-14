using System.Printing;
using System.Transactions;
using Newtonsoft.Json;
using System.IO;
using System.Timers;
using Timer = System.Timers.Timer;

namespace BankingApp.Models;

public class Bank
{
    private static readonly string BankJsonPath =
        Path.Combine(AppContext.BaseDirectory, "Data", "Bank.json");
    
    [JsonIgnore]
    private string _lastJson;

    public List<User> Users { get; private set; } = new List<User>();
    
    [JsonConstructor]
    public Bank()
    {
        Console.WriteLine(BankJsonPath);
        if (IsJsonAvailable())
        {
            LoadFromJson();
        }
        SetSaveTimer();
    }

    
    public static Bank Instance { get; } = new Bank();

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

    public void Transfer(Account sender, Account receiver, decimal amount)
    {
        sender.Withdraw(amount);
        receiver.Deposit(amount);
        
    }
    
    public User? FindUser(string name, string password)
    {
        return Users.FirstOrDefault(u => u.Name == name && u.Password == password);
    }

    public Account? FindUserRecieverAccount(string username)
    {
        return Users?
            .FirstOrDefault(u => u.Name == username)?
            .Accounts?
            .FirstOrDefault(a => a.IsOpen);
        
        
    }

   
    
    
    private async Task SaveIfNeeded()
    {
        if (!IsJsonAvailable()) return;
        if (!IsDataChanged()) return;
        await SaveToJsonAsync();

    }
    
    
    private bool IsDataChanged()
    {
        string currentJson = JsonConvert.SerializeObject(this, Formatting.Indented);
        return (currentJson != _lastJson);
    }
    
    private async Task SaveToJsonAsync()
    {
        var jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
        
        await File.WriteAllTextAsync(BankJsonPath, jsonString);
        _lastJson = jsonString;

    }

    private void LoadFromJson()
    {
        
        string jsonString = File.ReadAllText(BankJsonPath);
        JsonConvert.PopulateObject(jsonString, this); 
        _lastJson = jsonString;
    }

    private static bool IsJsonAvailable()
    {
        return File.Exists(BankJsonPath);
    }

    private static void SetSaveTimer()
    {
        var timer = new Timer(1000); 
        timer.Elapsed +=  async (s, e) => await Bank.Instance.SaveIfNeeded();
        timer.AutoReset = true;
        timer.Start();
    } 
    
    
    
    
}