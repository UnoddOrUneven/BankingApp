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
        
        _currentUser.CreateAccount();
        _currentUser.Accounts[0].Balance = 1000;
        LoadData();
    }

    private void LoadData()
    {
        UserNameText.Text = _currentUser.Name;
        AccountsList.ItemsSource = _currentUser.Accounts.Where(account => account.IsOpen);
    }

    private void OpenNewAccount_Click(object sender, RoutedEventArgs e)
    {
        _currentUser.CreateAccount();
        LoadData();
    }


    private Account? getAccountFromButton(object sender)
    {
        if (sender is not Button button) return null;
        return button.DataContext as Account;
    }
    
    private void CloseAccount_Click(object sender, RoutedEventArgs e)
    {
        var account = getAccountFromButton(sender);
        account.Close();
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

        var account = getAccountFromButton(sender);
        
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