using System.Windows;
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
        _currentUser.CreateAccount();
        _currentUser.Accounts[0].Balance = 1000;
    }

    private void LoadData()
    {
        UserNameText.Text = _currentUser.Name;
        AccountsList.ItemsSource = _currentUser.Accounts;
    }

    private void OpenNewAccount_Click(object sender, RoutedEventArgs e)
    {
        _currentUser.CreateAccount();
        LoadData();
    }
    
    
}