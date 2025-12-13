using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            
            FromAccountComboBox.SelectedValue = _selectedFromAccount;
            ToAccountComboBox.SelectedValue = _selectedToAccount;
            
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
        
        private void SwapAccounts_Click(object sender, RoutedEventArgs e)
        {
            
        }

        
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var from = FromAccountComboBox.SelectedItem as Account;
                var to = ToAccountComboBox.SelectedItem as Account;

                
                
                var ammount = GetTransferAmount();
                Bank.Instance.Transfer(from, to, ammount);
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
            var to = ToAccountComboBox.SelectedItem as Account;
            return (from != null && to != null);
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

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
            NavigationService?.Navigate(new UserDetails(_currentUser));
        }
        
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.SourceDataObject.GetDataPresent(DataFormats.Text))
            {
                e.CancelCommand();
                return;
            }

            var text = e.SourceDataObject.GetData(DataFormats.Text) as string;

            if (!int.TryParse(text, out var value) || value <= 0)
                e.CancelCommand();
        }
        
    }
}