using System.Windows;
using System.Windows.Controls;
using BankingApp.Models;

namespace BankingApp.View;

public partial class SignIn : Page
{
    public SignIn()
    {
        InitializeComponent();
    }

    private void ForgotPassword_Click(object sender, RoutedEventArgs e)
    {
        ResetPassword resetWindow = new ResetPassword();
        resetWindow.ShowDialog(); // modal
    }


    private void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        var signUpPage = new SignUp();
        NavigationService?.Navigate(new SignUp());
    }

    private void DisplayIncorrectLoginMessage()
    {
        ErrorLabel.Visibility = Visibility.Visible;
        ErrorLabel.Text = "No such user";
    }

    private void DisplayIncorrectPasswordMessage()
    {
        ErrorLabel.Visibility = Visibility.Visible;
        ErrorLabel.Text = "Wrong password";
    }

    private void SignInButton_Click(object sender, RoutedEventArgs e)
    {
        if (Bank.Instance.IsNameAvailable(LogInTextBox.Text))
        {
            DisplayIncorrectLoginMessage();
            return;
        }

        var user = Bank.Instance.FindUser(LogInTextBox.Text, PasswordTextBox.Text);

        if (user is null)
        {
            DisplayIncorrectPasswordMessage();
            return;
        }
        
        NavigationService?.Navigate(new AccountDetails(user));
        
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        ErrorLabel.Visibility = Visibility.Collapsed;

        var tb = sender as TextBox;
        if (tb.Text == "Password" || tb.Text == "Login")
        {
            tb.Text = "";
        }
    }

    private void Password_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            tb.Text = "Password";
        }
    }

    private void Login_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            tb.Text = "Login";
        }
    }
}