using System.Windows.Controls;
using BankingApp.Models;

namespace BankingApp.View;

public partial class AccountDetails : Page
{
    private User _currentUser;
    public AccountDetails(User user)
    {
        
        InitializeComponent();
        
        _currentUser = user;
        LoadData();
    }

    private void LoadData()
    {
        UserNameText.Text = _currentUser.Name;
        AccountsList.ItemsSource = _currentUser.Accounts;
    }
    
    
    
}