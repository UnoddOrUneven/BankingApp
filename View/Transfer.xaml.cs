using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BankingApp.Models;

namespace BankingApp.View
{
    public partial class Transfer : Page
    {
        private User _currentUser;
        private Account _firstSelectedFromAccount;
        private Account _firstSelectedToAccount;

        public Transfer(User user, Account? fromAccount = null, Account? toAccount = null)
        {
            _currentUser = user;
            _firstSelectedFromAccount = fromAccount;
            _firstSelectedToAccount = toAccount;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var openAccounts = _currentUser.Accounts
                .Where(a => a.IsOpen)
                .ToList();
            
            FromAccountComboBox.ItemsSource = openAccounts;
            ToAccountComboBox.ItemsSource = openAccounts;
            
            FromAccountComboBox.SelectedItem = openAccounts
                .FirstOrDefault(a => a == _firstSelectedFromAccount);
            
            ToAccountComboBox.SelectedItem = openAccounts
                .FirstOrDefault(a => a == _firstSelectedToAccount);
        }
        
        
        private void SwapAccounts_Click(object sender, RoutedEventArgs e)
        {
            
        }

        
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            var from = FromAccountComboBox.SelectedItem as Account;
            var to = ToAccountComboBox.SelectedItem as Account;
            var ammount = GetTransferAmount();
            try
            {
                Bank.Instance.Transfer(from, to, ammount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private decimal GetTransferAmount()
        {
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
            NavigationService?.GoBack();
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