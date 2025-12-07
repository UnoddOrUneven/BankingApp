using System.Windows;
using System.Windows.Controls;
using BankingApp.Models;

namespace BankingApp.View;

public partial class UserDetails : Page
{
    private User _currentUser;
    public UserDetails(User user)
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

    private bool IsViewAccountOpen(Account account)
    {
        return (AccountDetails.DataContext == account);
    }
    
    
    private void Hide_Click(object sender, RoutedEventArgs e)
    {
        AccountDetails.Visibility = Visibility.Hidden;
        AccountDetails.DataContext = null;
    }
    
    private void ViewAccount_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        var account = button.DataContext as Account;

        if (IsViewAccountOpen(account))
        {
            AccountDetails.Visibility = Visibility.Hidden;
            AccountDetails.DataContext = null;
            return;
        }
        
        AccountDetails.Visibility = Visibility.Visible;
        AccountDetails.DataContext = account;
        
    }
    
    
}