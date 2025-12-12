using System.Windows;
using System.Windows.Controls;
using BankingApp.Models;

namespace BankingApp.View
{
    public partial class Transfer : Page
    {
        private User _currentUser;

        public Transfer(User user)
        {
            _currentUser = user;
            InitializeComponent();
        }

        // Swap the From and To accounts
        private void SwapAccounts_Click(object sender, RoutedEventArgs e)
        {
            // Empty function for now
        }

        // Perform the transfer
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            // Empty function for now
        }
    }
}