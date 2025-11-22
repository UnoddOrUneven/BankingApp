using System.Windows;
using System.Windows.Controls;

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
        resetWindow.ShowDialog();  // modal
    }

  

    private void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        SignUp signUpPage = new SignUp();
        NavigationService?.Navigate(new SignUp());
        
        
    }
    
    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
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