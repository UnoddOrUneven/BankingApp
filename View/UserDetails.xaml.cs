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
        
        

        // Test money
        if (_currentUser.Accounts.Count == 0)
        {
            _currentUser.CreateAccount("Default");
            _currentUser.Accounts[0].Balance = 1000;
        }
        
        
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
        
        HideAccountDetails();
    }
    
    

    private bool IsViewAccountOpen(Account account)
    {
        return (AccountDetails.DataContext == account);
    }

    private void HideAccountDetails()
    {
        AccountDetails.Visibility = Visibility.Hidden;
        AccountDetails.DataContext = null;
    }
    private void showAccountDetails(Account account)
    {
        AccountDetails.Visibility = Visibility.Visible;
        AccountDetails.DataContext = account;
    }

    private void ChangeAccountName_Click(object sender, RoutedEventArgs e)
    {
        AccountName.Visibility = Visibility.Collapsed;
        AccountNameTextBox.Visibility = Visibility.Visible;
        ApplyAccountNameChangeButton.Visibility = Visibility.Visible;
    }

    private void ApplyAccountNameChange_Click(object sender, RoutedEventArgs e)
    {
        var account = getAccountFromButton(sender);
        account.Name = AccountNameTextBox.Text;
        
        AccountName.Visibility = Visibility.Visible;
        AccountNameTextBox.Visibility = Visibility.Hidden;
        ApplyAccountNameChangeButton.Visibility = Visibility.Collapsed;
    }

   
    
    
    
    
    private void Hide_Click(object sender, RoutedEventArgs e)
    {
        HideAccountDetails();
    }
    
    private void ViewAccount_Click(object sender, RoutedEventArgs e)
    {

        var account = getAccountFromButton(sender);
        
        if (IsViewAccountOpen(account))
        {
            HideAccountDetails();
            return;
        }
        
        showAccountDetails(account);
    }
    
    private void TransferButton_Click(object sender, RoutedEventArgs e)
    {
        var account = getAccountFromButton(sender);
        NavigationService?.Navigate(new Transfer(_currentUser, fromAccount: account));
    }

    private void DepositButton_Click(object sender, RoutedEventArgs e)
    {
        var account = getAccountFromButton(sender);
        NavigationService?.Navigate(new Transfer(_currentUser, toAccount: account));
        
    }

    private void ToMyAccount_Click(object sender, RoutedEventArgs e)
    {
        HideAccountDetails();
        ShowUsersAccount_list();
    }

    private void ShowUsersAccount_list()
    {
        
    }
    private void ToOtherUser_Click(object sender, RoutedEventArgs e)
    {
        HideAccountDetails();
        // Show a search box for other users
    }

    private void LogOut_Click(object sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new SignIn());
    }

    private void Loan_Button_Click(object sender, RoutedEventArgs e)
    {
        var account = getAccountFromButton(sender);
        NavigationService?.Navigate(new LoanContract(_currentUser, account));
    }
    
}