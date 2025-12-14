using System.Windows;
using System.Windows.Baml2006;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BankingApp.Models;

namespace BankingApp.View
{
    public partial class Transfer : Page
    {
        private User _currentUser;
        private Account? _selectedFromAccount;
        private Account? _selectedToAccount;
        private List<Account> _openAccounts;

        public Transfer(User user, Account? fromAccount = null, Account? toAccount = null)
        {
            _currentUser = user;
            _selectedFromAccount = fromAccount;
            _selectedToAccount = toAccount;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _openAccounts = _currentUser.Accounts
                .Where(a => a.IsOpen)
                .ToList();
            
            FromAccountComboBox.ItemsSource = _openAccounts;
            ToAccountComboBox.ItemsSource = _openAccounts;
            
            FromAccountComboBox.SelectedItem = _selectedFromAccount;
            ToAccountComboBox.SelectedItem = _selectedToAccount;
            
        }

        private void RefreshData()
        {
            RefreshTransferButton();
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFromAccount = FromAccountComboBox.SelectedItem as Account;
            _selectedToAccount = ToAccountComboBox.SelectedItem as Account;
            RefreshData();
        }
        
        private void SwapAccounts_OnClick(object sender, RoutedEventArgs e)
        {
            
        }



        private Account? FindToAccount(string username)
        {
            return Bank.Instance.FindUserRecieverAccount(username);
        }


        private void RefreshMessage(bool IsUserFound)
        {
            MessageTextBlock.Visibility = Visibility.Visible;
            if (IsUserFound)
            {
                MessageTextBlock.Foreground = Brushes.Lime;
                MessageTextBlock.Text = "Existing user";
            }
            else
            {
                MessageTextBlock.Foreground = Brushes.Red;
                MessageTextBlock.Text = "User not found"; 
            }
            
        }
        
        
        private Account? GetToAccount()
        {
            if (ToAccountComboBox.SelectedItem is Account selectedAccount)
            {
                return selectedAccount;
            }
            
            if (!string.IsNullOrWhiteSpace(FindUsersTextBox.Text))
            {
                return FindToAccount(FindUsersTextBox.Text);
            }

            return null;
        }

        
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var from = FromAccountComboBox.SelectedItem as Account;
                var to = GetToAccount();
                var amount = GetTransferAmount();
                Bank.Instance.Transfer(from, to, amount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshData();
            
        }
        
        
        
        
        
        

        private bool AreFieldsReady()
        {
            var from = FromAccountComboBox.SelectedItem as Account;

            bool hasInternalTarget = ToAccountComboBox.SelectedItem is Account;
            bool hasExternalTarget = FindToAccount(FindUsersTextBox.Text) != null;

            return from != null && (hasInternalTarget || hasExternalTarget);
        }

        private void RefreshTransferButton()
        {
            TransferButton.IsEnabled = AreFieldsReady();
        }
        
        

        private decimal GetTransferAmount()
        {
            if (AmountTextBox.Text == "")
            {
                throw new Exception("Amount cannot be empty");
            }
            if (int.TryParse(AmountTextBox.Text, out int amount))
            {
                decimal transferAmount = amount;
                return transferAmount;
            }
            else
            {
                throw new  Exception("Bad amount");
            }
        }

        private void GoBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshData();
            NavigationService?.Navigate(new UserDetails(_currentUser));
        }


        
        
        private void AllUsersClearSearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            FindUsersTextBox.Text = "";
        }

        private void FindUsersTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ToAccountComboBox.SelectedItem = null;
            RefreshData();
            RefreshMessage(FindToAccount(FindUsersTextBox.Text) != null);
        }

        private void FindUsersTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            ToAccountComboBox.SelectedItem = null;
            RefreshData();
        }

        private void ToAccountComboBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            FindUsersTextBox.Text = "";
        }

        private void FindUsersTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FindUsersTextBox.Text))
            {
                MessageTextBlock.Visibility = Visibility.Collapsed;
                return;
            }

            var account = FindToAccount(FindUsersTextBox.Text);
            RefreshMessage(account != null);
            RefreshData();
        }

        
    }
}