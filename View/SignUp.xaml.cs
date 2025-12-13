using System.Windows;
using System.Windows.Controls;
using BankingApp.Models;

namespace BankingApp.View;

public partial class SignUp : Page
{
    public SignUp()
    {
        InitializeComponent();
    }

    private void SignInButton_Click(object sender, RoutedEventArgs e)
    { 
        NavigationService?.Navigate(new SignIn());
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        ErrorLabel.Visibility = Visibility.Collapsed;
        var tb = sender as TextBox;
        if (tb.Text == "Name" || tb.Text == "Password" || tb.Text == "Repeat Password")
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

    private void Repeat_Password_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            tb.Text = "Repeat Password";
        }
    }

    private void Name_LostFocus(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (string.IsNullOrEmpty(tb.Text))
        {
            tb.Text = "Name";
        }
    }


    private void DisplayPasswordsDontMatch()
    {
        ErrorLabel.Text = "Passwords don't match";
        ErrorLabel.Visibility = Visibility.Visible;
    }

    private void DisplayNameTaken()
    {
        ErrorLabel.Text = "That name is unavailable";
        ErrorLabel.Visibility = Visibility.Visible;
    }

    private void DisplayPasswordWeak()
    {
        ErrorLabel.Text = "The password is too weak";
        ErrorLabel.Visibility = Visibility.Visible;
    }

    private bool ArePasswordsMatch()
    {
        return PasswordTextBox.Text == RepeatPasswordTextBox.Text;
    }

    private bool IsPasswordStrong()
    {
        return PasswordTextBox.Text.Length >= 8;
    }

    private void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        if (!ArePasswordsMatch())
        {
            DisplayPasswordsDontMatch();
            return;
        }

        if (!IsPasswordStrong())
        {
            DisplayPasswordWeak();
            return;
        }

        if (!Bank.Instance.IsNameAvailable(NameTextBox.Text))
        {
            DisplayNameTaken();
            return;
        }
        
        var name = NameTextBox.Text;
        var password = PasswordTextBox.Text;
        var user = new User(name, password);
        
        Bank.Instance.AddUser(user);
        NavigationService?.Navigate(new UserDetails(user));
    }
}